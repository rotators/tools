using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Extensions
{
    public static class ExtensionMethods
    {
        public static string ReadNullString(this BinaryReader reader)
        {
            List<char> ret = new List<char>();
            char c = '\0';
            do
            {
                c = (char)reader.ReadByte();
                if (c == '\0') break;
                ret.Add(c);
            } while (true);
            return new string(ret.ToArray());
        }
    }
}

namespace CodeAnalyzer
{
    using Extensions;
    class Analyzer
    {
        #region Internal classes
        private class Function
        {
            public int Id;
            public string Module;
            public string Name;

            public Function(int id, string module, string name)
            {
                this.Id = id;
                this.Module = module;
                this.Name = name;
            }
        }

        private class CallStack
        {
            private List<int> Ids;
            private List<int> Lines;
            public int Length { get { return Ids.Count; } }

            public CallStack()
            {
                Ids = new List<int>();
                Lines = new List<int>();
            }

            public int GetId(int idx)
            {
                return Ids[idx];
            }

            public int GetLine(int idx)
            {
                return Lines[idx];
            }

            public void AddCall(int id, int line)
            {
                Ids.Add(id);
                Lines.Add(line);
            }
        }

        private class CallPaths
        {
            public int Id;
            public Dictionary<int, CallPaths> Children;
            public int Incl;
            public int Excl;

            public CallPaths(int id)
            {
                Children = new Dictionary<int, CallPaths>();
                Id = id;
                Incl = 1;
            }

            public CallPaths AddChild(int id)
            {
                CallPaths child = null;
                if (!Children.ContainsKey(id))
                {
                    child = new CallPaths(id);
                    Children.Add(id, child);
                }
                else
                {
                    child = Children[id];
                    child.Incl += 1;
                }

                return child;
            }

            public void StackEnd()
            {
                Excl++;
            }
        }

        private class Sources
        {
            public class SourceFunction
            {
                public Dictionary<int,int> Hits;
                public int MaxHitLine;

                public SourceFunction()
                {
                    Hits = new Dictionary<int, int>();
                }

                public void AddHit(int line)
                {
                    if (!Hits.ContainsKey(line)) Hits.Add(line, 1);
                    else Hits[line] += 1;
                }
            }

            public class Source
            {
                public class Prefix
                {
                    public int Line;
                    public int Hits;
                    public string Text;
                    public Prefix(int line, int hits)
                    {
                        Line = line;
                        Hits = hits;
                    }
                    static public int Compare(Prefix p1, Prefix p2)
                    {
                        if (p1.Line == p2.Line) return 0;
                        if (p1.Line < p2.Line) return -1;
                        else return 1;
                    }
                }

                public Dictionary<string, SourceFunction> Funcs;
                public List<Prefix> PrefixLines;
                public string[] Lines;
                public int LongestPrefix;

                public Source(string module)
                {
                    Funcs = new Dictionary<string, SourceFunction>();
                    Lines = new string[1] { "There is no source for this module." };
                    PrefixLines = new List<Prefix>();
                }

                public void AddHit(string name, int line)
                {
                    if (!Funcs.ContainsKey(name)) Funcs.Add(name, new SourceFunction());
                    Funcs[name].AddHit(line);
                }

                public void Process()
                {
                    LongestPrefix = 0;
                    foreach (KeyValuePair<string, SourceFunction> pair in Funcs)
                    {
                        SourceFunction func = pair.Value;
                        List<Prefix> prefixes = new List<Prefix>();
                        float sum = 0.0f;
                        int maxHit = 0;
                        int maxHitLine = 0;
                        foreach (KeyValuePair<int, int> hit in func.Hits)
                        {
                            prefixes.Add(new Prefix(hit.Key - 1, hit.Value));
                            sum += (float)(hit.Value);
                            if (hit.Value > maxHit)
                            {
                                maxHitLine = hit.Key;
                                maxHit = hit.Value;
                            }
                        }
                        func.MaxHitLine = maxHitLine;

                        foreach (Prefix prefix in prefixes)
                        {
                            prefix.Text = String.Format("{0:00.00}%", 100.0f * (float)(prefix.Hits) / sum);
                            if (prefix.Text.Length > LongestPrefix) LongestPrefix = prefix.Text.Length;
                        }
                        PrefixLines.AddRange(prefixes);
                    }
                    PrefixLines.Sort(Prefix.Compare);
                }
            }

            public Dictionary<string, Source> Sections;

            public Sources()
            {
                Sections = new Dictionary<string, Source>();
            }

            public void ProcessHit(string module, string name, int line)
            {
                if (module == "(null)") return;
                if (!Sections.ContainsKey(module)) Sections.Add(module, new Source(module));
                Sections[module].AddHit(name, line);
            }

            public void AddModule(string module, string[] code)
            {
                if (Sections.ContainsKey(module)) return;
                Source src = new Source(module);
                src.Lines = code;
                Sections.Add(module, src);
            }
        }

        private class Models
        {
            public List<string> ModNames;
            public Dictionary<string, List<string>> FNames;
            public List<TreeModel> ModulesModel;
            public List<TreeModel> CallModel;
            public Dictionary<string, List<string>> Details;
            public Dictionary<string, int> MaxHits;

            public Models()
            {
                ModNames = new List<string>();
                FNames = new Dictionary<string, List<string>>();
                ModulesModel = new List<TreeModel>();
                CallModel = new List<TreeModel>();
                Details = new Dictionary<string, List<string>>();
                MaxHits = new Dictionary<string, int>();
            }
        }

        private class RunTime
        {
            public int TotalCalls;
            public Dictionary<string, Dictionary<string, Tuple<float,float>>> Modules;
            public Dictionary<int, CallPaths> Paths;
            public Dictionary<int, Function> Functions;
            public Sources SourceSections;

            public RunTime()
            {
                TotalCalls = 0;
                Modules = new Dictionary<string, Dictionary<string, Tuple<float, float>>>();
                Paths = new Dictionary<int, CallPaths>();
                Functions = new Dictionary<int, Function>();
                SourceSections = new Sources();
            }
        }
        #endregion

        RunTime runTime;
        Models models;
        static public string FileName;
        static private MainForm mainForm;

        public Analyzer(string fileName)
        {
            FileName = fileName;
        }

        public void Load(MainForm form)
        {
            mainForm = form;
            runTime = new RunTime();
            form.SetStatus("Reading callstacks:");
            form.ShowBar(true);
            form.Update();
            loadCalls();
            models = new Models();
            prepareModels();
            form.ShowBar(false);
            form.SetStatus("Loaded.");
            form.Update();
            runTime = null;
        }

        private void loadCalls()
        {
            BinaryReader reader = new BinaryReader(File.Open(FileName, FileMode.Open));
            int len = (int)reader.BaseStream.Length;

            // verify header
            uint header = (uint)(reader.ReadInt32());
            if (header != 0x10ADB10B) return;

            // version, ignored
            reader.ReadInt32();

            // load modules
            for (string s = reader.ReadNullString(); s.Length > 0; s = reader.ReadNullString())
            {
                //if (s.Contains("str")) MessageBox.Show(s);
                string lines = reader.ReadNullString();
                string[] split = lines.Split('\n');
                runTime.SourceSections.AddModule(s, split);
            }
            // load functions
            for (int funcId = reader.ReadInt32(); funcId != -1; funcId = reader.ReadInt32())
            {
                ;
                string module = reader.ReadNullString();
                string name = reader.ReadNullString();
                Function func = new Function(funcId, module, name);
                runTime.Functions[funcId] = func;
            }

            len -= (int)(reader.BaseStream.Position);
            int pos = 0;
            int id = 0;
            int line = 0;
            CallStack stack = new CallStack();
            while (pos < len)
            {
                mainForm.SetBar(pos, len);
                id = reader.ReadInt32();
                if (id != -1)
                {
                    pos += 2 * sizeof(int);
                    line = reader.ReadInt32();
                    stack.AddCall(id, line);
                    continue;
                }

                // end of call stack, process
                pos += sizeof(int);

                processCallStack(stack);

                stack = new CallStack();
            }
            reader.Close();
        }

        #region Public Getters

        public List<TreeModel> GetCallModel()
        {
            return models.CallModel;
        }

        public List<TreeModel> GetModulesModel()
        {
            return models.ModulesModel;
        }

        public List<string> GetModuleNames()
        {
            return models.ModNames;
        }

        public List<string> GetSourceSection(string module)
        {
            if(models.Details.ContainsKey(module)) return models.Details[module];
            else return models.Details["(null)"];
        }

        public List<string> GetModuleFunctionNames(string module)
        {
            return models.FNames[module];
        }

        public int GetMaxHits(string name)
        {
            if (models.MaxHits.ContainsKey(name)) return models.MaxHits[name];
            else return -1;
        }
        #endregion

        #region Preparation

        private void prepareModels()
        {
            prepareFnames();
            prepareCallViewModel();
            prepareModulesViewModel();
            prepareDetails();
        }

        private void prepareFnames()
        {
            mainForm.SetStatus("Preparing funcion names: ");
            mainForm.SetBar(0);
            int count = runTime.Functions.Count;
            int curr = 0;
            foreach (KeyValuePair<int,Function> pair in runTime.Functions)
            {
                string module = pair.Value.Module;
                string name = pair.Value.Name;
                if (!models.FNames.ContainsKey(module))
                {
                    models.ModNames.Add(module);
                    models.FNames.Add(module, new List<string>());
                }
                models.FNames[module].Add(name);
                mainForm.SetBar(++curr, count);
            }

            mainForm.SetStatus("Sorting functions:");
            mainForm.SetBar(0);
            count = models.FNames.Count + 1;
            curr = 0;

            models.ModNames.Sort();
            mainForm.SetBar(++curr, count);

            foreach (KeyValuePair<string, List<string>> pair in models.FNames)
            {
                pair.Value.Sort(CompareFullNames);
                mainForm.SetBar(++curr, count);
            }
        }

        private void prepareCallViewModel()
        {
            mainForm.SetStatus("Preparing callstacks view:");
            mainForm.SetBar(0);
            int count = runTime.Paths.Count;
            int curr = 0;
            foreach (KeyValuePair<int, CallPaths> pair in runTime.Paths)
            {
                Function func = runTime.Functions[pair.Key];
                TreeModel model = new TreeModel(func.Module + "@" + func.Name,
                    100 * (float)(pair.Value.Incl) / ((float)(runTime.TotalCalls)),
                    100 * (float)(pair.Value.Excl) / ((float)(runTime.TotalCalls)));
                appendChildren(model, pair.Value);
                models.CallModel.Add(model);
                mainForm.SetBar(++curr, count);
            }
        }

        private void prepareModulesViewModel()
        {
            mainForm.SetStatus("Preparing modules view:");
            mainForm.SetBar(0);
            int count = runTime.Modules.Count;
            int curr = 0;
            foreach (KeyValuePair<string, Dictionary<string, Tuple<float,float>>> pair in runTime.Modules)
            {
                TreeModel item = new TreeModel(pair.Key, 0, 0);
                //float modIncl = 0;
                float modExcl = 0;
                models.ModulesModel.Add(item);
                foreach (KeyValuePair<string, Tuple<float, float>> mpair in pair.Value)
                {
                    item.AddChild(mpair.Key,
                        100 * mpair.Value.Item1 / ((float)(runTime.TotalCalls)),
                        100 * mpair.Value.Item2 / ((float)(runTime.TotalCalls)));
                    //modIncl += mpair.Value.Item1;
                    modExcl += mpair.Value.Item2;
                }
                //item.Incl = 100 * modIncl / ((float)(runTime.TotalCalls));
                item.Excl = 100 * modExcl / ((float)(runTime.TotalCalls));
                mainForm.SetBar(++curr, count);
            }
        }

        private void appendChildren(TreeModel model, CallPaths path)
        {
            mainForm.SetStatus("Preparing function details:");
            mainForm.SetBar(0);
            int count = runTime.Paths.Count;
            int curr = 0;
            foreach (KeyValuePair<int, CallPaths> pair in path.Children)
            {
                Function func = runTime.Functions[pair.Key];
                TreeModel child=model.AddChild(func.Module + "@" + func.Name,
                    100 * (float)(pair.Value.Incl) / ((float)(runTime.TotalCalls)),
                    100 * (float)(pair.Value.Excl) / ((float)(runTime.TotalCalls)));
                appendChildren(child, pair.Value);
                mainForm.SetBar(++curr, count);
            }
        }

        private void prepareDetails()
        {
            foreach (KeyValuePair<string, Sources.Source> pair in runTime.SourceSections.Sections)
            {
                List<string> texts = new List<string>();
                Sources.Source source = pair.Value;
                source.Process();

                Sources.Source.Prefix[] prefixes = source.PrefixLines.ToArray();
                string[] lines = source.Lines;
                int pref = 0;
                for (int i = 0, j = lines.Length; i < j; i++)
                {
                    string text = String.Format("{0:00000}: ", i);
                    if (pref < prefixes.Length && i == prefixes[pref].Line)
                    {
                        text += (prefixes[pref].Text + (new string(' ', source.LongestPrefix - prefixes[pref].Text.Length)));
                        pref++;
                    }
                    else text += (new string(' ', source.LongestPrefix));
                    text += " | ";
                    text += lines[i];
                    texts.Add(text);
                }

                models.Details.Add(pair.Key, texts);

                foreach (KeyValuePair<string, Sources.SourceFunction> pair2 in pair.Value.Funcs)
                {
                    Sources.SourceFunction func = pair2.Value;
                    models.MaxHits.Add(pair.Key+"@"+pair2.Key, func.MaxHitLine);
                }
            }

            // null
            List<string> def = new List<string>() { "This module was not called." };
            models.Details.Add("(null)", def);
        }
        #endregion

        public static int CompareFullNames(string func1, string func2)
        {
            int j = func1.IndexOf(":");
            int i = func1.IndexOf(" ");
            string f1 = (i < j || j == -1) ? func1.Substring(i + 1) : func1;
            j = func2.IndexOf(":");
            i = func2.IndexOf(" ");
            string f2 = (i < j || j == -1) ? func2.Substring(i + 1) : func2;
            return f1.CompareTo(f2);
        }

        #region Processing
        private void processCallStack(CallStack stack)
        {
            runTime.TotalCalls++;
            processPaths(stack);
            processModules(stack);
        }

        private void processPaths(CallStack stack)
        {
            int topId = stack.GetId(stack.Length - 1);
            CallPaths path = null;
            if (!runTime.Paths.ContainsKey(topId))
            {
                path = new CallPaths(topId);
                runTime.Paths.Add(topId, path);
            }
            else
            {
                path = runTime.Paths[topId];
                path.Incl++;
            }

            for (int i = stack.Length - 2; i >= 0; i--)
                path = path.AddChild(stack.GetId(i));

            path.StackEnd();

            // load script section
            Function endFunc = runTime.Functions[stack.GetId(0)];
            runTime.SourceSections.ProcessHit(endFunc.Module, endFunc.Name, stack.GetLine(0));
        }

        private void processModules(CallStack stack)
        {
            for (int i = 0; i < stack.Length; i++)
            {
                Function func = runTime.Functions[stack.GetId(i)];
                if (!runTime.Modules.ContainsKey(func.Module))
                    runTime.Modules[func.Module] = new Dictionary<string, Tuple<float, float>>();
                Dictionary<string, Tuple<float, float>> mod = runTime.Modules[func.Module];
                if (!mod.ContainsKey(func.Name))
                {
                    mod.Add(func.Name, new Tuple<float,float>(1.0f, i==0? 1.0f : 0.0f));
                }
                else
                {
                    Tuple<float,float> prev = mod[func.Name];
                    mod[func.Name] = new Tuple<float, float>(prev.Item1 + 1.0f, prev.Item2 + (i == 0 ? 1.0f : 0.0f));
                }
            }
        }
        #endregion
    }
}

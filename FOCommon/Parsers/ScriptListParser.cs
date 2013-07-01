using System;
using System.Collections.Generic;
using System.IO;

using FOCommon.Parsers;
using FOCommon.Script;

namespace FOCommon.Parsers
{
    // This class is for parsing scripts.cfg
    // TODO: Add functionality to modify/save.
    public class ScriptListParser : BaseParser, IParser
    {
        string filename;
        List<ScriptDeclaration> Scripts = new List<ScriptDeclaration>();
        List<string> Lines = new List<string>();

        public ScriptListParser(string filename)
        {
            this.filename = filename;
        }

        public bool Parse()
        {
            if (!File.Exists(filename))
                return false;

            Lines.Clear();
            Lines.AddRange(File.ReadAllLines(filename));
            foreach (string Line in Lines)
            {
                if ((Line.Length>0)&&(Line[0] == '#'))
                    continue;

                if ((!Line.Contains("server") && !Line.Contains("client") && !Line.Contains("mapper")) || string.IsNullOrEmpty(Line))
                    continue;

                bool Enabled;
                string Reserved = "";
                ScriptApp App;
                ScriptType Type;
                int i = 0;

                char[] del = { ' ', '\t' };
                string[] param = Line.Split(del, StringSplitOptions.RemoveEmptyEntries);
                if (param[0] == "@")
                {
                    i = 1;
                    Enabled = true;
                }
                else
                    Enabled = false;

                string AppStr = param[i];
                if(AppStr=="server")
                    App=ScriptApp.Server;
                else if(AppStr=="client")
                    App=ScriptApp.Client;
                else
                    App=ScriptApp.Mapper;

                string TypeStr = param[i + 1];
                if (TypeStr == "module")
                    Type = ScriptType.Module;
                else
                    Type = ScriptType.Bind;

                char[] trim = { '\t', '#' };
                string Name = param[i + 2].TrimEnd(trim);
                List<string> Desc = new List<string>();

                if (Type == ScriptType.Bind)
                    Reserved = param[3 + i++];

                ScriptDeclaration script = new ScriptDeclaration(Name, App, Type, Enabled);
                for (int y = i + 4; y < param.Length; y++)
                    Desc.Add(param[y].Trim(trim));

                script.Description = String.Join(" ",Desc.ToArray());
                script.ReservedPlace = Reserved;
                Scripts.Add(script);
            }
            _IsParsed = true;
            return true;
        }

        public List<ScriptDeclaration> GetScripts()
        {
            return Scripts;
        }
    }
}

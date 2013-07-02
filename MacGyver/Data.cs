using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace MacGyver
{
    public static partial class Data
    {
        public static List<Recipe> Recipes = new List<Recipe>();

        public static List<string> Parameters { get; private set; }
        public static List<string> Pids { get; private set; }

        private static bool parseRecipe(string rec, List<Recipe> newlist)
        {
            //                          1          2       3             4                                        
            Regex regexp = new Regex(@"{([^}]+)}{}{([^@]+)@([^@]+)@([^@]*)@([^@]*)@([^@]*)@([^@]*)@([^@]+)@([^}]*)}");
            Match match = regexp.Match(rec);
            if(!match.Success) return false;
            // 1 pid
            // 2 name
            // 3 description
            // 4 view
            // 5 craft
            // 6 items
            // 7 tools
            // 8 output
            // 9 exp and script

            Recipe newr = new Recipe();
            newr.number = uint.Parse(match.Groups[1].Value);
            newr.name = match.Groups[2].Value;
            newr.desc = match.Groups[3].Value;
            Regex rx = new Regex(@"([A-Z0-9_]+) ([0-9]+)([&|]?)");
            MatchCollection ms = rx.Matches(match.Groups[4].Value);

            foreach(Match m in ms)
                newr.AddView(m.Groups[1].Value, uint.Parse(m.Groups[2].Value), m.Groups[3].Value == "|");

            ms = rx.Matches(match.Groups[5].Value);
            foreach(Match m in ms)
                newr.AddCraft(m.Groups[1].Value, uint.Parse(m.Groups[2].Value), m.Groups[3].Value == "|");

            ms = rx.Matches(match.Groups[6].Value);
            foreach(Match m in ms)
                newr.AddItem(m.Groups[1].Value, uint.Parse(m.Groups[2].Value), m.Groups[3].Value == "|");

            ms = rx.Matches(match.Groups[7].Value);
            foreach(Match m in ms)
                newr.AddTool(m.Groups[1].Value, uint.Parse(m.Groups[2].Value), m.Groups[3].Value == "|");

            ms = rx.Matches(match.Groups[8].Value);
            foreach(Match m in ms)
                newr.AddOutput(m.Groups[1].Value, uint.Parse(m.Groups[2].Value));

            string[] last = match.Groups[9].Value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if(last.Length < 2) return false;
            if(last[0] != "exp") return false;
            newr.exp = uint.Parse(last[1]);
            if(last.Length % 2 != 0) return false;
            if(last.Length > 2)
            {
                if(last[2] != "script") return false;
                newr.script = last[3];
            }
            else newr.script = "";
            newlist.Add(newr);
            return true;
        }

        public static bool ParseRecipes(string filename)
        {
            StreamReader file;
            file = File.OpenText(filename);
            string whole = "";
            while(!file.EndOfStream)
                whole += file.ReadLine();
            Regex regexp = new Regex(@"{([^}]+)}{}{([^}]+)}");
            MatchCollection matches = regexp.Matches(whole);
            List<Recipe> newrecipes = new List<Recipe>();

            foreach(Match match in matches)
                if(!parseRecipe(match.Value, newrecipes))
                {
                    MainForm.Log("Could not load all recipes");
                    file.Close();
                    return false;
                }
            Recipes = newrecipes;
            MainForm.Log("Recipes loaded");
            file.Close();
            return true;
        }

        public static void SaveRecipes(string filename)
        {
            Recipes.Sort();
            StreamWriter file;
            file = File.CreateText(filename);
            foreach(Recipe r in Recipes)
            {
                file.Write("{" + r.number.ToString() + "}{}{");
                file.Write(r.name + "@" + r.desc);
                bool amp = false;
                bool or = false;
                file.Write("@");
                foreach(StringNum sn in r.view)
                {
                    if(amp) file.Write(or ? "|" : "&");
                    file.Write(sn.ToSaveString());
                    amp = true;
                    or = sn.or;
                }
                amp = false;
                file.Write("@");
                foreach(StringNum sn in r.craft)
                {
                    if(amp) file.Write(or ? "|" : "&");
                    file.Write(sn.ToSaveString());
                    amp = true;
                    or = sn.or;
                }
                amp = false;
                file.Write("@");
                foreach(StringNum sn in r.items)
                {
                    if(amp) file.Write(or ? "|" : "&");
                    file.Write(sn.ToSaveString());
                    amp = true;
                    or = sn.or;
                }
                amp = false;
                file.Write("@");
                foreach(StringNum sn in r.tools)
                {
                    if(amp) file.Write(or ? "|" : "&");
                    file.Write(sn.ToSaveString());
                    amp = true;
                    or = sn.or;
                }
                amp = false;
                file.Write("@");
                foreach(StringNum sn in r.output)
                {
                    if(amp) file.Write(or ? "|" : "&");
                    file.Write(sn.ToSaveString());
                    amp = true;
                    or = sn.or;
                }
                file.Write("@exp " + r.exp.ToString());
                if(r.script != "") file.Write(" script " + r.script);
                file.WriteLine("}");
            }
            file.Close();
        }

        public static void ParseParameters()
        {
            Parameters = new List<string>();
            StreamReader file;
            string s;
            file = File.OpenText(Config.ParamNamesPath);
            while(!file.EndOfStream)
            {
                s = file.ReadLine();
                string[] sp = s.Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if(sp.Length > 1) Parameters.Add(sp[sp.Length - 1]);

            }
            //Parameters.Sort();
        }

        public static void ParsePids()
        {
            Pids = new List<string>();
            StreamReader file;
            string s;
            file = File.OpenText(Config.ItemNamesPath);
            while(!file.EndOfStream)
            {
                s = file.ReadLine();
                string[] sp = s.Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if(sp.Length > 1) Pids.Add(sp[sp.Length - 1]);

            }
            Pids.Sort();
        }

        public static Recipe GetRecipe(int num)
        {
            foreach(Recipe r in Recipes)
            {
                if(r.number == num)
                    return r;
            }
            return null;
        }
    }
}

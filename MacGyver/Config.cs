using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MacGyver
{
    public static class Config
    {
        public static String ParamNamesPath { get; private set; }
        public static String ItemNamesPath { get; private set; }

        public static void Init()
        {
            string s = ".";
            try
            {
                StreamReader f = File.OpenText("MacGyver.cfg");
                s = f.ReadLine();
                f.Close();
            }
            catch(Exception) { }
            ParamNamesPath = Path.Combine(s, @"data/ParamNames.lst");
            ItemNamesPath = Path.Combine(s, @"data/ItemNames.lst");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptsRefactorer
{
    class Program
    {
        private static String filename;
        private static String newline;
        public static Boolean Fixargs;

        public static void Log(String text, bool withfile = true)
        {
            if(withfile) Console.Write(filename + ": ");
            Console.WriteLine(text);
        }

        public static void LogNoLine(String text, bool withfile = true)
        {
            if (withfile) Console.Write(filename + ": ");
            Console.Write(text);
        }

        static void ProcessArg(String s)
        {
            if(s == "-a") Fixargs = true;
            if(s == "-lf") newline = "\n";
        }

        static void Main(String[] args)
        {
            if (args.Length < 2)
            {
                Log("Usage\nScriptsRefactorer.exe in.fos out.fos [-lf]");
                return;
            }
            newline = "\r\n";
            filename = args[0];
            Fixargs = false;
            for (int i = 2; i < args.Length; i++) ProcessArg(args[i]);
            Tokenizer tokenizer = new Tokenizer(args[0], newline);
            if(!tokenizer.Parse())
                Log("Failed to parse the file.");
            else
                tokenizer.Save(args[1]);
            Log("Finished.");
        }
    }
}

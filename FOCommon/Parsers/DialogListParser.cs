using System;
using System.Collections.Generic;
using System.IO;

using FOCommon.Dialog;

namespace FOCommon.Parsers
{
    // This class parses dialog.lst
    public class DialogListParser : BaseParser, IParser
    {
        string filename;
        List<ListDialog> Dialogs = new List<ListDialog>();

        public DialogListParser(string filename)
        {
            this.filename = filename;
        }

        public string GetNameById(int id)
        {
            foreach (ListDialog dialog in Dialogs)
                if (id == dialog.Id)
                    return dialog.Name;
            return "";
        }

        public List<ListDialog> GetDialogs(bool onlyEnabled)
        {
            List<ListDialog> retDialogs = new List<ListDialog>();
            foreach (ListDialog dialog in Dialogs)
            {
                if ((onlyEnabled && !dialog.Enabled))
                    continue;
                retDialogs.Add(dialog);
            }
            return retDialogs;
        }

        public bool Parse()
        {
            if (!File.Exists(filename))
                return false;
            Dialogs.Clear();
            List<String> lines = new List<string>(File.ReadAllLines(filename));
            foreach (String line in lines)
            {
                if (line.Contains("*")||string.IsNullOrEmpty(line))
                    continue;

                char[] del = {'\t', ' '};
                string[] parts = line.Split(del, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2)
                    continue;
                int i = 0;
                bool enabled;
                if (parts.Length == 3)
                {
                    enabled = true;
                    i = 1;
                }
                else
                    enabled = false;
                Dialogs.Add(new ListDialog(Int32.Parse(parts[i]), parts[i + 1], enabled));
                
            }
            _IsParsed = true;
            return true;
        }
    }
}

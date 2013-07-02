using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptsRefactorer
{
    public class Type
    {
        public static String[] BasicTypes = { "bool", "uint64", "uint", "uint16", "uint8", "int64", "int", "int16", "int8", "float" };
        public bool IsConst;
        public String Name;
        public bool IsTemplate;
        public Type SubType;
        public String ModName;

        public Type()
        {
            IsConst = false;
            Name = "";
            IsTemplate = false;
            SubType = null;
            ModName = "";
        }

        public bool IsBasic()
        {
            return BasicTypes.Contains<String>(Name); 
        }

        public String GetString()
        {
            String st="";
            if (IsConst) st += "const ";
            st += Name;
            if (IsTemplate) st += "<" + SubType.GetString() + ">";
            st +=ModName;
            return st;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MacGyver
{
    public class Recipe : IComparable
    {
        public uint number { get; set; }
        public string name { get; set; }
        public uint exp { get; set; }
        public string script { get; set; }
        public List<StringNum> view { get; set; }
        public List<StringNum> craft { get; set; }
        public List<StringNum> items { get; set; }
        public List<StringNum> tools { get; set; }
        public List<StringNum> output { get; set; }
        public string desc { get; set; }
        public Recipe()
        {
            number = 0;
            name = null;
            exp = 0;
            script = null;
            desc = null;
            view = new List<StringNum>();
            craft = new List<StringNum>();
            items = new List<StringNum>();
            tools = new List<StringNum>();
            output = new List<StringNum>();
        }
        public int CompareTo(object obj)
        {
            if(obj is Recipe)
            {
                Recipe r = (Recipe)obj;

                return number.CompareTo(r.number);
            }

            throw new ArgumentException("Terrible error number 789");
        }

        public void AddView(string pid, uint num, bool or)
        {
            StringNum stringnum = new StringNum();
            stringnum.st = pid;
            stringnum.num = num;
            stringnum.or = or;
            stringnum.type = 1;
            view.Add(stringnum);
        }

        public void AddCraft(string pid, uint num, bool or)
        {
            StringNum stringnum = new StringNum();
            stringnum.st = pid;
            stringnum.num = num;
            stringnum.or = or;
            stringnum.type = 1;
            craft.Add(stringnum);
        }

        public void AddItem(string pid, uint num, bool or)
        {
            StringNum stringnum = new StringNum();
            stringnum.st = pid;
            stringnum.num = num;
            stringnum.or = or;
            stringnum.type = 0;
            items.Add(stringnum);
        }

        public void AddTool(string pid, uint num, bool or)
        {
            StringNum stringnum = new StringNum();
            stringnum.st = pid;
            stringnum.num = num;
            stringnum.or = or;
            stringnum.type = 0;
            tools.Add(stringnum);
        }

        public void AddOutput(string pid, uint num)
        {
            StringNum stringnum = new StringNum();
            stringnum.st = pid;
            stringnum.num = num;
            stringnum.type = 2;
            output.Add(stringnum);
        }

    }

    public class StringNum
    {
        public string st;
        public uint num;
        public bool or;

        // 0 tool or item
        // 1 param
        // 2 output
        public uint type;
        public StringNum()
        {
            st = null;
            num = 0;
            type = 0;
            or = false;
        }
        public StringNum(string s, uint n)
        {
            st = s;
            num = n;
            type = 0;
            or = false;
        }
        public override string ToString()
        {
            if(type != 1)
                return st + " " + num.ToString() + " pcs." + (type == 0 ? (or ? " or" : " and") : "");
            else
                return st + " " + num.ToString() + (or ? " or" : " and");
        }

        public string ToSaveString()
        {
            return st + " " + num.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeAnalyzer
{
    class TreeModel
    {
        public TreeModel(string n, float i, float e)
        {
            children = new List<TreeModel>();
            Name = n;
            Incl = i;
            Excl = e;
        }

        public int ChildCount { get { return children.Count; } }
        public List<TreeModel> Children { get { return children; } }
        public string Label { get; set; }
        public TreeModel Parent { get; set; }
        public string ParentLabel { get { if (Parent == null) return ""; else return Parent.Label; } }

        public TreeModel AddChild(string n, float i, float e)
        {
            TreeModel child = new TreeModel(n, i, e);
            children.Add(child);
            return child;
        }

        private List<TreeModel> children;

        public string Name;
        public string InclStr { get { return String.Format("{0:00.00}%", Incl); } }
        public string ExclStr { get { return String.Format("{0:00.00}%", Excl); } }

        public float Incl;
        public float Excl;
    }
}

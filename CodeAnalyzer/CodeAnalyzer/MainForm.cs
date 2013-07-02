using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BrightIdeasSoftware;

namespace CodeAnalyzer
{
    public partial class MainForm : Form
    {
        private Analyzer analyzer;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            callView.CanExpandGetter = delegate(object x) { return ((TreeModel)x).Children.Count > 0; };
            callView.ChildrenGetter = delegate(object x) { return ((TreeModel)x).Children; };
            moduleView.CanExpandGetter = delegate(object x) { return ((TreeModel)x).Children.Count > 0; };
            moduleView.ChildrenGetter = delegate(object x) { return ((TreeModel)x).Children; };
            callView.PrimarySortColumn = callViewColIncl;
            callView.PrimarySortOrder = SortOrder.Descending;
            moduleView.PrimarySortColumn = moduleViewColExcl;
            moduleView.PrimarySortOrder = SortOrder.Descending;

            this.SetStatus("Ready.");
        }

        private void loadFile(string fileName)
        {
            analyzer = new Analyzer(fileName);
            analyzer.Load(this);

            this.cmbModules.DataSource = analyzer.GetModuleNames();
            this.moduleView.Roots = analyzer.GetModulesModel();
            this.callView.Roots = analyzer.GetCallModel();
            moduleView.Sort();
            callView.Sort();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Profiler file (*.foprof)|*.foprof";
            dlg.CheckFileExists = true;
            dlg.RestoreDirectory = true;
            DialogResult ret = dlg.ShowDialog();
            if (ret == System.Windows.Forms.DialogResult.OK)
                loadFile(dlg.FileName);
        }

        private void cmbModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mod = this.cmbModules.SelectedItem.ToString();
            string[] lines = analyzer.GetSourceSection(mod).ToArray();
            this.rtbFunctionDetails.Clear();
            this.rtbFunctionDetails.Lines = lines;
            this.cmbFunctions.DataSource = analyzer.GetModuleFunctionNames(mod);
        }

        private void cmbFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mod = this.cmbModules.SelectedItem.ToString();
            string func = this.cmbFunctions.SelectedItem.ToString();
            int line = analyzer.GetMaxHits(mod+"@"+func);
            if (line != -1)
            {
                string[] lines = this.rtbFunctionDetails.Lines;
                int total = 0;
                for (int i = 0; i < line; i++) total += lines[i].Length + 1;
                this.rtbFunctionDetails.SelectionStart = total;
                this.rtbFunctionDetails.ScrollToCaret();
            }
        }

        public void SetStatus(string status)
        {
            if (status == null) this.lblStatus.Visible = false;
            else
            {
                this.lblStatus.Visible = true;
                this.lblStatus.Text = status;
            }
        }

        public void ShowBar(bool show)
        {
            this.pbStatus.Visible = show;
        }

        public void SetBar(int val)
        {
            if (val != this.pbStatus.Value)
            {
                this.pbStatus.Value = val;
                this.Update();
            }
        }

        public void SetBar(int val, int max)
        {
            if (max == 0) return;
            Int64 tmp = (Int64)100 * (Int64)val;
            tmp /= max;
            this.SetBar((int)tmp);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

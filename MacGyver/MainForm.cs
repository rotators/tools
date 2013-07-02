using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MacGyver
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.recipeList.SetObjects(Data.Recipes);
            Data.ParseParameters();
            Data.ParsePids();
            this.cbParameters.DataSource = Data.Parameters;
            this.cbPids.DataSource = Data.Pids;
            this.lbItems.DataSource = curItems;
            this.lbTools.DataSource = curTools;
            this.lbOutput.DataSource = curOutput;
            this.ActiveControl = cbParameters;
            MainForm.self = this;
        }

        static MainForm self;

        List<StringNum> curView = new List<StringNum>();
        List<StringNum> curCraft = new List<StringNum>();
        List<StringNum> curItems = new List<StringNum>();
        List<StringNum> curTools = new List<StringNum>();
        List<StringNum> curOutput = new List<StringNum>();

        void loadRecipe(Recipe recipe)
        {
            curView.Clear();
            recipe.view.ForEach((item) => { curView.Add(item); });
            curCraft.Clear();
            recipe.craft.ForEach((item) => { curCraft.Add(item); });
            curItems.Clear();
            recipe.items.ForEach((item) => { curItems.Add(item); });
            curTools.Clear();
            recipe.tools.ForEach((item) => { curTools.Add(item); });
            curOutput.Clear();
            recipe.output.ForEach((item) => { curOutput.Add(item); });
            rtbDescription.Text = recipe.desc;
            txtName.Text = recipe.name;
            txtScript.Text = recipe.script;
            numNumber.Value = recipe.number;
            numExperience.Value = recipe.exp;
            refreshAll();
        }

        void addItem(List<StringNum> list, string s, uint n, bool set0)
        {
            if (n == 0) return;
            StringNum el = new StringNum(s, n);
            el.or = cbOperator.Checked;
            if (!set0) el.type = 2;
            list.Add(el);
        }

        void addParam(List<StringNum> list, string s, uint n)
        {
            if (n == 0) return;
            StringNum el = new StringNum(s, n);
            el.type = 1;
            el.or = cbOperator.Checked;
            list.Add(el);
        }

        void refreshAll()
        {
            this.lbCraftParams.DataSource = null;
            this.lbCraftParams.DataSource = curCraft;
            this.lbItems.DataSource = null;
            this.lbItems.DataSource = curItems;
            this.lbTools.DataSource = null;
            this.lbTools.DataSource = curTools;
            this.lbOutput.DataSource = null;
            this.lbOutput.DataSource = curOutput;
            this.lbViewParams.DataSource = null;
            this.lbViewParams.DataSource = curView;
        }

        void refreshRecipes()
        {
            this.recipeList.SetObjects(null);
            this.recipeList.SetObjects(Data.Recipes);
        }

        public static void Log(string s)
        {
            if(self == null) return;
            self.stripLog.Text = s;
        }

        #region Events
        private void btnReqItem_Click(object sender, EventArgs e)
        {
            addItem(curItems, cbPids.SelectedItem.ToString(), (uint)numCount.Value,true);
            this.lbItems.DataSource = null;
            this.lbItems.DataSource = curItems;
        }

        private void btnReqTool_Click(object sender, EventArgs e)
        {
            addItem(curTools, cbPids.SelectedItem.ToString(), (uint)numCount.Value,true);
            this.lbTools.DataSource = null;
            this.lbTools.DataSource = curTools;
        }

        private void btnOutputItem_Click(object sender, EventArgs e)
        {
            addItem(curOutput, cbPids.SelectedItem.ToString(), (uint)numCount.Value,false);
            this.lbOutput.DataSource = null;
            this.lbOutput.DataSource = curOutput;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            addParam(curView, cbParameters.SelectedItem.ToString(), (uint)numericUpDown3.Value);
            this.lbViewParams.DataSource = null;
            this.lbViewParams.DataSource = curView;
        }

        private void btnCraft_Click(object sender, EventArgs e)
        {
            addParam(curCraft, cbParameters.SelectedItem.ToString(), (uint)numericUpDown3.Value);
            this.lbCraftParams.DataSource = null;
            this.lbCraftParams.DataSource = curCraft;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            curView.Clear();
            curCraft.Clear();
            curItems.Clear();
            curTools.Clear();
            curOutput.Clear();
            numNumber.Value = 0;
            numExperience.Value = 0;
            txtName.Text = null;
            txtScript.Text = null;
            rtbDescription = null;
            refreshAll();
        }

        private void lbViewParams_DoubleClick(object sender, EventArgs e)
        {
            int n = lbViewParams.SelectedIndex;
            if (n < 0) return;
            curView.RemoveAt(n);
            lbViewParams.DataSource = null;
            lbViewParams.DataSource = curView;
        }

        private void lbCraftParams_DoubleClick(object sender, EventArgs e)
        {
            int n = lbCraftParams.SelectedIndex;
            if (n < 0) return;
            curCraft.RemoveAt(n);
            lbCraftParams.DataSource = null;
            lbCraftParams.DataSource = curCraft;
        }

        private void lbItems_DoubleClick(object sender, EventArgs e)
        {
            int n = lbItems.SelectedIndex;
            if (n < 0) return;
            curItems.RemoveAt(n);
            lbItems.DataSource = null;
            lbItems.DataSource = curItems;
        }

        private void lbItems_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                int n = lbItems.SelectedIndex;
                if (n < 0) return;
                if (e.Delta < 0)
                    if (curItems[n].num > 1) curItems[n].num--;
                if (e.Delta > 0)
                    curItems[n].num++;
                ((HandledMouseEventArgs)e).Handled = true;
                lbItems.DataSource = null;
                lbItems.DataSource = curItems;
                lbItems.SelectedIndex = n;
            }
        }

        private void lbTools_DoubleClick(object sender, EventArgs e)
        {
            int n = lbTools.SelectedIndex;
            if (n < 0) return;
            curTools.RemoveAt(n);
            lbTools.DataSource = null;
            lbTools.DataSource = curTools;
        }

        private void lbOutput_DoubleClick(object sender, EventArgs e)
        {
            int n = lbOutput.SelectedIndex;
            if (n < 0) return;
            curOutput.RemoveAt(n);
            lbOutput.DataSource = null;
            lbOutput.DataSource = curOutput;
        }

        private void btnAddUpdate_Click(object sender, EventArgs e)
        {
            // adding recipe
            if (txtName.Text == "")
            {
                Log("Name field cannot be empty");
                return;
            }
            if (curItems.Count == 0 || curOutput.Count == 0)
            {
                Log("Items or output fields cannot be empty");
                return;
            }
            for (int i = 0, j = Data.Recipes.Count; i < j; i++)
            {
                if (Data.Recipes[i].number == numNumber.Value)
                {
                    if(MessageBox.Show("A recipe with number " + numNumber.Value + " already exists (" + Data.Recipes[i].name + "), overwrite?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        return;
                    else
                    {
                        Data.Recipes.RemoveAt(i);
                        break;
                    }
                }
            }

            Recipe rec = new Recipe();
            rec.number = (uint)numNumber.Value;
            rec.name = txtName.Text;
            rec.exp = (uint)numExperience.Value;
            rec.script = txtScript.Text;
            curView.ForEach((item) => { rec.view.Add(item); });
            curCraft.ForEach((item) => { rec.craft.Add(item); });
            curItems.ForEach((item) => { rec.items.Add(item); });
            curTools.ForEach((item) => { rec.tools.Add(item); });
            curOutput.ForEach((item) => { rec.output.Add(item); });
            rec.desc = rtbDescription.Text;
            Data.Recipes.Add(rec);
            refreshRecipes();
            Log("Recipe added");
        }

        private void recipeList_DoubleClick(object sender, EventArgs e)
        {
            BrightIdeasSoftware.OLVListItem what = recipeList.GetItem(recipeList.SelectedIndex);
            decimal num = decimal.Parse(what.Text);
            Recipe r = Data.GetRecipe((int)num);

            if(r == null)
            {
                Log("Internal error: could not load recipe.");
                return;
            }

            loadRecipe(r);
            Log("Recipe loaded");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // load from file
            OpenFileDialog opend = new OpenFileDialog();
            opend.Filter = "MSG files (*.MSG)|*.MSG";
            if (opend.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if(Data.ParseRecipes(opend.FileName))
                {
                    refreshRecipes();
                    Log("Recipe list loaded");
                }
                else
                    Log("Error: couldn't parse the recipe list");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saved = new SaveFileDialog();
            saved.Filter = "MSG files (*.MSG)|*.MSG";

            if (saved.ShowDialog() == System.Windows.Forms.DialogResult.OK
                && saved.FileName.Length > 0)
                Data.SaveRecipes(saved.FileName);
        }

        #endregion
    }
}

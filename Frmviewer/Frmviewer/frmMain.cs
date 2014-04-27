using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using FOCommon.Graphic;
using FOCommon.Controls;

namespace Frmviewer
{
    public partial class frmMain : Form
    {
        Color Background;
        Color Mask;
        List<Bitmap> loadedFrm;

        public frmMain()
        {
            InitializeComponent();
            Background = pnlRender.BackColor;
            Mask = Color.FromArgb(11, 0, 11);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.CheckPathExists = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //FalloutFRM frm = new FalloutFRM();
                loadedFrm = FalloutFRM.Load(fileDialog.FileName, Mask);
                pnlRender.Invalidate();
            }
        }

        private void pnlRender_Paint(object sender, PaintEventArgs e)
        {
            if (loadedFrm == null) return;
            Pen pen = new Pen(Background);
            pnlRender.Width = 200 + loadedFrm[0].Width;
            pnlRender.Height = 200 + loadedFrm[0].Height;
            pnlRender.BackColor = Background;
            e.Graphics.DrawImage(loadedFrm[0], (pnlRender.Width / 2) - loadedFrm[0].Width, (pnlRender.Height / 2) - loadedFrm[0].Height);
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Background = colorDialog1.Color;
        }

        private void maskColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Mask = colorDialog1.Color;
        }
    }
}

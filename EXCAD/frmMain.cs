using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace EXCAD
{
    public partial class frmMain : Form
    {
        frmCommand commandForm;
        public static frmAppHost excelForm;
        public static frmAppHost autocadForm;

        SaveFileDialog saveFileDialog = new SaveFileDialog()
        {
            Filter = "EXCAD Files (*.excad)|*.excad|All files (*.*)|*.*",
            DefaultExt = "excad",
            AddExtension = true
        };
        OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Filter = "EXCAD Files (*.excad)|*.excad|All files (*.*)|*.*",
        };

        string WindowTitle
        {
            get => string.IsNullOrEmpty(AppManager.CurrentFile) ? "EXCAD" : $"EXCAD - {AppManager.CurrentFile}";
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
            string[] args = Environment.GetCommandLineArgs();
            if (args?.Count()>1)
                OpenEXCADFile(args[1]);
        }

        private void commandToolStripMenuItem_Click(object sender, EventArgs e) => ShowCommandForm();

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e) => ArrangeWindows(MdiLayout.Cascade);

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e) => ArrangeWindows(MdiLayout.TileHorizontal);

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e) => ArrangeWindows(MdiLayout.TileVertical);

        private void dockApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppManager.ApplicationsStarted)
            {
                if (excelForm == null || excelForm.IsDisposed)
                    (excelForm = new frmAppHost(AppManager.ExcelApp) { MdiParent = this }).Show();
                else
                    excelForm.DockApp();

                if (autocadForm == null || autocadForm.IsDisposed)
                    (autocadForm = new frmAppHost(AppManager.AutoCADApp) { MdiParent = this }).Show();
                else
                    autocadForm.DockApp();
            }
            else
                MessageBox.Show("Please Start Applications in File->Command->Start");
        }

        private void undockApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (excelForm != null && !excelForm.IsDisposed)
                excelForm.UndockApp();

            if (autocadForm != null && !autocadForm.IsDisposed)
                autocadForm.UndockApp();
        }

        private void ShowCommandForm()
        {
            if (commandForm == null || commandForm.IsDisposed)
                (commandForm = new frmCommand() { MdiParent = null }).Show(this);
            else
                commandForm.Activate();
        }

        private void ArrangeWindows(MdiLayout mdiLayout)
        {
            this.LayoutMdi(mdiLayout);
            excelForm?.DockApp();
            autocadForm?.DockApp();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AppManager.Saved)
            {
                DialogResult save = MessageBox.Show("Do you want to save changes?", "EXCAD", MessageBoxButtons.YesNoCancel);
                if (save == DialogResult.Yes)
                {
                    if (!string.IsNullOrEmpty(AppManager.CurrentFile))
                    {
                        AppManager.Save(AppManager.CurrentFile);
                        OpenEXCADFile("");
                    }
                    else
                    {
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            AppManager.Save(saveFileDialog.FileName);
                            OpenEXCADFile("");
                        }
                        else
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
                else if (save == DialogResult.No)
                {
                    OpenEXCADFile("");
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }

            try
            {
                AppManager.Exit();
            }
            finally { }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartExternalApplications();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppManager.Saved)
                OpenEXCADFile("");
            else
            {
                DialogResult save = MessageBox.Show("Do you want to save changes?", "EXCAD", MessageBoxButtons.YesNoCancel);
                if (save == DialogResult.Yes)
                {
                    if (!string.IsNullOrEmpty(AppManager.CurrentFile))
                    {
                        AppManager.Save(AppManager.CurrentFile);
                        OpenEXCADFile("");
                    }
                    else if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        AppManager.Save(saveFileDialog.FileName);
                        OpenEXCADFile("");
                    }
                }
                else if (save == DialogResult.No)
                {
                    OpenEXCADFile("");
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (AppManager.Saved)
                    OpenEXCADFile(openFileDialog.FileName);
                else
                {
                    DialogResult save = MessageBox.Show("Do you want to save changes?", "EXCAD", MessageBoxButtons.YesNoCancel);
                    if (save == DialogResult.Yes)
                    {
                        if (!string.IsNullOrEmpty(AppManager.CurrentFile))
                        {
                            AppManager.Save(AppManager.CurrentFile);
                            OpenEXCADFile(openFileDialog.FileName);
                        }
                        else if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            AppManager.Save(saveFileDialog.FileName);
                            OpenEXCADFile(openFileDialog.FileName);
                        }
                    }
                    else if (save == DialogResult.No)
                    {
                        OpenEXCADFile(openFileDialog.FileName);
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AppManager.CurrentFile))
                AppManager.Save(AppManager.CurrentFile);
            else if (saveFileDialog.ShowDialog() == DialogResult.OK)
                AppManager.Save(saveFileDialog.FileName);
            this.Text = WindowTitle;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                AppManager.Save(saveFileDialog.FileName);
            this.Text = WindowTitle;
        }



        private void OpenEXCADFile(string file)
        {
            this.Text = string.IsNullOrEmpty(file) ? "Creating new excad file..." : "Opening excad file...";
            Task.Factory.StartNew(() =>
            {
                if (string.IsNullOrEmpty(file))
                    AppManager.New();
                else
                    AppManager.Open(file);
            }).ContinueWith(task => Invoke(new Action(() => this.Text = WindowTitle)));
        }

        private void StartExternalApplications()
        {
            this.Text = "Starting external applications...";
            Task.Factory.StartNew(() => AppManager.Start())
                .ContinueWith(task => Invoke(new Action(() => this.Text = WindowTitle)));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EXCAD
{
    public partial class frmAppHost : Form
    {
        BackgroundWorker backgroundWorker;
        IApplication app;
        Size dockSize;
        IntPtr appPreviousParent;
        bool appUndocked = false;

        private frmAppHost()
        {
            InitializeComponent();
        }

        public frmAppHost(IApplication app) : this()
        {
            this.backgroundWorker = new BackgroundWorker();
            this.backgroundWorker.DoWork += BackgroundWorker_DoWork;
            this.backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            this.app = app;
            this.app.Exited += App_Exited;
        }

        private void App_Exited(object sender, EventArgs e) => this.Close();

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == Helper.WM_SYSCOMMAND)
                if (m.WParam == (IntPtr)Helper.SC_MAXIMIZE ||
                    m.WParam == (IntPtr)Helper.SC_MAXIMIZE2 ||
                    m.WParam == (IntPtr)Helper.SC_RESTORE ||
                    m.WParam == (IntPtr)Helper.SC_RESTORE2)
                    this.OnResizeEnd(EventArgs.Empty);
        }

        private void frmAppHost_Load(object sender, EventArgs e)
        {
            this.backgroundWorker.RunWorkerAsync();
        }
        private void frmAppHost_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.appUndocked)
                this.app.Quit();
        }

        private void frmAppHost_ResizeEnd(object sender, EventArgs e)
        {
            if (!this.dockSize.Equals(pnlHost.Size))
                DockApp();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.app.Process.WaitForInputIdle();
            while (this.app.Process.MainWindowHandle == IntPtr.Zero)
            {
                Thread.Sleep(100);
                this.app.Process.Refresh();
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.appPreviousParent = DockApp();
            lblLoading.Visible = false;
            if (e.Error != null)
                MessageBox.Show(e.Error.Message);
        }

        public IntPtr DockApp()
        {
            IntPtr previouseParent = IntPtr.Zero;
            Helper.ShowWindow(this.app.Process.MainWindowHandle, Helper.SW_RESTORE);
            var rootHwnds = Helper.GetRootWindowsOfProcess(this.app.Process.Id)
                .OrderBy(i => i == this.app.Process.MainWindowHandle ? 0 : 1);
            foreach (var rootHwnd in rootHwnds)
                Helper.SetParent(rootHwnd, pnlHost.Handle);
            this.app.Process.WaitForInputIdle();
            Helper.MoveWindow(this.app.Process.MainWindowHandle, 0, 0, pnlHost.Width, pnlHost.Height, true);
            this.dockSize = pnlHost.Size;
            return previouseParent;
        }

        public void UndockApp()
        {
            Rectangle hostRectInScreen = pnlHost.RectangleToScreen(pnlHost.ClientRectangle);
            
            Helper.ShowWindow(this.app.Process.MainWindowHandle, Helper.SW_RESTORE);
            Helper.SetParent(this.app.Process.MainWindowHandle, this.appPreviousParent);
            Helper.ShowWindow(this.app.Process.MainWindowHandle, Helper.SW_RESTORE);
            Helper.MoveWindow(this.app.Process.MainWindowHandle, hostRectInScreen.Left, hostRectInScreen.Top, pnlHost.Width, pnlHost.Height, true);
            this.appUndocked = true;
            this.Close();
        }
    }
}

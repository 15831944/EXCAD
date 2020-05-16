using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using EXCEL = Microsoft.Office.Interop.Excel;

namespace EXCAD
{
    class Excel : IApplication
    {
        public event EventHandler Exited;

        public static readonly string[] Extensions = { ".xlsx", ".xlsm" };

        EXCEL.Application excelApp;

        public Process Process { get; private set; }

        public object AppObj { get => this.excelApp; }

        public bool Saved
        {
            get
            {
                int count = this.excelApp.Workbooks.Count;
                for (int i = 1; i <= count; i++)
                {
                    EXCEL.Workbook document = this.excelApp.Workbooks[i];
                    if (!document.Saved)
                        return false;
                }
                return true;
            }
        }

        public bool Terminated
        {
            get
            {
                return Process.HasExited || !excelApp.Visible;
            }
        }

        public Excel()
        {
            Helper.COMCall(() => this.excelApp = new EXCEL.Application());
            this.excelApp.Visible = true;
            this.excelApp.SheetsInNewWorkbook = 1;
            Helper.GetWindowThreadProcessId(excelApp.Hwnd, out int processId);
            Process = Process.GetProcessById(processId);
        }

        public void New()
        {
            Helper.COMCall(() => this.excelApp.Workbooks.Add(Missing.Value));
        }

        public void CloseAll()
        {
            while (this.excelApp.Workbooks.Count > 0)
                this.excelApp.Workbooks[1].Close(false);
        }
        public IEnumerable<string> SaveAll(string saveFolder)
        {
            List<string> savedFiles = new List<string>();
            this.excelApp.DisplayAlerts = false;
            int count = this.excelApp.Workbooks.Count;
            for (int i = 1; i <= count; i++)
            {
                EXCEL.Workbook document = this.excelApp.Workbooks[i];
                string nameWithoutExtention = Path.GetFileNameWithoutExtension(document.Name);
                string extension = Path.GetExtension(document.Name) == ".xlsm" || document.HasVBProject ? ".xlsm" : ".xlsx";
                string name = $"{nameWithoutExtention}{extension}";
                int index = 1;
                while (savedFiles.Contains(name))
                    name = $"{nameWithoutExtention} ({++index}){extension}";
                string fullName = Path.Combine(saveFolder, name);
                Helper.COMCall(() => document.SaveAs(fullName));
                savedFiles.Add(name);
            }
            this.excelApp.DisplayAlerts = true;
            return savedFiles;
        }

        public void Open(string doc)
        {
            Helper.COMCall(() => this.excelApp.Workbooks.Open(doc));
        }

        public void Quit()
        {
            try
            {
                CloseAll();
            }
            catch { }
            this.Process.Kill();
        }

        private void OnExited() => this.Exited.Invoke(this, EventArgs.Empty);

        public void SetRangeValue(string range, object value)
        {
            this.excelApp.get_Range(range).Value = value;
        }

        public object GetRangeValue(string range)
        {
            return excelApp.get_Range(range).Value;
        }

        public object SetRangeForeground(string range, int color)
        {
            return excelApp.get_Range(range).Font.Color = color;
        }

        public object SetRangeBackground(string range, int color)
        {
            return excelApp.get_Range(range).Interior.Color = color;
        }
    }
}

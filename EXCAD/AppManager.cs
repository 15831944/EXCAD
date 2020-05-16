using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXCAD
{
    class AppManager
    {
        static string workspaceFolder;

        public static Excel ExcelApp { get; private set; }
        public static AutoCAD AutoCADApp { get; private set; }
        public static string CurrentFile { get; private set; }

        static AppManager()
        {
            ResetWorkspaceFolder();
        }

        public static bool ExcelStarted
        {
            get => ExcelApp != null && !ExcelApp.Terminated;
        }

        public static bool AutoCADStarted
        {
            get => AutoCADApp != null && !AutoCADApp.Terminated;
        }

        public static bool ApplicationsStarted
        {
            get => ExcelStarted && AutoCADStarted;
        }

        public static bool Saved
        {
            get =>
                (!ExcelStarted || ExcelApp.Saved) &&
                (!AutoCADStarted || AutoCADApp.Saved);
        }

        private static void ResetWorkspaceFolder()
        {
            string guid = Guid.NewGuid().ToString();
            string ws = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EXCAD", guid);
            Directory.CreateDirectory(ws);
            workspaceFolder = ws;
        }

        public static void Start()
        {
            var tskStartExcel = Task.Factory.StartNew(() =>
            {
                if (!ExcelStarted)
                    ExcelApp = new Excel();
            });
            var tskStartAutoCAD = Task.Factory.StartNew(() =>
            {
                if (!AutoCADStarted)
                    AutoCADApp = new AutoCAD();
            });
            tskStartExcel.Wait();
            tskStartAutoCAD.Wait();
        }

        public static void Exit()
        {
            var tskStartExcel = Task.Factory.StartNew(() =>
            {
                if (ExcelStarted)
                    ExcelApp.Quit();
            });
            var tskStartAutoCAD = Task.Factory.StartNew(() =>
            {
                if (AutoCADStarted)
                    AutoCADApp.Quit();
            });
            tskStartExcel.Wait();
            tskStartAutoCAD.Wait();

            if (!string.IsNullOrEmpty(workspaceFolder))
                Directory.Delete(workspaceFolder, true);
        }

        public static void New()
        {
            CurrentFile = null;

            var tskCloseExcelDocs = Task.Factory.StartNew(() =>
            {
                if (!ExcelStarted)
                    ExcelApp = new Excel();
                else
                    ExcelApp.CloseAll();
                ExcelApp.New();
            });
            var tskCloseAutoCADDocs = Task.Factory.StartNew(() =>
            {
                if (!AutoCADStarted)
                    AutoCADApp = new AutoCAD();
                else
                    AutoCADApp.CloseAll();
                AutoCADApp.New();
            });

            tskCloseExcelDocs.Wait();
            tskCloseAutoCADDocs.Wait();
        }

        public static void Save(string file)
        {
            var tskSaveExcelDocs = Task<IEnumerable<string>>.Factory.StartNew(() =>
            {
                if (ExcelStarted)
                    return ExcelApp.SaveAll(workspaceFolder);
                else
                    return new string[0];
            });
            var tskSaveAutoCADDocs = Task<IEnumerable<string>>.Factory.StartNew(() =>
            {
                if (AutoCADStarted)
                    return AutoCADApp.SaveAll(workspaceFolder);
                else
                    return new string[0];
            });
            tskSaveExcelDocs.Wait();
            tskSaveAutoCADDocs.Wait();
            List<string> allDocs = new List<string>();
            allDocs.AddRange(tskSaveExcelDocs.Result);
            allDocs.AddRange(tskSaveAutoCADDocs.Result);
            string guid = Guid.NewGuid().ToString();
            string tempArchiveFolder = Path.Combine(Path.GetTempPath(), guid);
            string tempArchiveFile = Path.Combine(Path.GetTempPath(), guid + ".tmp");
            Directory.CreateDirectory(tempArchiveFolder);
            foreach (string doc in allDocs)
                File.Copy(Path.Combine(workspaceFolder, doc), Path.Combine(tempArchiveFolder, doc), true);
            ZipFile.CreateFromDirectory(tempArchiveFolder, tempArchiveFile);
            File.Copy(tempArchiveFile, file, true);
            Directory.Delete(tempArchiveFolder, true);
            File.Delete(tempArchiveFile);
            CurrentFile = file;
        }

        public static void Open(string file)
        {
            CurrentFile = null;

            var tskCloseExcelDocs = Task.Factory.StartNew(() =>
            {
                if (!ExcelStarted)
                    ExcelApp = new Excel();
                else
                    ExcelApp.CloseAll();
            });
            var tskCloseAutoCADDocs = Task.Factory.StartNew(() =>
            {
                if (!AutoCADStarted)
                    AutoCADApp = new AutoCAD();
                else
                    AutoCADApp.CloseAll();
            });

            string tempArchiveFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            ZipFile.ExtractToDirectory(file, tempArchiveFolder);
            var allDocs = new DirectoryInfo(tempArchiveFolder).GetFiles();
            var excelDocs = new List<string>();
            var autocadDocs = new List<string>();
            string oldWorkspaceFolder = workspaceFolder;
            ResetWorkspaceFolder();
            foreach (var doc in allDocs)
            {
                string workspaceDoc = Path.Combine(workspaceFolder, doc.Name);
                if (Excel.Extensions.Contains(doc.Extension, StringComparer.OrdinalIgnoreCase))
                    excelDocs.Add(workspaceDoc);
                else if (AutoCAD.Extensions.Contains(doc.Extension, StringComparer.OrdinalIgnoreCase))
                    autocadDocs.Add(workspaceDoc);
                else
                    continue;
                File.Copy(doc.FullName, Path.Combine(workspaceFolder, workspaceDoc), true);
            }
            Directory.Delete(tempArchiveFolder, true);

            var tskOpenExcelDocs = Task.Factory.StartNew(() =>
            {
                tskCloseExcelDocs.Wait();
                foreach (string doc in excelDocs)
                    ExcelApp.Open(doc);
            });
            var tskOpenAutoCADDocs = Task.Factory.StartNew(() =>
            {
                tskCloseAutoCADDocs.Wait();
                foreach (string doc in autocadDocs)
                    AutoCADApp.Open(doc);
            });

            tskOpenExcelDocs.Wait();
            tskOpenAutoCADDocs.Wait();

            if (!string.IsNullOrEmpty(oldWorkspaceFolder))
                Directory.Delete(oldWorkspaceFolder, true);

            CurrentFile = file;
        }
    }
}

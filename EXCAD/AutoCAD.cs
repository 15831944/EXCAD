using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AUTOCAD = AutoCAD;

namespace EXCAD
{
    class AutoCAD : IApplication
    {
        public event EventHandler Exited;

        public static readonly string[] Extensions = { ".dwg" };

        AUTOCAD.AcadApplication autocadApp;

        public Process Process { get; private set; }

        public object AppObj { get => this.autocadApp; }

        public bool Saved
        {
            get
            {
                int count = this.autocadApp.Documents.Count;
                for (int i = 0; i < count; i++)
                {
                    AUTOCAD.AcadDocument document = this.autocadApp.Documents.Item(i);
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
                return Process.HasExited;
            }
        }

        public AutoCAD()
        {
            Helper.COMCall(() => this.autocadApp = new AUTOCAD.AcadApplication());
            Helper.COMCall(() => this.autocadApp.Documents.Item(0).Close(false));
            this.autocadApp.Visible = true;
            Helper.GetWindowThreadProcessId((int)this.autocadApp.HWND, out int processId);
            this.Process = Process.GetProcessById(processId);
        }

        public void New()
        {
            Helper.COMCall(() => this.autocadApp.Documents.Add(Missing.Value));
        }

        public void CloseAll()
        {
            while (this.autocadApp.Documents.Count > 0)
                Helper.COMCall(() => this.autocadApp.Documents.Item(0).Close(false));
        }

        public IEnumerable<string> SaveAll(string saveFolder)
        {
            List<string> savedFiles = new List<string>();
            int count = this.autocadApp.Documents.Count;
            for (int i = 0; i < count; i++)
            {
                AUTOCAD.AcadDocument document = this.autocadApp.Documents.Item(i);
                string nameWithoutExtention = Path.GetFileNameWithoutExtension(document.Name);
                string extension = Path.GetExtension(document.Name);
                string name = $"{nameWithoutExtention}{extension}";
                int index = 1;
                while (savedFiles.Contains(name))
                    name = $"{nameWithoutExtention} ({++index}){extension}";
                string fullName = Path.Combine(saveFolder, name);
                Helper.COMCall(() => document.SaveAs(fullName));
                savedFiles.Add(name);
            }
            return savedFiles;
        }

        public void Open(string doc)
        {
            Helper.COMCall(() => this.autocadApp.Documents.Open(doc));
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

        public void DrawCircle(double centerX, double centerY, double radius)
        {
            this.autocadApp.ActiveDocument.ModelSpace.AddCircle(new double[] { centerX, centerY, 0 }, radius);
        }

        public void DrawLine(double startX, double startY, double endX, double endY)
        {
            this.autocadApp.ActiveDocument.ModelSpace.AddLine(new double[] { startX, startY, 0 }, new double[] { endX, endY, 0 });
        }

        public IEnumerable<Dictionary<string, object>> SelectedObjectsProperties()
        {
            List<Dictionary<string, object>> objectsProperties = new List<Dictionary<string, object>>();
            for (int i = 0; i < this.autocadApp.ActiveDocument.ActiveSelectionSet.Count; i++)
            {
                AUTOCAD.AcadEntity acadEntity = this.autocadApp.ActiveDocument.ActiveSelectionSet.Item(i);
                Dictionary<string, object> objectProperties = new Dictionary<string, object>();
                objectProperties.Add("ObjectID", acadEntity.ObjectID);
                objectProperties.Add("ObjectName", acadEntity.ObjectName);
                switch (acadEntity.EntityType)
                {
                    case 4:
                        AUTOCAD.AcadArc acadArc = (AUTOCAD.AcadArc)acadEntity;
                        objectProperties.Add("CenterX", acadArc.Center[0]);
                        objectProperties.Add("CenterY", acadArc.Center[1]);
                        objectProperties.Add("Radius", acadArc.Radius);
                        objectProperties.Add("Area", acadArc.Area);
                        objectProperties.Add("StartPoint", acadArc.StartPoint);
                        objectProperties.Add("EndPoint", acadArc.EndPoint);
                        objectProperties.Add("StartAngle", acadArc.StartAngle);
                        objectProperties.Add("EndAngle", acadArc.EndAngle);
                        break;
                    case 8:
                        AUTOCAD.AcadCircle acadCircle = (AUTOCAD.AcadCircle)acadEntity;
                        objectProperties.Add("CenterX", acadCircle.Center[0]);
                        objectProperties.Add("CenterY", acadCircle.Center[1]);
                        objectProperties.Add("Radius", acadCircle.Radius);
                        objectProperties.Add("Diameter", acadCircle.Diameter);
                        objectProperties.Add("Circumference", acadCircle.Circumference);
                        objectProperties.Add("Area", acadCircle.Area);
                        break;
                    case 19:
                        AUTOCAD.AcadLine acadLine = (AUTOCAD.AcadLine)acadEntity;
                        objectProperties.Add("Linetype", acadLine.Linetype);
                        objectProperties.Add("Length", acadLine.Length);
                        objectProperties.Add("StartPoint", acadLine.StartPoint);
                        objectProperties.Add("EndPoint", acadLine.EndPoint);
                        objectProperties.Add("Angle", acadLine.Angle);
                        objectProperties.Add("Delta", acadLine.Delta);
                        break;
                }
                objectsProperties.Add(objectProperties);
            }
            return objectsProperties;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EXCAD
{
    public interface IApplication
    {
        event EventHandler Exited;
        Process Process { get; }
        object AppObj { get; }
        void New();
        bool Saved { get; }
        bool Terminated { get; }
        void CloseAll();
        IEnumerable<string> SaveAll(string saveFolder);
        void Open(string doc);
        void Quit();
    }
}

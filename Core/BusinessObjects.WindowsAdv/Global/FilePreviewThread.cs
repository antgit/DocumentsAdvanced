using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BusinessObjects.Windows
{
    internal sealed class FilePreviewThread<T> where T : class, IBase, new()
    {
        private ChainAdvanced<T, FileData> m_Link;
        private System.Threading.Thread _runer;
        private string _pathToSave;
        private System.Diagnostics.Process _run;

        public FilePreviewThread(ChainAdvanced<T, FileData> link)
        {
            m_Link = link;
            _runer = new System.Threading.Thread(ProcessPreview);
            _runer.Start();
        }

        private void ProcessPreview()
        {
            if (m_Link == null)
                return;
            _pathToSave = System.IO.Path.Combine(System.IO.Path.GetTempPath(), m_Link.Right.Name + "." + m_Link.Right.FileExtention);
            try
            {
                foreach (FilePreviewThread<T> d in
                    OpennedDocs.Where(d => d.CurrentProcess != null).Where(d => System.IO.Path.GetExtension(d.CurrentProcess.StartInfo.FileName).ToUpper().Substring(1) == m_Link.Right.FileExtention.ToUpper()))
                {
                    d.Kill();
                    break;
                }
                System.IO.File.Delete(_pathToSave);
                if (!System.IO.File.Exists(_pathToSave))
                {
                    m_Link.Right.ExportStreamDataToFile(_pathToSave);
                    _run = Process.Start(_pathToSave);
                    _run.WaitForExit();
                    System.IO.File.Delete(_pathToSave);
                }
            }
            catch (Exception)
            { }
        }

        public void Kill()
        {
            if (!IsExit)
            {
                _run.CloseMainWindow();
                _run.Close();
                _runer.Join();
                try
                {
                    System.IO.File.Delete(_pathToSave);
                }
                catch (Exception)
                { }
                OpennedDocs.Remove(this);
            }
        }

        public Process CurrentProcess
        {
            get { return _run; }
        }

        public bool IsExit
        {
            get { return !_runer.IsAlive; }
        }

        public List<FilePreviewThread<T>> OpennedDocs;
        //public BaseDocumentView<T> DocView { get; set; }
    }
}
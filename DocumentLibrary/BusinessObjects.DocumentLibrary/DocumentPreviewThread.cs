using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using BusinessObjects.Documents;

namespace BusinessObjects.DocumentLibrary
{
    public sealed class DocumentPreviewThread<T> where T : class, IDocument, new()
    {
        private ChainAdvanced<Document, FileData> m_Link;
        private System.Threading.Thread _runer;
        private string _pathToSave;
        private System.Diagnostics.Process _run;

        public DocumentPreviewThread(ChainAdvanced<Document, FileData> link)
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
                foreach (DocumentPreviewThread<T> d in
                    DocView.OpennedDocs.Where(d => d.CurrentProcess != null).Where(d => System.IO.Path.GetExtension(d.CurrentProcess.StartInfo.FileName).ToUpper().Substring(1) == m_Link.Right.FileExtention.ToUpper()))
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
                DocView.OpennedDocs.Remove(this);
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

        public BaseDocumentView<T> DocView { get; set; }
    }
}
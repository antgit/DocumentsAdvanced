using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessObjects.Documents;

namespace BusinessObjects.DocumentLibrary
{
    public interface IExtentionPanelBase
    {
        string Name { get; }
        string Memo { get; }
        void Build();
        void Refresh();
        Control Control { get; }
        int ParamValue { get; set; }

    }
    public interface IExtentionPanelHost<TH, T> : IExtentionPanelBase
    {
        TH Host { get; set; }
        T Item { get; set; }
    }

    //public class ExtentionDocumentPanelInfo : IExtentionPanelHost<BaseDocumentView, Document>
    //{
    //    public ExtentionDocumentPanelInfo()
    //    {
            
    //    }

    //    #region IExtentionPanelHost<BaseDocumentView,Document> Members

    //    public BaseDocumentView Host { get; set; }
        

    //    public Document Item{get;set;}

    //    #endregion

    //    #region IExtentionPanelBase Members

    //    public void Refresh()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Build()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Control Control
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public int ParamValue
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }

    //    public string Name
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public string Memo
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    #endregion
    //}
    //public class ExtentionDocumentPanelLinks: IExtentionPanelHost<BaseDocumentView, Document>
    //{
    //    public ExtentionDocumentPanelLinks()
    //    {
            
    //    }

    //    #region IExtentionPanelHost<BaseDocumentView,Document> Members

    //    public BaseDocumentView Host
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public Document Item
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    #endregion

    //    #region IExtentionPanelBase Members

    //    public void Refresh()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Build()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Control Control
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public int ParamValue
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }

    //    public string Name
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public string Memo
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    #endregion
    //}
    //public class ExtentionDocumentPanelReports: IExtentionPanelHost<BaseDocumentView, Document>
    //{
    //    public void Refresh()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Build()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Control Control
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public int ParamValue
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }

    //    public string Name
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public string Memo
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public BaseDocumentView Host
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }

    //    public Document Item
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }
    //}
    //public class ExtentionDocumentPanelProductInfo : IExtentionPanelHost<BaseDocumentView, Document>
    //{
    //    public void Refresh()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Build()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Control Control
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public int ParamValue
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }

    //    public string Name
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public string Memo
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public BaseDocumentView Host
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }

    //    public Document Item
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }
    //}
    //public class ExtentionDocumentPanelAgentBalance : IExtentionPanelHost<BaseDocumentView, Document>
    //{
    //    public void Refresh()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Build()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Control Control
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public int ParamValue
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }

    //    public string Name
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public string Memo
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public BaseDocumentView Host
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }

    //    public Document Item
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }
    //}
}

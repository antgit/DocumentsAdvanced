using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;

namespace BusinessObjects.Windows.Controls
{
    public class SearchLookUpEditCore : SearchLookUpEdit
    {
        public SearchLookUpEditCore() : base() { }
        protected override DevExpress.XtraEditors.Popup.PopupBaseForm CreatePopupForm()
        {
            return new MyPopupSearchLookUpEditForm(this);
        }

        public event Action<string> BeforFilterChange;

        protected internal virtual void OnBeforFilterChange(string value)
        {
            BeforFilterChange(value);
        }
    }

    public class MyPopupSearchLookUpEditForm : PopupSearchLookUpEditForm
    {
        public MyPopupSearchLookUpEditForm(SearchLookUpEditCore edit) : base(edit) { }
        protected override void UpdateDisplayFilter(string displayFilter)
        {
            (this.OwnerEdit as SearchLookUpEditCore).OnBeforFilterChange(displayFilter);
            base.UpdateDisplayFilter(displayFilter);
        }
    }
}

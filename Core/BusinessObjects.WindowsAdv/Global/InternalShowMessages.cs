using System;
using System.Drawing;
using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        public static void ShowMessageDatabaseExeption(IWorkarea wa, string caption, string action, string message, int dbeId)
        {
            FormDataBaseExeption frm = new FormDataBaseExeption
                                           {
                                               Text = caption,
                                               txtAction = {Text = action},
                                               txtMessage = {Text = message},
                                               layoutControlGroupInnerEx = {Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never}
                                           };
            frm.layoutControlGroupDbe.Shown += delegate
            {
                if (dbeId == 0) return;
                if (wa == null) return;
                ErrorLog err = wa.GetErrorLog(dbeId);
                frm.dbeId.Text = err.Id.ToString();
                frm.dbeMessage.Text = err.Message;
                frm.dbeNumber.Text = err.Number.ToString();
                frm.dbeProcedure.Text = err.Procedure;
                frm.dbeSeverity.Text = err.Severity.ToString();
                frm.dbeState.Text = err.State.ToString();
            };
            frm.ShowDialog();
        }
        public static void ShowMessagesExeption(IWorkarea wa, string caption, string action, Exception ex)
        {
            FormDataBaseExeption frm = new FormDataBaseExeption
                                           {
                                               Text = caption,
                                               txtAction = {Text = action},
                                               txtMessage = {Text = ex.Message},
                                               layoutControlGroupDbe =
                                                   {Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never},
                                               layoutControlGroupInnerEx =
                                                   {Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always}
                                           };
            if (ex.InnerException != null)
                frm.txtInnerEx.Text =
                    string.Format("Сообщение:{0}{1}{0} StackTrace:{0}{2}{0}StackTrace(главное):{0}{3}", Environment.NewLine,
                    ex.InnerException.Message, ex.InnerException.StackTrace, ex.StackTrace);
            frm.ShowDialog();
        }

        public static int ShowMessageChoice(IWorkarea workarea, string caption, string message, string content, string choice, int[] disableItems=null)
        {
            FormChoiceAction frm = new FormChoiceAction
                                       {
                                           ribbon = {ApplicationIcon = ResourceImage.GetByCode(workarea, ResourceImage.HELP_X16)},
                                           Icon = SystemIcons.Question,
                                           Text = caption,
                                           labelHeader = {Text = message},
                                           labelMessage = {Text = content}
                                       };
            string[] val = choice.Split('|');
            for (int i = 0; i < val.Length; i++)
            {
                frm.radioGroup.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(i, val[i]));
            }
            if(disableItems!=null)
            {
                foreach (int i in disableItems)
                {
                    frm.radioGroup.Properties.Items[i].Enabled = false;
                }
                
            }
            frm.btnSelect.Glyph = ResourceImage.GetByCode(workarea, ResourceImage.SELECT_X32);
            // подгонка размеров формы...
            float fontHieght = frm.labelHeader.Font.GetHeight();
            int contentLines = content.Length / 50;
            float contentHeight = fontHieght * contentLines;
            int withChoice = val.Length * 30;
            float height = (frm.labelHeader.Height + contentHeight + withChoice + 50);
            frm.radioGroup.Size = new System.Drawing.Size(frm.radioGroup.Width, withChoice);
            frm.Size = new System.Drawing.Size(frm.Width, frm.Ribbon.Height + (int)height);

            frm.btnSelect.ItemClick += delegate
            {
                frm.DialogResult = DialogResult.OK;
                frm.Close();
            };
            //int h = frm.radioGroup.CalcMinHeight();
            //frm.radioGroup.Size = new System.Drawing.Size(frm.radioGroup.Width, h);
            if (frm.ShowDialog() == DialogResult.Cancel) return -1;

            return frm.radioGroup.SelectedIndex;
        }
    }
}

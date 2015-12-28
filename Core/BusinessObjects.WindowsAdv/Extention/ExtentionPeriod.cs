using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.Xml.Serialization;
using System.IO;
using System.Text;
namespace BusinessObjects.Windows
{
    /// <summary>
    /// Хранит настройки периода
    /// </summary>
    public sealed class PeriodStates
    {
        public PeriodStates()
        { }

        /// <summary>Показывать "Вчера"</summary>
        private bool _showYest = true;
        [XmlAttribute]
        public bool ShowYesterday
        {
            get { return _showYest; }
            set { _showYest = value; }
        }

        /// <summary>Показывать "Сегодня"</summary>
        private bool _showToday = true;
        [XmlAttribute]
        public bool ShowToday
        {
            get { return _showToday; }
            set { _showToday = value; }
        }

        /// <summary>Показывать "Завтра"</summary>
        private bool _showTomorrow = true;
        [XmlAttribute]
        public bool ShowTomorrow
        {
            get { return _showTomorrow; }
            set { _showTomorrow = value; }
        }

        /// <summary>Отображение месяцев</summary>
        private bool[] _showMonths = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true };
        [XmlArray]
        public bool[] ShowMonths
        {
            get { return _showMonths; }
            set { _showMonths = value; }
        }

        /// <summary>Показывать "Все данные за текущий год"</summary>
        private bool _showNowYear = true;
        [XmlAttribute]
        public bool ShowNowYear
        {
            get { return _showNowYear; }
            set { _showNowYear = value; }
        }

        /// <summary>Показывать "Все данные за первое полугодие"</summary>
        private bool _showFirstPartYear = true;
        [XmlAttribute]
        public bool ShowFirstPartYear
        {
            get { return _showFirstPartYear; }
            set { _showFirstPartYear = value; }
        }
        
        /// <summary>Показывать "Все данные за второе полугодие"</summary>
        private bool _showSecondPartYear = true;
        [XmlAttribute]
        public bool ShowSecondPartYear
        {
            get { return _showSecondPartYear; }
            set { _showSecondPartYear = value; }
        }

        /// <summary>Показывать "Прошлый месяц"</summary>
        private bool _showPrevMonth = true;
        [XmlAttribute]
        public bool ShowPrevMonth
        {
            get { return _showPrevMonth; }
            set { _showPrevMonth = value; }
        }

        /// <summary>Показывать "Следующий месяц"</summary>
        private bool _showNextMonth = true;
        [XmlAttribute]
        public bool ShowNextMonth
        {
            get { return _showNextMonth; }
            set { _showNextMonth = value; }
        }

        /// <summary>Показывать "Прошлый год"</summary>
        private bool _showPrevYear = true;
        [XmlAttribute]
        public bool ShowPrevYear
        {
            get { return _showPrevYear; }
            set { _showPrevYear = value; }
        }

        /// <summary>Показывать "Следующий год"</summary>
        private bool _showNextYear = true;
        [XmlAttribute]
        public bool ShowNextYear
        {
            get { return _showNextYear; }
            set { _showNextYear = value; }
        }

        /// <summary>Показывать "Произвольно"</summary>
        private bool _showCustom = true;
        [XmlAttribute]
        public bool ShowCustom
        {
            get { return _showCustom; }
            set { _showCustom = value; }
        }
    }

    public static partial class Extentions
    {
        public static DialogResult ShowDialog(this Period value, Workarea workarea)
        {
            FormProperties frm = new FormProperties
                                     {
                                         StartPosition = FormStartPosition.CenterParent,
                                         ShowInTaskbar = false,
                                         Ribbon =
                                             {ApplicationIcon = ResourceImage.GetSystemImage(ResourceImage.HISTORY_X16)},
                                         btnSave = {Visibility = BarItemVisibility.Never},
                                         btnSelect =
                                             {
                                                 Visibility = BarItemVisibility.Always,
                                                 Glyph = ResourceImage.GetByCode(workarea, ResourceImage.SELECT_X32)
                                             }
                                     };
            ControlCustomPeriod ctl = new ControlCustomPeriod();
            frm.clientPanel.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
            int maxWith = (frm.clientPanel.Width.CompareTo(ctl.MinimumSize.Width) > 0) ? frm.clientPanel.Width : ctl.MinimumSize.Width;
            int maxHeight = (frm.clientPanel.Height.CompareTo(ctl.MinimumSize.Height) > 0) ? frm.clientPanel.Height : ctl.MinimumSize.Height;
            Size mix = (frm.Size - frm.clientPanel.Size) + new Size(maxWith, maxHeight);
            frm.MinimumSize = mix;
            frm.MaximumSize = mix;
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;

            ctl.dtStart.DateTime = value.Start;
            ctl.dtEnd.DateTime = value.End;
            ctl.tmStart.Time = value.Start;
            ctl.tmEnd.Time = value.End;
            DialogResult res = DialogResult.Cancel;
            frm.btnSelect.ItemClick += delegate
                                           {
                                               value.Start = ctl.dtStart.DateTime;
                                               value.End = ctl.dtEnd.DateTime;
                                               frm.Close();
                                               res = DialogResult.OK;
                                           };
            frm.ShowDialog();
            return res;
        }

		/// <summary>
		/// Формирует выпадающее меню для изменения периода.
		/// </summary>
		/// <param name="value">Объект управления периодом.</param>
		/// <param name="workarea">Рабочая область</param>
		/// <param name="ribbon"></param>
		/// <example>
		/// Пример вызова меню в произвольном месте:
		/// Program.WA.Period.GetPeriodPopupMenu(this.ribbon).ShowPopup(Cursor.Position);
		/// </example>
		/// <returns></returns>
		public static PopupMenu GetPeriodPopupMenu(this Period value, Workarea workarea, RibbonControl ribbon)
		{
            int oneGroupCount = 0;
            int twoGroupCount = 0;
            int threeGroupCount = 0;
            int fourGroupCount = 0;
            PeriodStates currentPeriod = null;
            SystemParameterUser periodProp = null;

            periodProp = workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("PERIODPROPERTIES").GetParameterCurrentUser();
            if (periodProp.ValueString == null)
                currentPeriod = new PeriodStates();
            else
            {
                StringReader reader = new StringReader(periodProp.ValueString);
                XmlSerializer dsr = new XmlSerializer(typeof(PeriodStates));
                currentPeriod = (PeriodStates)dsr.Deserialize(reader);
            }

            #region Первая группа
            PopupMenu popupMenuPeriod = new PopupMenu {Ribbon = ribbon};
		    BarButtonItem itemYest = new BarButtonItem {Caption = "Вчера"};
		    popupMenuPeriod.AddItem(itemYest);
            itemYest.ItemClick += delegate
			{
				value.SetDay(DateTime.Today.AddDays(-1));
            };

            BarButtonItem mnuItemToday = new BarButtonItem {Caption = "Сегодня"};
		    popupMenuPeriod.AddItem(mnuItemToday);
            mnuItemToday.ItemClick += delegate
            {
                value.SetDay(DateTime.Today);
            };

            BarButtonItem mnuItemTomorrow = new BarButtonItem {Caption = "Завтра"};
		    popupMenuPeriod.AddItem(mnuItemTomorrow);
            mnuItemTomorrow.ItemClick += delegate
            {
                value.SetDay(DateTime.Today.AddDays(1)); 
            };
            #endregion

            #region Вторая группа
            BarButtonItem mnuItemPrevMonth = new BarButtonItem {Caption = "Прошлый месяц"};
		    popupMenuPeriod.AddItem(mnuItemPrevMonth).BeginGroup = true;
            mnuItemPrevMonth.ItemClick += delegate
            {
                if (value.Start.Month == 1)
                {
                    value.SetYear(value.Start.Year - 1);
                    value.SetMonth(12);
                }
                else
                    value.SetMonth(value.Start.Month - 1);

            };

            BarButtonItem mnuItemNextMonth = new BarButtonItem {Caption = "Следующий месяц"};
		    popupMenuPeriod.AddItem(mnuItemNextMonth);
            mnuItemNextMonth.ItemClick += delegate
            {
                if (value.Start.Month == 12)
                {
                    value.SetYear(value.Start.Year + 1);
                    value.SetMonth(1);
                }
                else
                    value.SetMonth(value.Start.Month + 1);
            };

            BarButtonItem mnuItemFy = new BarButtonItem {Caption = "Первой полугодие"};
		    popupMenuPeriod.AddItem(mnuItemFy);
            mnuItemFy.ItemClick += delegate
            {
                value.SetMonthRange(1, 5);
            };

            BarButtonItem mnuItemSy = new BarButtonItem {Caption = "Второе полугодие"};
		    popupMenuPeriod.AddItem(mnuItemSy);
            mnuItemSy.ItemClick += delegate
            {
                value.SetMonthRange(6, 12);
            };
            #endregion

            #region Третья группа
            BarButtonItem mnuItemY = new BarButtonItem();
            mnuItemY.Caption = "Все данные за текущий год";
            popupMenuPeriod.AddItem(mnuItemY).BeginGroup = true;
            mnuItemY.ItemClick += delegate
            {
                value.SetYear(DateTime.Today.Year);
            };
            
            BarButtonItem mnuItemPrevYear = new BarButtonItem();
            mnuItemPrevYear.Caption = "Прошлый год";
            popupMenuPeriod.AddItem(mnuItemPrevYear);
            mnuItemPrevYear.ItemClick += delegate
            {
                value.SetYear(value.Start.Year - 1);
            };

            BarButtonItem mnuItemNextYear = new BarButtonItem();
            mnuItemNextYear.Caption = "Следующий год";
            popupMenuPeriod.AddItem(mnuItemNextYear);
            mnuItemNextYear.ItemClick += delegate
            {
                value.SetYear(value.Start.Year + 1);
            };
            #endregion

            #region Четвертая группа
            DateTime month = Convert.ToDateTime("1/1/2000");
            for (int i = 0; i < 12; i++)
            {

                DateTime NextMont = month.AddMonths(i);
                BarButtonItem mnuItemMonth = new BarButtonItem();
                mnuItemMonth.Caption = NextMont.ToString("MMMM");
                mnuItemMonth.Tag = i + 1;
                mnuItemMonth.Name = "Month_" + (i + 1).ToString();
                mnuItemMonth.ItemClick += delegate
                {
                    int monthNo = (int)mnuItemMonth.Tag;
                    value.SetMonth(monthNo);
                };
                if(i == 0)
                {
                    popupMenuPeriod.AddItem(mnuItemMonth).BeginGroup = true; 
                }
                else
                    popupMenuPeriod.AddItem(mnuItemMonth);
            }
            #endregion

            #region Пятая группа
            BarButtonItem mnuItemCustom = new BarButtonItem();
            mnuItemCustom.Caption = "Произвольно...";
            popupMenuPeriod.AddItem(mnuItemCustom).BeginGroup = true;
            mnuItemCustom.ItemClick += delegate
            {
                if(value.ShowDialog(workarea)== DialogResult.OK)
                {
                    value.IsChanged = true;
                }
            };
            #endregion

            BarSubItem mnuItemOptions = new BarSubItem {Caption = "Настройки...", Name = "Options"};
		    popupMenuPeriod.AddItem(mnuItemOptions).BeginGroup = true; ;

            #region Вчера
            BarCheckItem mnuShowYest = new BarCheckItem {Caption = "Вчера", CloseSubMenuOnClick = false};
		    mnuShowYest.CheckedChanged += delegate
            {
                if (!mnuShowYest.Checked)
                    oneGroupCount--;
                else
                    oneGroupCount++;
                itemYest.Visibility = mnuShowYest.Checked ? BarItemVisibility.Always : BarItemVisibility.Never;
                currentPeriod.ShowYesterday = mnuShowYest.Checked;
                XmlSerializer sr = new XmlSerializer(currentPeriod.GetType());
                StringBuilder sb = new StringBuilder();
                StringWriter w = new StringWriter(sb, CultureInfo.InvariantCulture);
                sr.Serialize(w, currentPeriod);
                periodProp.ValueString = sb.ToString();
                periodProp.Save();
            };
            mnuItemOptions.AddItem(mnuShowYest);
            #endregion

            #region Сегодня
            BarCheckItem mnuShowToday = new BarCheckItem {CloseSubMenuOnClick = false, Caption = "Сегодня"};
		    mnuShowToday.CheckedChanged += delegate
            {
                if (!mnuShowToday.Checked)
                    oneGroupCount--;
                else
                    oneGroupCount++;
                mnuItemToday.Visibility = mnuShowToday.Checked ? BarItemVisibility.Always : BarItemVisibility.Never;
                currentPeriod.ShowToday = mnuShowToday.Checked;
                XmlSerializer sr = new XmlSerializer(currentPeriod.GetType());
                StringBuilder sb = new StringBuilder();
                StringWriter w = new StringWriter(sb, CultureInfo.InvariantCulture);
                sr.Serialize(w, currentPeriod);
                periodProp.ValueString = sb.ToString();
                periodProp.Save();
            };
            mnuItemOptions.AddItem(mnuShowToday);
            #endregion

            #region Завтра
            BarCheckItem mnuShowTomorrow = new BarCheckItem {CloseSubMenuOnClick = false, Caption = "Завтра"};
		    mnuShowTomorrow.CheckedChanged += delegate
            {
                if (!mnuShowTomorrow.Checked)
                    oneGroupCount--;
                else
                    oneGroupCount++;
                mnuItemTomorrow.Visibility = mnuShowTomorrow.Checked ? BarItemVisibility.Always : BarItemVisibility.Never;
                currentPeriod.ShowTomorrow = mnuShowTomorrow.Checked;
                XmlSerializer sr = new XmlSerializer(currentPeriod.GetType());
                StringBuilder sb = new StringBuilder();
                StringWriter w = new StringWriter(sb, CultureInfo.InvariantCulture);
                sr.Serialize(w, currentPeriod);
                periodProp.ValueString = sb.ToString();
                periodProp.Save();
            };
            mnuItemOptions.AddItem(mnuShowTomorrow);
            #endregion

         
            #region Прошлый месяц
            BarCheckItem mnuShowPrevMonth = new BarCheckItem {CloseSubMenuOnClick = false, Caption = "Прошлый месяц"};
		    mnuItemOptions.AddItem(mnuShowPrevMonth).BeginGroup = true;
            mnuShowPrevMonth.CheckedChanged += delegate
            {
                if (!mnuShowPrevMonth.Checked)
                    twoGroupCount--;
                else
                    twoGroupCount++;
                if (twoGroupCount == 1 && oneGroupCount > 0)
                    popupMenuPeriod.ItemLinks[mnuItemPrevMonth.GroupIndex].BeginGroup = true;
                else
                    popupMenuPeriod.ItemLinks[mnuItemPrevMonth.GroupIndex].BeginGroup = false;
                mnuItemPrevMonth.Visibility = mnuShowPrevMonth.Checked ? BarItemVisibility.Always : BarItemVisibility.Never;
                currentPeriod.ShowPrevMonth = mnuShowPrevMonth.Checked;
                XmlSerializer sr = new XmlSerializer(currentPeriod.GetType());
                StringBuilder sb = new StringBuilder();
                StringWriter w = new StringWriter(sb, CultureInfo.InvariantCulture);
                sr.Serialize(w, currentPeriod);
                periodProp.ValueString = sb.ToString();
                periodProp.Save();
            };
            #endregion

            #region Следующий месяц
            BarCheckItem mnuShowNextMonth = new BarCheckItem();
            mnuShowNextMonth.CloseSubMenuOnClick = false;
            mnuShowNextMonth.Caption = "Следующий месяц";
            mnuItemOptions.AddItem(mnuShowNextMonth);
            mnuShowNextMonth.CheckedChanged += delegate
            {
                if (!mnuShowNextMonth.Checked)
                    twoGroupCount--;
                else
                    twoGroupCount++;
                if (twoGroupCount == 1 && oneGroupCount > 0)
                    popupMenuPeriod.ItemLinks[mnuItemNextMonth.GroupIndex].BeginGroup = true;
                else
                    popupMenuPeriod.ItemLinks[mnuItemNextMonth.GroupIndex].BeginGroup = false;
                mnuItemNextMonth.Visibility = mnuShowNextMonth.Checked ? BarItemVisibility.Always : BarItemVisibility.Never;
                currentPeriod.ShowNextMonth = mnuShowNextMonth.Checked;
                XmlSerializer sr = new XmlSerializer(currentPeriod.GetType());
                StringBuilder sb = new StringBuilder();
                StringWriter w = new StringWriter(sb, CultureInfo.InvariantCulture);
                sr.Serialize(w, currentPeriod);
                periodProp.ValueString = sb.ToString();
                periodProp.Save();
            };
            #endregion

            #region Первое полугодие
            BarCheckItem mnuShowFY = new BarCheckItem();
            mnuShowFY.CloseSubMenuOnClick = false;
            mnuShowFY.Caption = "Первое полугодие";
            mnuItemOptions.AddItem(mnuShowFY);
            mnuShowFY.CheckedChanged += delegate
            {
                if (!mnuShowFY.Checked)
                    twoGroupCount--;
                else
                    twoGroupCount++;
                if (twoGroupCount == 1 && oneGroupCount > 0)
                    popupMenuPeriod.ItemLinks[mnuItemFy.GroupIndex].BeginGroup = true;
                else
                    popupMenuPeriod.ItemLinks[mnuItemFy.GroupIndex].BeginGroup = false;
                mnuItemFy.Visibility = mnuShowFY.Checked ? BarItemVisibility.Always : BarItemVisibility.Never;
                currentPeriod.ShowFirstPartYear = mnuShowFY.Checked;
                XmlSerializer sr = new XmlSerializer(currentPeriod.GetType());
                StringBuilder sb = new StringBuilder();
                StringWriter w = new StringWriter(sb, CultureInfo.InvariantCulture);
                sr.Serialize(w, currentPeriod);
                periodProp.ValueString = sb.ToString();
                periodProp.Save();
            };
            #endregion

            #region Второе полугодие
            BarCheckItem mnuShowSY = new BarCheckItem();
            mnuShowSY.CloseSubMenuOnClick = false;
            mnuShowSY.Caption = "Второе полугодие";
            mnuItemOptions.AddItem(mnuShowSY);
            mnuShowSY.CheckedChanged += delegate
            {
                if (!mnuShowSY.Checked)
                    twoGroupCount--;
                else
                    twoGroupCount++;
                if (twoGroupCount == 1 && oneGroupCount > 0)
                    popupMenuPeriod.ItemLinks[mnuItemSy.GroupIndex].BeginGroup = true;
                else
                    popupMenuPeriod.ItemLinks[mnuItemSy.GroupIndex].BeginGroup = false;
                mnuItemSy.Visibility = mnuShowSY.Checked ? BarItemVisibility.Always : BarItemVisibility.Never;
                currentPeriod.ShowSecondPartYear = mnuShowSY.Checked;
                XmlSerializer sr = new XmlSerializer(currentPeriod.GetType());
                StringBuilder sb = new StringBuilder();
                StringWriter w = new StringWriter(sb, CultureInfo.InvariantCulture);
                sr.Serialize(w, currentPeriod);
                periodProp.ValueString = sb.ToString();
                periodProp.Save();
            };
            #endregion


            #region Все данные за текущий год
            BarCheckItem mnuShowY = new BarCheckItem();
            mnuShowY.CloseSubMenuOnClick = false;
            mnuShowY.Caption = "Все данные за текущий год";
            mnuItemOptions.AddItem(mnuShowY).BeginGroup = true;
            mnuShowY.CheckedChanged += delegate
            {
                if (!mnuShowY.Checked)
                    threeGroupCount--;
                else
                    threeGroupCount++;
                if (threeGroupCount == 1 && (oneGroupCount > 0 || twoGroupCount > 0))
                    popupMenuPeriod.ItemLinks[mnuItemY.GroupIndex].BeginGroup = true;
                else
                    popupMenuPeriod.ItemLinks[mnuItemY.GroupIndex].BeginGroup = false;
                mnuItemY.Visibility = mnuShowY.Checked ? BarItemVisibility.Always : BarItemVisibility.Never;
                currentPeriod.ShowNowYear = mnuShowY.Checked;
                XmlSerializer sr = new XmlSerializer(currentPeriod.GetType());
                StringBuilder sb = new StringBuilder();
                StringWriter w = new StringWriter(sb, CultureInfo.InvariantCulture);
                sr.Serialize(w, currentPeriod);
                periodProp.ValueString = sb.ToString();
                periodProp.Save();
            };
            #endregion

            #region Прошлый год
            BarCheckItem mnuShowPrevYear = new BarCheckItem();
            mnuShowPrevYear.CloseSubMenuOnClick = false;
            mnuShowPrevYear.Caption = "Прошлый год";
            mnuShowPrevYear.CheckedChanged += delegate
            {
                if (!mnuShowPrevYear.Checked)
                    threeGroupCount--;
                else
                    threeGroupCount++;
                if (threeGroupCount == 1 && (oneGroupCount > 0 || twoGroupCount > 0))
                    popupMenuPeriod.ItemLinks[mnuItemPrevYear.GroupIndex].BeginGroup = true;
                else
                    popupMenuPeriod.ItemLinks[mnuItemPrevYear.GroupIndex].BeginGroup = false;
                mnuItemPrevYear.Visibility = mnuShowPrevYear.Checked ? BarItemVisibility.Always : BarItemVisibility.Never;
                currentPeriod.ShowPrevYear = mnuShowPrevYear.Checked;
                XmlSerializer sr = new XmlSerializer(currentPeriod.GetType());
                StringBuilder sb = new StringBuilder();
                StringWriter w = new StringWriter(sb, CultureInfo.InvariantCulture);
                sr.Serialize(w, currentPeriod);
                periodProp.ValueString = sb.ToString();
                periodProp.Save();
            };
            mnuItemOptions.AddItem(mnuShowPrevYear);
            #endregion

            #region Следующий год
            BarCheckItem mnuShowNextYear = new BarCheckItem();
            mnuShowNextYear.CloseSubMenuOnClick = false;
            mnuShowNextYear.Caption = "Следующий год";
            mnuShowNextYear.CheckedChanged += delegate
            {
                if (!mnuShowNextYear.Checked)
                    threeGroupCount--;
                else
                    threeGroupCount++;
                if (threeGroupCount == 1 && (oneGroupCount > 0 || twoGroupCount > 0))
                    popupMenuPeriod.ItemLinks[mnuItemNextYear.GroupIndex].BeginGroup = true;
                else
                    popupMenuPeriod.ItemLinks[mnuItemNextYear.GroupIndex].BeginGroup = false;
                mnuItemNextYear.Visibility = mnuShowNextYear.Checked ? BarItemVisibility.Always : BarItemVisibility.Never;
                currentPeriod.ShowNextYear = mnuShowNextYear.Checked;
                XmlSerializer sr = new XmlSerializer(currentPeriod.GetType());
                StringBuilder sb = new StringBuilder();
                StringWriter w = new StringWriter(sb, CultureInfo.InvariantCulture);
                sr.Serialize(w, currentPeriod);
                periodProp.ValueString = sb.ToString();
                periodProp.Save();
            };
            mnuItemOptions.AddItem(mnuShowNextYear);
            #endregion
           
            #region По месяцам
            for (int i = 0; i < 12; i++)
            {
                DateTime NextMont = month.AddMonths(i);
                BarCheckItem mnuItemMonth = new BarCheckItem();
                mnuItemMonth.Caption = NextMont.ToString("MMMM");
                mnuItemMonth.CloseSubMenuOnClick = false;
                mnuItemMonth.Tag = i + 1;
                mnuItemMonth.Name = "SMonth_" + (i + 1);
                mnuItemMonth.CheckedChanged += delegate
                {
                    if (!mnuItemMonth.Checked)
                        fourGroupCount--;
                    else
                        fourGroupCount++;
                    foreach (BarItemLink itm in popupMenuPeriod.ItemLinks)
                    {
                        if (itm.Item.Tag != null)
                        {
                            int k = (int)itm.Item.Tag;
                            if (k == (int)mnuItemMonth.Tag && itm.Item.Name == "Month_" + k.ToString())
                            {
                                BarButtonItem btn = (BarButtonItem)itm.Item;
                                if (fourGroupCount == 1 && (oneGroupCount > 0 || twoGroupCount > 0 || threeGroupCount > 0))
                                    popupMenuPeriod.ItemLinks[btn.GroupIndex].BeginGroup = true;
                                else
                                    popupMenuPeriod.ItemLinks[btn.GroupIndex].BeginGroup = false;
                                btn.Visibility = mnuItemMonth.Checked ? BarItemVisibility.Always : BarItemVisibility.Never;
                                currentPeriod.ShowMonths[k - 1] = mnuItemMonth.Checked;
                                XmlSerializer sr = new XmlSerializer(currentPeriod.GetType());
                                StringBuilder sb = new StringBuilder();
                                StringWriter w = new StringWriter(sb, CultureInfo.InvariantCulture);
                                sr.Serialize(w, currentPeriod);
                                periodProp.ValueString = sb.ToString();
                                periodProp.Save();
                                break;
                            }
                        }
                    }
                };
                if (i == 0)
                {
                    mnuItemOptions.AddItem(mnuItemMonth).BeginGroup = true;
                }
                else
                    mnuItemOptions.AddItem(mnuItemMonth);
            }
            #endregion

            #region Произвольно
            BarCheckItem mnuShowCustom = new BarCheckItem();
            mnuShowCustom.CloseSubMenuOnClick = false;
            mnuShowCustom.Caption = "Произвольно";
            mnuShowCustom.CheckedChanged += delegate
            {
                if (mnuShowCustom.Checked && (oneGroupCount > 0 || twoGroupCount > 0 || twoGroupCount > 0))
                    popupMenuPeriod.ItemLinks[mnuItemCustom.GroupIndex].BeginGroup = true;
                else
                    popupMenuPeriod.ItemLinks[mnuItemCustom.GroupIndex].BeginGroup = false;
                mnuItemCustom.Visibility = mnuShowCustom.Checked ? BarItemVisibility.Always : BarItemVisibility.Never;
                currentPeriod.ShowCustom = mnuShowCustom.Checked;
                XmlSerializer sr = new XmlSerializer(currentPeriod.GetType());
                StringBuilder sb = new StringBuilder();
                StringWriter w = new StringWriter(sb, CultureInfo.InvariantCulture);
                sr.Serialize(w, currentPeriod);
                periodProp.ValueString = sb.ToString();
                periodProp.Save();
            };
            mnuItemOptions.AddItem(mnuShowCustom).BeginGroup = true;
            #endregion

            popupMenuPeriod.BeforePopup += delegate
            {
                oneGroupCount = 0;
                twoGroupCount = 0;
                threeGroupCount = 0;
                fourGroupCount = 0;


                #region Настройка элементов меню
                itemYest.Visibility = currentPeriod.ShowYesterday ? BarItemVisibility.Always : BarItemVisibility.Never;
                if (currentPeriod.ShowYesterday) oneGroupCount++;

                mnuItemToday.Visibility = currentPeriod.ShowToday ? BarItemVisibility.Always : BarItemVisibility.Never;
                if (currentPeriod.ShowToday) oneGroupCount++;

                mnuItemTomorrow.Visibility = currentPeriod.ShowTomorrow ? BarItemVisibility.Always : BarItemVisibility.Never;
                if (currentPeriod.ShowTomorrow) oneGroupCount++;


                mnuItemPrevMonth.Visibility = currentPeriod.ShowPrevMonth ? BarItemVisibility.Always : BarItemVisibility.Never;
                if (currentPeriod.ShowPrevMonth) twoGroupCount++;

                mnuItemNextMonth.Visibility = currentPeriod.ShowNextMonth ? BarItemVisibility.Always : BarItemVisibility.Never;
                if (currentPeriod.ShowNextMonth) twoGroupCount++;

                mnuItemFy.Visibility = currentPeriod.ShowFirstPartYear ? BarItemVisibility.Always : BarItemVisibility.Never;
                if (currentPeriod.ShowFirstPartYear) twoGroupCount++;

                mnuItemSy.Visibility = currentPeriod.ShowSecondPartYear ? BarItemVisibility.Always : BarItemVisibility.Never;
                if (currentPeriod.ShowSecondPartYear) twoGroupCount++;

                
                mnuItemY.Visibility = currentPeriod.ShowNowYear ? BarItemVisibility.Always : BarItemVisibility.Never;
                if (currentPeriod.ShowNowYear) threeGroupCount++;

                mnuItemPrevYear.Visibility = currentPeriod.ShowPrevYear ? BarItemVisibility.Always : BarItemVisibility.Never;
                if (currentPeriod.ShowPrevYear) threeGroupCount++;

                mnuItemNextYear.Visibility = currentPeriod.ShowNextYear ? BarItemVisibility.Always : BarItemVisibility.Never;
                if (currentPeriod.ShowNextYear) threeGroupCount++;


                foreach (BarItemLink itm in popupMenuPeriod.ItemLinks)
                {
                    if (itm.Item.Tag != null)
                    {
                        int i = (int)itm.Item.Tag;
                        if (itm.Item.Name == "Month_" + i)
                        {
                            itm.Item.Visibility = currentPeriod.ShowMonths[i - 1] ? BarItemVisibility.Always : BarItemVisibility.Never;
                            if (currentPeriod.ShowMonths[i - 1]) fourGroupCount++;
                        }
                    }
                }

                mnuItemCustom.Visibility = currentPeriod.ShowCustom ? BarItemVisibility.Always : BarItemVisibility.Never;
                #endregion

                #region Настройка параметров
                mnuShowYest.Checked = currentPeriod.ShowYesterday;
                mnuShowToday.Checked = currentPeriod.ShowToday;
                mnuShowTomorrow.Checked = currentPeriod.ShowTomorrow;
                foreach (BarItemLink itm in mnuItemOptions.ItemLinks)
                {
                    if (itm.Item.Tag != null)
                    {
                        int i = (int)itm.Item.Tag;
                        if (itm.Item.Name == "SMonth_" + i)
                        {
                            BarCheckItem check = (BarCheckItem)itm.Item;
                            check.Checked = currentPeriod.ShowMonths[i - 1];
                        }
                    }
                }
                mnuShowY.Checked = currentPeriod.ShowNowYear;
                mnuShowFY.Checked = currentPeriod.ShowFirstPartYear;
                mnuShowSY.Checked = currentPeriod.ShowSecondPartYear;
                mnuShowPrevMonth.Checked = currentPeriod.ShowPrevMonth;
                mnuShowNextMonth.Checked = currentPeriod.ShowNextMonth;
                mnuShowPrevYear.Checked = currentPeriod.ShowPrevYear;
                mnuShowNextYear.Checked = currentPeriod.ShowNextYear;
                mnuShowCustom.Checked = currentPeriod.ShowCustom;
                #endregion
            };

			return popupMenuPeriod;
		}
    }
}

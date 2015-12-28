using System;
using System.Drawing;
using BusinessObjects.Developer;
using BusinessObjects.Documents;
using BusinessObjects.Security;
namespace BusinessObjects.Windows
{
    public static class ExtentionsImage
    {
        #region Изображения
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Наиемнование кода</param>
        /// <returns></returns>
        public static Bitmap GetImage(this EquipmentDetail value)
        {
            if (value == null) return null;
            Bitmap image = null;
            switch (value.KindValue)
            {
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Наиемнование кода</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Calendar value)
        {
            if (value == null) return null;
            Bitmap image = null;
            switch (value.KindValue)
            {
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CALENDAR_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Наиемнование кода</param>
        /// <returns></returns>
        public static Bitmap GetImage(this CodeName value)
        {
            if (value == null) return null;
            Bitmap image = null;
            switch (value.KindValue)
            {
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CODENAME_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Событие</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Event value)
        {
            if (value == null) return null;
            Bitmap image = null;
            switch (value.KindValue)
            {
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.EVENT_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Задача</param>
        /// <returns></returns>
        public static Bitmap GetImage(this UserAccount value)
        {
            if (value == null) return null;
            Bitmap image = null;
            switch (value.KindValue)
            {
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.USERS_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Задача</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Task value)
        {
            if (value == null) return null;
            Bitmap image = null;
            switch (value.KindValue)
            {
                case Task.KINDVALUE_TASKSYSTEM:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.TASKRED_X16);
                    break;
                case Task.KINDVALUE_TASKUSER:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.TASK_X16);
                    break;
                case Task.KINDVALUE_TASKADVANCED:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.TASKADV_X16);
                    break;
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.TASK_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Каталог данных</param>
        /// <returns></returns>
        public static Bitmap GetImage(this DataCatalog value)
        {
            if (value == null) return null;
            Bitmap image = null;
            switch (value.KindValue)
            {
                case DataCatalog.KINDVALUE_DATA:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDER_X16);
                    break;
                case DataCatalog.KINDVALUE_IN:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDERGREEN_X16);
                    break;
                case DataCatalog.KINDVALUE_OUT:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDERLIGHTBLUE_X16);
                    break;
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDER_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Пользовательское примечание</param>
        /// <returns></returns>
        public static Bitmap GetImage(this TimePeriod value)
        {
            if (value == null) return null;
            Bitmap image = null;
            switch (value.KindValue)
            {
                case TimePeriod.KINDVALUE_WORK:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CALENDAR_X16);
                    break;
                case TimePeriod.KINDVALUE_BREAK:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CALENDARRED_X16);
                    break;
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CALENDAR_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Пользовательское примечание</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Note value)
        {
            if (value == null) return null;
            Bitmap image = null;
            switch (value.KindValue)
            {
                case Note.KINDVALUE_COMMENT:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.NOTEGREEN_X16);
                    break;
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.NOTE_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Сообщение</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Message value)
        {
            if (value == null) return null;
            Bitmap image = null;

            switch (value.KindValue)
            {
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.MESSAGEBALLOON_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Тип сообщения</param>
        /// <returns></returns>
        public static Bitmap GetImageMessage(Workarea wa, int value)
        {
            if (wa == null) return null;
            Bitmap image = null;
            switch (value)
            {
                case Message.KINDVALUE_USER:
                    image = ResourceImage.GetByCode(wa, ResourceImage.MESSAGEBALLOON_X16);
                    break;
                case Message.KINDVALUE_SYSTEM:
                    image = ResourceImage.GetByCode(wa, ResourceImage.MESSAGEBALLOON_X16);
                    break;
            }
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Интервал времени</param>
        /// <returns></returns>
        public static Bitmap GetImage(this DateRegion value)
        {
            if (value == null) return null;
            Bitmap image = null;
            
            switch (value.KindValue)
            {
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.HISTORY_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Веб служба</param>
        /// <returns></returns>
        public static Bitmap GetImage(this WebService value)
        {
            if (value == null) return null;
            Bitmap image = null;
            switch (value.KindValue)
            {
                case WebService.KINDVALUE_SSRS:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.REPORT_X16);
                    break;
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGTWEB_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Партия товара</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Series value)
        {
            Bitmap image = null;
            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESYELLOW_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Страна</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Country value)
        {
            Bitmap image = null;
            switch (value.Iso)
            {
                case "RU":
                    image = ResourceImage.GetByCode(value.Workarea, "FLAG_RUSSIA_X16");
                    break;
                case "BY":
                    image = ResourceImage.GetByCode(value.Workarea, "FLAG_BELARUS_X16");
                    break;
                case "PL":
                    image = ResourceImage.GetByCode(value.Workarea, "FLAG_POLAND_X16");
                    break;    
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FLAFGSUA_X16);
                    break;
            }
            
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Статья базы знаний</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Knowledge value)
        {
            if (value == null) return null;
            Bitmap image = null;
            switch (value.KindValue)
            {
                case Knowledge.KINDVALUE_LOCAL:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.BOOKGREEN_X16);
                    break;
                case Knowledge.KINDVALUE_ONLINE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.BOOKBLUE_X16);
                    break;
                case Knowledge.KINDVALUE_FILELINK:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.BOOKYELLOW_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Статья базы знаний</param>
        /// <returns></returns>
        public static Bitmap GetImageKnowledge(Workarea wa, int value)
        {
            if (wa == null) return null;
            Bitmap image = null;
            switch (value)
            {
                case Knowledge.KINDVALUE_LOCAL:
                    image = ResourceImage.GetByCode(wa, ResourceImage.BOOKGREEN_X16);
                    break;
                case Knowledge.KINDVALUE_ONLINE:
                    image = ResourceImage.GetByCode(wa, ResourceImage.BOOKBLUE_X16);
                    break;
                case Knowledge.KINDVALUE_FILELINK:
                    image = ResourceImage.GetByCode(wa, ResourceImage.BOOKYELLOW_X16);
                    break;
            }
            return image;
        }

        
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Бухгалтерский счет</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Account value)
        {
            if (value == null) return null;
            Bitmap image = null;
            switch (value.KindValue)
            {
                case Account.KINDVALUE_ACTIVE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCOUNTACTIVE_X16);
                    break;
                case Account.KINDVALUE_PASSIVE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCOUNTPASSIVE_X16);
                    break;
                case Account.KINDVALUE_PASSIVEACTIVE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCOUNTPASSIVEACTIVE_X16);
                    break;
                case Account.KINDVALUE_OFFBALANCE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCOUNTOUTBALANCE_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>Изображение 16x16</summary>
        /// <param name="value">Корреспондент</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Agent value)
        {
            if (value == null) return null;

            Bitmap image = null;
            switch (value.KindValue)
            {
                case Agent.KINDVALUE_COMPANY:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTFIRM_X16);
                    break;
                case Agent.KINDVALUE_PEOPLE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTWORKER_X16);
                    break;
                case Agent.KINDVALUE_STORE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTSTORE_X16);
                    break;
                case Agent.KINDVALUE_MYCOMPANY:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTMYFIRM_X16);
                    break;
                case Agent.KINDVALUE_MYSTORE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTSTORE_X16);
                    break;
                case Agent.KINDVALUE_BANK:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTBANK_X16);
                    break;
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTFIRM_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Расчетный счет</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Ruleset value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.RULESET_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Расчетный счет</param>
        /// <returns></returns>
        public static Bitmap GetImage(this AgentBankAccount value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.MONEY_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Наименование набора</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Recipe value)
        {
            Bitmap image = null;
            switch (value.KindValue)
            {
                case Recipe.KINDVALUE_KOMPLEKT:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESGREEN_X16);
                    break;
                case Recipe.KINDVALUE_SET:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESBLUE_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Наименование набора</param>
        /// <returns></returns>
        public static Bitmap GetImage(this ProductRecipe value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBEGREEN_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Аналитика</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Analitic value)
        {
            Bitmap image = null;
            switch (value.KindValue)
            {
                case 1:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.MISCMAGENTA_X16); 
                    break;
                case 2:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.MISCGREEN_X16); 
                    break;
                case 3:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.MISCBLUE_X16); 
                    break;
                case 4:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.MONEY_X16);
                    break;
                case 5:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 6:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 7:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 8:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 9:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 10:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 11:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 12:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 13:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.EDUCATION_X16);
                    break;
                case 14:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 15:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 16:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 17:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 18:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 19:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 20:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 21:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 22:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.REPORT_X16);
                    break;
                case 23:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 24:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 25:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 26:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 27:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 28:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 29:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                case 30:
                    image = ResourceImage.GetByCode(value.Workarea, string.Format("MISCCUSTOM{0}_X16", value.KindValue));
                    break;
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.MISCMAGENTA_X16); 
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Владелец</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Branche value)
        {
            Bitmap image = null;
            switch (value.KindValue)
            {
                case Branche.KINDVALUE_DEFAULT:
                    image = Properties.Resources.DATABASE_X16;
                    break;
                case Branche.KINDVALUE_ACCENT7:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCENT7_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Валюта</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Currency value)
        {
            Bitmap image = null;
            switch (value.KindValue)
            {
                case Currency.KINDVALUE_CURRENCY:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICEUSD_X16);
                    break;
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICEUSD_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Колонка</param>
        /// <returns></returns>
        public static Bitmap GetImage(this CustomViewColumn value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.COLUMN_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Графические ресурсы</param>
        /// <returns></returns>
        public static Bitmap GetImage(this ResourceImage value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.IMAGE_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Список</param>
        /// <returns></returns>
        public static Bitmap GetImage(this CustomViewList value)
        {
            Bitmap image = null;
            switch (value.KindValue)
            {
                case 1:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.TABLE_X16);
                    break;
                case 2:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.STOREDPROC_X16);
                    break;
                case 3:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.VIEW_X16);
                    break;
                case 4:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.TABLEFUNCTION_X16);
                    break;
                case 5:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.STOREDPROC_X16);
                    break;
                case 6:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.TABLEFUNCTION_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Папка</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Folder value)
        {
            Bitmap image = null;
            switch (value.KindValue)
            {
                case Folder.KINDVALUE_FOLDER:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDERFLD_X16);
                    break;
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDERFLD_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Документ</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Document value)
        {
            if (value == null) return null;

            Bitmap image = null;
            switch (value.StateId)
            {
                case 1:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16); 
                    break;
                case 5:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTTRASH_X16);
                    break;
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTNOTDONE_X16); 
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение документа 16x16
        /// </summary>
        /// <param name="workarea">Рабочая область</param>
        /// <param name="stateId">Идентификатор состояния</param>
        /// <returns></returns>
        public static Bitmap GetImageDocument(Workarea workarea, int stateId)
        {
            Bitmap image = null;
            switch (stateId)
            {
                case 1:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.DOCUMENTDONE_X16);
                    break;
                case 5:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.DOCUMENTTRASH_X16);
                    break;
                default:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.DOCUMENTNOTDONE_X16);
                    break;
            }
            if (stateId == State.STATEDELETED)
                image = GetImageDeleted(workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16х16
        /// </summary>
        /// <param name="value">Содержимое иерархии</param>
        /// <returns></returns>
        public static Bitmap GetImage(this HierarchyContent value)
        {
            Bitmap image = null;
            switch (value.ToEntityId)
            {
                case 2:
                    switch (value.KindValue)
                    {
                        case 1:
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCOUNTACTIVE_X16);
                            break;
                        case 2:
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCOUNTPASSIVE_X16);
                            break;
                        case 3:
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCOUNTPASSIVEACTIVE_X16);
                            break;
                        case 4:
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCOUNTOUTBALANCE_X16);
                            break;
                    }
                    break;
                case 3:
                    switch (value.KindValue)
                    {
                        case 1:
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTFIRM_X16);
                            break;
                        case 2:
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTWORKER_X16);
                            break;
                        case 4:
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTMYFIRM_X16);
                            break;
                        case 8:
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTBANK_X16);
                            break;
                        case 9:
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTBANK_X16);
                            break;
                    }
                    if (image == null)
                    {
                        if ((value.KindValue & 1) == 1)
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTFIRM_X16);
                        if ((value.KindValue & 2) == 2)
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTWORKER_X16);
                        if ((value.KindValue & 4) == 4)
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTMYFIRM_X16);
                        if ((value.KindValue & 8) == 8)
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTBANK_X16);
                        if ((value.KindValue & 16) == 16)
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTSTORE_X16);
                    }
                    break;
                case 4:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.MISCMAGENTA_X16);
                    break;
                case 6:
                    switch (value.KindValue)
                    {
                        case 1:
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESBLUE_X16);
                            break;
                        case 2:
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESGREEN_X16);
                            break;
                    }
                    if (image == null)
                        image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESBLUE_X16);
                    break;
                case 7:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDERFLD_X16);
                    break;
                case 10:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.UNIT_X16);
                    break;
                case 14:
                    switch (value.KindValue)
                    {
                        case 1:
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FORM_X16); 
                            break;
                        case 2:
                            image = ResourceImage.GetByCode(value.Workarea, ResourceImage.REPORT_X16); 
                            break;
                    }
                    break;
                case 15:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.LIBRARY_X16);
                    break;
                case 70:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CODENAME_X16);
                    break;
                case 71:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CODENAME_X16);
                    break;
                case 79:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.NOTE_X16);
                    break;
                case 96:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.TASK_X16);
                    break;
                case 97:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.USERS_X16);
                    break;
                case 1:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PRODUCT_X16);
                    break;
            }


            return image;
        }

        /// <summary>
        /// Изображение 16х16
        /// </summary>
        /// <param name="value">Иерархия</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Hierarchy value)
        {
            return GetImageHierarchy(value.Workarea, value.ContentEntityId, ((value.ViewListId != 0 && value.ViewList.KindValue!= CustomViewList.KINDVALUE_LIST)   || value.ViewListDocumentsId != 0));
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Библиотека</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Library value)
        {
            Bitmap image = null;
            switch (value.KindValue)
            {
                case Library.KINDVALUE_LIBRARY :
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.LIBRARY_X16);
                    break;
                case Library.KINDVALUE_RESOURCE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.LIBRARY_X16);
                    break;
                case Library.KINDVALUE_APP:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.APPLICATION_X16);
                    break;
                case Library.KINDVALUE_REPSQL:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.REPORT_X16);
                    break;
                case Library.KINDVALUE_REPTBL:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.INTERACTIVE_REPORT_X16);
                    break;
                case Library.KINDVALUE_WINDOW:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.WINDOWSLIST_X16);
                    break;
                case Library.KINDVALUE_PAGE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.WINDOWSCASCADE_X16);
                    break;
                case Library.KINDVALUE_PRINTFORM:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PRINTFORM_X16);
                    break;
                case Library.KINDVALUE_DOCFORM:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FORM_X16); 
                    break;
                case Library.KINDVALUE_METHOD:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CLASS_X16);
                    break;
                case Library.KINDVALUE_UIMODULE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.INTERFACE_MODULE_X16);
                    break;
                case Library.KINDVALUE_REPPRINT:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.STIREPORT_X16);
                    break;
                case Library.KINDVALUE_WEBREPORT:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.STIREPORT_X16);
                    break;
                case Library.KINDVALUE_DXREPORT:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.XTRAREPORTS_X16);
                    break;
                case Library.KINDVALUE_CONFIGFILE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXML_X16);
                    break;   
                case Library.KINDVALUE_WEBPRINTFORM:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.STIREPORTV4_X16);
                    break;   
                    

            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }


        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Вид цены</param>
        /// <returns></returns>
        public static Bitmap GetImage(this PriceName value)
        {
            Bitmap image = null;
            switch (value.KindValue)
            {
                case PriceName.KINDVALUE_PRICENAME:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICE_POLICY_X16);
                    break;
                case PriceName.KINDVALUE_PROVIDER:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICE_POLICY_X16);
                    break;
                case PriceName.KINDVALUE_COMPETITOR:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICE_POLICY_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Элемент проекта</param>
        /// <returns></returns>
        public static Bitmap GetImage(this PriceValue value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICEUAH_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Ячейка хранения</param>
        /// <returns></returns>
        public static Bitmap GetImage(this StorageCell value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESYELLOW_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }


        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Объект учета</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Product value)
        {
            Bitmap image = null;
            switch (value.KindValue)
            {
                case Product.KINDVALUE_DEFAULT: // неизвестный
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PRODUCT_X16);
                    break;
                case Product.KINDVALUE_PRODUCT: // товар
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PRODUCT_X16);
                    break;
                case Product.KINDVALUE_MONEY: // денежный
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.MONEY_X16);
                    break;
                case Product.KINDVALUE_SERVICE: // услуга
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.COMPONENTS_X16);
                    break;
                case Product.KINDVALUE_AUTO: // авто
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CARSEDANGREEN_X16);
                    break;
                case Product.KINDVALUE_PACK: // упаковка
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PACKAGE_X16);
                    break;
                case Product.KINDVALUE_MBP: // мбп
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.COMPONENTGREEN_X16);
                    break;
                case Product.KINDVALUE_ASSETS: // основные средства
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.COMPONENTYELLOW_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Курс валюты</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Rate value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICEUSD_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Маппинг процедуры</param>
        /// <returns></returns>
        public static Bitmap GetImage(this ProcedureMap value)
        {
            // TODO: собственное изображение
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.CLASS_X16);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">XML хранилице</param>
        /// <returns></returns>
        public static Bitmap GetImage(this XmlStorage value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.XMLSTORAGE_X16);
            return image;
        }


        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Единица измерения</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Unit value)
        {
            Bitmap image = null;
            switch (value.KindValue)
            {
                case Unit.KINDVALUE_UNIT:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.UNIT_X16);
                    break;
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.UNIT_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Единица измерения</param>
        /// <returns></returns>
        public static Bitmap GetImage(this ProductUnit value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.UNIT_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Паспорт</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Passport value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PASSPORTBLUE_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }
        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Город</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Town value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.TOWN_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Город</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Territory value)
        {
            Bitmap image = null;
            string codeValue = string.IsNullOrEmpty(value.Code) ? string.Empty : value.Code;
            if (codeValue == "UA_DN_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_DN_REGION");
            if (codeValue == "UA_CHERKASY_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_CHERKASY_REGION");
            if (codeValue == "UA_CHERNIHIV_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_CHERNIHIV_REGION");
            if (codeValue == "UA_CHERNIVTSI_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_CHERNIVTSI_REGION");
            if (codeValue == "UA_DNIPROPETROVSK_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_DNIPROPETROVSK_REGION");
            if (codeValue == "UA_IVANOFRANKIVSK_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_IVANOFRANKIVSK_REGION");
            if (codeValue == "UA_KHARKIV_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_KHARKIV_REGION");
            if (codeValue == "UA_KIROVOHRAD_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_KIROVOHRAD_REGION");
            if (codeValue == "UA_KYIV_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_KYIV_REGION");
            if (codeValue == "UA_LUHANSK_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_LUHANSK_REGION");
            if (codeValue == "UA_LVIV_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_LVIV_REGION");
            if (codeValue == "UA_MYKOLAIV_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_MYKOLAIV_REGION");
            if (codeValue == "UA_ODESA_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_ODESA_REGION");
            if (codeValue == "UA_POLTAVA_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_POLTAVA_REGION");
            if (codeValue == "UA_RIVNE_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_RIVNE_REGION");
            if (codeValue == "UA_SUMY_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_SUMY_REGION");
            if (codeValue == "UA_TERNOPIL_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_TERNOPIL_REGION");
            if (codeValue == "UA_VINNYTSIA_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_VINNYTSIA_REGION");
            if (codeValue == "UA_ZAPORIZHZHYA_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_ZAPORIZHZHYA_REGION");
            if (codeValue == "UA_ZHYTOMYR_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_ZHYTOMYR_REGION");
            if (codeValue == "UA_KYIV_REGIONSPESIAL")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_KYIV_REGIONSPESIAL");
            if (codeValue == "UA_VOLYN_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_VOLYN_REGION");
            if (codeValue == "UA_SEVASTOPOL_REGIONSPESIAL")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_SEVASTOPOL_REGIONSPESIAL");
            if (codeValue == "UA_ZAKARPATTIA_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_ZAKARPATTIA_REGION");
            if (codeValue == "UA_CRIMEA_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_CRIMEA_REGION");
            if (codeValue == "UA_KHMELNYTSKYI_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_KHMELNYTSKYI_REGION");
            if (codeValue == "UA_KHERSON_REGION")
                image = ResourceImage.GetByCode(value.Workarea, "FLAG_UA_KHERSON_REGION");
            
            if(image==null)
            {
                image = ResourceImage.GetByCode(value.Workarea, ResourceImage.TOWN_X16);
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Файловые данные</param>
        /// <returns></returns>
        public static Bitmap GetImage(this FileData value)
        {
            Bitmap image = null;
            string ext = String.IsNullOrEmpty(value.FileExtention) ? String.Empty : value.FileExtention;
            switch (ext.ToUpper())
            {
                case "PDF":
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEPDF_X16);
                    break;
                case "XLS":
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEEXCEL_X16);
                    break;
                case "XLSX":
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEEXCELXML_X16);
                    break;
                case "HTML":
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEHTML_X16);
                    break;
                case "MHT":
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEMHT_X16);
                    break;
                case "PNG":
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEPNG_X16);
                    break;
                case "RTF":
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FILERTF_X16);
                    break;
                case "TXT":
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FILETXT_X16);
                    break;
                case "DOC":
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEWORD_X16);
                    break;
                case "XML":
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXML_X16);
                    break;
                case "XPS":
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXPS_X16);
                    break;
                case "DOCX":
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEWORD_X16);
                    break;
                default:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16); 
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>Изображение 16x16</summary>
        /// <param name="value">Пользователь или группа</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Uid value)
        {
            Bitmap image = null;
            switch (value.KindValue)
            {
                case Uid.KINDVALUE_USER:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTWORKER_X16);
                    break;
                case Uid.KINDVALUE_GROUP:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.USERGROUP_X16);
                    break;
            }
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16х16
        /// </summary>
        /// <param name="value">Системный объект</param>
        /// <returns></returns>
        public static Bitmap GetImage(this EntityType value)
        {
            switch (value.Id)
            {
                case 1:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PRODUCT_X16);
                case 2:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCOUNTACTIVE_X16);
                case 3:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTMYFIRM_X16);
                case 4:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.MISCMAGENTA_X16);
                case 5:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICEUSD_X16);
                case 6: 
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESGREEN_X16);
                case 7:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDERFLD_X16);
                case 8: //???
                    // TODO: Добавить правильное изображение
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
                case 9:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICE_POLICY_X16);
                case 10:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.UNIT_X16);
                case 11:
                    return Properties.Resources.DATABASE_X16;
                case 12:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.RECIPE_X16);
                case 13:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICEUSD_X16);
                case 14:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16);
                case 15:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.LIBRARY_X16);
                case 16:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICEUAH_X16);
                case 17:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FACTNAME_X16);
                case 18:
                    return Properties.Resources.PROPERTIES_X16;
                case 19:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.CLASS_X16);
                case 20:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16);
                case 21:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.VIEW_X16);
                case 22:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.COLUMN_X16);
                case 23:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILERTF_X16);
                case 24:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.MONEY_X16);
                case 25:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.SETTINGS_X16);
                case 26:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTWORKER_X16);
                case 27:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.KEY_X16);
                case 28:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDER_X16);
                case 29:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.HIERARCHY_CONTENT_X16);
                case 30:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESYELLOW_X16);
                case 31:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.CONTACT_INFO_X16);
                case 32:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.UNIT_X16);
                case 33:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FLAFGSUA_X16);
                case 34:
                    return Properties.Resources.PROPERTIES_X16;
                case 35:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.RULES_X16);
                case 36: //Xml данные
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.XMLSTORAGE_X16);
                case 37: //Регионы
                    // TODO: Добавить правильное изображение
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.TOWN_X16);
                case 38: //Города
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.TOWN_X16);
                case 39: // Ячейка хранения
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESYELLOW_X16);
                case 40: // Типы связей
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.LINK_X16);
                case 41: // Объекты базы данных
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.DATABASE_X16);
                case 42: // Составляющая объекта базы данных
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.STOREDPROC_X16);
                case 43: // Паспорт
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PASSPORTBLUE_X16);
                case 44: // Водительское удостоверение
                    // TODO: Добавить правильное изображение
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
                case 45: // Склады
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTSTORE_X16);
                case 46: // Предприятие
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTFIRM_X16);
                case 47: // Банк
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTBANK_X16);
                case 48: // Пользовательское значение системного параметра
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.SETTINGS_X16);
                case 49: // Основное содержимое библиотеки
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.CLASS_X16);
                case 50: // Связь объекта с отчетами
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.REPORT_X16);
                case 51: // Значение флага
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FLAGRED_X16);
                case 52: // Подпись документа
                    // TODO: Добавить правильное изображение
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.CERTIFICATE_X16);
                case 53: // Налог документа
                    // TODO: Добавить правильное изображение
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
                case 54: // Графические ресурсы
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.IMAGE_X16);
                case 55: // Строковые ресурсы
                    // TODO: Добавить правильное изображение
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
                case 56: // Состояния
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FLAGGREEN_X16);
                case 57: // Физическое лицо
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTWORKER_X16);
                case 58: // Сотрудник
                    // TODO: Добавить правильное изображение
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTWORKER_X16);
                case 59: // Тип связей
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.LINK_X16);
                case 60: // Значение факта
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
                case 61: // Общие разрешения
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.KEY_X16);
                case 62: // Разрешения для элементов
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.KEY_X16);
                case 63: // Дата факта
                    // TODO: Добавить правильное изображение
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
                case 64: // Связи колонок факта и подтипа объекта
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.LINK_X16);
                case 65: // Протокол действий пользователя в документах
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTCHANGES_X16);
                case 66: // Адрес корреспондента
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.HOME_X16);
                case 67: // Протокол действий пользователя в документах
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.EVENT_X16);
                case 69: // Тип связи - допустимый тип в связи
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.LINK_X16);
                case 70: // Наименование кода
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.CODENAME_X16);
                case 71: // Значение кода
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.CODENAME_X16);
                case 72: // Документ-процесс
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.RULESET_X16);
                case 73: // Документ - Автонумерация
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
                case 74: // Версии файлов
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEVERSION_X16);
                case 75: // Статья базы знаний
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.HELP_X16);
                case 76:
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.MESSAGEBALLOON_X16);
                case 77: // xml данные документа
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXML_X16);
                case 78: // Тип связи - допустимый тип в связи
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.LINK_X16);
                case 79: // Статья базы знаний
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.NOTE_X16);
                case 80: // Тип связи - допустимый тип в связи
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.LINK_X16);
                case 81: // 
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDER_X16);
                case 82: // WebService
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGTWEB_X16);
                case 83: //Ценовой диапазон
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICEUAH_X16);
                case 84: // DataRegion
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.HISTORY_X16);
                case 85: // Строка документа торговли
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16);
                case 86: // Строка документа склада
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16);
                case 87: // Строка документа налогов
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16);
                case 88: // Строка документа услуг
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16);
                case 89: // Строка документа цен
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16);
                case 90: // Строка документа финансов
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16);
                case 91: // Строка документа договора
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16);
                case 92: // Строка бухгалтерского документа
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16);
                case 93: // TimePeriod
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.CALENDAR_X16);
                case 94: // Строка аналитики документа
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.MISCMAGENTA_X16);
                case 95: // Строка аналитики детализации документа
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.MISCMAGENTA_X16);
                case 96: // Task
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.TASK_X16);
                case 97: // UserAccount
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.USERS_X16);
                case 98: // xml данные документа
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXML_X16);
                case 99: // xml данные документа
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXML_X16);
                case 100: // xml данные документа
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXML_X16);
                case 101: // xml данные документа
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXML_X16);
                case 102: // xml данные документа
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXML_X16);
                case 103: // xml данные документа
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXML_X16);
                case 104: // xml данные документа
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXML_X16);
                case 105: // xml данные документа
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXML_X16);
                case 106: // xml данные документа
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXML_X16);
                case 107: // календарь
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.CALENDAR_X16);
                case 109: // подразделение
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTFIRM_X16);
                case 111: // Устройство
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
                case 112: // Участник маршрута
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
                case 116: // Оборудование
                    return ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
                default:
                    return null;
            }
        }

        public static Bitmap GetImage32(this EntityType item)
        {
            switch (item.Id)
            {
                case 1:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.PRODUCT_X32); ;
                case 2:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.ACCOUNT_X32);
                case 3:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.AGENT_X32);
                case 4:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.ANALITICMAGENTA_X32);
                case 5:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.CURRENCY_X32); 
                case 6:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.CUBESGREEN_X16);
                case 7:
                    return Properties.Resources.FOLDER_X32;
                case 8:
                    return Properties.Resources.PRICELIST_X16;
                case 9:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.PRICEUAH_X16);
                case 10:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.UNIT_X16);
                case 11:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.DATABASE_X32);
                case 13:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.PRICEUSD_X16); ;
                case 14:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.FORM_X16); 
                case 15:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.LIBRARY_X32);
                case 16:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.PRICEUAH_X16);
                case 17:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.FACTNAME_X32);
                case 18:
                    return Properties.Resources.PROPERTIES_X16;
                case 19:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.CLASS_X32);
                case 20:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.DOCUMENTDONE_X16); 
                case 21:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.TABLE_X32);
                case 22:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.COLUMN_X16);
                case 23:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.FILERTF_X16);
                case 24:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.MONEY_X16);
                case 25:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.SETTINGS_X32);
                case 26:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.USER_X32);
                case 27:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.KEYS_X32);
                case 30:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.PRICEUAH_X16);
                case 33:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.FLAGSUA_X32);
                case 35:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.RULESET_X32);
                case 36:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.XMLSTORAGE_X32);
                case 38:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.TOWN_X32);
                case 39:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.CUBESYELLOW_X32);
                case 40:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.CHAIN_X32);
                case 43:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.PASSPORTBLUE_X32);
                case 70:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.MISCGREEN_X32);
                case 71:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.MISCGREEN_X32);
                case 79:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.NOTE_X32);
                case 84:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.HISTORY_X16);
                case 93:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.CALENDAR_X16);
                case 96:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.TASK_X16);
                case 97:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.USERS_X16);
                case 107:
                    return ResourceImage.GetByCode(item.Workarea, ResourceImage.CALENDAR_X32);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Изображение 16х16
        /// </summary>
        /// <param name="value">Вид системного объекта</param>
        /// <returns></returns>
        public static Bitmap GetImage(this EntityKind value)
        {
            if (Enum.IsDefined(typeof(WhellKnownDbEntity), value.EntityId))
            {
                WhellKnownDbEntity kind = (WhellKnownDbEntity)value.EntityId;
                switch (kind)
                {
                    case WhellKnownDbEntity.Calendar:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.CALENDAR_X16);
                    case WhellKnownDbEntity.AgentAddress:
                        switch (value.SubKind)
                        {
                            case AgentAddress.KINDVALUE_LEGALADDRESS:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.HOME_X16);
                            case AgentAddress.KINDVALUE_ACTUALADDRESS:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.HOUSE_X16);
                            case AgentAddress.KINDVALUE_DELIVERYADDRESS:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.CARBLUE_X16);
                            default:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
                        }
                    case WhellKnownDbEntity.Contact:
                        switch (value.SubKind)
                        {
                            case Contact.KINDVALUE_LOCATIONCURRENT:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.HOME_X16);
                                
                            case Contact.KINDVALUE_LOCATIONFACT:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.HOUSE_X16);
                            case Contact.KINDVALUE_WWW:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTCONTACTWWW_X16);
                            case Contact.KINDVALUE_EMAIL:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTCONTACTEMAIL_X16);
                            case Contact.KINDVALUE_PHONEWORK:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.PHONE_X16);
                            case Contact.KINDVALUE_PHONEMOBILE:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.PHONE_X16);
                            case Contact.KINDVALUE_PHONEHOME:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.PHONE_X16);
                            default:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
                        }
                    case WhellKnownDbEntity.CodeName:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.CODENAME_X16);
                    case WhellKnownDbEntity.Column:
                        switch (value.SubKind)
                        {
                            case CustomViewColumn.KINDVALUE_COLUMN:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.COLUMN_X16);
                            default:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.COLUMN_X16);
                        }
                    case WhellKnownDbEntity.Series:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESYELLOW_X16);
                    case WhellKnownDbEntity.Event:
                        switch (value.SubKind)
                        {
                            default:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.EVENT_X16);
                        }
                    case WhellKnownDbEntity.UserAccount:
                        switch (value.SubKind)
                        {
                            default:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.USERS_X16);
                        }
                    case WhellKnownDbEntity.Task:
                        switch (value.SubKind)
                        {
                            case Task.KINDVALUE_TASKSYSTEM:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.TASKRED_X16);
                            case Task.KINDVALUE_TASKUSER:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.TASK_X16);
                            case Task.KINDVALUE_TASKADVANCED:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.TASKADV_X16);
                            default:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.TASK_X16);
                        }
                    case WhellKnownDbEntity.DataCatalog:
                        switch (value.SubKind)
                        {
                            case DataCatalog.KINDVALUE_IN:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDERGREEN_X16);
                            case DataCatalog.KINDVALUE_OUT:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDERLIGHTBLUE_X16);
                            default:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDER_X16);
                        }
                    case WhellKnownDbEntity.TimePeriod:
                        switch (value.SubKind)
                        {
                            case TimePeriod.KINDVALUE_WORK:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.CALENDAR_X16);
                            case TimePeriod.KINDVALUE_BREAK:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.CALENDARRED_X16);
                            default:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.CALENDAR_X16);
                        }
                    case WhellKnownDbEntity.Note:
                        switch (value.SubKind)
                        {
                            case Note.KINDVALUE_USER:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.NOTE_X16);
                            case Note.KINDVALUE_COMMENT:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.NOTEGREEN_X16);
                            default:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.NOTE_X16);
                        }
                    case WhellKnownDbEntity.Message:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.MESSAGEBALLOON_X16);
                    case WhellKnownDbEntity.DateRegion:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.HISTORY_X16);
                    case WhellKnownDbEntity.WebService:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGTWEB_X16);
                    case WhellKnownDbEntity.StorageCell:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESYELLOW_X16);
                    case WhellKnownDbEntity.Town:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.TOWN_X16);
                    case WhellKnownDbEntity.FileData:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILERTF_X16);
                    case WhellKnownDbEntity.Product:
                        switch (value.SubKind)
                        {
                            case Product.KINDVALUE_PRODUCT:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.PRODUCT_X16);
                            case Product.KINDVALUE_MONEY:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.MONEY_X16);
                            case Product.KINDVALUE_AUTO: 
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.CARSEDANGREEN_X16);
                            case Product.KINDVALUE_PACK: // авто
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.PACKAGE_X16);
                            case Product.KINDVALUE_SERVICE: 
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.COMPONENTS_X16);
                            case Product.KINDVALUE_MBP:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.COMPONENTGREEN_X16);
                            case Product.KINDVALUE_ASSETS:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.COMPONENTYELLOW_X16);
                            default:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.PRODUCT_X16);
                        }
                    case WhellKnownDbEntity.Account:
                        switch (value.SubKind)
                        {
                            case Account.KINDVALUE_ACTIVE:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCOUNTACTIVE_X16);
                            case Account.KINDVALUE_PASSIVE :
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCOUNTPASSIVE_X16);
                            case Account.KINDVALUE_PASSIVEACTIVE:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCOUNTPASSIVEACTIVE_X16);
                            case Account.KINDVALUE_OFFBALANCE:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCOUNTOUTBALANCE_X16);
                            default:
                                return null;
                        }
                    case WhellKnownDbEntity.Knowledge:
                        switch (value.SubKind)
                        {
                            case Knowledge.KINDVALUE_LOCAL:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.BOOKGREEN_X16);
                            case Knowledge.KINDVALUE_ONLINE:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.BOOKBLUE_X16);
                            case Knowledge.KINDVALUE_FILELINK:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.BOOKYELLOW_X16);
                            default:
                                return null;
                        }
                    case WhellKnownDbEntity.Agent:
                        switch (value.SubKind)
                        {
                            case Agent.KINDVALUE_COMPANY:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTFIRM_X16);
                            case Agent.KINDVALUE_PEOPLE:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTWORKER_X16);
                            case Agent.KINDVALUE_STORE:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTSTORE_X16);
                            case Agent.KINDVALUE_MYCOMPANY:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTMYFIRM_X16);
                            case Agent.KINDVALUE_MYSTORE:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTSTORE_X16);
                            case Agent.KINDVALUE_BANK:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTBANK_X16);
                            default:
                                return null;
                        }
                    case WhellKnownDbEntity.Passport:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.PASSPORTBLUE_X16);
                    case WhellKnownDbEntity.Analitic:
                        switch (value.SubKind)
                        {
                            case 1:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.MISCMAGENTA_X16); 
                            case 2:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.MISCGREEN_X16); 
                            case 3:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.MISCBLUE_X16);
                            case 4:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.MONEY_X16); 
                            default:
                                {
                                    Analitic val = value.Workarea.Empty<Analitic>();
                                    val.KindValue = value.SubKind;
                                    return val.GetImage();
                                }
                        }
                    case WhellKnownDbEntity.Currency:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICEUSD_X16);
                    case WhellKnownDbEntity.Recipe:
                        switch (value.SubKind)
                        {
                            case Recipe.KINDVALUE_KOMPLEKT:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESGREEN_X16);
                            case Recipe.KINDVALUE_SET:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBESBLUE_X16);
                            default:
                                return null;
                        }
                    case WhellKnownDbEntity.DbObject:
                        switch (value.SubKind)
                        {
                            case DbObject.KINDVALUE_TABLE:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.TABLE_X16);    
                            case DbObject.KINDVALUE_VIEW:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.VIEW_X16);
                            case DbObject.KINDVALUE_STOREDPROC:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.STOREDPROC_X16);
                            case DbObject.KINDVALUE_FUNC:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.TABLEFUNCTION_X16);
                            case DbObject.KINDVALUE_TABLEFUNC:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.TABLEFUNCTION_X16);
                            default:
                                return null;
                        }
                    case WhellKnownDbEntity.Folder:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDERFLD_X16);
                    case WhellKnownDbEntity.PriceName:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICEUAH_X16);
                    case WhellKnownDbEntity.ProductUnit:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.UNIT_X16);
                    case WhellKnownDbEntity.Unit:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.UNIT_X16);
                    case WhellKnownDbEntity.Branche:
                        switch(value.SubKind)
                        {
                            case Branche.KINDVALUE_DEFAULT:
                                return Properties.Resources.DATABASE_X16;
                            case Branche.KINDVALUE_ACCENT7:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.ACCENT7_X16);
                            default:
                                return Properties.Resources.DATABASE_X16;
                        }
                    case WhellKnownDbEntity.ProductRecipeItem:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.CUBEGREEN_X16);
                    case WhellKnownDbEntity.Rate:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICEUSD_X16); 
                    case WhellKnownDbEntity.Library:
                        switch (value.SubKind)
                        {
                            case Library.KINDVALUE_LIBRARY:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.LIBRARY_X16);
                            case Library.KINDVALUE_RESOURCE:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.LIBRARY_X16);
                            case Library.KINDVALUE_PRINTFORM:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.PRINTFORM_X16);
                            case Library.KINDVALUE_APP:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.APPLICATION_X16);
                            case Library.KINDVALUE_WINDOW:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.WINDOWSLIST_X16);
                            case Library.KINDVALUE_PAGE:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.WINDOWSCASCADE_X16);    
                            case Library.KINDVALUE_DOCFORM:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.FORM_X16); 
                            case Library.KINDVALUE_REPSQL:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.REPORT_X16); 
                            case Library.KINDVALUE_METHOD:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.CLASS_X16);
                            case Library.KINDVALUE_UIMODULE:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.INTERFACE_MODULE_X16);
                            case Library.KINDVALUE_WEBREPORT:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.STIREPORT_X16);
                            case Library.KINDVALUE_REPPRINT:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.STIREPORT_X16);
                            case Library.KINDVALUE_WEBPRINTFORM:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.STIREPORTV4_X16);
                            case Library.KINDVALUE_DXREPORT:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.XTRAREPORTS_X16);
                            case Library.KINDVALUE_REPTBL:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.INTERACTIVE_REPORT_X16);
                            case Library.KINDVALUE_CONFIGFILE:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.FILEXML_X16);
                            default:
                                return null;
                        }

                    case WhellKnownDbEntity.Price:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICEUAH_X16);
                    case WhellKnownDbEntity.FactName:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.FACTNAME_X16);
                    case WhellKnownDbEntity.DbEntity:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.CLASS_X16);
                    case WhellKnownDbEntity.Document:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16); 
                    case WhellKnownDbEntity.CustomViewList:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.TABLE_X16);
                    case WhellKnownDbEntity.BankAccount:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.MONEY_X16);
                    case WhellKnownDbEntity.Acl:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.KEY_X16);
                    case WhellKnownDbEntity.Users:
                        switch (value.SubKind)
                        {
                            case Uid.KINDVALUE_USER:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.AGENTWORKER_X16);
                            case Uid.KINDVALUE_GROUP:
                                return ResourceImage.GetByCode(value.Workarea, ResourceImage.USERGROUP_X16);
                            default:
                                return null;
                        }
                    case WhellKnownDbEntity.XmlStorage:
                        return ResourceImage.GetByCode(value.Workarea, ResourceImage.XMLSTORAGE_X16);
                    default:
                        return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Разрешение</param>
        /// <returns></returns>
        public static Bitmap GetImage(this Right value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.KEY_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16х16
        /// </summary>
        /// <param name="value">Состояние</param>
        /// <returns></returns>
        public static Bitmap GetImage(this State value)
        {
            Bitmap image = null;
            if (value == null)
                return image;
            switch (value.Id)
            {
                case State.STATEDEFAULT:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FLAGYELOW_X16);
                    break;
                case State.STATEACTIVE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FLAGGREEN_X16);
                    break;
                case State.STATENOTDONE:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FLAGBLUE_X16);
                    break;
                case State.STATEDENY:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FLAGRED_X16);
                    break;
                case State.STATEDELETED:
                    image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FLAGBLACK_X16);
                    break;
            }
            return image;
        }
        /// <summary>
        /// Изображение 16х16
        /// </summary>
        /// <param name="value">Состояние</param>
        /// <returns></returns>
        public static Bitmap GetImageState(Workarea wa, int value)
        {
            Bitmap image = null;
            
            switch (value)
            {
                case State.STATEDEFAULT:
                    image = ResourceImage.GetByCode(wa, ResourceImage.FLAGYELOW_X16);
                    break;
                case State.STATEACTIVE:
                    image = ResourceImage.GetByCode(wa, ResourceImage.FLAGGREEN_X16);
                    break;
                case State.STATENOTDONE:
                    image = ResourceImage.GetByCode(wa, ResourceImage.FLAGBLUE_X16);
                    break;
                case State.STATEDENY:
                    image = ResourceImage.GetByCode(wa, ResourceImage.FLAGRED_X16);
                    break;
                case State.STATEDELETED:
                    image = ResourceImage.GetByCode(wa, ResourceImage.FLAGBLACK_X16);
                    break;
            }
            return image;
        }

        /// <summary>
        /// Изображение
        /// </summary>
        /// <param name="workarea">Рабочая область</param>
        /// <param name="value">Числовое значение</param>
        /// <returns></returns>
        public static Bitmap GetImageState(IWorkarea workarea, int value)
        {
            Bitmap image = null;
            if (value < 0)
                return image;
            switch (value)
            {
                case State.STATEDEFAULT:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.FLAGYELOW_X16);
                    break;
                case State.STATEACTIVE:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.FLAGGREEN_X16);
                    break;
                case State.STATENOTDONE:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.FLAGBLUE_X16);
                    break;
                case State.STATEDENY:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.FLAGRED_X16);
                    break;
                case State.STATEDELETED:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.FLAGBLACK_X16);
                    break;
            }
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Системный параметр</param>
        /// <returns></returns>
        public static Bitmap GetImage(this SystemParameter value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.SETTINGS_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16x16
        /// </summary>
        /// <param name="value">Тип документа</param>
        /// <returns></returns>
        public static Bitmap GetImage(this EntityDocument value)
        {
            Bitmap image = ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16); 
            return image;
        }

        /// <summary>
        /// Изображение 16х16 
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="value">Объект</param>
        /// <returns></returns>
        public static Bitmap GetImage<T>(this T value) where T : ICoreObject
        {
            Bitmap image = null;
            WhellKnownDbEntity dbentity = (WhellKnownDbEntity)value.EntityId;
            if (value is EntityKind)
            {
                return (value as EntityKind).GetImage();
            }
            else if (value is EntityDocumentKind)
            {
                return ResourceImage.GetByCode(value.Workarea, ResourceImage.DOCUMENTDONE_X16);
            }
            else if (value is ProcedureMap)
            {
                return (value as ProcedureMap).GetImage();
            }
            else
            {
                switch (dbentity)
                {
                    case WhellKnownDbEntity.Calendar:
                        image = (value as Calendar).GetImage();
                        break;
                    case WhellKnownDbEntity.CodeName:
                        image = (value as CodeName).GetImage();
                        break;
                    case WhellKnownDbEntity.Event:
                        image = (value as Event).GetImage();
                        break;
                    case WhellKnownDbEntity.UserAccount:
                        image = (value as UserAccount).GetImage();
                        break;
                    case WhellKnownDbEntity.Task:
                        image = (value as Task).GetImage();
                        break;
                    case WhellKnownDbEntity.DataCatalog:
                        image = (value as DataCatalog).GetImage();
                        break;
                    case WhellKnownDbEntity.TimePeriod:
                        image = (value as TimePeriod).GetImage();
                        break;
                    case WhellKnownDbEntity.Message:
                        image = (value as Message).GetImage();
                        break;
                    case WhellKnownDbEntity.Note:
                        image = (value as Note).GetImage();
                        break;
                    case WhellKnownDbEntity.DateRegion:
                        image = (value as DateRegion).GetImage();
                        break;
                    case WhellKnownDbEntity.WebService:
                        image = (value as WebService).GetImage();
                        break;
                    case WhellKnownDbEntity.Series:
                        image = (value as Series).GetImage();
                        break;
                    case WhellKnownDbEntity.StorageCell:
                        image = (value as StorageCell).GetImage();
                        break;
                    case WhellKnownDbEntity.Town:
                        image = (value as Town).GetImage();
                        break;
                    case WhellKnownDbEntity.Territory:
                        image = (value as Territory).GetImage();
                        break;
                    case WhellKnownDbEntity.Passport:
                        image = (value as Passport).GetImage();
                        break;
                    case WhellKnownDbEntity.Country:
                        image = (value as Country).GetImage();
                        break;
                    case WhellKnownDbEntity.Ruleset:
                        image = (value as Ruleset).GetImage();
                        break;
                    case WhellKnownDbEntity.Knowledge:
                        image = (value as Knowledge).GetImage();
                        break;
                    case WhellKnownDbEntity.FileData:
                        image = (value as FileData).GetImage();
                        break;
                    case WhellKnownDbEntity.Document:
                        image = (value as Document).GetImage();
                        break;
                    case WhellKnownDbEntity.EntityDocument:
                        image = (value as EntityDocument).GetImage();
                        break;
                    case WhellKnownDbEntity.Rate:
                        image = (value as Rate).GetImage();
                        break;
                    case WhellKnownDbEntity.PriceName:
                        image = (value as PriceName).GetImage();
                        break;
                    case WhellKnownDbEntity.Unit:
                        image = (value as Unit).GetImage();
                        break;
                    case WhellKnownDbEntity.Recipe:
                        image = (value as Recipe).GetImage();
                        break;
                    case WhellKnownDbEntity.ProductRecipeItem:
                        image = (value as ProductRecipe).GetImage();
                        break;
                    case WhellKnownDbEntity.ProductUnit:
                        image = (value as ProductUnit).GetImage();
                        break;
                    case WhellKnownDbEntity.Column:
                        image = (value as CustomViewColumn).GetImage();
                        break;
                    case WhellKnownDbEntity.CustomViewList:
                        image = (value as CustomViewList).GetImage();
                        break;
                    case WhellKnownDbEntity.Acl:
                        image = (value as Right).GetImage();
                        break;
                    case WhellKnownDbEntity.SystemParameter:
                        image = (value as SystemParameter).GetImage();
                        break;
                    case WhellKnownDbEntity.Hierarchy:
                        image = (value as Hierarchy).GetImage();
                        break;
                    case WhellKnownDbEntity.Users:
                        image = (value as Uid).GetImage();
                        break;
                    case WhellKnownDbEntity.Product:
                        image = (value as Product).GetImage();
                        break;
                    case WhellKnownDbEntity.Account:
                        image = (value as Account).GetImage();
                        break;
                    case WhellKnownDbEntity.Agent:
                        image = (value as Agent).GetImage();
                        break;
                    case WhellKnownDbEntity.Analitic:
                        image = (value as Analitic).GetImage();
                        break;
                    case WhellKnownDbEntity.FactName:
                        image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FACTNAME_X16);
                        break;
                    case WhellKnownDbEntity.BankAccount:
                        image = ResourceImage.GetByCode(value.Workarea, ResourceImage.MONEY_X16);
                        break;
                    case WhellKnownDbEntity.Branche:
                        image = (value as Branche).GetImage();
                        break;
                    case WhellKnownDbEntity.Currency:
                        image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PRICEUSD_X16);
                        break;
                    case WhellKnownDbEntity.Folder:
                        image = ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDERFLD_X16);
                        break;
                    case WhellKnownDbEntity.Library:
                        image = (value as Library).GetImage();
                        break;
                    case WhellKnownDbEntity.DbEntity:
                        image = (value as EntityType).GetImage();
                        break;
                    case WhellKnownDbEntity.XmlStorage:
                        image = ResourceImage.GetByCode(value.Workarea, ResourceImage.XMLSTORAGE_X16);
                        break;
                }
            }
            if(image==null)
                image = ResourceImage.GetByCode(value.Workarea, ResourceImage.PROPERTIES_X16);
            if (value.StateId == State.STATEDELETED)
                image = GetImageDeleted(value.Workarea, image);
            return image;
        }

        /// <summary>
        /// Изображение 16х16 
        /// </summary>
        /// <param name="workarea">Рабочая область</param>
        /// <param name="dbEntitySubKind">Идентификатор подтипа</param>
        /// <param name="stateId">Идентификатор состояния</param>
        /// <returns></returns>
        public static Bitmap GetImage(IWorkarea workarea, int dbEntitySubKind, int stateId)
        {
            short entityId = BaseKind.ExtractEntityKind(dbEntitySubKind);
            short subKind = BaseKind.ExtractSubKind(dbEntitySubKind);
            Bitmap image = null;
            WhellKnownDbEntity dbentity = (WhellKnownDbEntity)entityId;

            switch (dbentity)
            {
                case WhellKnownDbEntity.Contact:
                    switch (subKind)
                    {
                        case Contact.KINDVALUE_LOCATIONCURRENT:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.HOME_X16);
                            break;
                        case Contact.KINDVALUE_LOCATIONFACT:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.HOUSE_X16);
                            break;
                        case Contact.KINDVALUE_WWW:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.AGENTCONTACTWWW_X16);
                            break;
                        case Contact.KINDVALUE_EMAIL:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.AGENTCONTACTWWW_X16);
                            break;
                        case Contact.KINDVALUE_PHONEWORK:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.PHONE_X16);
                            break;
                        case Contact.KINDVALUE_PHONEMOBILE:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.PHONE_X16);
                            break;
                        case Contact.KINDVALUE_PHONEHOME:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.PHONE_X16);
                            break;
                        default:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.PROPERTIES_X16);
                            break;
                    }
                    break;
                case WhellKnownDbEntity.AgentAddress:
                    switch (subKind)
                    {
                        case AgentAddress.KINDVALUE_LEGALADDRESS:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.HOME_X16);
                            break;
                        case AgentAddress.KINDVALUE_ACTUALADDRESS:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.HOUSE_X16);
                            break;
                        case AgentAddress.KINDVALUE_DELIVERYADDRESS:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.CARBLUE_X16);
                            break;
                        default:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.HOME_X16);
                            break;
                    }
                    break;
                case WhellKnownDbEntity.FileDataVersion:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.FILEVERSION_X16);
                    break;
                case WhellKnownDbEntity.Calendar:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.CALENDAR_X16);
                    break;
                case WhellKnownDbEntity.CodeName:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.CODENAME_X16);
                    break;
                case WhellKnownDbEntity.DocumentWorkflow:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.RULESET_X16);
                    break;
                case WhellKnownDbEntity.Event:
                    switch (subKind)
                    {
                        default:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.EVENT_X16);
                            break;
                    }
                    break;
                case WhellKnownDbEntity.UserAccount:
                    switch (subKind)
                    {
                        default:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.USERS_X16);
                            break;
                    }
                    break;
                case WhellKnownDbEntity.Task:
                    switch (subKind)
                    {
                        case Task.KINDVALUE_TASKSYSTEM:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.TASKRED_X16);
                            break;
                        case Task.KINDVALUE_TASKUSER:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.TASK_X16);
                            break;
                        case Task.KINDVALUE_TASKADVANCED:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.TASKADV_X16);
                            break;
                        default:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.TASK_X16);
                            break;
                    }
                    break;
                case WhellKnownDbEntity.DataCatalog:
                    switch (subKind)
                    {
                        case DataCatalog.KINDVALUE_IN:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.FOLDERGREEN_X16);
                            break;
                        case DataCatalog.KINDVALUE_OUT:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.FOLDERLIGHTBLUE_X16);
                            break;
                        default:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.FOLDER_X16);
                            break;
                    }
                    break;
                case WhellKnownDbEntity.TimePeriod:
                    switch (subKind)
                    {
                        case TimePeriod.KINDVALUE_WORK:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.CALENDAR_X16);
                            break;
                        case TimePeriod.KINDVALUE_BREAK:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.CALENDARRED_X16);
                            break;
                        default:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.CALENDAR_X16);
                            break;
                    }
                    break;
                case WhellKnownDbEntity.Note:
                     switch (subKind)
                    {
                        case Note.KINDVALUE_USER:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.NOTE_X16);
                            break;
                        case Note.KINDVALUE_COMMENT:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.NOTEGREEN_X16);
                            break;
                         default:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.NOTE_X16);
                            break;
                    }
                    break;
                case WhellKnownDbEntity.Message:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.MESSAGEBALLOON_X16);
                    break;
                case WhellKnownDbEntity.DateRegion:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.HISTORY_X16);
                    break;
                case WhellKnownDbEntity.WebService:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.AGTWEB_X16);
                    break;
                case WhellKnownDbEntity.Series:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.CUBESYELLOW_X16);
                    break;
                case WhellKnownDbEntity.StorageCell:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.CUBESYELLOW_X16);
                    break;
                case WhellKnownDbEntity.Town:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.TOWN_X16);
                    break;
                case WhellKnownDbEntity.Passport:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.PASSPORTBLUE_X16);
                    break;
                case WhellKnownDbEntity.FactName:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.FACTNAME_X16);
                    break;
                case WhellKnownDbEntity.Country:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.FLAFGSUA_X16);
                    break;
                case WhellKnownDbEntity.Ruleset:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.RULESET_X16);
                    break;
                case WhellKnownDbEntity.FileData:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.FILERTF_X16);
                    break;
                case WhellKnownDbEntity.Chain:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.LINK_X16);
                    break;
                case WhellKnownDbEntity.EntityDocument:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.DOCUMENTDONE_X16); 
                    break;
                case WhellKnownDbEntity.ProductUnit:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.UNIT_X16);
                    break;
                case WhellKnownDbEntity.Unit:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.UNIT_X16);
                    break;
                case WhellKnownDbEntity.Acl:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.KEY_X16);
                    break;
                case WhellKnownDbEntity.SystemParameter:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.SETTINGS_X16);
                    break;
                case WhellKnownDbEntity.CustomViewList:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.TABLE_X16);
                    break;
                case WhellKnownDbEntity.ProductRecipeItem:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.CUBEGREEN_X16);
                    break;
                case WhellKnownDbEntity.Recipe:
                    switch (subKind)
                    {
                        case Recipe.KINDVALUE_KOMPLEKT:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.CUBESGREEN_X16);
                            break;
                        case Recipe.KINDVALUE_SET:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.CUBESBLUE_X16);
                            break;
                    }
                    break;
                case WhellKnownDbEntity.DbObject:
                    switch (subKind)
                    {
                        case DbObject.KINDVALUE_TABLE:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.TABLE_X16);
                            break;
                        case DbObject.KINDVALUE_VIEW:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.VIEW_X16);
                            break;
                        case DbObject.KINDVALUE_STOREDPROC:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.STOREDPROC_X16);
                            break;
                        case DbObject.KINDVALUE_FUNC:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.TABLEFUNCTION_X16);
                            break;
                        case DbObject.KINDVALUE_TABLEFUNC:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.TABLEFUNCTION_X16);
                            break;
                    }
                    break;
                case WhellKnownDbEntity.Product:
                    switch (subKind)
                    {
                        case Product.KINDVALUE_PRODUCT:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.PRODUCT_X16); 
                            break;
                        case Product.KINDVALUE_MONEY:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.MONEY_X16); 
                            break;
                        case Product.KINDVALUE_AUTO:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.CARSEDANGREEN_X16);
                            break;
                        case Product.KINDVALUE_PACK: // авто
                            image = ResourceImage.GetByCode(workarea, ResourceImage.PACKAGE_X16);
                            break;
                        case Product.KINDVALUE_SERVICE: // услуга
                            image = ResourceImage.GetByCode(workarea, ResourceImage.COMPONENTS_X16);
                            break;
                        case Product.KINDVALUE_MBP: // мбп
                            image = ResourceImage.GetByCode(workarea, ResourceImage.COMPONENTGREEN_X16);
                            break;
                        case Product.KINDVALUE_ASSETS: // основные средства
                            image = ResourceImage.GetByCode(workarea, ResourceImage.COMPONENTYELLOW_X16);
                            break;

                    }
                    break;
                case WhellKnownDbEntity.Knowledge:
                    switch (subKind)
                    {
                        case Knowledge.KINDVALUE_LOCAL:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.BOOKGREEN_X16);
                            break;
                        case Knowledge.KINDVALUE_ONLINE:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.BOOKBLUE_X16);
                            break;
                        case Knowledge.KINDVALUE_FILELINK:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.BOOKYELLOW_X16);
                            break;
                    }
                    break;
                case WhellKnownDbEntity.Account:
                    switch (subKind)
                    {
                        case Account.KINDVALUE_ACTIVE:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.ACCOUNTACTIVE_X16);
                            break;
                        case Account.KINDVALUE_PASSIVE:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.ACCOUNTPASSIVE_X16);
                            break;
                        case Account.KINDVALUE_PASSIVEACTIVE:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.ACCOUNTPASSIVEACTIVE_X16);
                            break;
                        case Account.KINDVALUE_OFFBALANCE:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.ACCOUNTOUTBALANCE_X16);
                            break;
                    }
                    break;

                case WhellKnownDbEntity.Agent:
                    switch (subKind)
                    {
                        case Agent.KINDVALUE_COMPANY:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.AGENTFIRM_X16);
                            break;
                        case Agent.KINDVALUE_PEOPLE:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.AGENTWORKER_X16);
                            break;
                        case Agent.KINDVALUE_MYCOMPANY:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.AGENTMYFIRM_X16); ;
                            break;
                        case Agent.KINDVALUE_BANK:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.AGENTBANK_X16);
                            break;
                        case Agent.KINDVALUE_STORE:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.AGENTSTORE_X16);
                            break;
                        case Agent.KINDVALUE_MYSTORE:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.AGENTSTORE_X16);
                            break;
                        default:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.AGENTFIRM_X16);
                            break;

                    }
                    //if (stateId == State.STATEDELETED)
                    //    image = GetImageDeleted(workarea, image);
                    break;

                case WhellKnownDbEntity.Analitic:
                    switch (subKind)
                    {
                        case Analitic.KINDVALUE_ANALITIC:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.MISCMAGENTA_X16); 
                            break;
                        case Analitic.KINDVALUE_TRADEGROUP:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.MISCGREEN_X16); 
                            break;
                        case 4:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.MISCBLUE_X16); 
                            break;
                    }
                    break;
                case WhellKnownDbEntity.BankAccount:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.MONEY_X16);
                    break;
                case WhellKnownDbEntity.Branche:
                    switch(subKind)
                    {
                        case Branche.KINDVALUE_DEFAULT:
                            image = Properties.Resources.DATABASE_X16;
                            break;
                        case Branche.KINDVALUE_ACCENT7:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.ACCENT7_X16);
                            break;
                        default:
                            image = Properties.Resources.DATABASE_X16;
                            break;
                    }
                    break;
                case WhellKnownDbEntity.Currency:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.PRICEUSD_X16); 
                    break;
                case WhellKnownDbEntity.Folder:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.FOLDER_X16);
                    break;
                case WhellKnownDbEntity.Library:
                    switch (subKind)
                    {
                        case Library.KINDVALUE_LIBRARY:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.LIBRARY_X16);
                            break;
                        case Library.KINDVALUE_RESOURCE:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.LIBRARY_X16);
                            break;
                        case Library.KINDVALUE_APP:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.APPLICATION_X16);
                            break;
                        case Library.KINDVALUE_DOCFORM:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.FORM_X16); 
                            break;
                        case Library.KINDVALUE_REPSQL:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.REPORT_X16); 
                            break;
                        case Library.KINDVALUE_WINDOW:
                            return ResourceImage.GetByCode(workarea, ResourceImage.WINDOWSLIST_X16);
                        case Library.KINDVALUE_PAGE:
                            return ResourceImage.GetByCode(workarea, ResourceImage.WINDOWSCASCADE_X16);
                        case Library.KINDVALUE_METHOD:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.CLASS_X16);
                            break;
                        case Library.KINDVALUE_PRINTFORM:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.PRINTFORM_X16);
                            break;
                        case Library.KINDVALUE_UIMODULE:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.INTERFACE_MODULE_X16);
                            break;
                        case Library.KINDVALUE_REPPRINT:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.STIREPORT_X16);
                            break;
                        case Library.KINDVALUE_WEBREPORT:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.STIREPORT_X16);
                            break;
                        case Library.KINDVALUE_WEBPRINTFORM:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.STIREPORTV4_X16);
                            break;
                        case Library.KINDVALUE_DXREPORT:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.XTRAREPORTS_X16);
                            break;
                        case Library.KINDVALUE_REPTBL:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.INTERACTIVE_REPORT_X16);
                            break;
                        case Library.KINDVALUE_CONFIGFILE:
                            image = ResourceImage.GetByCode(workarea, ResourceImage.FILEXML_X16);
                            break;
                    }
                    break;
                case WhellKnownDbEntity.XmlStorage:
                    image = ResourceImage.GetByCode(workarea, ResourceImage.XMLSTORAGE_X16);
                    break;

            }
            if (stateId == State.STATEDELETED)
                image = GetImageDeleted(workarea, image);
            return image;
        }

        public static Bitmap GetImageHierarchy(IWorkarea workarea, int kindType, bool isFind)
        {
            Bitmap img;
            switch (kindType)
            {
                case 7:
                    img = ResourceImage.GetByCode(workarea, ResourceImage.FOLDER_X16);
                    break;
                case 1:
                    img = ResourceImage.GetByCode(workarea, ResourceImage.FOLDERBLUE_X16);
                    break;
                case 2:
                    img = ResourceImage.GetByCode(workarea, ResourceImage.FOLDERGREEN_X16);
                    break;
                case 3:
                    img = ResourceImage.GetByCode(workarea, ResourceImage.FOLDERLIGHTBLUE_X16);
                    break;
                case 4:
                    img = ResourceImage.GetByCode(workarea, ResourceImage.FOLDERMAGENTA_X16);
                    break;
                case 6:
                    img = ResourceImage.GetByCode(workarea, ResourceImage.FOLDERGREEN_X16);
                    break;
                case 10:
                    img = ResourceImage.GetByCode(workarea, ResourceImage.FOLDERLIGHTBLUE_X16);
                    break;
                case 14:
                    img = ResourceImage.GetByCode(workarea, ResourceImage.FOLDERRED_X16);
                    break;
                case 82:
                    img = ResourceImage.GetByCode(workarea, ResourceImage.FOLDERLIGHTBLUE_X16);
                    break;
                case 84:
                    img = ResourceImage.GetByCode(workarea, ResourceImage.FOLDERLIGHTBLUE_X16);
                    break;
                case 93:
                    img = ResourceImage.GetByCode(workarea, ResourceImage.FOLDERLIGHTBLUE_X16);
                    break;
                case 96:
                    img = ResourceImage.GetByCode(workarea, ResourceImage.FOLDERBLUE_X16);
                    break;
                default:
                    img = ResourceImage.GetByCode(workarea, ResourceImage.FOLDER_X16);
                    break;
            }
            if (isFind)
            {
                img = GetImageHierarchyFind(img);
            }
            return img;

        }

        public static Bitmap GetImageDeleted(IWorkarea workarea, Bitmap source)
        {
            if (source != null)
            {
                Bitmap image = new Bitmap(source);
                Bitmap image2 = ResourceImage.GetByCode(workarea, ResourceImage.DELETEDELEMENT_X16);
                Graphics myGraphic = Graphics.FromImage(image);
                myGraphic.DrawImageUnscaled(image2, 0, 0);
                myGraphic.Save();
                return image;
            }
            return ResourceImage.GetByCode(workarea, ResourceImage.DELETEDELEMENT_X16);
        }

        public static Bitmap GetImageHierarchyFind(Bitmap source)
        {
            if (source != null)
            {
                Bitmap image = new Bitmap(source);
                Bitmap image2 = Properties.Resources.HIERARCHYFIND_X16;
                Graphics myGraphic = Graphics.FromImage(image);
                myGraphic.DrawImageUnscaled(image2, 0, 0);
                myGraphic.Save();
                return image;
            }
            return null;
        }
        #endregion
    }
}
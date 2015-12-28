using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace BusinessObjects
{
    public class ResourceImage : KeyResource<Image>
    {
        public static List<ResourceImage> Collection(Workarea workarea)
        {
            List<ResourceImage> coll = new List<ResourceImage>();

            using (SqlConnection cnn = workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException();

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = workarea.FindMethod("ResourceImageLoadAll").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ResourceImage item = new ResourceImage { Workarea = workarea };
                                item.Load(reader);
                                coll.Add(item);
                            }
                            reader.Close();
                        }
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return coll;
        }

        private static ResourceImage InternalGetByCode(IWorkarea workarea, string value)
        {
            ResourceImage item = new ResourceImage { Workarea = (Workarea)workarea }; 

            using (SqlConnection cnn = workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException();

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = workarea.FindMethod("ResourceImageLoadByCode").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 255).Value = value;
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                item.Load(reader);
                            }
                            reader.Close();
                        }
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return item;
        }

        private static List<ResourceImage> _localCasheData = new List<ResourceImage>();

        public static Bitmap GetByCode(IWorkarea workarea, string value)
        {
            ResourceImage item =_localCasheData.FirstOrDefault(s => s.Code.ToUpper() == value.ToUpper());
            if(item==null)
            {
                item = InternalGetByCode(workarea, value);
                if (item != null && item.Value!=null)
                    _localCasheData.Add(item);
            }
            if (item != null)
                return (Bitmap) item.Value;
            return null;
        }
        public static Bitmap GetSystemImage(string value)
        {
            switch (value)
            {
                case HELP_X32:
                    return Windows.Properties.Resources.HELP_X32;
                case PRINT_X32:
                    return Windows.Properties.Resources.PRINT_X32;
                case DOCUMENTDONE_X16:
                    return Windows.Properties.Resources.DOCUMENTDONE_X16;
                case KEY_X16:
                    return Windows.Properties.Resources.KEY_X16;
                case NEW_X32:
                    return Windows.Properties.Resources.NEW_X32;
                case CREATE_X16:
                    return Windows.Properties.Resources.CREATE_X16;
                case CREATE_X32:
                    return Windows.Properties.Resources.CREATE_X32;
                case DATABASE_X16:
                    return Windows.Properties.Resources.DATABASE_X16;
                case DELETE_X16:
                    return Windows.Properties.Resources.DELETE_X16;
                case EDIT_X32:
                    return Windows.Properties.Resources.EDIT_X32;
                case EXIT_X32:
                    return Windows.Properties.Resources.EXIT_X32;
                case FOLDER_X32:
                    return Windows.Properties.Resources.FOLDER_X32;
                case KEYS_X32:
                    return Windows.Properties.Resources.KEYS_X32;
                case SAVECLOSE_X32:
                    return Windows.Properties.Resources.SAVECLOSE_X32;   
                case PERIOD_X32:
                    return Windows.Properties.Resources.PERIOD_X32;      
                case HISTORY_X16:
                    return Windows.Properties.Resources.HISTORY_X16;      
                 case PROPERTIES_X16:
                    return Windows.Properties.Resources.PROPERTIES_X16;      
                case REFRESHGREEN_X16:
                    return BusinessObjects.Windows.Properties.Resources.REFRESH_X16;      
                case REFRESHGREEN_X32:
                    return BusinessObjects.Windows.Properties.Resources.REFRESH_X32;      
                case SAVE_X32:
                    return BusinessObjects.Windows.Properties.Resources.SAVE_X32;
            }
            return null;
        }

        public ResourceImage(): base()
        {
            EntityId = (short) WhellKnownDbEntity.ResourceImage;
        }
        // TODO: Добавить столбец в таблицу и изменить ХП
        private string _memo;
        /// <summary>
        /// Примечание
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set
            {
                if (value == _memo) return;
                OnPropertyChanging(GlobalPropertyNames.Memo);
                _memo = value;
                OnPropertyChanged(GlobalPropertyNames.Memo);
            }
        }
        

        private byte[] _sourceValue;
        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            Memo = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
            _sourceValue  = !reader.IsDBNull(12) ? reader.GetSqlBinary(12).Value : null;
            if (_sourceValue != null && !_sourceValue.All(v => v == 0))
            {
                Value = ByteArrayToImage(_sourceValue);
            }
            if (!endInit) return;
            OnEndInit();
        }
        private static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(Memo))
                prm.Value = DBNull.Value;
            else
                prm.Value = Memo;
            sqlCmd.Parameters.Add(prm);
                    
            prm = new SqlParameter(GlobalSqlParamNames.Value, SqlDbType.VarBinary);
            _sourceValue = ImageToByteArray(Value);
            if (_sourceValue == null || _sourceValue.All(v => v == 0))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _sourceValue.Length;
                prm.Value = _sourceValue;
            }
            sqlCmd.Parameters.Add(prm);
        }

        #region Константы
// ReSharper disable InconsistentNaming
        public const string FILEVERSION_X32 = "FILEVERSION_X32";
        public const string FILEVERSION_X16 = "FILEVERSION_X16";

        public const string HOME_X16 = "HOME_X16";
        public const string HOUSE_X16 = "HOUSE_X16";
        public const string PHONE_X16 = "PHONE_X16";
        
        public const string AGENTCONTACTEMAIL_X32 = "AGENTCONTACTEMAIL_X32";
        public const string AGENTCONTACTEMAIL_X16 = "AGENTCONTACTEMAIL_X16";
        public const string AGENTCONTACTWWW_X32 = "AGENTCONTACTWWW_X32";
        public const string AGENTCONTACTWWW_X16 = "AGENTCONTACTWWW_X16";
        public const string CODENAME_X16 = "CODENAME_X16";
        public const string CERTIFICATE_X16 = "CERTIFICATE_X16";
        public const string CERTIFICATE_X32 = "CERTIFICATE_X32";

        public const string EVENT_X16 = "EVENT_X16";
        public const string EVENT_X32 = "EVENT_X32";
        public const string USERS_X16 = "USERS_X16";
        public const string USERS_X32 = "USERS_X32";

        public const string SPELLCHECK_X32 = "SPELLCHECK_X32";

        public const string TASK_X32 = "TASK_X32";
        public const string TASK_X16 = "TASK_X16";
        public const string TASKRED_X16 = "TASKRED_X16";
        public const string TASKADV_X16 = "TASKADV_X16";
        
        public const string COMPONENTGREEN_X16 = "COMPONENTGREEN_X16";
        public const string COMPONENTRED_X16 = "COMPONENTRED_X16";
        public const string COMPONENTYELLOW_X16 = "COMPONENTYELLOW_X16";
        public const string XTRAREPORTS_X16 = "XTRAREPORTS_X16";
        public const string DOC_X32 = "DOC_X32";
        public const string CALENDAR_X32 = "CALENDAR_X32";
        public const string CALENDAR_X16 = "CALENDAR_X16";
        public const string CALENDARRED_X16 = "CALENDARRED_X16";
        public const string MESSAGEBALLOON_X16 = "MESSAGEBALLOON_X16";
        public const string NOTEGREEN_X16 = "NOTEGREEN_X16";
        public const string NOTE_X16 = "NOTE_X16";
        public const string SAVE_X16 = "SAVE_X16";
        public const string ADD_X16 = "ADD_X16";
        public const string OPEN_X16 = "OPEN_X16";
        public const string FAVORITES_X16 = "FAVORITES_X16";
        public const string FORMBLUE_X32 = "FORMBLUE_X32";
        public const string ARROWDOWNBLUE_X16 = "ARROWDOWNBLUE_X16";
        public const string ARROWUPBLUE_X16 = "ARROWUPBLUE_X16";
        public const string PASTE_X16 = "PASTE_X16";
        public const string STRUCTUREVIEW_X16 = "STRUCTUREVIEW_X16";
        public const string STRUCTUREVIEW_X32 = "STRUCTUREVIEW_X32";
        public const string ACCENT7_X16 = "ACCENT7_X16";
        public const string AGTWEB_X16 = "AGTWEB_X16";
        public const string BOOKBLUE_X16 = "BOOKBLUE_X16";
        public const string BOOKGREEN_X16 = "BOOKGREEN_X16";
        public const string BOOKYELLOW_X16 = "BOOKYELLOW_X16";
        public const string AUTONUM_X32 = "AUTONUM_X32";
        public const string CLIENTTOSERVER_X32 = "CLIENTTOSERVER_X32";
        public const string FOLDERHOME_X32 = "FOLDERHOME_X32";
        public const string BOOK_X32 = "BOOK_X32";
        public const string EXEC_X16 = "EXEC_X16";
        public const string PACKAGE_X16 = "PACKAGE_X16";
        public const string CARBLUE_X16 = "CARBLUE_X16";
        public const string PRINTREPORT_X16 = "PRINTREPORT_X16";
        public const string PRINTREPORT_X32 = "PRINTREPORT_X32";
        public const string EDUCATION_X32 = "EDUCATION_X32";
        public const string EDUCATION_X16 = "EDUCATION_X16";
        public const string STIREPORT_X16 = "STIREPORT_X16";
        public const string STIREPORTV4_X16 = "STIREPORTV4_X16";
        public const string WINDOWSCASCADE_X16 = "WINDOWSCASCADE_X16";
        public const string WINDOWSLIST_X16 = "WINDOWSLIST_X16";
        public const string MISCGREEN_X32 = "MISCGREEN_X32";
        public const string NOTE_X32 = "NOTE_X32";
        public const string CHARTCOLUMN_X32 = "CHARTCOLUMN_X32";
        public const string NETWORK_X32 = "NETWORK_X32";
        public const string COMPONENTS_X32 = "COMPONENTS_X32";
        public const string COMPONENTS_X16 = "COMPONENTS_X16";
        public const string CONTRACT_X32 = "CONTRACT_X32";
        public const string CREDITCARD_X32 = "CREDITCARD_X32";
        public const string DOCUMENTNEWEDIT_X16 = "DOCUMENTNEWEDIT_X16";
        public const string FOLDERFAVORITE_X32 = "FOLDERFAVORITE_X32";
        public const string NODE_X32 = "NODE_X32";
        public const string FILEHTML_X32 = "FILEHTML_X32";
        public const string STACKOFMONEY_X32 = "STACKOFMONEY_X32";
        public const string BOX_X32 = "BOX_X32";
        public const string SALES_X32 = "SALES_X32";
        public const string CUBESYELLOW_X32 = "CUBESYELLOW_X32";
        public const string ERROR_X16 = "ERROR_X16";
        public const string OK_X16 = "OK_X16";
        public const string TOWN_X32 = "TOWN_X32";
        public const string TOWN_X16 = "TOWN_X16";
        public const string PASSPORTBLUE_X32 = "PASSPORTBLUE_X32";
        public const string PASSPORTBLUE_X16 = "PASSPORTBLUE_X16";
        public const string CARSEDANGREEN_X16 = "CARSEDANGREEN_X16";
        public const string CUBEGREEN_X16 = "CUBEGREEN_X16";
        public const string CUBESBLUE_X16 = "CUBESBLUE_X16";
        public const string CUBESGREEN_X16 = "CUBESGREEN_X16";
        public const string CUBESGREEN_X32 = "CUBESGREEN_X32";
        public const string EDITCOPY_X32 = "EDITCOPY_X32";
        public const string ACCENT_X32 = "ACCENT_X32";
        public const string APPROVEGREEN_X32 = "APPROVEGREEN_X32";
        public const string APPROVERED_X32 = "APPROVERED_X32";
        public const string ROLLBACKRED_X32 = "ROLLBACKRED_X32";
        public const string EXCEL_X32 = "EXCEL_X32";
        public const string BACKGREEN_X32 = "BACKGREEN_X32";
        public const string FORWARDGREEN_X32 = "FORWARDGREEN_X32";
        public const string INFO_X32 = "INFO_X32";
        public const string INFO_X16 = "INFO_X16";
        public const string RUNROUNDGREEN_X32 = "RUNROUNDGREEN_X32";
        public const string RUNROUNDGREEN_X16 = "RUNROUNDGREEN_X16";
        public const string HELP_X32 = "HELP_X32";
        public const string PREVIEW_X32 = "PREVIEW_X32";
        public const string PRINT_X32 = "PRINT_X32";
        public const string ACTION_X32 = "ACTION_X32";
        public const string DOCUMENTDONE_X16 = "DOCUMENTDONE_X16";
        public const string DOCUMENTTRASH_X16 = "DOCUMENTTRASH_X16";
        public const string DOCUMENTNOTDONE_X16 = "DOCUMENTNOTDONE_X16";
        public const string CREATE_X32 = "CREATE_X32";
        public const string PERIOD_X32 = "PERIOD_X32";
        public const string HISTORY_X16 = "HISTORY_X16";
        public const string PROPERTIES_X16 = "PROPERTIES_X16";
        public const string REFRESHGREEN_X16 = "REFRESHGREEN_X16";
        public const string REFRESHGREEN_X32 = "REFRESHGREEN_X32";
        public const string SAVE_X32 = "SAVE_X32";
        public const string SAVECLOSE_X32 = "SAVECLOSE_X32";
        public const string KEYS_X32 = "KEYS_X32";
        public const string FOLDER_X32 = "FOLDER_X32";
        public const string EXIT_X32 = "EXIT_X32";
        public const string EDIT_X32 = "EDIT_X32";
        public const string DELETE_X16 = "DELETE_X16";
        public const string DATABASE_X16 = "DATABASE_X16";
        public const string NEW_X32 = "NEW_X32";
        public const string LINKNEW_X32 = "LINKNEW_X32";
        public const string FORM_X16 = "FORM_X16";
        public const string MISCBLUE_X16 = "MISCBLUE_X16";
        public const string MISCGREEN_X16 = "MISCGREEN_X16";
        public const string SETTINGS_X32 = "SETTINGS_X32";
        public const string PRINTFORM_X16 = "PRINTFORM_X16";
        public const string PRICEUAH_X16 = "PRICEUAH_X16";
        public const string REPORT_X16 = "REPORT_X16";
        public const string TABLE_X32 = "TABLE_X32";
        public const string SEARCH_X32 = "SEARCH_X32";
        public const string PRICEUSD_X16 = "PRICEUSD_X16";
        public const string PRODUCT_X16 = "PRODUCT_X16";
        public const string AGENTFIRM_X16 = "AGENTFIRM_X16";
        public const string AGENTMYFIRM_X16 = "AGENTMYFIRM_X16";
        public const string AGENTWORKER_X16 = "AGENTWORKER_X16";
        public const string AGENTSTORE_X16 = "AGENTSTORE_X16";
        public const string AGENTBANK_X16 = "AGENTBANK_X16";
        public const string AGENT_X32 = "AGENT_X32";
        public const string ACCOUNT_X32 = "ACCOUNT_X32";
        public const string ACCOUNTACTIVE_X16 = "ACCOUNTACTIVE_X16";
        public const string ACCOUNTOUTBALANCE_X16 = "ACCOUNTOUTBALANCE_X16";
        public const string ACCOUNTPASSIVE_X16 = "ACCOUNTPASSIVE_X16";
        public const string ACCOUNTPASSIVEACTIVE_X16 = "ACCOUNTPASSIVEACTIVE_X16";
        public const string HELP_X16 = "HELP_X16";
        public const string MISCMAGENTA_X16 = "MISCMAGENTA_X16";
        public const string DELETEDELEMENT_X16 = "DELETEDELEMENT_X16";
        public const string DOCUMENTNEW_X16 = "DOCUMENTNEW_X16";
        public const string DOCUMENTCHANGES_X16 = "DOCUMENTCHANGES_X16";
        public const string FOLDERTML_X16 = "FOLDERTML_X16";
        public const string FOLDERFLD_X16 = "FOLDERFLD_X16";
        public const string FOLDER_X16 = "FOLDER_X16";
        public const string FOLDERLIGHTBLUE_X16 = "FOLDERLIGHTBLUE_X16";
        public const string FOLDERGREEN_X16 = "FOLDERGREEN_X16";
        public const string FOLDERMAGENTA_X16 = "FOLDERMAGENTA_X16";
        public const string FOLDERBLUE_X16 = "FOLDERBLUE_X16";
        public const string FOLDERRED_X16 = "FOLDERRED_X16";
        public const string LIBRARY_X32 = "LIBRARY_X32";
        public const string LIBRARY_X16 = "LIBRARY_X16";
        public const string SELECT_X32 = "SELECT_X32";
        public const string REPORT_X32 = "REPORT_X32";
        public const string DELETE_X32 = "DELETE_X32";
        public const string DATAINTO_X32 = "DATAINTO_X32";
        public const string DATAOUT_X32 = "DATAOUT_X32";
        public const string RULESET_X16 = "RULESET_X16";
        public const string RULESET_X32 = "RULESET_X32";
        public const string STOREDPROC_X32 = "STOREDPROC_X32";
        public const string PRICELIST_X32 = "PRICELIST_X32";
        public const string PRICENAME_X32 = "PRICENAME_X32";
        public const string CURRENCY_X32 = "CURRENCY_X32";
        public const string RATE_X32 = "RATE_X32";
        public const string PRICEPOLICY_X32 = "PRICEPOLICY_X32";
        public const string FILEDATA_X32 = "FILEDATA_X32";
        public const string UNIT_X16 = "UNIT_X16";
        public const string ARROWBLUEDOWN_X16 = "ARROWBLUEDOWN_X16";
        public const string ARROWBLUEUP_X16 = "ARROWBLUEUP_X16";
        public const string ARROWLEFTGREEN_X16 = "ARROWLEFTGREEN_X16";
        public const string ATTACHMENT_X16 = "ATTACHMENT_X16";
        public const string EDITCUT_X16 = "EDITCUT_X16";
        public const string EDITUNDO_X16 = "EDITUNDO_X16";
        public const string CLASS_X16 = "CLASS_X16";
        public const string LINK_X16 = "LINK_X16";
        public const string ANALITICMAGENTA_X32 = "ANALITICMAGENTA_X32";
        public const string CHAIN_X32 = "CHAIN_X32";
        public const string USER_X32 = "USER_X32";
        public const string USERGROUP_X16 = "USERGROUP_X16";
        public const string IMAGE_X32 = "IMAGE_X32";
        public const string IMAGE_X16 = "IMAGE_X16";
        public const string CLASS_X32 = "CLASS_X32";
        public const string APPLY_X16 = "APPLY_X16";
        public const string BALLGREEN_X16 = "BALLGREEN_X16";
        public const string BALLRED_X16 = "BALLRED_X16";
        public const string BALLYELOW_X16 = "BALLYELOW_X16";
        public const string COLUMN_X16 = "COLUMN_X16";
        public const string CLOSE_X16 = "CLOSE_X16";
        public const string DATABASE_X32 = "DATABASE_X32";
        public const string COPY_X16 = "COPY_X16";
        public const string CREATE_X16 = "CREATE_X16";
        public const string KEY_X16 = "KEY_X16";
        public const string VIEW_X16 = "VIEW_X16";
        public const string SORTAZ_X16 = "SORTAZ_X16";
        public const string SORTZA_X16 = "SORTZA_X16";
        public const string FILEEXCEL_X16 = "FILEEXCEL_X16";
        public const string FILEEXCELXML_X16 = "FILEEXCELXML_X16";
        public const string FILEHTML_X16 = "FILEHTML_X16";
        public const string FILEMHT_X16 = "FILEMHT_X16";
        public const string FILEPDF_X16 = "FILEPDF_X16";
        public const string FILEPNG_X16 = "FILEPNG_X16";
        public const string FILERTF_X16 = "FILERTF_X16";
        public const string FILETXT_X16 = "FILETXT_X16";
        public const string FILEWORD_X16 = "FILEWORD_X16";
        public const string FILEXML_X16 = "FILEXML_X16";
        public const string FILEXPS_X16 = "FILEXPS_X16";
        public const string TRIANGLEGREEN_X32 = "TRIANGLEGREEN_X32";
        public const string FLAGRED_X16 = "FLAGRED_X16";
        public const string FLAGYELOW_X16 = "FLAGYELOW_X16";
        public const string FLAGGREEN_X16 = "FLAGGREEN_X16";
        public const string FLAGBLUE_X16 = "FLAGBLUE_X16";
        public const string FLAGBLACK_X16 = "FLAGBLACK_X16";
        public const string FLAGSUA_X32 = "FLAGSUA_X32";
        public const string FLAFGSUA_X16 = "FLAFGSUA_X16";
        public const string MONEY_X16 = "MONEY_X16";
        public const string MONEY_X32 = "MONEY_X32";
        public const string PRODUCT_X32 = "PRODUCT_X32";
        public const string STOREDPROC_X16 = "STOREDPROC_X16";
        public const string TABLE_X16 = "TABLE_X16";
        public const string TABLEFUNCTION_X16 = "TABLEFUNCTION_X16";
        public const string SETTINGS_X16 = "SETTINGS_X16";
        public const string SEARCHHIERARCHY_X32 = "SEARCHHIERARCHY_X32";
        public const string SEARCHAGENT_X32 = "SEARCHAGENT_X32";
        public const string ORGCHART_X16 = "ORGCHART_X16";
        public const string RULES_X16 = "RULES_X16";
        public const string RECIPE_X16 = "RECIPE_X16";
        public const string PRICE_POLICY_X16 = "PRICE_POLICY_X16";
        public const string HIERARCHY_CONTENT_X16 = "HIERARCHY_CONTENT_X16";
        public const string CONTACT_INFO_X16 = "CONTACT_INFO_X16";
        public const string APPLICATION_X16 = "APPLICATION_X16";
        public const string INTERFACE_MODULE_X16 = "INTERFACE_MODULE_X16";
        public const string INTERACTIVE_REPORT_X16 = "INTERACTIVE_REPORT_X16";
        public const string ARROWDOWNBLUE_X32 = "ARROWDOWNBLUE_X32";
        public const string ARROWUPBLUE_X32 = "ARROWUPBLUE_X32";
        public const string XMLSTORAGE_X32 = "XMLSTORAGE_X32";
        public const string XMLSTORAGE_X16 = "XMLSTORAGE_X16";
        public const string MANAGEMENTSTUDIO_X16 = "MANAGEMENTSTUDIO_X16";
        public const string MANAGEMENTSTUDIO_X32 = "MANAGEMENTSTUDIO_X32";
        public const string FACTNAME_X32 = "FACTNAME_X32";
        public const string FACTNAME_X16 = "FACTNAME_X16";
        public const string SHIELDGREEN_X32 = "SHIELDGREEN_X32";
        public const string SERVER_X32 = "SERVER_X32";
        public const string CUBESYELLOW_X16 = "CUBESYELLOW_X16";
        public const string TREE_LIST_X32 = "TREE_LIST_X32";
        public const string PROTECTBLUE_X32 = "PROTECTBLUE_X32";
        public const string PROTECTRED_X32 = "PROTECTRED_X32";
        public const string PROTECTGREEN_X32 = "PROTECTGREEN_X32";
        // ReSharper restore InconsistentNaming

        #endregion



    }
}

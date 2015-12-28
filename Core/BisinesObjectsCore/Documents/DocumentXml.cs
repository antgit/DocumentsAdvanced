using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects.Documents
{


    /// <summary>Xml данные документа</summary>
    /// <remarks>Объект в базе даных имеет уникальность по свойствам OwnId и GroupNo</remarks>
    public class DocumentXml : LinkedXmlData<Document>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentXml()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentXml;
        }
        /// <summary>
        /// Коллекция XML данных
        /// </summary>
        /// <param name="owner">Владелец</param>
        /// <returns></returns>
        /// <exception cref="SqlReturnException"></exception>
        /// <exception cref="DatabaseException"></exception>
        public static List<DocumentXml> GetCollection(Document owner)
        {
            List<DocumentXml> collection = new List<DocumentXml>();
            using (SqlConnection cnn = owner.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = owner.FindProcedure(GlobalMethodAlias.LoadXmlByOwnerId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = owner.Id;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DocumentXml item = new DocumentXml(){ Workarea = owner.Workarea, Owner = owner};
                                item.Load(reader);
                                collection.Add(item);
                            }

                        }
                        reader.Close();
                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(owner.Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((Int32)retval != 0)
                            throw new DatabaseException(owner.Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }

                }
            }
            return collection;
        }
    }
    /// <summary>Xml данные документа</summary>
    /// <remarks>Объект в базе даных имеет уникальность по свойствам OwnId и GroupNo</remarks>
    public sealed class DocumentDetailContractXml : LinkedXmlData<DocumentDetailContract>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailContractXml()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentDetailContractXml;
        }
    }

    /// <summary>Xml данные строки документа маркетинга</summary>
    /// <remarks>Объект в базе даных имеет уникальность по свойствам OwnId и GroupNo</remarks>
    public sealed class DocumentDetailMktgXml : LinkedXmlData<DocumentDetailMktg>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailMktgXml()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentDetailMktgXml;
        }
    }
    /// <summary>Xml данные строки документа бухгалтерии</summary>
    /// <remarks>Объект в базе даных имеет уникальность по свойствам OwnId и GroupNo</remarks>
    public sealed class DocumentDetailBookKeepXml : LinkedXmlData<DocumentDetailBookKeep>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailBookKeepXml()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentDetailBookKeepXml;
        }
    }

    /// <summary>Xml данные строки документа финансов</summary>
    /// <remarks>Объект в базе даных имеет уникальность по свойствам OwnId и GroupNo</remarks>
    public sealed class DocumentDetailFinanceXml : LinkedXmlData<DocumentDetailFinance>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailFinanceXml()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentDetailFinanceXml;
        }
    }
    /// <summary>Xml данные строки документа ценоообразования</summary>
    /// <remarks>Объект в базе даных имеет уникальность по свойствам OwnId и GroupNo</remarks>
    public sealed class DocumentDetailPriceXml : LinkedXmlData<DocumentDetailPrice>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailPriceXml()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentDetailPriceXml;
        }
    }
    /// <summary>Xml данные строки документа склада</summary>
    /// <remarks>Объект в базе даных имеет уникальность по свойствам OwnId и GroupNo</remarks>
    public sealed class DocumentDetailStoreXml : LinkedXmlData<DocumentDetailStore>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailStoreXml()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentDetailStoreXml;
        }
    }

    /// <summary>Xml данные строки документа торговли</summary>
    /// <remarks>Объект в базе даных имеет уникальность по свойствам OwnId и GroupNo</remarks>
    public sealed class DocumentDetailSaleXml : LinkedXmlData<DocumentDetailSale>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailSaleXml()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentDetailSaleXml;
        }
    }
    /// <summary>Xml данные строки документа услуг</summary>
    /// <remarks>Объект в базе даных имеет уникальность по свойствам OwnId и GroupNo</remarks>
    public sealed class DocumentDetailServiceXml : LinkedXmlData<DocumentDetailService>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailServiceXml()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentDetailServiceXml;
        }
    }

    /// <summary>Xml данные строки аналитики документа</summary>
    /// <remarks>Объект в базе даных имеет уникальность по свойствам OwnId и GroupNo</remarks>
    public sealed class DocumentAnaliticXml : LinkedXmlData<DocumentAnalitic>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentAnaliticXml()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentAnaliticXml;
        }
    }
    

}
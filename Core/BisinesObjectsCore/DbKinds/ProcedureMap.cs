using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// ������� �������� ��������
    /// </summary>
    [Serializable]
    public sealed class ProcedureMap : BaseCoreObject
    {
        private static List<ProcedureMap> _dbEntityMethodsCollection;
        /// <summary>
        /// ��������� �������� � ������� �������
        /// </summary>
        /// <param name="workarea">������� �������</param>
        /// <returns></returns>
        public static List<ProcedureMap> Collection( Workarea workarea)
        {
            if (_dbEntityMethodsCollection == null)
                _dbEntityMethodsCollection = new List<ProcedureMap>();
            else
                return _dbEntityMethodsCollection;

            return RefreshCollection(workarea);
        }

        private static List<ProcedureMap> RefreshCollection(Workarea workarea)
        {
            _dbEntityMethodsCollection = new List<ProcedureMap>();
            using (SqlConnection cnn = workarea.GetDatabaseConnection())
            {
                if (cnn == null) return _dbEntityMethodsCollection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Core].[ProcedureMapLoadAll]";
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ProcedureMap item = new ProcedureMap { Workarea = workarea };
                            item.Load(reader);
                            _dbEntityMethodsCollection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return _dbEntityMethodsCollection;
        }

        #region ��������
        // private Int16 _entityId;
        private int _subKindId;
        private string _name;
        private string _method;
        private string _procedure;
        private string _schema;

        /// <summary>�����������</summary>
        public ProcedureMap(): base()
        {
        }
        /// <summary>
        /// ������������� ����
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// ������������ ����: "������", "��������" ��� "����������������"
        /// </summary>
        public string TypeName
        {
            get
            {
                if (TypeId == 0)
                    return "������";
                return TypeId == 1 ? "��������" : "����������������";
            }
        }
        ///// <summary>������������� ����</summary>
        //public new Int16 EntityId
        //{
        //    get { return _entityId; }
        //    set
        //    {
        //        if (value == _entityId) return;
        //        OnPropertyChanging("EntityId");
        //        _entityId = value;
        //        OnPropertyChanged("EntityId");
        //    }
        //}

        /// <summary>������</summary>
        public int SubKindId
        {
            get { return _subKindId; }
            set
            {
                if (value == _subKindId) return;
                OnPropertyChanging(GlobalPropertyNames.SubKindId);
                _subKindId = value;
                OnPropertyChanged(GlobalPropertyNames.SubKindId);
            }
        }

        /// <summary>������������</summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                OnPropertyChanging(GlobalPropertyNames.Name);
                _name = value;
                OnPropertyChanged(GlobalPropertyNames.Name);
            }
        }

        /// <summary>������������ ������</summary>
        public string Method
        {
            get { return _method; }
            set
            {
                if (value == _method) return;
                OnPropertyChanging(GlobalPropertyNames.Method);
                _method = value;
                OnPropertyChanged(GlobalPropertyNames.Method);
            }
        }

        /// <summary>������������ �������� ���������</summary>
        public string Procedure
        {
            get { return _procedure; }
            set
            {
                if (value == _procedure) return;
                OnPropertyChanging(GlobalPropertyNames.Procedure);
                _procedure = value;
                OnPropertyChanged(GlobalPropertyNames.Procedure);
            }
        }

        /// <summary>����� ������</summary>
        public string Schema
        {
            get { return _schema; }
            set
            {
                if (value == _schema) return;
                OnPropertyChanging(GlobalPropertyNames.Schema);
                _schema = value;
                OnPropertyChanged(GlobalPropertyNames.Schema);
            }
        }
        #endregion

        #region ������������

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="writer">������ ������ XML ������</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_subKindId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SubKindId, XmlConvert.ToString(_subKindId));
            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, _name);
            if (!string.IsNullOrEmpty(_method))
                writer.WriteAttributeString(GlobalPropertyNames.Method, _method);
            if (!string.IsNullOrEmpty(_procedure))
                writer.WriteAttributeString(GlobalPropertyNames.Procedure, _procedure);
            if (!string.IsNullOrEmpty(_schema))
                writer.WriteAttributeString(GlobalPropertyNames.Schema, _schema);
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.SubKindId) != null)
                _subKindId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.SubKindId));
            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                _name = reader.GetAttribute(GlobalPropertyNames.Name);
            if (reader.GetAttribute(GlobalPropertyNames.Method) != null)
                _method = reader.GetAttribute(GlobalPropertyNames.Method);
            if (reader.GetAttribute(GlobalPropertyNames.Procedure) != null)
                _procedure = reader.GetAttribute(GlobalPropertyNames.Procedure);
            if (reader.GetAttribute(GlobalPropertyNames.Schema) != null)
                _schema = reader.GetAttribute(GlobalPropertyNames.Schema);
        }
        #endregion

        /// <summary>������ ������������ ��������� � ������ �����</summary>
        public string FullName
        {
            get{ return string.Format("[{0}].[{1}]", _schema, _procedure); }
        }

        /// <summary>���������</summary>
        /// <param name="reader">������ ������ ������</param>
        /// <param name="endInit">��������� ������������� �������</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                EntityId = reader.IsDBNull(9) ? (short)0 : reader.GetInt16(9);
                //_entityId = reader.IsDBNull(9) ? (short) 0 : reader.GetInt16(9);
                _subKindId = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                _name = reader.GetString(11);
                _method = reader.GetString(12);
                _schema = reader.GetString(13);
                _procedure = reader.GetString(14);
                TypeId = reader.GetInt32(15);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }

        protected override void Save(bool endSave=true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnSaving(e);
            if (e.Cancel)
                return;
            Validate();
            if (TypeId == 1)
            {
                if (Id == 0)
                    Create(Workarea.FindMethod("Core.DocumentTypeMethodInsert").FullName);
                else
                    Update(Workarea.FindMethod("Core.DocumentTypeMethodUpdate").FullName, true);
            }
            if (TypeId != 0) return;
            if (Id == 0)
                Create(Workarea.FindMethod("Core.EntityMethodInsert").FullName);
            else
                Update(Workarea.FindMethod("Core.EntityMethodUpdate").FullName, true);

            ProcedureMap map = Entity.Methods.FirstOrDefault(f => f.Id == Id);
            if (map!=null)
            {
                map = this;
                int idxEntity = Workarea.CollectionEntities.FindIndex(f => f.Id == EntityId);
                if(Workarea.CollectionEntities[idxEntity].Methods.Exists(f=>f.Id==Id))
                {
                    int idxMethod = Workarea.CollectionEntities[idxEntity].Methods.FindIndex(f => f.Id == Id);
                    Workarea.CollectionEntities[idxEntity].Methods[idxMethod] = this;
                }
                else
                {
                    Workarea.CollectionEntities[idxEntity].Methods.Add(this);
                }
            }
            

            if (endSave)
                OnSaved();
        }
        /// <summary>���������</summary>
        /// <param name="value">�������������</param>
        public override void Load(int value)
        {
            switch (TypeId)
            {
                case 1:
                    Load(value, Workarea.FindMethod("Core.DocumentTypeMethodLoad").FullName);
                    break;
                case 0:
                    Load(value, Workarea.FindMethod("Core.EntityMethodLoad").FullName);
                    break;
            }
        }

        /// <summary>�������� �� ���� ������</summary>
        /// <remarks>�������� ����������� ��� ����� ���� ��������. ��� ������������� �������� ����������� ����� 
        /// <see cref="BaseCoreObject.CanDeleteFromDataBase"/>.
        /// ����� ���������� �������� ��������� "[Core].[DocumentTypeMethodDelete]"
        /// </remarks>
        /// <param name="checkVersion">��������� �������� ������</param>
        public override void Delete(bool checkVersion = true)
        {
            switch (TypeId)
            {
                case 1:
                    Delete("[Core].[DocumentTypeMethodDelete]", checkVersion);
                    break;
                case 0:
                    Delete("Core.EntityMethodDelete", checkVersion);
                    break;
            }            
        }
        /// <summary>�������� ������������ ������� ��������� �����������</summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        /// <exception cref="ValidateException">������������ �� ����� ���� ������</exception>
        public override void Validate()
        {
            base.Validate();
            // TODO: �������������� �������� ��� ����������� ���� � ��� ������������ � ����
            //if (_entityId == 0)
            //    throw new ValidateException("��������� ��� ������� �� ����� ���� ����� 0");
            if (string.IsNullOrEmpty(_name))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_NAMEISEMPTY, 1049));
            if (string.IsNullOrEmpty(_method))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_METHODISEMPTY, 1049));
            if (string.IsNullOrEmpty(_procedure))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_PROCNAMEISEMPTY, 1049));
            if (string.IsNullOrEmpty(_schema))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_SHEMANAMEISEMPTY, 1049));
        }

        /// <summary>���������� �������� ���������� ��� �������� ��������</summary>
        /// <param name="sqlCmd">�������� ��������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ����������</param>
        /// <param name="validateVersion">��������� �� �������� ������</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt)
                                   {
                                       IsNullable = true,
                                       Value = (EntityId == 0 ? (object) DBNull.Value : EntityId)
                                   };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.KindId, SqlDbType.Int)
                      {
                          IsNullable = true,
                          Value = (_subKindId == 0 ? (object) DBNull.Value : _subKindId)
                      };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255)
                      {
                          IsNullable = false,
                          Value = _name
                      };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Method, SqlDbType.NVarChar, 255) {IsNullable = false, Value = _method};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Schema, SqlDbType.NVarChar, 128) {IsNullable = false, Value = _schema};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ProcedureName, SqlDbType.NVarChar, 128)
                      {
                          IsNullable = false,
                          Value = _procedure
                      };
            sqlCmd.Parameters.Add(prm);
        }
        /// <summary>������������� ������� � ���� ������</summary>
        /// <rereturns>������������� ������������ ������� <see cref="ProcedureMap.Name"/></rereturns>
        public override string ToString()
        {
            return _name;
        }
        /// <summary>
        /// ������������� �������� � ���� ��������������� ������
        /// </summary>
        /// <param name="mask">����� 
        /// <para> </para>
        /// <para> </para>
        /// <list type="table">
        /// <listheader>
        /// <term>�����</term>
        /// <description>��������</description></listheader>
        /// <item>
        /// <term>%name%</term>
        /// <description><see
        /// cref="P:BusinessObjects.ProcedureMap.Name">������������</see></description></item>
        /// <item>
        /// <term>%method%</term>
        /// <description><see
        /// cref="P:BusinessObjects.ProcedureMap.Method">�����</see></description></item>
        /// <item>
        /// <term>%procedure%</term>
        /// <description><see
        /// cref="P:BusinessObjects.ProcedureMap.Procedure">���������</see></description></item>
        /// <item>
        /// <term>%fulname%</term>
        /// <description><see cref="P:BusinessObjects.ProcedureMap.FullName">������
        /// ������������</see> </description></item>
        /// <item>
        /// <term>%schema%</term>
        /// <description><see
        /// cref="P:BusinessObjects.ProcedureMap.Schema">�����</see></description></item></list></param>
        /// <seealso
        /// cref="BusinessObjects.BaseCoreObject.ToString(System.String)">BaseCoreObject.ToString()</seealso>
        public override string ToString(string mask)
        {
            if (string.IsNullOrEmpty(mask))
            {
                return ToString();
            }
            string res = mask;
            
            res = res.Replace("%name%", Name);
            res = res.Replace("%method%", Method);
            res = res.Replace("%procedure%", Procedure);
            res = res.Replace("%schema%", Schema);
            res = res.Replace("%fullname%", FullName);
            res = res.Replace("%typename%", TypeName);
            return res;
        }
    }
}
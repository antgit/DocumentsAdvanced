using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BusinessObjects.Models
{
    /// <summary>
    /// ��������� ������ ������
    /// </summary>
    public interface IModelData
    {
        /// <summary>����������� ������������� ������</summary>
        string ModelId { get; set; }
        /// <summary>������������� ������������</summary>
        int ModelUserOwnerId { get; set; }
    }
    /// <summary>
    /// ������ 1 ������
    /// </summary>
    public class CoreModel : IModelData
    {
        /// <summary>
        /// �����������
        /// </summary>
        public CoreModel()
        {
            ModelId = Guid.NewGuid().ToString();
        }
        private Dictionary<string, string> _errors = new Dictionary<string, string>();

        /// <summary>
        /// ������
        /// </summary>
        [XmlIgnore]
        public Dictionary<string, string> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }
        /// <summary>
        /// �������� ������
        /// </summary>
        public void ClearErrors()
        {
            _errors.Clear();
        }
        /// <summary>��������� ��������� ���������� ������</summary>
        public bool ReloadOnEdit { get; set; }
        /// <summary>������������� ������</summary>
        public string ModelId { get; set; }
        /// <summary>������������� ������������</summary>
        public int ModelUserOwnerId { get; set; }
        /// <summary>�������������</summary>
        public int Id { get; set; }
        /// <summary>���������� �������������</summary>
        public Guid Guid { get; set; }
        /// <summary>������������� � ���� ���������</summary>
        public int DbSourceId { get; set; }
        /// <summary>������������� ���������</summary>
        public int DatabaseId { get; set; }
        /// <summary>������������� ���������</summary>
        public int StateId { get; set; }
        /// <summary>������������ ���������</summary>
        public string StateName { get; set; }
        /// <summary>���� �������� ��� ���������� ��������� ������</summary>
        public DateTime? DateModified { get; set; }
        /// <summary>������������� ���������� ����</summary>
        public short EntityId { get; set; }
        /// <summary>����� ������</summary>
        public int FlagsValue { get; set; }
        /// <summary>������ ��� ������</summary>
        public bool IsReadOnly { get; set; }
        /// <summary>���� "���������"</summary>
        public bool IsSystem { get; set; }
        /// <summary>���� "�������"</summary>
        public bool IsActive { get; set; }
        /// <summary>��� ������������ ���������� ��� ����������� ������</summary>
        public string UserName { get; set; }

        public virtual void GetData(ICoreObject value)
        {
            Id = value.Id;
            Guid = value.Guid;
            UserName = value.UserName;
            DbSourceId = value.DbSourceId;
            DatabaseId = value.DatabaseId;
            StateId = value.StateId;
            StateName = value.State.Name;
            DateModified = value.DateModified;
            EntityId = value.EntityId;
            FlagsValue = value.FlagsValue;
            IsReadOnly = value.IsReadOnly;
            IsSystem = value.IsSystem;
            IsActive = value.IsStateActive;
        }
    }
}
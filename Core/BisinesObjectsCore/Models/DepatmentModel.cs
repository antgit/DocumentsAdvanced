using System;

namespace BusinessObjects.Models
{
    /// <summary>
    /// Модель подразделения
    /// </summary>
    public class DepatmentModel : BaseModel<Depatment>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DepatmentModel()
        {

        }
        /// <summary>
        /// Получение данных из объекта
        /// </summary>
        /// <param name="value">Отдел</param>
        public override void GetData(Depatment value)
        {
            base.GetData(value);
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
            DepatmentHeadId = value.DepatmentHeadId;
            DepatmentSubHeadId = value.DepatmentSubHeadId;
            DepatmentHeadName = value.DepatmentHeadId != 0 ? value.DepatmentHead.Name : string.Empty;
            DepatmentSubHeadName = value.DepatmentSubHeadId != 0 ? value.DepatmentSubHead.Name : string.Empty;
            Phone = value.Phone;
        }

        /// <summary>Основная група</summary>
        public string DefaultGroup { get; set; }
        /// <summary>Идентификатор руководителя</summary>
        public int DepatmentHeadId { get; set; }    
        /// <summary>Идентификатор заместителя</summary>
        public int DepatmentSubHeadId { get; set; }
        /// <summary>Наименование руководителя</summary>
        public string DepatmentHeadName { get; set; }
        /// <summary>Наименование заместителя</summary>
        public string DepatmentSubHeadName { get; set; }
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId { get; set; }
        /// <summary>Наименование предприятия, которому принадлежит объект</summary>
        public string MyCompanyName { get; set; }
        /// <summary>Основной телефон</summary>
        public string Phone { get; set; }    
    }
}
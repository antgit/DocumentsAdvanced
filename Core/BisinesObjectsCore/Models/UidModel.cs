using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects.Security;

namespace BusinessObjects.Models
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class UidModel : BaseModel<Uid>
    {
        /// <summary>Конструктор</summary>
        public UidModel()
        {
        }
        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="value">Аналитика</param>
        public override void GetData(Uid value)
        {
            base.GetData(value);
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;

            if (!string.IsNullOrEmpty(value.Name))
            {
                Uid grp = value.Groups.FirstOrDefault(f => f.Name == Uid.GROUP_GROUPWEBADMIN);
                IsAdmin = (grp != null && grp.Id != 0);
                //value.Workarea.Access.IsUserExistsInGroup(value.Name, Uid.GROUP_GROUPWEBADMIN);
            }
            else
                IsAdmin = false;
            Email = value.Email;
            Password = value.Password;
            AllowChangePassword = value.AllowChangePassword;
            RecommendedDateChangePassword = value.RecommendedDateChangePassword;
            AutogenerateNextPassword = value.AutogenerateNextPassword;
            EmployerId = value.AgentId;
            
            if (value.MyCompanyId != 0)
                CompanyDefaultName = value.MyCompany.Name;

            TimePeriodId = value.TimePeriodId != 0 ? (int?) value.TimePeriodId : null;

            if (value.Agent != null)
            {
                NameFull = string.IsNullOrEmpty(value.Agent.NameFull) ? value.Agent.Name : value.Agent.NameFull;
                EmployerId = value.AgentId;
                WorkerName = value.Agent.Name;
                string filePath = "~/Images/" + value.Id + ".png";
                if (System.IO.File.Exists(filePath))
                {
                    Avatar = value.Id.ToString();
                }
                else
                {
                    Avatar = "noavatar.png";
                }

                if (value.MyCompanyId == 0)
                {
                    List<Agent> company =
                        (value.Agent as IChains<Agent>).DestinationList(value.Workarea.WorkresChainId());
                    if (company != null && company.Count > 0)
                    {
                        MyCompanyName = company[0].Name;
                        value.MyCompanyId = company[0].Id;
                    }
                }
            }
        }
        /// <summary>Идентификатор графика разрешенного входа в систему</summary>
        public int? TimePeriodId { get; set; }
        
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId { get; set; }
        /// <summary>Наименование предприятия, которому принадлежит объект</summary>
        public string MyCompanyName { get; set; }
        /// <summary>Основная група</summary>
        public string DefaultGroup { get; set; }

        /// <summary>Разрешить смену пароля пользователем</summary>
        public bool AllowChangePassword { get; set; }
        /// <summary>Рекомендуемая дата смены пароля</summary>
        public DateTime? RecommendedDateChangePassword { get; set; }
        /// <summary>Автоматически генерировать следующий пароль</summary>
        public bool AutogenerateNextPassword { get; set; }
        /// <summary>Адрес интернет почты учетной записи пользователя</summary>
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        
        public string Password { get; set; }
        public string Avatar { get; set; }
        /// <summary>
        /// Компания в которой зарегестрирован 
        /// </summary>
        public string CompanyDefaultName { get; set; }
        /// <summary>
        /// Компания в которой зарегестрирован сотрудник
        /// </summary>
        //public string CompanyName { get; set; }
        /// <summary>
        /// Идентификатор компании
        /// </summary>
        //public int CompanyId { get; set; }
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int? EmployerId { get; set; }
        /// <summary>
        /// Наименование сотрудника
        /// </summary>
        public string WorkerName { get; set; }

    }
}
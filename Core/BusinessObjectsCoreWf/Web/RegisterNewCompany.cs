//using System.Activities;
//using System.Collections;
//using System.Linq;
//using System.Text;
//using BusinessObjects.Security;

//namespace BusinessObjects.Workflows.Web
//{
//    /// <summary>
//    /// Регистрация новой компании...
//    /// </summary>
//    /// <remarks>При регистрации новой компании:
//    /// - создается новое предприятие
//    /// - создается новый пользователь
//    /// - создается новый сотрудник компании
//    /// - создается новый вид цены для компании
//    /// - выдаются разрешения для пользователя в разрезе компании.
//    /// </remarks>
//    public class ActivityRegisterNewCompany : CodeActivity
//    {
//        public ActivityRegisterNewCompany()
//            : base()
//        {
//            this.DisplayName = "Регистрация новой компании";
//        }
        
//        /// <summary>
//        /// Рабочая область
//        /// </summary>
//        [RequiredArgument]
//        public InArgument<Workarea> CurrentWorkarea { get; set; }

//        /// <summary>
//        /// Email администратора новой компании
//        /// </summary>
//        [RequiredArgument]
//        public InArgument<string> Email { get; set; }

//        /// <summary>
//        /// Логин пользователя-администратора новой компании
//        /// </summary>
//        [RequiredArgument]
//        public InArgument<string> UserLogin { get; set; }


//        /// <summary>
//        /// Имя пользователя-администратора новой компании
//        /// </summary>
//        [RequiredArgument]
//        public InArgument<string> UserName { get; set; }

//        /// <summary>
//        /// Наименование компании
//        /// </summary>
//        [RequiredArgument]
//        public InArgument<string> CompanyName { get; set; }

//        /// <summary>
//        /// Пароль для нового пользователя системы
//        /// </summary>
//        public OutArgument<string> Password { get; set; }

//        // If your activity returns a value, derive from CodeActivity<TResult>
//        // and return the value from the Execute method.
//        protected override void Execute(CodeActivityContext context)
//        {
//            string password = RandomPasswordGenerator.Generate();

//            Workarea wa = CurrentWorkarea.Get(context);
//            string email = context.GetValue(this.Email);
//            string companyName = context.GetValue(this.CompanyName);
//            string userLogin = context.GetValue(this.UserLogin);
//            string userName = context.GetValue(this.UserName);

//            Agent tmlMyCompany = wa.GetTemplates<Agent>().First(s => s.KindId == Agent.KINDID_MYCOMPANY);
//            Agent newMyCompany = wa.CreateNewObject(tmlMyCompany);
//            newMyCompany.Name = companyName;
//            newMyCompany.Save();
//            newMyCompany.MyCompanyId = newMyCompany.Id;
//            newMyCompany.Save();

//            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYCOMPANY);
//            h.ContentAdd(newMyCompany, true);

//            h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYDEPATMENTS);
//            h.ContentAdd(newMyCompany, true);

//            Agent tmlMyWorker = wa.GetTemplates<Agent>().First(s => s.KindId == Agent.KINDID_PEOPLE);
//            Agent newWorker = wa.CreateNewObject<Agent>(tmlMyWorker);
//            newWorker.Name = userName;
//            newWorker.MyCompanyId = newMyCompany.Id;
//            newWorker.Save();

//            h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYWORKERS);
//            h.ContentAdd(newWorker, true);

//            Chain<Agent> ch = new Chain<Agent>(newMyCompany);
//            ch.KindId = wa.WorkresChainId();
//            ch.RightId = newWorker.Id;
//            ch.StateId = State.STATEACTIVE;
//            ch.Save();

//            ch = new Chain<Agent>(newMyCompany);
//            ch.KindId = wa.TradersChainId();
//            ch.RightId = newWorker.Id;
//            ch.StateId = State.STATEACTIVE;
//            ch.Save();

//            #region Виды цен...
//            PriceName tmlPriceName = wa.GetTemplates<PriceName>().First(s => s.Code == "PRICENAME");
//            PriceName newPriceName = wa.CreateNewObject<PriceName>(tmlPriceName);
//            newPriceName.MyCompanyId = newMyCompany.Id;
//            newPriceName.FlagsValue = FlagValue.FLAGSYSTEM;
//            newPriceName.Save();

//            h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_PRICENAME_OUTPRICES);
//            h.ContentAdd(newPriceName, true);

//            tmlPriceName = wa.GetTemplates<PriceName>().First(s => s.Code == "PROVIDER");
//            newPriceName = wa.CreateNewObject<PriceName>(tmlPriceName);
//            newPriceName.MyCompanyId = newMyCompany.Id;
//            newPriceName.FlagsValue = FlagValue.FLAGSYSTEM;
//            newPriceName.Save();

//            h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_PRICENAME_INPRICES);
//            h.ContentAdd(newPriceName, true);


//            tmlPriceName = wa.GetTemplates<PriceName>().First(s => s.Code == "COMPETITOR");
//            newPriceName = wa.CreateNewObject<PriceName>(tmlPriceName);
//            newPriceName.MyCompanyId = newMyCompany.Id;
//            newPriceName.FlagsValue = FlagValue.FLAGSYSTEM;
//            newPriceName.Save();

//            h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_PRICENAME_COMPETITORPRICES);
//            h.ContentAdd(newPriceName, true); 
//            #endregion

//            Uid user = wa.CreateNewObject<Uid>("UID_USER");
//            user.Name = userLogin;
//            user.Code = userLogin;
//            user.AuthenticateKind = (int)AuthenticateKind.NoLogin;
//            user.Password = password;
//            user.Email = email;
//            user.AgentId = newWorker.Id;
//            user.MyCompanyId = newMyCompany.Id;
//            user.Save();

//            ChainAdvanced<Uid, Agent> userView = new ChainAdvanced<Uid, Agent>(user);
//            int userViewKind = wa.CollectionChainKinds.First(
//                    s =>
//                    s.FromEntityId == (int)WhellKnownDbEntity.Users && s.ToEntityId == (int)WhellKnownDbEntity.Agent &&
//                    s.Code == "SCOPEVIEWLIST").Id;
//            userView.KindId = userViewKind;
                
//            userView.RightId = newMyCompany.Id;
//            userView.StateId = State.STATEACTIVE;
//            userView.Save();

//            userView = new ChainAdvanced<Uid, Agent>(user);
//            userView.KindId = userViewKind;

//            userView.RightId = -1;
//            userView.StateId = State.STATEACTIVE;
//            userView.Save();


//            Uid grp = wa.GetCollection<Uid>().FirstOrDefault(s => s.Name == "Пользователи");
//            if (!wa.Access.IsUserExistsInGroup(user.Name, "Пользователи") && user.Name != "Пользователи")
//                user.IncludeInGroup(grp);

//            grp = wa.GetCollection<Uid>().FirstOrDefault(s => s.Name == Uid.GROUP_GROUPWEBUSER);
//            if (!wa.Access.IsUserExistsInGroup(user.Name, Uid.GROUP_GROUPWEBUSER) && user.Name != Uid.GROUP_GROUPWEBUSER)
//                user.IncludeInGroup(grp);

//            grp = wa.GetCollection<Uid>().FirstOrDefault(s => s.Name == Uid.GROUP_WEBMYBIZ);
//            if (!wa.Access.IsUserExistsInGroup(user.Name, Uid.GROUP_WEBMYBIZ) && user.Name != Uid.GROUP_WEBMYBIZ)
//                user.IncludeInGroup(grp);

//            grp = wa.GetCollection<Uid>().FirstOrDefault(s => s.Name == Uid.GROUP_GROUPTAX);
//            if (!wa.Access.IsUserExistsInGroup(user.Name, Uid.GROUP_GROUPTAX) && user.Name != Uid.GROUP_GROUPTAX)
//                user.IncludeInGroup(grp);

//            grp = wa.GetCollection<Uid>().FirstOrDefault(s => s.Name == Uid.GROUP_GROUPSERVICES);
//            if (!wa.Access.IsUserExistsInGroup(user.Name, Uid.GROUP_GROUPSERVICES) && user.Name != Uid.GROUP_GROUPSERVICES)
//                user.IncludeInGroup(grp);

//            grp = wa.GetCollection<Uid>().FirstOrDefault(s => s.Name == Uid.GROUP_GROUPSALES);
//            if (!wa.Access.IsUserExistsInGroup(user.Name, Uid.GROUP_GROUPSALES) && user.Name != Uid.GROUP_GROUPSALES)
//                user.IncludeInGroup(grp);

//            grp = wa.GetCollection<Uid>().FirstOrDefault(s => s.Name == Uid.GROUP_GROUPPRICES);
//            if (!wa.Access.IsUserExistsInGroup(user.Name, Uid.GROUP_GROUPPRICES) && user.Name != Uid.GROUP_GROUPPRICES)
//                user.IncludeInGroup(grp);

//            grp = wa.GetCollection<Uid>().FirstOrDefault(s => s.Name == Uid.GROUP_GROUPFINANCE);
//            if (!wa.Access.IsUserExistsInGroup(user.Name, Uid.GROUP_GROUPFINANCE) && user.Name != Uid.GROUP_GROUPFINANCE)
//                user.IncludeInGroup(grp);
            
//            context.SetValue(Password, password);

//        }
//    }
//}

using System.Collections.Generic;

namespace BusinessObjects
{
    /// <summary>
    /// Построитель виртуальных групп корреспондентов по алфавиту
    /// </summary>
    public class VirtualHierarchyAnaliticTradeGroup : IVirtualGroupBuilder<Analitic>
    {
        /// <summary>
        /// Идентификатор системного объекта для которого будут строится группы
        /// </summary>
        public int EntityId
        {
            get { return (int)WhellKnownDbEntity.Analitic; }
            set { }
        }
        /// <summary>
        /// Выполнить построение
        /// </summary>
        public void Build()
        {

        }

        public CustomViewList DefaultViewList { get; set; }

        private Hierarchy _hierarchy;
        /// <summary>
        /// Корневая иерархия, к которой привызан соответствующий построитель
        /// </summary>
        public Hierarchy Hierarchy
        {
            get { return _hierarchy; }
            set
            {
                _hierarchy = value;
                if (_hierarchy != null)
                {
                    SystemParameter prm = _hierarchy.Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DEFAULT_LISTVIEWANALITIC");
                    if (prm != null && prm.ReferenceId != 0)
                    {
                        DefaultViewList = _hierarchy.Workarea.Cashe.GetCasheData<CustomViewList>().Item(prm.ReferenceId);

                    }
                    else
                    {
                        DefaultViewList = _hierarchy.Workarea.Cashe.GetCasheData<CustomViewList>().ItemCode<CustomViewList>("DEFAULT_LISTVIEWANALITIC");
                    }
                }
            }
        }

        private List<IVirtualGroup<Analitic>> _root;
        /// <summary>
        /// Коллекция корневых виртуальных групп
        /// </summary>
        /// <returns></returns>
        public List<IVirtualGroup<Analitic>> Roots()
        {
            if (_root == null)
            {
                _root = new List<IVirtualGroup<Analitic>>();
                foreach (Analitic c in GetRoots(_hierarchy.Workarea))
                {
                    Hierarchy h = new Hierarchy { Workarea = _hierarchy.Workarea, IsVirtual = true, Name = c.ToString(), KindValue = 2, Code=c.Code };
                    VirtualGroup<Analitic> val = new VirtualGroup<Analitic>
                                                     {
                                                         ContentKind = 0,
                                                         CurrentViewList = DefaultViewList,
                                                         Owner = this,
                                                         Parent = Hierarchy,
                                                         Value = h
                                                     };
                    //h.VirtualBuildId = _hierarchy.VirtualBuildId;
                    val.ContentRequest += ValContentRequest;
                    if(h.Code.Length==2)
                    {
                        
                        string filter = string.Format("{0}____", h.Code);
                        List<Analitic> coll = h.Workarea.Empty<Analitic>().FindBy(useAndFilter: true, code: filter, kindId: Analitic.KINDID_TRADEGROUP, count: System.Data.SqlTypes.SqlInt32.MaxValue.Value);

                        foreach (Analitic c2 in coll)
                        {
                            Hierarchy h2 = new Hierarchy { Workarea = _hierarchy.Workarea, IsVirtual = true, Name = c2.ToString(), KindValue = 2, Code = c2.Code };
                            h2.Parent = h;
                            h2.KindValue = 2;
                            //h2.VirtualBuildId = _hierarchy.VirtualBuildId;
                            VirtualGroup<Analitic> val2 = new VirtualGroup<Analitic>
                            {
                                ContentKind = 0,
                                CurrentViewList = DefaultViewList,
                                Owner = this,
                                Parent = h,
                                Value = h2
                            };
                            val2.ContentRequest += ValContentRequest;
                            if (val.Children == null)
                                val.Children = new List<IVirtualGroup<Analitic>>();
                            val.Children.Add(val2);
                        }
                    }
                    _root.Add(val);
                }
            }
            return _root;
        }

        List<Analitic> GetRoots(Workarea wa)
        {
            string filter = "__";
            List<Analitic> coll = wa.Empty<Analitic>().FindBy(useAndFilter: true, code: filter, kindId: Analitic.KINDID_TRADEGROUP, count: System.Data.SqlTypes.SqlInt32.MaxValue.Value);
            //foreach (Analitic level1 in coll)
            //{
            //    filter = "______";
            //    List<Analitic> coll = wa.Empty<Analitic>().FindBy(useAndFilter: true, code: filter, kindId: Analitic.KINDID_TRADEGROUP, count: System.Data.SqlTypes.SqlInt32.MaxValue.Value);
            //}
            return coll;
        }
        void ValContentRequest(IVirtualGroup<Analitic> obj)
        {
            //01.010 - 6
            //01.010.010 - 100
            string filter = string.Format("{0}____", obj.Value.Code);
            
            List<Analitic> coll = obj.Owner.Hierarchy.Workarea.Empty<Analitic>().FindBy(useAndFilter: true, code: filter, kindId: Analitic.KINDID_TRADEGROUP, count: System.Data.SqlTypes.SqlInt32.MaxValue.Value);
            obj.SetContents(coll);
        }
    }
}
using System.Collections.Generic;

namespace BusinessObjects
{
    /// <summary>
    /// Построитель виртуальных групп товара по алфавиту
    /// </summary>
    public class VirtualHierarchyProductAlphabet: IVirtualGroupBuilder<Product>
    {
        const string Alphbet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"; 
        /// <summary>
        /// Идентификатор системного объекта для которого будут строится группы
        /// </summary>
        public int EntityId
        {
            get { return (int) WhellKnownDbEntity.Product; }
            set {}
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
                if(_hierarchy!=null)
                {
                    SystemParameter prm = _hierarchy.Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DEFAULT_LISTVIEWPRODUCT");
                    if (prm != null && prm.ReferenceId != 0)
                    {
                        DefaultViewList = _hierarchy.Workarea.Cashe.GetCasheData<CustomViewList>().Item(prm.ReferenceId);
                        
                    }
                    else
                    {
                        DefaultViewList = _hierarchy.Workarea.Cashe.GetCasheData<CustomViewList>().ItemCode<CustomViewList>("DEFAULT_LISTVIEWPRODUCT");
                    }
                }
            }
        }

        private List<IVirtualGroup<Product>> _root;
        /// <summary>
        /// Коллекция корневых виртуальных групп
        /// </summary>
        /// <returns></returns>
        public List<IVirtualGroup<Product>> Roots()
        {
            if (_root == null)
            {
                _root = new List<IVirtualGroup<Product>>();
                foreach (char c in Alphbet)
                {
                    Hierarchy h = new Hierarchy {Workarea = _hierarchy.Workarea, IsVirtual = true, Name = c.ToString(), KindValue = 2};
                    VirtualGroup<Product> val = new VirtualGroup<Product>
                                                    {
                                                        ContentKind = 0,
                                                        CurrentViewList = DefaultViewList,
                                                        Owner = this,
                                                        Parent = Hierarchy,
                                                        Value = h
                                                    };

                    val.ContentRequest += ValContentRequest;
                    _root.Add(val);
                }
            }
            return _root;
        }

        void ValContentRequest(IVirtualGroup<Product> obj)
        {
            string filter = string.Format("{0}%",obj.Value.Name);
            List<Product> coll = obj.Owner.Hierarchy.Workarea.Empty<Product>().FindBy(name: filter, count: System.Data.SqlTypes.SqlInt32.MaxValue.Value);
            //List<Product> coll = obj.Owner.Hierarchy.Workarea.GetCollectionByName<Product>(0, filter, System.Data.SqlTypes.SqlInt32.MaxValue.Value, null);
            obj.SetContents(coll);
        }
    }
}
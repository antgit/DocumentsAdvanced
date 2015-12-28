using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessObjects
{
    /// <summary>
    /// Построитель виртуальных иерархий товара по брендам
    /// </summary>
    public class VirtualHierarchyProductBrand : IVirtualGroupBuilder<Product>
    {
        /// <summary>
        /// Идентификатор системного объекта для которого будут строится группы
        /// </summary>
        public int EntityId
        {
            get { return (int)WhellKnownDbEntity.Product; }
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

        private List<Product> _allData;
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
                //
                Hierarchy analiticRoot = _hierarchy.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("BRAND");
                if (analiticRoot != null)
                {
                    List<Analitic> col = analiticRoot.GetTypeContents<Analitic>();
                    foreach (Analitic c in col)
                    {
                        Hierarchy h = new Hierarchy
                                          {
                                              Workarea = _hierarchy.Workarea,
                                              IsVirtual = true,
                                              Name = c.Name,
                                              KindValue = 2,
                                              Code = c.Id.ToString()
                                          };
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
            }
            return _root;
        }

        void ValContentRequest(IVirtualGroup<Product> obj)
        {
            if (_allData == null)
                _allData = obj.Owner.Hierarchy.Workarea.GetCollection<Product>();
            int id = Convert.ToInt32(obj.Value.Code);
            List<Product> coll = _allData.Where(s => s.TradeMarkId == id).ToList();
            obj.SetContents(coll);
        }
    }
}
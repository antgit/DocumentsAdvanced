using System;
using System.Linq;
using System.Collections.Generic;
using BusinessObjects.Documents;

namespace BusinessObjects
{
    /// <summary>
    /// Кеширование данных объектов
    /// </summary>
    /// <typeparam name="T">Тип объекта</typeparam>
    public class CasheData<T> where T : class, ICoreObject, new()
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public CasheData()
        {
        }
        private IWorkarea _workarea;
        /// <summary>
        /// Рабочая область
        /// </summary>
        public IWorkarea Workarea
        {
            get { return _workarea; }
            set { _workarea = value; }
        }
	
        /// <summary>
        /// Обновить все данные в коллекции из базы данных
        /// </summary>
        public void Refresh()
        {
            foreach (int key in Dictionary.Keys)
            {
                Refresh(key);
            }
        }
        /// <summary>
        /// Обновить данные о объекте по ключу
        /// </summary>
        /// <param name="key">Идентификатор объекта</param>
        public T Refresh(int key)
        {
            if (!Dictionary.ContainsKey(key))
            {
                T value = _workarea.GetObject<T>(key);
                Dictionary.Add(key, value);
            }
            else
            {
                Dictionary[key].Load(key);
            }
            return Dictionary[key];
        }
        /// <summary>
        /// Удалить объекты из кеша
        /// </summary>
        public void Remove()
        {
            Dictionary = new Dictionary<int, T>();
        }
        /// <summary>
        /// Удалить объект из кеша
        /// </summary>
        /// <param name="key">Идентификатор объекта</param>
        public void Remove(int key)
        {
            if (Dictionary.ContainsKey(key))
                Dictionary.Remove(key);
        }
        private Dictionary<int, T> _dictionary;
        internal Dictionary<int, T> Dictionary
        {
            get { return _dictionary ?? (_dictionary = new Dictionary<int, T>()); }
            set { _dictionary = value; }
        }
        /// <summary>
        /// Поиск объекта по идентификатору
        /// </summary>
        /// <remarks>Поиск производится в кешированой коллекции, если объект не найден - в базе данных.</remarks>
        /// <param name="key">Идентификатор объекта</param>
        /// <returns></returns>
        public T Item(int key)
        {
            //if (key==0)
            //{
            //    T value = _workarea.GetObject<T>(key);
            //    return value;
            //}
            if (!Dictionary.ContainsKey(key))
            {
                T value = _workarea.GetObject<T>(key);
                Dictionary.Add(key, value);
            }
            return Dictionary[key];
        }
        /// <summary>
        /// Проверка наличия объекта в кэше данных
        /// </summary>
        /// <param name="key">Идентификатор объекта</param>
        /// <returns></returns>
        public bool Exists(int key)
        {
            return Dictionary.ContainsKey(key);
        }
        /// <summary>
        /// Добавить объект
        /// </summary>
        /// <param name="item">Объект</param>
        public void Add(T item)
        {
            lock (Dictionary)
            {
                if (!Dictionary.ContainsKey(item.Id))
                    Dictionary.Add(item.Id, item);
                else
                {
                    Dictionary[item.Id] = item;
                }
            }
        }
        /// <summary>
        /// Добавить объект
        /// </summary>
        /// <param name="item">Объект</param>
        public void Update(T item)
        {
            lock (Dictionary)
            {
                if (Dictionary.ContainsKey(item.Id))
                    Dictionary[item.Id] = item;
            }
        }
        /// <summary>
        /// Поиск объекта по коду
        /// </summary>
        /// <remarks>Поиск производится в кешированной коллекции по коду, если объект не найден поиск осуществляется в базе данных.</remarks>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="value">Значение кода</param>
        /// <returns>Первый объект с указаным кодом или null</returns>
        public T ItemCode<T>(string value) where T : class, IBase, IFindBy<T>, new()
        {
            lock (this)
            {
                //var val = Dictionary.Values.ToList().ConvertAll<IBase>()
                var val = Dictionary.Values.ToList().Cast<IBase>().FirstOrDefault(s => !string.IsNullOrEmpty(s.Code) && s.Code.ToUpper() == value.ToUpper());
                //var val = Dictionary.Values.Cast<IBase>().FirstOrDefault(s => !string.IsNullOrEmpty(s.Code) && s.Code.ToUpper() == value.ToUpper());
                if (val != null)
                    return val as T;
                //foreach (var s in values.Where(s => !string.IsNullOrEmpty(s.Code) && s.Code.ToUpper() == value.ToUpper()))
                //{
                //    return s as T;
                //}
                List<T> coll = Workarea.Empty<T>().FindBy(code: value);
                return coll.FirstOrDefault(s => s.Code == value);

                //List<IBase> values = Dictionary.Values.Cast<IBase>().ToList();

                //foreach (var s in values.Where(s => !string.IsNullOrEmpty(s.Code) && s.Code.ToUpper() == value.ToUpper()))
                //{
                //    return s as T;
                //}
                //List<T> coll = Workarea.Empty<T>().FindBy(code: value);
                //return coll.FirstOrDefault(s => s.Code == value);
            }
            
            //return Workarea.GetCollection<T>().FirstOrDefault(s => s.Code == value);
        }
        //public T ItemCode<T>(string value) where T : class, IBase, new()
        //{
        //    foreach (var s in Dictionary.Values.Where(s => !string.IsNullOrEmpty(s.Code) && s.Code == value))
        //    {
        //        return s;
        //     }
        //     return Workarea.GetCollection<T>().FirstOrDefault(s => s.Code == value);
        //}
    }
    /// <summary>
    /// Кешироваение объектов Chains
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListChainCasheData<T> where T: class, ICoreObject
    {
        /// <summary>
        /// Словарь данных кешированных коллекций объектов
        /// </summary>
        /// <remarks>
        /// T1 (BigInteger) - составное значение двух идентификаторов объектов:
        /// например, идентификатор объекта аналитики и типа связи;
        /// T2 (List<T>) - коллекция словаря
        /// </remarks>
        public ListChainCasheData()
        {
            Data = new Dictionary<Int64, List<T>>();
        }
        public Dictionary<Int64, List<T>> Data { get; set; }
        public static Int64 CreateId(Int32 dbEntityKindValue, Int32 subKindValue)
        {
            byte[] aBytes = BitConverter.GetBytes(dbEntityKindValue);
            byte[] bBytes = BitConverter.GetBytes(subKindValue);
            byte[] dValue = new[] { bBytes[0], bBytes[1], bBytes[2], bBytes[3], aBytes[0], aBytes[1], aBytes[2], aBytes[3] };
            return BitConverter.ToInt64(dValue, 0);
        }

        public static Int32 ExtractValueKind(Int64 value)
        {
            return (Int32)(value & 0xffffffff);
        }

        public static Int32 ExtractValueId(Int64 value)
        {
            return (Int32)(value >> 32);
        }
        /// <summary>
        /// Обновить все данные
        /// </summary>
        public void RefreshAll()
        {
            Data = new Dictionary<Int64, List<T>>();
        }
        /// <summary>
        /// Проверка наличия коллекции по идентификатору источника и типу связи 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public bool Exists(int id, int kind)
        {
            Int64 value = CreateId(id, kind);
            return Data.ContainsKey(value);
        }
        /// <summary>
        /// Добавить коллекцию
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kind"></param>
        /// <param name="collection"></param>
        public void Add(int id, int kind, List<T> collection)
        {
            Int64 value = CreateId(id, kind);
            if (Data.ContainsKey(value))
            {
                Data[value] = collection;
            }
            else
            {
                Data.Add(value, collection);
            }

        }
        /// <summary>
        /// Добавить элемент
        /// </summary>
        /// <param name="id">Идентификатор источника</param>
        /// <param name="kind">Идентификатор типа связи</param>
        /// <param name="valueToAdd">Объект добавляемый в коллекцию</param>
        public void AddElement(int id, int kind, T valueToAdd)
        {
            Int64 value = CreateId(id, kind);
            if (Data.ContainsKey(value))
            {
                if(Data[value]!=null)
                {
                    if(!Data[value].Exists(f=>f.Id==valueToAdd.Id))
                    {
                        Data[value].Add(valueToAdd);
                    }
                }
            }

        }
        public List<T> Get(int id, int kind)
        {
            Int64 value = CreateId(id, kind);
            if (Data.ContainsKey(value))
            {
                return Data[value];
            }
            else
                return null;
        }

    }

    /// <summary>
    /// Кеширование данных о коллекциях в привязке к идентификатору объекта
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListCasheData<T> where T : class, ICoreObject
    {
        /// <summary>
        /// Словарь данных кешированных коллекций объектов
        /// </summary>
        public ListCasheData()
        {
            Data = new Dictionary<int, List<T>>();
        }
        public Dictionary<int, List<T>> Data { get; set; }
        
        /// <summary>
        /// Обновить все данные
        /// </summary>
        public void RefreshAll()
        {
            Data = new Dictionary<int, List<T>>();
        }

        public bool Exists(int id)
        {
            return Data.ContainsKey(id);
        }
        public void Add(int id, List<T> collection)
        {
            if (Data.ContainsKey(id))
            {
                Data[id] = collection;
            }
            else
            {
                Data.Add(id, collection);
            }

        }
        public List<T> Get(int id)
        {
            if (Data.ContainsKey(id))
            {
                return Data[id];
            }
            else
                return null;
        }

    }

    /// <summary>
    /// Кеширование данных о связанных документах
    /// </summary>
    internal class InternalCasheDataListDocChain
    {
        /// <summary>
        /// Словарь данных кешированных коллекций объектов
        /// </summary>
        public InternalCasheDataListDocChain()
        {
            Data = new Dictionary<int, List<DocChain>>();
        }
        public Dictionary<int, List<DocChain>> Data { get; set; }

        /// <summary>
        /// Обновить все данные
        /// </summary>
        public void RefreshAll()
        {
            Data = new Dictionary<int, List<DocChain>>();
        }

        public bool Exists(int id)
        {
            return Data.ContainsKey(id);
        }
        public void Add(int id, List<DocChain> collection)
        {
            if (Data.ContainsKey(id))
            {
                Data[id] = collection;
            }
            else
            {
                Data.Add(id, collection);
            }

        }
        public List<DocChain> Get(int id)
        {
            if (Data.ContainsKey(id))
            {
                return Data[id];
            }
            else
                return null;
        }

    }

    /// <summary>
    /// Кеширование данных о коллекциях в привязке к строковому ключу объекта
    /// </summary>
    /// 
    /// <typeparam name="T"></typeparam>
    public class ListCodeCasheData<T> where T : class, ICoreObject
    {
        /// <summary>
        /// Словарь данных кешированных коллекций объектов
        /// </summary>
        public ListCodeCasheData()
        {
            Data = new Dictionary<string, List<T>>();
            DateValues = new Dictionary<string, DateTime>();
        }
        /// <summary>
        /// Словать ключей и данных
        /// </summary>
        public Dictionary<string, List<T>> Data { get; set; }
        /// <summary>
        /// Словарь ключей и дат размещения
        /// </summary>
        public Dictionary<string, DateTime> DateValues { get; set; }   

        /// <summary>
        /// Обновить все данные
        /// </summary>
        public void RefreshAll()
        {
            Data = new Dictionary<string, List<T>>();
            DateValues = new Dictionary<string, DateTime>();
        }

        public bool Exists(string id)
        {
            return Data.ContainsKey(id);
        }
        public void Add(string id, List<T> collection)
        {
            if (Data.ContainsKey(id))
            {
                Data[id] = collection;
                DateValues[id] = DateTime.Now;
            }
            else
            {
                Data.Add(id, collection);
                DateValues[id] = DateTime.Now;
            }

        }
        public List<T> Get(string id)
        {
            if (Data.ContainsKey(id))
            {
                return Data[id];
            }
            else
                return null;
        }
    }
    
}

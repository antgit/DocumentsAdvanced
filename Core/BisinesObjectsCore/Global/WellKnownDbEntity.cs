namespace BusinessObjects
{
    /// <summary>Базовые сущности</summary>
    public enum WhellKnownDbEntity : short
    {
        /// <summary>Товар, соответствует значению 1</summary>
        Product = 1,
        /// <summary>Счет, соответствует значению 2</summary>
        Account = 2,
        /// <summary>Кореспондент, соответствует значению 3</summary>
        Agent = 3,
        /// <summary>Аналитика, соответствует значению 4</summary>
        Analitic = 4,
        /// <summary>Валюта, соответствует значению 5</summary>
        Currency = 5,
        /// <summary>Рецепт, соответствует значению 6</summary>
        Recipe = 6,
        /// <summary>Папка, соответствует значению 7</summary>
        Folder = 7,
        /// <summary>Движение в ячейке хранения, соответствует значению 8</summary>
        StorageCellTurn = 8,
        /// <summary>Вид цены, соответствует значению 9</summary>
        PriceName = 9,
        /// <summary>Единица измерения, соответствует значению 10</summary>
        Unit = 10,
        /// <summary>Владелец, соответствует значению 11</summary>
        Branche = 11,
        /// <summary>Элемент рецепта или состава, соответствует значению 12</summary>
        ProductRecipeItem = 12,
        /// <summary>Курс валюты, соответствует значению 13</summary>
        Rate = 13,
        /// <summary>Типы документов, соответствует значению 14</summary>
        EntityDocument = 14,
        /// <summary>Библиотека, соответствует значению 15</summary>
        Library = 15,
        /// <summary>Цена, соответствует значению 16</summary>
        Price = 16,
        /// <summary>Наименование факта, соответствует значению 17</summary>
        FactName = 17,
        /// <summary>Колонка факта, соответствует значению 18</summary>
        FactColumn = 18,
        /// <summary>Системный объект, соответствует значению 19</summary>
        DbEntity = 19,
        /// <summary>Документ, соответствует значению 20</summary>
        Document = 20,
        /// <summary>Список, соответствует значению 21</summary>
        CustomViewList = 21,
        /// <summary>Колонка списка, соответствует значению 22</summary>
        Column = 22,
        /// <summary>Хранилище файлов, соответствует значению 23</summary>
        FileData = 23,
        /// <summary>Расчетный счет, соответствует значению 24</summary>
        BankAccount = 24,
        /// <summary>Системный параметр, соответствует значению 25</summary>
        SystemParameter = 25,
        /// <summary>Пользователи, соответствует значению 26</summary>
        Users = 26,
        /// <summary>Разрешения, соответствует значению 27</summary>
        Acl = 27,
        /// <summary>Иерархии, соответствует значению 28</summary>
        Hierarchy = 28,
        /// <summary>Содержимое статической иерархии, соответствует значению 29</summary>
        HierarchyContent = 29,
        /// <summary>Партия товара, соответствует значению 30</summary>
        Series = 30,
        /// <summary>Тип контактной информации, соответствует значению 31</summary>
        Contact = 31,
        /// <summary>Производная идиница измерения, соответствует значению 32</summary>
        ProductUnit = 32,
        /// <summary>Страны, соответствует значению 33</summary>
        Country = 33,
        /// <summary>Группы свойств, соответствует значению 34</summary>
        PropertyGroup = 34,
        /// <summary>Наборы правил, соответствует значению 35</summary>
        Ruleset = 35,
        /// <summary>Xml данные, соответствует значению 36</summary>
        XmlStorage = 36,
        /// <summary>Регионы, соответствует значению 37</summary>
        Territory = 37,
        /// <summary>Города, соответствует значению 38</summary>
        Town = 38,
        /// <summary>Ячейка хранения товара, соответствует значению 39</summary>
        StorageCell = 39,
        /// <summary>Типы связей, соответствует значению 40</summary>
        Chain = 40,
        /// <summary>Объекты базы данных, соответствует значению 41</summary>
        DbObject = 41,
        /// <summary>Составляющая объекта базы данных, соответствует значению 42</summary>
        DbObjectChild = 42,
        /// <summary>Паспорт, соответствует значению 43</summary>
        Passport = 43,
        /// <summary>Водительское удостоверение, соответствует значению 44</summary>
        DrivingLicence = 44,
        /// <summary>Склад, соответствует значению 45</summary>
        Store = 45,
        /// <summary>Предприятие, соответствует значению 46</summary>
        Company = 46,
        /// <summary>Банк, соответствует значению 47</summary>
        Bank = 47,
        /// <summary>Пользовательское значение системного параметра, соответствует значению 48</summary>
        SystemParameterUser = 48,
        /// <summary>Основное содержимое библиотеки, соответствует значению 49</summary>
        LibraryContent = 49,
        /// <summary>Связь объекта с отчетами, соответствует значению 50</summary>
        ReportChain = 50,
        /// <summary>Значение флага, соответствует значению 51</summary>
        FlagValue = 51,
        /// <summary>Подпись документа, соответствует значению 52</summary>
        DocumentSign = 52,
        /// <summary>Налог документа, соответствует значению 53</summary>
        Taxe = 53,
        /// <summary>Графические ресурсы, соответствует значению 54</summary>
        ResourceImage = 54,
        /// <summary>Строковые ресурсы, соответствует значению 55</summary>
        ResourceString = 55,
        /// <summary>Состояния, соответствует значению 56</summary>
        State = 56,
        /// <summary>Физическое лицо, соответствует значению 57</summary>
        People = 57,
        /// <summary>Сотрудник, соответствует значению 58</summary>
        Employer = 58,
        /// <summary>Тип связей, соответствует значению 59</summary>
        ChainKind = 59,
        /// <summary>Значение факта, соответствует значению 60</summary>
        FactValue = 60,
        /// <summary>Общие разрешения, соответствует значению 61</summary>
        UserRightCommon = 61,
        /// <summary>Разрешения для элементов, соответствует значению 62</summary>
        UserRightElement = 62,
        /// <summary>Дата факта, соответствует значению 63</summary>
        FactDate = 63,
        /// <summary>Связи колонок факта и подтипа объекта, соответствует значению 64</summary>
        FactColumnEntityKind = 64,
        /// <summary>Протокол действий пользователя в документах, соответствует значению 65</summary>
        LogUserAction = 65,
        /// <summary>Адресс корреспондента, соответствует значению 66</summary>
        AgentAddress = 66,
        /// <summary>Событие, сообщение, соответствует значению 67</summary>
        Event = 67,
        /// <summary>Вид объекта для включения в иерархию, соответствует значению 68</summary>
        HierarchyContentType = 68,
        /// <summary>Вид объекта для типа связи, соответствует значению 69</summary>
        ChainKindContentType = 69,
        /// <summary>Наименование кода, соответствует значению 70</summary>
        CodeName = 70,
        /// <summary>Значение кода, соответствует значению 71</summary>
        CodeValue = 71,
        /// <summary>Процессы документа, соответствует значению 72</summary>
        DocumentWorkflow = 72,
        /// <summary>Автонумерация документа, соответствует значению 73</summary>
        Autonum = 73,
        /// <summary>Версии файловых данных, соответствует значению 74</summary>
        FileDataVersion = 74,
        /// <summary>Статья базы знаний, соответствует значению 75</summary>
        Knowledge = 75,
        /// <summary>Сообщения, соответствует значению 76</summary>
        Message = 76,
        /// <summary>Xml дополнения документа, соответствует значению 77</summary>
        DocumentXml = 77,
        /// <summary>Допустимый подтип для кодов, соответствует значению 78</summary>
        CodeNameEntityKind = 78,
        /// <summary>Примечание, соответствует значению 79</summary>
        Note=79,
        /// <summary>Допустимый подтип документов для кодов, соответствует значению 80</summary>
        CodeNameDocumentKind = 80,
        /// <summary>Каталог данных, соответствует значению 81</summary>
        DataCatalog = 81,
        /// <summary>Служба, соответствует значению 82</summary>
        WebService = 82,
        /// <summary>Ценовые колебания, соответствует значению 83</summary>
        PriceRegion = 83,
        /// <summary>Временные интервалы, соответствует значению 84</summary>
        DateRegion = 84,
        /// <summary>Временные интервалы, соответствует значению 93</summary>
        TimePeriod = 93,
        /// <summary>Задача</summary>
        Task = 96,
        /// <summary>Аккаунты пользователя</summary>
        UserAccount = 97,
        /// <summary>Xml данные строки документа договоров, соответствует значению 98</summary>
        DocumentDetailContractXml = 98,
        /// <summary>Xml данные строки документа маркетинга, соответствует значению 99</summary>
        DocumentDetailMktgXml = 99,
        /// <summary>Xml данные строки документа бухгалтерии, соответствует значению 100</summary>
        DocumentDetailBookKeepXml = 100,
        /// <summary>Xml данные строки документа финансов, соответствует значению 101</summary>
        DocumentDetailFinanceXml = 101,
        /// <summary>Xml данные строки документа ценообразования, соответствует значению 102</summary>
        DocumentDetailPriceXml = 102,
        /// <summary>Xml данные строки документа склада, соответствует значению 103</summary>
        DocumentDetailStoreXml = 103,
        /// <summary>Xml данные строки документа торговли, соответствует значению 104</summary>
        DocumentDetailSaleXml = 104,
        /// <summary>Xml данные строки документа услуг, соответствует значению 105</summary>
        DocumentDetailServiceXml = 105,
        /// <summary>Xml данные строки аналитики документа, соответствует значению 106</summary>
        DocumentAnaliticXml = 106,
        /// <summary>Календарь, соответствует значению 107</summary>
        Calendar = 107,
        /// <summary>Дополнительные (персональные) разрешения документа, соответствует значению 108</summary>
        DocumentSecure = 108,
        /// <summary>Подразделение/отдел, соответствует значению 109</summary>
        Depatment = 109,
        /// <summary>Строковые данные документа, соответствует значению 110</summary>
        DocumentStrinData = 110,
        /// <summary>Устройство, соответствует значению 111</summary>
        Device = 111,
        /// <summary>Участник маршрута, соответствует значению 112</summary>
        RouteMember = 112,
        /// <summary>Событие, протокол устройств</summary>
        RouteEvent = 113,
        /// <summary>Дополнительные корреспонденты документа</summary>
        DocumentContractor = 114,
        /// <summary>Списки координат для строк детализации</summary>
        DocumentDetailCoordinates = 115,
        /// <summary>Деталь оборудования, соответствует значению 116</summary>
        EquipmentDetail = 116,
        /// <summary>Узел оборудования, соответствует значению 117</summary>
        EquipmentNode = 117,
        /// <summary>Оборудование, соответствует значению 118</summary>
        Equipment = 118,
        /// <summary>Автомобиль, соответствует значению 119</summary>
        Auto = 119
    }
}

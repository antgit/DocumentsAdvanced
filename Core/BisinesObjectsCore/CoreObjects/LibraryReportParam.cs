using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BusinessObjects
{
    /// <summary>
    /// Параметры для построения отчета службы ReportingService, "Табличных отчетов"
    /// </summary>
    [Serializable]
    public sealed class LibraryReportParams
    {
        public LibraryReportParams()
        {
            Params = new List<LibraryReportParam>();
        }
        /// <summary>
        /// Строка после 
        /// </summary>
        /// <remarks>Например, в URL отчета задано:
        /// http://servername/reportserver?/SampleReports/Employee Sales Summary,
        /// в значении свойства может содержаться "&rs:Command=Render"
        /// </remarks>
        public string Befor { get; set; }
        /// <summary>
        /// Строка "До"
        /// </summary>
        /// <remarks>Например, в URL отчета задано:
        /// http://servername/reportserver?/SampleReports/Employee Sales Summary,
        /// в значении свойства может содержаться "&rs:format=HTML4.0"
        /// </remarks>
        public string After { get; set; }
        /// <summary>
        /// Коллекция параметров
        /// </summary>
        public List<LibraryReportParam> Params { get; set; }

        public static LibraryReportParams GetLibraryParams(Library value)
        {
            LibraryReportParams libParams;
            if (string.IsNullOrEmpty(value.Params))
                libParams = new LibraryReportParams();
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LibraryReportParams));
                TextReader reader = new StringReader(value.Params);
                libParams = (LibraryReportParams)serializer.Deserialize(reader);
                reader.Close();
            }
            return libParams;
        }
    }
    /// <summary>
    /// Класс параметра билиотек для отчетов ReportingService, "Табличных отчетов"
    /// </summary>
    [Serializable]
    public sealed class LibraryReportParam
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public LibraryReportParam()
        {
            
        }

        [NonSerialized]
        private Workarea _Workarea;
        /// <summary>Рабочая область</summary>
        [XmlIgnore]
        public Workarea Workarea
        {
            get { return _Workarea; }
            set
            {
                if (_Workarea == value) return;
                _Workarea = value;
            }
        }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Наименование параметра ReportingService
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// Разрешить значение Null
        /// </summary>
        public bool AllowNull { get; set; }
        /// <summary>
        /// Строковое значение типа
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        public string Default { get; set; }
        /// <summary>
        /// Текущее значение
        /// </summary>
        public string CurrentValue { get; set; }
        /// <summary>
        /// Тип редактора
        /// </summary>
        public string TypeEditor { get; set; }

    }
}
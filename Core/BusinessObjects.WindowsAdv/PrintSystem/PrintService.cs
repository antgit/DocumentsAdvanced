using System.IO;
using System.Linq;
using System.Text;
using Stimulsoft.Base.Services;
using Stimulsoft.Report;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Viewer;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Настройка системы печати
    /// </summary>
    public sealed class PrintService
    {
        static PrintService()
        {
            
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        public PrintService()
        {
            
        }
        /// <summary>
        /// Рабочая область
        /// </summary>
        public static Workarea Workarea { get; set; }
        /// <summary>
        /// Выполненая ли инициализация системы печати
        /// </summary>
        public static bool IsInit { get; set; }
        /// <summary>
        /// Инициализация системы печати
        /// </summary>
        /// <remarks>Настройки системы печати сохраняются в системном параметре "PRINTCONFIG"</remarks>
        public static void InitConfig()
        {
            /*
              StreamReader reader = new StreamReader(pair.Value, Encoding.UTF8);
                    string xml = reader.ReadToEnd();
             
             byte[] byteArray = Encoding.UTF8.GetBytes(xmlKeyValue.Value);
                MemoryStream stream = new MemoryStream( byteArray );
             */
            SystemParameter prm = Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("PRINTCONFIG");
            if(string.IsNullOrEmpty(prm.ValueString))
            {
                StiConfig.Load();
                StiServiceContainer services = StiConfig.Services.GetServices(typeof(StiExportService));

                foreach (StiService service in services)
                {
                    //if (service is StiExcel2007ExportService) service.ServiceEnabled = false;
                    //if (service is StiExcel2007ExportService) service.ServiceEnabled = false;
                    if (service is StiBmpExportService) service.ServiceEnabled = false;
                    if (service is StiCsvExportService) service.ServiceEnabled = false;
                    if (service is StiEmfExportService) service.ServiceEnabled = false;
                    if (service is StiGifExportService) service.ServiceEnabled = false;
                    //if (service is StiHtmlExportService) service.ServiceEnabled = false;
                    if (service is StiJpegExportService) service.ServiceEnabled = false;
                    //if (service is StiPdfExportService) service.ServiceEnabled = false;
                    if (service is StiRtfExportService) service.ServiceEnabled = true;
                    if (service is StiTiffExportService) service.ServiceEnabled = false;
                    if (service is StiTxtExportService) service.ServiceEnabled = false;
                    if (service is StiXmlExportService) service.ServiceEnabled = false;
                    if (service is StiOdsExportService) service.ServiceEnabled = false;
                    if (service is StiOdtExportService) service.ServiceEnabled = false;
                    if (service is StiDbfExportService) service.ServiceEnabled = false;
                    if (service is StiDifExportService) service.ServiceEnabled = false;
                    if (service is StiSylkExportService) service.ServiceEnabled = false;
                    if (service is StiPngExportService) service.ServiceEnabled = true;
                    if (service is StiPcxExportService) service.ServiceEnabled = false;
                }

                StiViewerConfigService config = StiConfig.Services.GetService(typeof(StiViewerConfigService)) as StiViewerConfigService;
                //Turn off all buttons of changes of the rendered report
                config.PageNewEnabled = false;
                config.PageDeleteEnabled = false;
                config.PageDesignEnabled = false;
                config.PageSizeEnabled = true;
                config.OpenEnabled = false;
                StiOptions.Configuration.DirectoryLocalization = "ru";
                //Stimulsoft.Report.StiSelectGuiHelper.IsRibbonGui = true;


                MemoryStream stream = new MemoryStream();
                StiConfig.Save(stream);
                System.Text.Encoding enc = System.Text.Encoding.UTF8;
                string myString = enc.GetString(stream.ToArray());
                //stream.Seek(0, System.IO.SeekOrigin.Begin);
                //StreamReader reader = new StreamReader(stream);
                //StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                prm.ValueString = myString;
                prm.Save();
                //StiConfig.Load();
            }
            else
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(prm.ValueString);
                MemoryStream stream = new MemoryStream(byteArray);
                StiConfig.Load(stream);
                
                StiOptions.Configuration.DirectoryLocalization = "ru";
            }
            IsInit = true;
        }

        /// <summary>
        /// Идентификатор вида связи для печатных форм
        /// </summary>
        public static int PrintFormChainId
        {
            get { return Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.PRINTFORM && s.FromEntityId == (int)WhellKnownDbEntity.Library).Id; }
        }

    }
}

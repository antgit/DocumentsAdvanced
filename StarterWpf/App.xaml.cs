using System;
using System.Configuration;
using System.Threading;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

namespace StarterWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App 
    {
        private AplicationLoader loader;
        /// <summary>
        /// BackgroundWorker для загрузки обновлений выбранного приложения
        /// </summary>
        BackgroundWorker backgroundUpdate = new BackgroundWorker();

        /// <summary>
        /// Запуск потока для загрузки обновлений выбранного приложения
        /// </summary>
        /// <param name="strApp">Приложение</param>
        public void AppSelected(string strApp)
        {
            if (loader != null)
            {
                loader.ApplicationCode = strApp;
                backgroundUpdate.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Действия после загрузки обновлений выбранного проложения
        /// </summary>
        void backgroundUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Shutdown();
        }

        /// <summary>
        /// Загрузка обновлений выбранного приложения
        /// </summary>
        void backgroundUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            loader.LoadApplication();
            ((BackgroundWorker)sender).ReportProgress(0, "Обновление завершено, запускаем программу...");
            System.Diagnostics.Process.Start(loader.StartExe);
            Thread.Sleep(2000);
        }

        protected override void OnStartup(StartupEventArgs ee)
        {
            base.OnStartup(ee);
            WaitWindow frm = new WaitWindow();
            frm.Show();
        
            //((WaitWindow)Current.MainWindow).imgLogo.Source = new BitmapImage(new Uri("Resources/imageMain.png",UriKind.Relative));

            Current.MainWindow.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Escape)
                {
                    //Thread.CurrentThread.Abort(); //Генерирует ThreadAbortException.
                    Shutdown(); //Не генерирует исключений
                }
            };
            loader = new AplicationLoader();

            BackgroundWorker backgroundConnect = new BackgroundWorker();

            loader.CurrentAction += delegate(string obj)
            {
                frm.Dispatcher.Invoke(new Action(() => { frm.lbMessage.Text = obj;}));
            };

            backgroundUpdate.DoWork +=backgroundUpdate_DoWork;
            backgroundUpdate.RunWorkerCompleted += backgroundUpdate_RunWorkerCompleted;
            backgroundUpdate.ProgressChanged += backgroundConnect_ProgressChanged;
            backgroundUpdate.WorkerReportsProgress = true;

            backgroundConnect.DoWork += backgroundConnect_DoWork;
            backgroundConnect.ProgressChanged += backgroundConnect_ProgressChanged;
            backgroundConnect.WorkerReportsProgress = true;
            backgroundConnect.RunWorkerCompleted += backgroundConnect_RunWorkerCompleted;

            backgroundConnect.RunWorkerAsync(loader);
        }

        /// <summary>
        /// Отображение на главной форме списка возможных приложений для запуска 
        /// </summary>
        void backgroundConnect_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is string[])
            {
                //Вариант 1:
                //Отображать список возможных для запуска приложений если их в списке > 1
                /*
                if (((string[])e.Result).Length == 1)
                    AppSelected(((string[])e.Result)[0]);
                else
                    ((WaitWindow)Current.MainWindow).FillListOfApplications((string[])e.Result);
                */

                //Вариант 2:
                //Всегда отображать список возможных для запуска приложений, независимо от их количества в списке
                ((WaitWindow)Current.MainWindow).FillListOfApplications((string[])e.Result);
            }
            /*else
            {
                //((WaitWindow)Current.MainWindow).Close();
            }*/
        }

        /// <summary>
        /// Обновление строки состояния на главной форме
        /// </summary>
        void backgroundConnect_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is string)
                ((WaitWindow)Current.MainWindow).lbMessage.Text = (string)e.UserState;
        }

        /// <summary>
        /// Проверка возможности подключения к серверам и формирование списка возможных для запуска приложений
        /// </summary>
        void backgroundConnect_DoWork(object sender, DoWorkEventArgs e)
        {
            loader = (AplicationLoader)e.Argument;
            BackgroundWorker bck = (BackgroundWorker)sender;

            loader.ConnectionString = null;
            int i = 1;
            bool isConnectionStringNotNull = false;
            do
            {
                try
                {
                    loader.ConnectionString = (string)StarterWpf.Properties.Settings.Default["Server" + i];

                    if (loader.ConnectionString != null)
                        isConnectionStringNotNull = true;
                        
                    bck.ReportProgress(0, String.Format("Подключаемся к серверу {0}...", i));
                }
                catch (SettingsPropertyNotFoundException)
                {}
            } while ((loader.ConnectionString == null || !loader.CanConnect()) && i++ < 100);

            if (isConnectionStringNotNull == false)
            {
                bck.ReportProgress(0, "В настройках программы не указан сервер для подключения!");
                return;
            }
            if (i>=100)
            {
                bck.ReportProgress(0, "Не удалось подключиться ни к одному серверу!");
                return;
            }
            
            //string[] arrApp = new[] { "Документы 2010"/*, "Application2","Application3" */};
            //e.Result = arrApp;

            e.Result = loader.GetAppNames();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace StarterWpf
{
    class AplicationLoader
    {
        public void SendInfo(string value)
        {
            CurrentAction.Invoke(value);
        }
        public event Action<string> CurrentAction; 
        /// <summary>
        /// Строка соединения с сервером
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Код приложения, которое необходимо найти
        /// </summary>
        public string ApplicationCode { get; set; } // "Документы 2010"
        /// <summary>
        /// Проверка возможности соединения с сервером
        /// </summary>
        /// <returns></returns>
        public bool CanConnect()
        {
            CurrentAction.Invoke("Проверка возможности соединения с сервером...");
            SqlConnection cnn = new SqlConnection {ConnectionString = ConnectionString};
            try
            {
                cnn.Open();
                cnn.Close();
            }
            catch (Exception)
            {
                CurrentAction.Invoke("Проверка возможности соединения с сервером завершена, нет возможности подключиться к серверу...");
                return false;
            }
            CurrentAction.Invoke("Проверка возможности соединения с сервером завершена, начинаем проверять обновления...");
            return true;
        }
        /// <summary>Имя файла для запуска</summary>
        public string StartExe { get; private set; }
        private SqlConnection cnn;
        public void LoadApplication()
        {
            CurrentAction.Invoke("Загрузка приложений...");
            cnn = new SqlConnection(ConnectionString);
            cnn.Open();
            AppRoot root = FindAppRoot();

            string myDocsFolder =  Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            CurrentAction.Invoke("Настройка главных каталогов...");
            string currentPath = Path.Combine(myDocsFolder, root.Name);
            if(!Directory.Exists(currentPath))
                Directory.CreateDirectory(currentPath);

            
            List<AppicationData> coll = AppicationData.Load(this, cnn, root.HierarchyId);
            AppicationData mainexe = coll.Find(s => s.Id == root.Id);
            CurrentAction.Invoke("Поиск главных приложений...");
            string appRootPath = Path.Combine(currentPath, mainexe.AssemblyVersion);
            if (!Directory.Exists(appRootPath))
                Directory.CreateDirectory(appRootPath);

            string fuleInfoPath = Path.Combine(appRootPath, "files.xml");
            if (File.Exists(fuleInfoPath))
                _files = AplicationFile.Load(fuleInfoPath);
            if (_files == null)
                _files = new List<AplicationFile>();

            StartExe = Path.Combine(appRootPath, mainexe.FileName);
            int maxLevel = coll.Max(f => f.Level)+1;
            for (int i = 0; i < maxLevel; i++)
            {
                
                foreach (AppicationData lib in coll.Where(f => f.Level == i))
                {
                    if(i==0)
                        WriteLibData(appRootPath, lib);
                    else
                    {
                        string currentFolder = Path.Combine(appRootPath, lib.Folder);
                        if (!Directory.Exists(currentFolder))
                            Directory.CreateDirectory(currentFolder);
                        WriteLibData(currentFolder, lib);
                    }
                }    
            }
            AplicationFile.Save(_files, fuleInfoPath);
            //CreateShotcut();
        }

        private void WriteLibData(string appRootPath, AppicationData lib)
        {
            DateTime val = DateTime.Now;
            if (lib.DateModified != null)
                val = lib.DateModified.Value;
            string assFile = Path.Combine(appRootPath, lib.FileName);
            CurrentAction.Invoke(string.Format("Проверка файла {0}...", lib.FileName));
            if (!File.Exists(assFile))
            {
                CurrentAction.Invoke(string.Format("Создание файла {0}...", lib.FileName));
                using (FileStream stream = File.Create(assFile, lib.AssemblyDll.Length))
                {
                    stream.Write(lib.AssemblyDll, 0, lib.AssemblyDll.Length);
                    stream.Close();
                    stream.Dispose();
                }
            }
            else
            {
                if (!_files.Exists(f => f.FileName == lib.FileName && f.Date == val && lib.Folder== f.Folder))
                {
                    CurrentAction.Invoke(string.Format("Обновление файла {0}...", lib.FileName));
                    using (FileStream stream = File.Create(assFile, lib.AssemblyDll.Length))
                    {
                        stream.Write(lib.AssemblyDll, 0, lib.AssemblyDll.Length);
                        stream.Close();
                        stream.Dispose();
                    }   
                }
            }

            if (_files.Exists(f => f.FileName == lib.FileName && lib.Folder == f.Folder))
            {

                _files.First(f => f.FileName == lib.FileName && lib.Folder == f.Folder).Date = val;
            }
            else
            {
                _files.Add(new AplicationFile {FileName = lib.FileName, Date = val, Folder = lib.Folder});
            }


        }

        private void CreateShotcut()
        {
            string currentLine = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Option Explicit ");
            builder.AppendLine("Dim objShell, objDesktop, objLink");
            builder.AppendLine("Dim strAppPath, strWorkDir, strIconPath");
            /*
' CreateShortCut.vbs - Create a Desktop Shortcut.
' VBScript to create .lnk file

' ----------------------------------------------------------' 
Option Explicit
Dim objShell, objDesktop, objLink
Dim strAppPath, strWorkDir, strIconPath

' --------------------------------------------------
' Here are the variables that to change if you are making a 'real' script

strWorkDir ="C:\windows"
strAppPath = "%SystemRoot%\notepad.exe"
strIconPath = "%SystemRoot%\system32\SHELL32.dll,5"

Set objShell = CreateObject("WScript.Shell")
objDesktop = objShell.SpecialFolders("Desktop")
Set objLink = objShell.CreateShortcut(objDesktop & "\WordProcess.lnk")

' ---------------------------------------------------
' Section which adds the shortcut's key properties

objLink.Description = "WordProcess"
objLink.HotKey = "CTRL+SHIFT+X"
objLink.IconLocation = strIconPath 
objLink.TargetPath = strAppPath
objLink.WindowStyle = 3
objLink.WorkingDirectory = strWorkDir
objLink.Save

WScript.Quit

' End of creating a desktop shortcut

             */
        }

        AppRoot FindAppRoot()
        {
            List<AppRoot> coll = AppRoot.Load(this, cnn);
            return coll.FirstOrDefault(s => s.Name == ApplicationCode);
        }

        /// <summary>
        /// Получение списка возможных для запуска приложений
        /// </summary>
        /// <returns></returns>
        public string[] GetAppNames()
        {
            cnn = new SqlConnection(ConnectionString);
            cnn.Open();
            List<AppRoot> coll = AppRoot.Load(this, cnn);

            List<string> apps = new List<string>();
            foreach(AppRoot app in coll)
                apps.Add(app.Name);
            return apps.ToArray();
        }

        // Информация о ранее установленных файлах
        private List<AplicationFile> _files;
        
    }
}

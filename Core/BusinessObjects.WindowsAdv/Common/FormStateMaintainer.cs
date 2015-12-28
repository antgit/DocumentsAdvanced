using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.IO.IsolatedStorage;
namespace BusinessObjects.Windows
{
    /// <summary>
    /// Сохранение размера, позиционирование формы.
    /// </summary>
    /// <remarks>Создайте экземпляр данного класса в контрукторе формы. 
    /// Данный клас дает возможность сохранять и востанавливать состояние, размер, расположение
    /// </remarks>
    public class FormStateMaintainer
    {    
        private readonly Form _formToRemember;    
        private string windowStateFileName = @"undefined.xml";    
        /// <summary>    
        /// The location of the form    
        /// </summary>    
        public Point Location { get; set; }    
        /// <summary>    
        /// Размеры формы
        /// </summary>    
        public Size Size  { get; set; }    
        /// <summary>    
        /// Состояние формы
        /// </summary>    
        public FormWindowState State { get; set; }

        [System.Runtime.Serialization.OptionalField]
        private int _splitterDistance;
        
        public int SplitterDistance 
        { 
            get{return _splitterDistance;} 
            set{_splitterDistance = value;} 
        }
        /// <summary>    
        /// Инициализирует экземпляр класса FormStateMaintainer.    
        /// </summary>    
        /// <param name="form">    
        /// Форма, для которой необходимо сохранять настройки.    
        /// </param>    
        /// <param name="keyName">Ключ</param>
        public FormStateMaintainer(Form form, string keyName): this()
        {
            _formToRemember = form;
            windowStateFileName = string.Format("{0}{1}.xml", form.Name, keyName); 
            _formToRemember.Load += OnLoad; 
            _formToRemember.FormClosed += OnFormClosed;
        } 
        /// <summary>    
        /// Конструктор по умолчанию должен существовать 
        /// для сериализации.
        /// </summary>    
        public FormStateMaintainer()    
        {       
            Location = new Point(0, 0);     
            Size = new Size(300, 300);       
            State = FormWindowState.Normal;    
        }    
        void FormToRememberLocationChanged(object sender, EventArgs e)   
        {       
            RememberParameters();    
        }
        void FormToRememberSizeChanged(object sender, EventArgs e)
        {
            RememberParameters();
        }
        void RememberParameters()
        {
            if (_formToRemember.WindowState == FormWindowState.Normal)
            {
                Location = _formToRemember.DesktopBounds.Location;
                Size = _formToRemember.DesktopBounds.Size;
            }
            if (_formToRemember.WindowState != FormWindowState.Minimized)
            { 
                State = _formToRemember.WindowState; 
            }
        }

        static IsolatedStorageFile GetMyIsolatedStorageFile()
        {
            return IsolatedStorageFile.GetUserStoreForDomain();
        }
        private void OnLoad(object sender, EventArgs e)
        {
            _formToRemember.Load -= OnLoad;
            // create X,Y defaults from the form itself, in case the xml is missing        
            int X = _formToRemember.DesktopBounds.X;
            int y = _formToRemember.DesktopBounds.Y;
            IsolatedStorageFile isoStore = GetMyIsolatedStorageFile();
            string[] foundFilenames = isoStore.GetFileNames(windowStateFileName);
            System.Diagnostics.Debug.Assert(foundFilenames.Length <= 1);
            if (foundFilenames.Length == 1)
            {
                using (IsolatedStorageFileStream strm = new IsolatedStorageFileStream(windowStateFileName,
                    FileMode.Open, FileAccess.Read, FileShare.Read, isoStore))
                {
                    // Load the state from xml                
                    using (TextReader tr = new StreamReader(strm))
                    {
                        System.Xml.Serialization.XmlSerializer x =
                            new System.Xml.Serialization.XmlSerializer(GetType());
                        FormStateMaintainer loadedState = (FormStateMaintainer)x.Deserialize(tr);
                        Location = loadedState.Location;
                        if (loadedState.Size.Height > _formToRemember.MinimumSize.Height | loadedState.Size.Width > _formToRemember.MinimumSize.Width)
                            Size = loadedState.Size;
                        else
                            Size = _formToRemember.MinimumSize;
                        State = loadedState.State;
                        X = Location.X;
                        y = Location.Y;
                        SplitterDistance = loadedState.SplitterDistance;
                    }
                }
            }
            // In case of multi screen desktops, check if we got the screen the form was on when closed.        
            Rectangle boundsNow = Screen.GetWorkingArea(new Point(X, y));
            if (X > boundsNow.X + boundsNow.Width)
            {
                // screen to the right is missing            
                X = boundsNow.X + boundsNow.Width - Size.Width;
            }
            else if (X + Size.Width < boundsNow.X)
            {
                // screen to the left is missing            
                X = boundsNow.X;
            }
            if (y > boundsNow.Y + boundsNow.Height)
            {
                y = boundsNow.Y + boundsNow.Height - Size.Height;
            }
            else if (y + Size.Height < boundsNow.Y)
            {
                y = boundsNow.Y;
            }
            if (_formToRemember.Controls.ContainsKey("splitContainer"))
            {
                (_formToRemember.Controls["splitContainer"] as SplitContainer).SplitterDistance = this.SplitterDistance;
            }
            Location = new Point(X, y);
            _formToRemember.DesktopBounds = new Rectangle(Location, Size);
            _formToRemember.WindowState = State;
            
            // now we can watch for changes (not before, since we ourselves were changing the size/state!)        
            _formToRemember.SizeChanged += FormToRememberSizeChanged;
            _formToRemember.LocationChanged += FormToRememberLocationChanged;
        }    
        private void OnFormClosed(object sender, FormClosedEventArgs e)    
        {
            try
            {
                _formToRemember.FormClosed -= OnFormClosed;
                _formToRemember.SizeChanged -= FormToRememberSizeChanged;
                _formToRemember.LocationChanged -= FormToRememberLocationChanged;
                if (_formToRemember.Controls.ContainsKey("splitContainer"))
                {
                    this.SplitterDistance = (_formToRemember.Controls["splitContainer"] as SplitContainer).SplitterDistance;
                }
                IsolatedStorageFile isoStore = GetMyIsolatedStorageFile();
                using (IsolatedStorageFileStream strm = new IsolatedStorageFileStream(windowStateFileName,
                    FileMode.Create, FileAccess.Write, FileShare.None, isoStore))
                {
                    // Save the state as xml            
                    using (TextWriter tw = new StreamWriter(strm))
                    {
                        System.Xml.Serialization.XmlSerializer x =
                            new System.Xml.Serialization.XmlSerializer(GetType());
                        x.Serialize(tw, this);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }    

}

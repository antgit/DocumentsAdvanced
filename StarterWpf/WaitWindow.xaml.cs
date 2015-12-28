using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace StarterWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WaitWindow : Window
    {
        public WaitWindow()
        {
            InitializeComponent();
            pathAnimation.BeginStoryboard((Storyboard)FindResource("activityAnimation"), HandoffBehavior.SnapshotAndReplace, true);
        }

        public void FillListOfApplications(string[] inApplications)
        {
            ((cAppItemList)lstboxApplications.ItemsSource).Clear();
            
            foreach (string s in inApplications)
                ((cAppItemList)lstboxApplications.ItemsSource).Add(new cAppItem(s));

            BeginStoryboard((Storyboard)FindResource("showAppList"), HandoffBehavior.SnapshotAndReplace, true);
            ((Storyboard)FindResource("activityAnimation")).Stop(pathAnimation);
        }
        
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
		    Application.Current.Shutdown();
        }

        private void borderMove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        	DragMove();
        }

        private void btnAppItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ((App)Application.Current).AppSelected((string)((Button)sender).Content);
            BeginStoryboard((Storyboard)FindResource("hideAppList"), HandoffBehavior.SnapshotAndReplace, true);
            pathAnimation.BeginStoryboard((Storyboard)FindResource("activityAnimation"), HandoffBehavior.SnapshotAndReplace, true);
        }
    }

    public class cAppItem
    {
        public string ApplicationName { get; set; }

        public cAppItem(string inApplicationName)
        {
            ApplicationName = inApplicationName;
        }
        
    }
    public class cAppItemList : System.Collections.ObjectModel.ObservableCollection<cAppItem>
    {
        public cAppItemList()
        {
            //Add(new cAppItem("Приложение 1"));
            //Add(new cAppItem("Test Application 2"));
        }
    } 
}

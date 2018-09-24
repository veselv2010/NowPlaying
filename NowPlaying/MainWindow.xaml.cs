using System.Windows;

namespace NowPlaying
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();

            var browserWindow = new OAuth.BrowserWindow();
            browserWindow.ShowDialog();

            TextBoxToken.Text = browserWindow.ResultToken;

            this.Show();
        }

        private void ButtonDo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

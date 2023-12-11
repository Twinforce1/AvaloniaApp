using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

namespace AvaloniaApplication2
{
    public partial class ProfilePlayer : UserControl
    {
        private readonly MainWindow mainWindow;
        public ProfilePlayer(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        public ProfilePlayer()
        {
            throw new System.NotImplementedException();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        public void SetPlayerInfo(string info)
        {
            TextBlock infoTextBlock = this.FindControl<TextBlock>("infoTextBlock");
            infoTextBlock.Text = info;
        }
        
        private void OnGoToMainPageClick(object sender, RoutedEventArgs e)
        {
            mainWindow.ShowMainPage();
        }
        
        private void OnGoBackSecondButtonClick(object sender, RoutedEventArgs e)
        {
            // Переход назад на вторую страницу
            mainWindow.GoBackToSecondPage();
        }
        private void OnSearchButtonClick(object? sender, RoutedEventArgs e)
        {
            // Вызывайте метод поиска здесь
            mainWindow.NavigateToSearchResult();
        }

    }
}
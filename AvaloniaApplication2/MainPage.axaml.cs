using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace AvaloniaApplication2
{
    public partial class MainPage : UserControl
    {
        private readonly MainWindow mainWindow;
        private TextBox? searchBox;

        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            
            
            
        }

        private void OnGoBackMainButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.GoBackToMainPage();
        }

        private void OnGoBackSecondButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.GoBackToSecondPage();
        }
        

        private void OnSearchButtonClick(object? sender, RoutedEventArgs e)
        {
            // Вызывайте метод поиска здесь
            mainWindow.NavigateToSearchResult();
        }
    }
}
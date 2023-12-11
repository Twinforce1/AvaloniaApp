using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Threading.Tasks;

namespace AvaloniaApplication2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            ShowMainPage();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnNavigateToSecondPageClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ShowSecondPage();
        }

        public void ShowMainPage()
        {
            var mainPage = new MainPage(this);
            this.Content = mainPage;
        }

        public void ShowSecondPage()
        {
            var secondPage = new SecondPage(this);
            this.Content = secondPage;
        }

        public void NavigateToSearchResult()
        {
            var searchResult = new SearchResult(this);
            this.Content = searchResult;
        }

        public void NavigateToPlayerClick()
        {
            var playerProfile = new ProfilePlayer(this);
            this.Content = playerProfile;
        }
        public void GoBackToMainPage()
        {
            ShowMainPage();
        }

        public void GoBackToSecondPage()
        {
            ShowSecondPage();
        }


        
    }
}
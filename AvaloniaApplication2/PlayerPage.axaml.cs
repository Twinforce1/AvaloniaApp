using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media;
using Newtonsoft.Json.Linq;

namespace AvaloniaApplication2
{
    public partial class PlayerPage : UserControl
    {
        private TextBlock _playerNameTextBlock;
        private Image _avatarImage;
        private Image _imagePath;
        private TextBlock _winLossTextBlock;
        private TextBlock _matchesTextBlock;
        private MainWindow mainWindow;
        private TextBlock _killsTextBlock;
        private TextBlock _deathsTextBlock;
        private TextBlock _assistsTextBlock;
        private Image _heroImage;
        private TextBlock _resultmatch;
        

        public PlayerPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
#if DEBUG
            // this.AttachDevTools();
#endif
            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _playerNameTextBlock = this.FindControl<TextBlock>("PlayerNameTextBlock");
            _avatarImage = this.FindControl<Image>("AvatarImage");
            _imagePath = this.FindControl<Image>("ImagePath");
            _winLossTextBlock = this.FindControl<TextBlock>("WinLossTextBlock");
            _matchesTextBlock = this.FindControl<TextBlock>("MatchesTextBlock");
            _killsTextBlock = this.FindControl<TextBlock>("KillsTextBlock");
            _deathsTextBlock = this.FindControl<TextBlock>("DeathsTextBlock");
            _assistsTextBlock = this.FindControl<TextBlock>("AssistsTextBlock");
            _heroImage = this.FindControl<Image>("HeroImage");
            _resultmatch = this.FindControl<TextBlock>("Resultmatch");

        }
        
        
        public void SetPlayerInfo(string playerName, Bitmap avatar, Bitmap imagePath)
        {
            _playerNameTextBlock.Text = playerName;
            _avatarImage.Source = avatar;
            _imagePath.Source = imagePath;
            
        }

        public void SetPlayerInfo1(string winLoss)
        {
            _winLossTextBlock.Text = winLoss;
            
             
        }
        
        public void SetPlayerInfo2(string matches)
        {
            _matchesTextBlock.Text = matches;
            
        }

        public void SetPlayerInfo3(int kills, int deaths, int assists)
        {
            _killsTextBlock.Text = $"{kills}";
            _deathsTextBlock.Text = $"{deaths}";
            _assistsTextBlock.Text = $"{assists}";
        }

        public void SetPlayerInfo4(Bitmap heroInfo)
        {
            _heroImage.Source = heroInfo; 
        }

        public void ResultMatch(string matchOutcome)
        {
            _resultmatch.Text = matchOutcome;
            // Добавляем проверку результата и устанавливаем цвет текста
            if (matchOutcome == "Победа")
            {
                _resultmatch.Foreground = Brushes.Green; // Зелёный цвет для победы
            }
            else if (matchOutcome == "Поражение")
            {
                _resultmatch.Foreground = Brushes.Red; // Красный цвет для поражения
            }
        }
        
        

        public void OnGoToMainPageClick(object sender, RoutedEventArgs e)
        {
            mainWindow.GoBackToMainPage();
        }
        
        public void OnGoBackSecondButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.GoBackToSecondPage();
        }
        
        public void OnSearchButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.NavigateToSearchResult();
        }
        
        
    }
}
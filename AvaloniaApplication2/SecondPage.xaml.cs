using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;




namespace AvaloniaApplication2;

public class SecondPage : UserControl
{
    private readonly MainWindow mainWindow;
    private readonly UserSettingsRepository _userSettingsRepository;
    private TextBox _usernameTextBox;
    private TextBox _emailTextBox;
    private CheckBox _notificationsCheckBox;
    private CheckBox _mailingCheckBox;
    
    

    public SecondPage(MainWindow mainWindow)
    {
        InitializeComponent();
        
        this.mainWindow = mainWindow;
        _userSettingsRepository = new UserSettingsRepository("Host=localhost;Port=5432;Username=postgres;Password=Qwerty12345;Database=Settings;");
        
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        _usernameTextBox = this.FindControl<TextBox>("UsernameTextBox");
        _emailTextBox = this.FindControl<TextBox>("EmailTextBox");
        _notificationsCheckBox = this.FindControl<CheckBox>("NotificationsCheckBox");
        _mailingCheckBox = this.FindControl<CheckBox>("MailingCheckBox");
    }



    private async void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
    {
        var email = _emailTextBox.Text;
        var notificationsEnabled = _notificationsCheckBox.IsChecked ?? false;
        var mailingEnabled = _mailingCheckBox.IsChecked ?? false;

        await _userSettingsRepository.SaveSettingsAsync(email, notificationsEnabled, mailingEnabled);

    }
    


    private void OnGoToMainPageClick(object sender, RoutedEventArgs e)
    {
        mainWindow.ShowMainPage();
    }
    private void OnSearchButtonClick(object? sender, RoutedEventArgs e)
    {
        // Вызывайте метод поиска здесь
        mainWindow.NavigateToSearchResult();
    }
}
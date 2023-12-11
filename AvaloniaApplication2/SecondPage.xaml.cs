using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

namespace AvaloniaApplication2;

public class SecondPage : UserControl
{
    private readonly MainWindow mainWindow;

    public SecondPage(MainWindow mainWindow)
    {
        InitializeComponent();
        this.mainWindow = mainWindow;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
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
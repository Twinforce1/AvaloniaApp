// SecondWindow.xaml.cs
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication2;

public class SecondWindow : Window
{
    public SecondWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        // Здесь вы можете добавить код для инициализации компонентов в XAML
    }
}
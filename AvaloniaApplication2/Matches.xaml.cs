using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication2
{
    public class Matches : UserControl
    {
        public Matches()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            var goBackMainButton = this.FindControl<Button>("goBackMainButton");
            goBackMainButton.Click += OnGoBackMainButtonClick;

            var goBackSecondButton = this.FindControl<Button>("goBackSecondButton");
            goBackSecondButton.Click += OnGoBackSecondButtonClick;
        }

        private void OnGoBackMainButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // Ваш код для возврата на главную страницу
        }

        private void OnGoBackSecondButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // Ваш код для возврата на вторую страницу
        }
    }
}
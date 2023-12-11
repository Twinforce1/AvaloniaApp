using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using ExCSS;
using Newtonsoft.Json;

namespace AvaloniaApplication2
{
    public partial class SearchResult : UserControl
    {
        private readonly MainWindow _mainWindow;
        private TextBox _playerNameTextBox;
        private Image _avatarImage;
        private TextBlock _resultTextBlock;

        

        public SearchResult(MainWindow mainWindow)
        {
            
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            playerNameTextBox = this.FindControl<TextBox>("playerNameTextBox");
            avatarImage = this.FindControl<Image>("avatarImage");
            resultTextBlock = this.FindControl<TextBlock>("resultTextBlock");
            
            

            // Подписываемся на событие клика кнопки
            Button onSearchButtonClick = this.FindControl<Button>("OnSearchButtonClick");
            onSearchButtonClick.Click += OnSearchButtonClick_Click;
        }
        private async void OnSearchButtonClick_Click(object sender, RoutedEventArgs e)
        {
            string playerName = playerNameTextBox.Text;
            if (!string.IsNullOrEmpty(playerName))
            {
                await SearchPlayer(playerName);
            }
            else
            {
                resultTextBlock.Text = "Введите ник игрока.";
            }
        }

        private void OnGoBackMainButtonClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.GoBackToMainPage();
        }

        private void OnGoBackSecondButtonClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.GoBackToSecondPage();
        }
        

        private void ProfilePlayerProfileClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToPlayerClick();
        }
        

        private async Task SearchPlayer(string playerName)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiKey = "Ваш API-ключ OpenDota"; // Получите API-ключ на https://www.opendota.com/api-keys
            
                    string apiUrl = $"https://api.opendota.com/api/search?q={playerName}&api_key={apiKey}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    ParseAndDisplayResults(jsonResponse);
                }
                catch (HttpRequestException ex)
                {
                    _resultTextBlock.Text = $"Ошибка при выполнении HTTP-запроса: {ex.Message}";
                }
            }
        }
        
        
        
        private async void ParseAndDisplayResults(string jsonResponse)
        {
            try
            {
                JArray resultArray = JArray.Parse(jsonResponse);

                if (resultArray.Count > 0)
                {
                    JObject firstResult = (JObject)resultArray[0];

                    string personaname = firstResult.Value<string>("personaname");
                    string avatarfull = firstResult.Value<string>("avatarfull");
                    long account_id = firstResult.Value<long>("account_id");

                    resultTextBlock.Text = personaname;

                    // Отображаем изображение
                    using (HttpClient client = new HttpClient())
                    {
                        byte[] imageData = await client.GetByteArrayAsync(avatarfull);
                        Bitmap bitmap = new Bitmap(new MemoryStream(imageData));
                        avatarImage.Source = bitmap;
                        // // Запрос информации о рейтинге
                        // string additionalInfoUrl = $"https://api.opendota.com/api/players/{account_id}";
                        // string additionalInfoResponse = await client.GetStringAsync(additionalInfoUrl);
                        //
                        // try
                        // {
                        //     JToken additionalInfoData = JToken.Parse(additionalInfoResponse);
                        //
                        //     if (additionalInfoData.Type == JTokenType.Object)
                        //     {
                        //         JObject additionalInfoObject = (JObject)additionalInfoData;
                        //
                        //         // Проверяем, существует ли поле 'rank_tier' в объекте
                        //         if (additionalInfoObject.TryGetValue("rank_tier", out JToken rankTier))
                        //         {
                        //             // Преобразуем rank_tier в строку
                        //             string rankTierString = rankTier.ToString();
                        //
                        //             // Проверяем, достаточно ли символов в строке
                        //             if (rankTierString.Length >= 2)
                        //             {
                        //                 // Извлекаем первую и вторую цифры
                        //                 char firstDigit = rankTierString[0];
                        //                 char secondDigit = rankTierString[1];
                        //
                        //                 // Определяем изображение на основе диапазона значений rank_tier
                        //                 string imagePath = GetImagePath(firstDigit, secondDigit);
                        //
                        //                 // Выводим результат
                        //                 resultTextBlock.Text = $"Personaname: {personaname}\nRank Tier: {rankTier}\nImage Path: {imagePath}";
                        //             }
                        //             else
                        //             {
                        //                 resultTextBlock.Text = "Недостаточно цифр в значении rank_tier.";
                        //             }
                        //         }
                        //         else
                        //         {
                        //             resultTextBlock.Text = "Информация о rank_tier отсутствует в полученном JSON.";
                        //         }
                        //     }
                        //     else
                        //     {
                        //         resultTextBlock.Text = "Полученный JSON не является объектом.";
                        //     }
                        // }
                        // catch (JsonReaderException)
                        // {
                        //     resultTextBlock.Text = "Ошибка при чтении JSON. Возможно, получен массив, а не объект.";
                        // }
                        // catch (Exception ex)
                        // {
                        //     resultTextBlock.Text = $"Ошибка: {ex.Message}";
                        // }
                        // Запрос информации о рейтинге
                        string additionalInfoUrl = $"https://api.opendota.com/api/players/{account_id}";
                        string additionalInfoResponse = await client.GetStringAsync(additionalInfoUrl);

                        try
                        {
                            JToken additionalInfoData = JToken.Parse(additionalInfoResponse);

                            if (additionalInfoData.Type == JTokenType.Object)
                            {
                                JObject additionalInfoObject = (JObject)additionalInfoData;

                                // Проверяем, существует ли поле 'rank_tier' в объекте
                                if (additionalInfoObject.TryGetValue("rank_tier", out JToken rankTier))
                                {
                                    // Объединяем информацию о personaname и rank_tier в одной строке
                                    resultTextBlock.Text = $"Personaname: {personaname}\nRank Tier: {rankTier}";
                                }
                                else
                                {
                                    resultTextBlock.Text = "Информация о rank_tier отсутствует в полученном JSON.";
                                }
                            }
                            else
                            {
                                resultTextBlock.Text = "Полученный JSON не является объектом.";
                            }
                        }
                        catch (JsonReaderException)
                        {
                            resultTextBlock.Text = "Ошибка при чтении JSON. Возможно, получен массив, а не объект.";
                        }
                        catch (Exception ex)
                        {
                            resultTextBlock.Text = $"Ошибка: {ex.Message}";
                        }
                        avatarImage.Tapped += async (sender, args) =>
                        {
                            // Добавьте здесь код, который выполнится при клике на изображение
                            // Например, можно открыть большую версию изображения или выполнить другие действия
                            // MessageBox.Show("Изображение было кликнуто!");
                            _mainWindow.NavigateToPlayerClick();
                        };
                    }
                }
                else
                {
                    resultTextBlock.Text = "Нет результатов для указанного игрока.";
                }
            }
            catch (Exception ex)
            {
                resultTextBlock.Text = $"Ошибка при обработке JSON-ответа: {ex.Message}";
            }
        }
        // private string GetImagePath(char firstDigit, char secondDigit)
        // {
        //     int rankTierValue = int.Parse($"{firstDigit}{secondDigit}");
        //
        //     // Создаем словарь для соответствия между числами и путями к изображениям
        //     Dictionary<int, string> imagePaths = new Dictionary<int, string>
        //     {
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         { 1, "/ranks/herald1.png" },
        //         { 2, "/ranks/herald2.png" },
        //         // Добавьте соответствия для остальных чисел в диапазонах
        //     };
        //
        //     // Проверяем, есть ли соответствующий путь в словаре
        //     if (imagePaths.ContainsKey(rankTierValue))
        //     {
        //         return imagePaths[rankTierValue];
        //     }
        //
        //     return "DefaultImage.png"; // Путь к изображению по умолчанию
        // }

    }
}

using System.Windows;
using System.Windows.Media;
using IA_Coello.Configuration;
using IA_Coello.Services;

namespace IA_Coello
{
    public partial class MainWindow : Window
    {
        private ChatGptService? _chatService;
        private bool _isLoading = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeChat();
        }

        private void InitializeChat()
        {
            try
            {
                var settings = AppSettings.LoadSettings();
                _chatService = new ChatGptService(settings.OpenAI.ApiKey);

                MessageBox.Show("Connexion établie avec Azure AI Model Inference", "Succès", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur d'initialisation: {ex.Message}", "Erreur", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (_chatService == null || _isLoading)
                return;

            var userMessage = MessageInput.Text.Trim();
            if (string.IsNullOrEmpty(userMessage))
                return;

            _isLoading = true;
            MessageInput.IsEnabled = false;

            try
            {
                AddMessageToDisplay("user", userMessage);
                MessageInput.Clear();

                var response = await _chatService.SendMessageAsync(userMessage);
                AddMessageToDisplay("ai", response);

                MessagesListBox.ScrollIntoView(MessagesListBox.Items[MessagesListBox.Items.Count - 1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur d'envoi", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _isLoading = false;
                MessageInput.IsEnabled = true;
                MessageInput.Focus();
            }
        }

        private void AddMessageToDisplay(string role, string content)
        {
            var displayMessage = new DisplayMessage
            {
                Content = content,
                IsUserMessage = role == "user",
                UserVisibility = role == "user" ? Visibility.Visible : Visibility.Collapsed,
                AIVisibility = role == "ai" ? Visibility.Visible : Visibility.Collapsed
            };
            MessagesListBox.Items.Add(displayMessage);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MessagesListBox.Items.Clear();
            MessageInput.Clear();
            _chatService?.ClearHistory();
        }

        private void MessageInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }

        private void MessageInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Envoyer le message en appuyant sur Entrée
            if (e.Key == System.Windows.Input.Key.Return)
            {
                SendButton_Click(null, null);
                e.Handled = true; // Évite l'ajout d'une nouvelle ligne
            }
            // Effacer l'historique avec Ctrl+Delete
            else if (e.Key == System.Windows.Input.Key.Delete && 
                     (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) == System.Windows.Input.ModifierKeys.Control)
            {
                ClearButton_Click(null, null);
                e.Handled = true;
            }
        }
    }

    public class DisplayMessage
    {
        public string Content { get; set; } = string.Empty;
        public bool IsUserMessage { get; set; }
        public Visibility UserVisibility { get; set; } = Visibility.Collapsed;
        public Visibility AIVisibility { get; set; } = Visibility.Collapsed;
    }
}

using ModernMetadata.Model.Metadata.Readers;
using ModernMetadata.Model.Metadata.Users;
using System.Windows;
using System.Windows.Input;

namespace ModernMetadata.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    /// 

    public partial class LoginView : Window
    {
        private IUserConfigReader _userConfigReader;

        public LoginView()
        {
            InitializeComponent();

            _userConfigReader = new UserConfigReader();

            InputLanguageManager.Current.InputLanguageChanged += OnInputLanguageChanged;
            this.KeyDown += OnKeyDown;

            OnInputLanguageChanged(this, null);
            textBlockCapslock.Text = "Клавиша Capslock " + (Keyboard.IsKeyToggled(Key.CapsLock) ? "" : "не") + " нажата";
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.CapsLock)
                textBlockCapslock.Text = "Клавиша Capslock " + (Keyboard.IsKeyToggled(Key.CapsLock) ? "" : "не") + " нажата";
        }

        private void OnInputLanguageChanged(object sender, InputLanguageEventArgs? e)
        {
            textBlockLanguage.Text = "Язык ввода " + (InputLanguageManager.Current.CurrentInputLanguage.Name != "ru-RU" ? "Английский" : "Русский");
        }

        private void OnLoginClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text) || string.IsNullOrEmpty(passwordBox.Password))
                MessageBox.Show("Введите все данные!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

            IUserMenuData? data = _userConfigReader.ReadUserMenuData(textBoxLogin.Text, passwordBox.Password);
            if (data != null)
            {
                MenuView view = new(data);
                view.ShowDialog();
            }
            else 
                MessageBox.Show("Нет пользователя с заданным именем, либо неправильно введен пароль!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}

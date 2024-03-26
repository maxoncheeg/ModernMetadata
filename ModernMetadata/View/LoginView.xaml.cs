using System.Reflection;
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
        private object _userConfigReader;

        public LoginView()
        {
            InitializeComponent();
            
            Assembly assembly = Assembly.LoadFrom(@"C:\Users\maksg\Desktop\CODING\methodsandtech\ModernMetadata\ModernMetadata.Library\bin\Debug\net8.0\ModernMetadata.Library.dll");
            Type type = assembly.GetTypes().First(type => type.Name == "UserConfigReader");

            _userConfigReader = Activator.CreateInstance(type, ["userConfig.txt"]) ?? throw new DllNotFoundException();

            textBlockVersion.Text = $"Версия {Assembly.GetExecutingAssembly().GetName().Version}";

            InputLanguageManager.Current.InputLanguageChanged += OnInputLanguageChanged;
            this.KeyDown += OnKeyDown;

            OnInputLanguageChanged(this, null);
            textBlockCapslock.Text = "Клавиша Capslock " + (Keyboard.IsKeyToggled(Key.CapsLock) ? "" : "не") + " нажата";
        }

        /// <summary>
        /// Проверка на нажатие клавиши Capslock.
        /// </summary>

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.CapsLock)
                textBlockCapslock.Text = "Клавиша Capslock " + (Keyboard.IsKeyToggled(Key.CapsLock) ? "" : "не") + " нажата";
        }

        /// <summary>
        /// Определение раскладки.
        /// </summary>

        private void OnInputLanguageChanged(object sender, InputLanguageEventArgs? e)
        {
            textBlockLanguage.Text = "Язык ввода " + (InputLanguageManager.Current.CurrentInputLanguage.Name != "ru-RU" ? "Английский" : "Русский");
        }

        /// <summary>
        /// Проверка введённых данных.
        /// </summary>

        private void OnLoginClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text) || string.IsNullOrEmpty(passwordBox.Password))
                MessageBox.Show("Введите все данные!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

            object? data = _userConfigReader.GetType().GetMethod("ReadUserMenuData")?.Invoke(_userConfigReader, [textBoxLogin.Text, passwordBox.Password]);
            if (data != null)
            {
                MenuView view = new(data);
                view.ShowDialog();
            }
            else 
                MessageBox.Show("Нет пользователя с заданным именем, либо неправильно введен пароль!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Нажатие на кнопку Отмена.
        /// </summary>

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            textBoxLogin.Text = string.Empty;
            passwordBox.Password = string.Empty;
        }
    }
}

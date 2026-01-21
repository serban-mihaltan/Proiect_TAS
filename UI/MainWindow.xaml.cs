using Project_App;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IUserDB _userDb;
        private readonly IPassword _passwordManager;

        public MainWindow()
        {
            InitializeComponent();

            _userDb = new MyUserDB();
            _passwordManager = new MyPasswordManager();
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            ClearStatus();

            try
            {
                string password =SignupPasswordBox.Visibility == Visibility.Visible
                ? SignupPasswordBox.Password
                : SignupPasswordText.Text;

                UserOperations.CreateNewUser(
                    SignupUsername.Text,
                    password,
                    SignupFirstName.Text,
                    SignupLastName.Text,
                    SignupPhone.Text,
                    _passwordManager,
                    _userDb);

                WriteStatus("User created successfully.");
            }
            catch (Exception ex)
            {
                WriteStatus($"Signup failed:\n{ex.Message}");
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ClearStatus();
            

            try
            {
                string password = LoginPasswordBox.Visibility == Visibility.Visible
                ? LoginPasswordBox.Password
                : LoginPasswordText.Text;
                bool success = UserOperations.Loggin(
                    LoginUsername.Text,
                    password,
                    _userDb,
                    _passwordManager);

                if (success)
                    WriteStatus("Login successful!");
                else
                    WriteStatus("Login failed: Wrong password.");
            }
            catch (Exception ex)
            {
                WriteStatus($"Login failed:\n{ex.Message}");
            }
        }

        private void WriteStatus(string message)
        {
            StatusBox.Text += $"{DateTime.Now:T} - {message}\n";
        }

        private void ClearStatus()
        {
            StatusBox.Clear();
        }
        private void TogglePasswordVisibility(PasswordBox passwordBox,TextBox textBox)
        {
            if (passwordBox.Visibility == Visibility.Visible)
            {
                textBox.Text = passwordBox.Password;
                passwordBox.Visibility = Visibility.Collapsed;
                textBox.Visibility = Visibility.Visible;
            }
            else
            {
                passwordBox.Password = textBox.Text;
                textBox.Visibility = Visibility.Collapsed;
                passwordBox.Visibility = Visibility.Visible;
            }
        }
        private void ToggleSignupPassword_Click(object sender, RoutedEventArgs e)
        {
            TogglePasswordVisibility(
                SignupPasswordBox,
                SignupPasswordText);
        }

        private void ToggleLoginPassword_Click(object sender, RoutedEventArgs e)
        {
            TogglePasswordVisibility(
                LoginPasswordBox,
                LoginPasswordText);
        }

    }
}

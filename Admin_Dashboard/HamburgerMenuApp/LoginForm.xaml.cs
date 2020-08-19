using HamburgerMenuApp.Core;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HamburgerMenuApp
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        string constring = ConfigurationManager.AppSettings["Constring"];
        private void Btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btnlogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!validate())
                {
                    return;
                }
                string user = txtusername.Text;
                string password = txtpassword.Password;
                MySqlConnection con = new MySqlConnection(constring);
                con.Open();
                string query = "Select * from login where username='" + user + "'  and password='" + password + "'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd; //query execution
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string username = dataSet.Tables[0].Rows[0]["username"].ToString() + " " + dataSet.Tables[0].Rows[0]["password"].ToString();
                    MessageBox.Show("Logged in Successfully", "Login Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    MessageBox.Show("Welcome " + txtusername.Text, " GREETINGS", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Sorry! Please enter existing email-id/password.", "Login Information", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Forgotpass_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ForgotPassword mw = new ForgotPassword();
            mw.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(txtusername);
        }

        private bool validate()
        {
            if (string.IsNullOrWhiteSpace(txtusername.Text))
            {
                MessageBox.Show("Please enter the valid username", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtpassword.Password))
            {
                MessageBox.Show("Please enter the password", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
    }
}

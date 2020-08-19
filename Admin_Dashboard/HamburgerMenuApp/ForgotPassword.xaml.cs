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
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }
        static public string Username_emailId
        {
            get { return ConfigurationManager.AppSettings["EmailUsername"]; }
        }

        string constring = ConfigurationManager.AppSettings["Constring"];

        const string message1 = "\n\nYour details are :\n";
        const string message2 = "\n\nThank You. Have a nice day !!";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(txtusername);
        }

        private void Forgotpass_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginForm mw = new LoginForm();
            mw.Show();
            Close();
        }

        private void Btn_recover_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!validate())
                {
                    return;
                }
                string user = txtusername.Text;
                string email = txtemail.Text;
                MySqlConnection con = new MySqlConnection(constring);
                con.Open();
                string query = "Select * from login where username='" + user + "'  and email='" + email + "'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd; //query execution
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string pass = dataSet.Tables[0].Rows[0]["password"].ToString();
                    string name = dataSet.Tables[0].Rows[0]["admin_name"].ToString();
                    bool status = emailClass.SendEmail(email, Username_emailId, "Application - Registration", "Dear " + name + "," + message1 + "Username: " + user + " Password: " + pass + message2);
                    if (status)
                    {
                        MessageBox.Show("The Password has been sent to your mail.", "Forgot Password Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoginForm mw = new LoginForm();
                        mw.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Check your internet connection or contact the administrator", "Forgot Password Information", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Sorry! Please enter valid username/email-id.", "Forgot Password Information", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        private bool validate()
        {
            if (string.IsNullOrWhiteSpace(txtusername.Text))
            {
                MessageBox.Show("Please enter the valid username", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtemail.Text) || (!ValidationFile.IsValidEmail(txtemail.Text)))
            {
                MessageBox.Show("Please enter the valid Email-ID", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
    }
}

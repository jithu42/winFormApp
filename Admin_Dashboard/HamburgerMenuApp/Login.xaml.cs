using System;
using System.Collections.Generic;
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
using MahApps.Metro.Controls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace HamburgerMenuApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        public Login()
        {
            InitializeComponent();
        }
        string constring = ConfigurationManager.AppSettings["Constring"];
        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                    MessageBox.Show("Sorry! Please enter existing emailid/password.", "Invalid User Login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(txtusername);
        }
    }
}

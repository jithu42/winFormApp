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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HamburgerMenuApp.Core.Views
{
    /// <summary>
    /// Interaction logic for reset_password.xaml
    /// </summary>
    public partial class reset_password : UserControl
    {
        public reset_password()
        {
            InitializeComponent();
        }

        string constring = ConfigurationManager.AppSettings["Constring"];

        MysqlClass _mysql = null;

        private void Btn_reset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!validate())
                {
                    return;
                }
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string user = "admin";
                string password = curr_pass.Password;
                string query = "Select * from login where username='" + user + "'  and password='" + password + "'";
                DataSet dataSet = _mysql.ExecuteQueryReturnDataset(query);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    if(new_pass.Password != con_pass.Password)
                    {
                        MessageBox.Show("The new password and confirm password doesn't match.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        string updatequery = "update login set password = '" + new_pass.Password + "' where username='"+ user + "'";
                        if (change_pass(updatequery))
                        {
                            clear();
                            MessageBox.Show("The password has been changed.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The current password is invalid.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        public void clear()
        {
            new_pass.Password = string.Empty;
            curr_pass.Password = string.Empty;
            con_pass.Password = string.Empty;
            curr_pass.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private bool validate()
        {
            if (string.IsNullOrWhiteSpace(curr_pass.Password))
            {
                MessageBox.Show("Please enter the current password", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(new_pass.Password))
            {
                MessageBox.Show("Please enter the new password", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(con_pass.Password))
            {
                MessageBox.Show("Please enter the confirm password", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        private bool change_pass(string query)
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                _mysql.Execute_query(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}

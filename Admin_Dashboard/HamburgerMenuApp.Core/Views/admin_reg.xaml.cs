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
    /// Interaction logic for admin_reg.xaml
    /// </summary>
    public partial class admin_reg : UserControl
    {
        public admin_reg()
        {
            InitializeComponent();
        }

        static public string Username_emailId
        {
            get { return ConfigurationManager.AppSettings["EmailUsername"]; }
        }

        static public string constring
        {
            get { return ConfigurationManager.AppSettings["Constring"]; }
        }

        const string message1 = "\n\nYour account has been registered at Anne's College App. For your convience the below are the credentials to login into the Admin Dashboard application.\n";
        const string message2 = "\n\nThank You. Have a nice day !!";

        MysqlClass _mysql = null;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            clear();
            std_name.Focus();
            loadgrid();
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string pass = getpassword();
                if (!validate())
                {
                    return;
                }
                if (IsAlreadyRegistered(reg_no.Text))
                {
                    MessageBox.Show("The Admin record with " + reg_no.Text + " already Exists", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = "insert into login(admin_name,username,password,email,ph_no,gender) values ('" + std_name.Text + "','" + reg_no.Text + "','" + pass + "','" + email.Text + "','" + ph_no.Text + "','" + gender.Text + "')";
                _mysql.Execute_query(query);
                _mysql.CloseConnection();
                _mysql = null;
                bool status = emailClass.SendEmail(email.Text, Username_emailId, "Application - Registration", "Dear " + std_name.Text + "," + message1 + "Username: " + reg_no.Text + " Password: " + pass + message2);
                if (status)     
                {
                    MessageBox.Show("Admin Detail added sucessfully", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (_mysql == null)
                    {
                        _mysql = new MysqlClass(constring);
                    }
                    string delquery = "delete from login where username = '" + reg_no.Text + "'";
                    _mysql.Execute_query(delquery);
                    _mysql.CloseConnection();
                    _mysql = null;
                    MessageBox.Show("Check your internet connection", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                loadgrid();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Search_std_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = string.Empty;
                if(!string.IsNullOrWhiteSpace(search_std_name.Text))
                {
                    query = "Select * from login where admin_name='" + search_std_name.Text + "'";
                }
                else
                {
                    MessageBox.Show("Enter Admin Name", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    student_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                else
                {
                    MessageBox.Show("Student details not found", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_search_clear_Click(object sender, RoutedEventArgs e)
        {
            search_std_name.Text = string.Empty;
            loadgrid();
        }
        static public string temp { get; set; }
        private void Btn_update_Click(object sender, RoutedEventArgs e)
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
                string query = "update login set admin_name = '" + std_name.Text + "', username = '" + reg_no.Text + "', ph_no = '" + ph_no.Text + "', email = '" + email.Text + "', gender = '" + gender.Text + "' where id = '" + id.Content + "'";
                MessageBoxResult result = MessageBox.Show("Are you sure? The Admin record(" + reg_no.Text + ") will be updated.", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    if (temp != reg_no.Text && IsAlreadyRegistered(reg_no.Text))
                    {
                        MessageBox.Show("The Admin record with " + reg_no.Text + " already Exists", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    _mysql.Execute_query(query);
                    MessageBox.Show("The Admin Record has been updated successfully.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {

                }
                _mysql.CloseConnection();
                _mysql = null;
                loadgrid();
                btn_add.IsEnabled = true;
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn_del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string delquery = "delete from login where id = '" + id.Content + "'";
                MessageBoxResult result = MessageBox.Show("Are you sure? The Admin record(" + reg_no.Text + ") will be deleted.", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    _mysql.Execute_query(delquery);
                    MessageBox.Show("The Admin Record has been deleted successfully.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {

                }
                _mysql.CloseConnection();
                _mysql = null;
                loadgrid();
                btn_add.IsEnabled = true;
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void Student_grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView dataRow = (DataRowView)student_grid.SelectedItem;
            id.Content = dataRow.Row.ItemArray[0].ToString();
            std_name.Text = dataRow.Row.ItemArray[1].ToString();
            reg_no.Text = dataRow.Row.ItemArray[2].ToString();
            temp = dataRow.Row.ItemArray[2].ToString();
            ph_no.Text = dataRow.Row.ItemArray[5].ToString();
            email.Text = dataRow.Row.ItemArray[4].ToString();
            var _gender = gender.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == dataRow.Row.ItemArray[6].ToString());
            int _gender_index = gender.SelectedIndex = gender.Items.IndexOf(_gender);
            btn_del.IsEnabled = true;
            btn_update.IsEnabled = true;
            btn_add.IsEnabled = false;
        }

        private string getpassword()
        {
            int lengthOfPassword = 8;
            string passCode = DateTime.Now.Ticks.ToString();
            string pass = BitConverter.ToString(new System.Security.Cryptography.SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(passCode))).Replace("-", String.Empty);
            return pass.Substring(0, lengthOfPassword);
        }

        public void clear()
        {
            std_name.Text = string.Empty;
            reg_no.Text = string.Empty;
            ph_no.Text = string.Empty;
            email.Text = string.Empty;
            gender.SelectedIndex = 0;
            search_std_name.Text = string.Empty;
            btn_del.IsEnabled = false;
            btn_update.IsEnabled = false;
            btn_add.IsEnabled = true;
        }

        public void loadgrid()
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = "Select * from login order by id desc";
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables.Count > 0)
                {
                    student_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        public bool validate()
        {
            if (string.IsNullOrWhiteSpace(std_name.Text))
            {
                MessageBox.Show(Properties.Resources.validname, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                Keyboard.Focus(std_name);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(reg_no.Text) || (!ValidationFile.IsAlphaNumeric(reg_no.Text)))
            {
                MessageBox.Show(Properties.Resources.validusername, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                Keyboard.Focus(reg_no);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(ph_no.Text) || (!ValidationFile.IsNumeric(ph_no.Text)))
            {
                MessageBox.Show(Properties.Resources.validphoneno, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                Keyboard.Focus(ph_no);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(email.Text) || (!ValidationFile.IsValidEmail(email.Text)))
            {
                MessageBox.Show(Properties.Resources.validemail, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                Keyboard.Focus(email);
                return false;
            }
            else if (gender.SelectedIndex == 0)
            {
                MessageBox.Show(Properties.Resources.validgender, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                Keyboard.Focus(gender);
                return false;
            }
            return true;
        }

        private bool IsAlreadyRegistered(string value)
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = "Select * from login where username='" + value + "'";
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    _mysql.CloseConnection();
                    _mysql = null;
                    return true;
                }
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }
    }
}

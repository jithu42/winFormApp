using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

namespace HamburgerMenuApp.Core.Views
{
    /// <summary>
    /// Interaction logic for std_reg.xaml
    /// </summary>
    public partial class std_reg : UserControl
    {
        static public string Username_emailId
        {
            get { return ConfigurationManager.AppSettings["EmailUsername"]; }
        }
        static public string constring
        {
            get { return ConfigurationManager.AppSettings["Constring"]; }
        }
        MysqlClass _mysql = new MysqlClass(constring);
        public std_reg()
        {
            InitializeComponent();           
        }
        

        const string message1 = "\n\nYour account has been registered at Anne's College App. For your convience the below are the credentials to login into the Mobile application.\n";
        const string message2 = "\n\nThank You. Have a nice day !!";

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string pass = getpassword();
                if(!validate())
                {
                    return;
                }
                string query = "insert into std_register(name,reg_no,password,class,sem,ph_no,email,address,gender,dob) values ('" + std_name.Text + "','" + reg_no.Text + "','" + pass + "','" + class_dept.Text + "','" + sem.Text + "','" + ph_no.Text + "','" + email.Text + "','" + address.Text + "','" + gender.Text + "','" + dob.Text + "')";
                _mysql.Execute_query(query);
                bool status = emailClass.SendEmail(email.Text, Username_emailId, "Application - Registration", "Dear " + std_name.Text + "," + message1 + "Username: " + reg_no.Text + " Password: " + pass + message2);
                if (status)
                {
                    MessageBox.Show("Student Detail added sucessfully");
                }
                else
                {
                    string delquery = "delete from std_register where reg_no = '" + reg_no.Text + "'";
                    _mysql.Execute_query(delquery);          
                    MessageBox.Show("Check your internet connection");
                }
                loadgrid();
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        public void clear()
        {
            std_name.Text = string.Empty;
            reg_no.Text = string.Empty;
            ph_no.Text = string.Empty;
            email.Text = string.Empty;
            address.Text = string.Empty;
            dob.Text = string.Empty;
            class_dept.SelectedIndex = 0;
            sem.SelectedIndex = 0;
            gender.SelectedIndex = 0;
            search_class_dept.SelectedIndex = 0;
            search_sem.SelectedIndex = 0;
            search_std_name.Text = string.Empty;
        }

        private string getpassword()
        {
            int lengthOfPassword = 8;
            string passCode = DateTime.Now.Ticks.ToString();
            string pass = BitConverter.ToString(new System.Security.Cryptography.SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(passCode))).Replace("-", String.Empty);
            return pass.Substring(0, lengthOfPassword);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            clear();
            std_name.Focus();
            loadgrid();
        }

        public void loadgrid()
        {            
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = "Select * from std_register order by id desc";
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables.Count > 0)
                {
                    student_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                if (search_std_name.Text != string.Empty)
                {
                    query = "Select * from std_register where class='" + search_class_dept.Text + "' and sem ='" + search_sem.Text + "' and name='" + search_std_name.Text + "' or reg_no ='"+ search_std_name.Text + "'";
                }
                else
                {
                    query = "Select * from std_register where class='" + search_class_dept.Text + "' and sem ='" + search_sem.Text + "'";
                }
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    student_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                else
                {
                    MessageBox.Show("Student details not found");
                }
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Student_grid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataRowView dataRow = (DataRowView)student_grid.SelectedItem;
            std_name.Text = dataRow.Row.ItemArray[1].ToString();
            reg_no.Text = dataRow.Row.ItemArray[3].ToString();
            var _dept_class = class_dept.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == dataRow.Row.ItemArray[4].ToString());
            int _class_index = class_dept.SelectedIndex = class_dept.Items.IndexOf(_dept_class);
            var _sem = sem.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == dataRow.Row.ItemArray[5].ToString());
            int _sem_index = sem.SelectedIndex = sem.Items.IndexOf(_sem);
            ph_no.Text = dataRow.Row.ItemArray[6].ToString(); 
            email.Text = dataRow.Row.ItemArray[7].ToString(); 
            address.Text = dataRow.Row.ItemArray[8].ToString();
            var _gender = gender.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == dataRow.Row.ItemArray[9].ToString());
            int _gender_index = gender.SelectedIndex = gender.Items.IndexOf(_gender);
            dob.Text = dataRow.Row.ItemArray[10].ToString(); 
        }

        private void Btn_search_clear_Click(object sender, RoutedEventArgs e)
        {
            search_class_dept.SelectedIndex = 0;
            search_sem.SelectedIndex = 0;
            search_std_name.Text = string.Empty;
            loadgrid();
        }

        private void Std_name_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //if ((e.Key >= System.Windows.Input.Key.D0 && e.Key <= System.Windows.Input.Key.D9))
            //{

            //}
            //else if (e.Key >= System.Windows.Input.Key.A && e.Key <= System.Windows.Input.Key.Z)
            //{

            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }

        public bool validate()
        {
            if(string.IsNullOrWhiteSpace(std_name.Text))
            {
                MessageBox.Show("Enter the Student Name");
                return false;
            }
            else if(string.IsNullOrWhiteSpace(reg_no.Text))
            {
                MessageBox.Show("Enter the Register Number");
                return false;
            }
            return true;
        }
    }
}

﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace HamburgerMenuApp.Core.Views
{
    /// <summary>
    /// Interaction logic for teacher_reg.xaml
    /// </summary>
    public partial class teacher_reg : UserControl
    {
        public teacher_reg()
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

        static public string temp { get; set; }

        MysqlClass _mysql = null;
        const string message1 = "\n\nYour account has been registered at Anne's College App. For your convience the below are the credentials to login into the Mobile application.\n";
        const string message2 = "\n\nThank You. Have a nice day !!";

        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
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
                    MessageBox.Show("The Faculty record with " + reg_no.Text + " already Exists", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = "insert into std_register(name,reg_no,password,dept,sem,designation,ph_no,email,address,gender,dob) values ('" + std_name.Text + "','" + reg_no.Text + "','" + pass + "','" + class_dept.Text + "','" + sem.Text + "','faculty','" + ph_no.Text + "','" + email.Text + "','" + address.Text + "','" + gender.Text + "','" + dob.Text + "')";
                _mysql.Execute_query(query);
                bool status = emailClass.SendEmail(email.Text, Username_emailId, "Application - Registration", "Dear " + std_name.Text + "," + message1 + "Username: " + reg_no.Text + " Password: " + pass + message2);
                if (status)
                {
                    MessageBox.Show("Faculty Detail added sucessfully", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    string delquery = "delete from std_register where reg_no = '" + reg_no.Text + "'";
                    _mysql.Execute_query(delquery);
                    MessageBox.Show("Check your internet connection", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                _mysql.CloseConnection();
                _mysql = null;
                loadgrid();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
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
            btn_del.IsEnabled = false;
            btn_update.IsEnabled = false;
            btn_add.IsEnabled = true;
        }

        public bool validate()
        {
            if (string.IsNullOrWhiteSpace(std_name.Text))
            {
                MessageBox.Show("Enter the Faculty Name");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(reg_no.Text))
            {
                MessageBox.Show("Enter the Register Number");
                return false;
            }
            return true;
        }

        private string getpassword()
        {
            int lengthOfPassword = 8;
            string passCode = DateTime.Now.Ticks.ToString();
            string pass = BitConverter.ToString(new System.Security.Cryptography.SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(passCode))).Replace("-", String.Empty);
            return pass.Substring(0, lengthOfPassword);
        }

        public void loadgrid()
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = "Select * from std_register where designation='faculty' order by id desc";
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables.Count > 0)
                {
                    faculty_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

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
                string query = "update std_register set name = '" + std_name.Text + "', reg_no = '" + reg_no.Text + "', dept = '" + class_dept.Text + "' , sem = '" + sem.Text + "', ph_no = '" + ph_no.Text + "', email = '" + email.Text + "', address = '" + address.Text + "', gender = '" + gender.Text + "', dob = '" + dob.Text + "' where id = '" + id.Content + "'";
                MessageBoxResult result = MessageBox.Show("Are you sure?, The Faculty record(" + reg_no.Text + ") will be updated.", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    if (temp != reg_no.Text && IsAlreadyRegistered(reg_no.Text))
                    {
                        MessageBox.Show("The Faculty record with " + reg_no.Text + " already Exists", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    _mysql.Execute_query(query);
                    MessageBox.Show("The Faculty Record has been updated successfully.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
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
                string delquery = "delete from std_register where id = '" + id.Content + "'";
                MessageBoxResult result = MessageBox.Show("Are you sure?, The faculty record(" + reg_no.Text + ") will be deleted.", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    _mysql.Execute_query(delquery);
                    MessageBox.Show("The Faculty Record has been deleted successfully.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void Search_faculty_Click(object sender, RoutedEventArgs e)
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
                    query = "Select * from std_register where dept='" + search_class_dept.Text + "' and sem ='" + search_sem.Text + "' and name='" + search_std_name.Text + "' or reg_no ='" + search_std_name.Text + "' and designation = 'faculty'";
                }
                else
                {
                    query = "Select * from std_register where dept='" + search_class_dept.Text + "' and sem ='" + search_sem.Text + "' and designation = 'faculty'";
                }
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    faculty_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                else
                {
                    MessageBox.Show("Faculty details not found", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            search_class_dept.SelectedIndex = 0;
            search_sem.SelectedIndex = 0;
            search_std_name.Text = string.Empty;
            loadgrid();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            clear();
            std_name.Focus();
            loadgrid();
        }

        private bool IsAlreadyRegistered(string value)
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = "Select * from std_register where reg_no='" + value + "'";
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

        private void Faculty_grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView dataRow = (DataRowView)faculty_grid.SelectedItem;
            id.Content = dataRow.Row.ItemArray[0].ToString();
            std_name.Text = dataRow.Row.ItemArray[1].ToString();
            reg_no.Text = dataRow.Row.ItemArray[2].ToString();
            temp = dataRow.Row.ItemArray[2].ToString();
            var _dept_class = class_dept.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == dataRow.Row.ItemArray[4].ToString());
            int _class_index = class_dept.SelectedIndex = class_dept.Items.IndexOf(_dept_class);
            var _sem = sem.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == dataRow.Row.ItemArray[5].ToString());
            int _sem_index = sem.SelectedIndex = sem.Items.IndexOf(_sem);
            ph_no.Text = dataRow.Row.ItemArray[7].ToString();
            email.Text = dataRow.Row.ItemArray[8].ToString();
            address.Text = dataRow.Row.ItemArray[9].ToString();
            var _gender = gender.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == dataRow.Row.ItemArray[10].ToString());
            int _gender_index = gender.SelectedIndex = gender.Items.IndexOf(_gender);
            dob.Text = dataRow.Row.ItemArray[11].ToString();
            btn_del.IsEnabled = true;
            btn_update.IsEnabled = true;
            btn_add.IsEnabled = false;
        }
    }
}

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

namespace HamburgerMenuApp.Core.Views
{
    /// <summary>
    /// Interaction logic for std_reg.xaml
    /// </summary>
    public partial class std_reg : UserControl
    {
        public std_reg()
        {
            InitializeComponent();
        }

        string constring = ConfigurationManager.AppSettings["Constring"];
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(constring);
                con.Open();
                string query = "insert into std_register(name,reg_no,password,class,sem,ph_no,email,address,gender,dob) values ('" + std_name.Text + "','" + reg_no.Text + "','" + getpassword() + "','" + class_dept.Text + "','" + sem.Text + "','" + ph_no.Text + "','" + email.Text + "','" + address.Text + "','" + gender.Text + "','" + dob.Text + "')";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Detail added sucessfully");
                loadgrid();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            std_name.Text = string.Empty;
            reg_no.Text = string.Empty;
            //class_dept.Text = string.Empty;
            ph_no.Text = string.Empty;
            email.Text = string.Empty;
            address.Text = string.Empty;
            dob.Text = string.Empty;
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
            std_name.Focus();
            loadgrid();
        }

        public void loadgrid()
        {
            try
            {
                string query = "Select * from std_register order by id desc";
                MySqlConnection con = new MySqlConnection(constring);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd = da.SelectCommand;
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    student_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                con.Close();
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
                string query;
                if (search_std_name.Text != string.Empty)
                {
                    query = "Select * from std_register where class='" + search_class_dept.Text + "' and sem ='" + search_sem.Text + "' and name='" + search_std_name.Text + "' or reg_no ='"+ search_std_name.Text + "'";
                }
                else
                {
                    query = "Select * from std_register where class='" + search_class_dept.Text + "' and sem ='" + search_sem.Text + "'";
                }
                MySqlConnection con = new MySqlConnection(constring);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd = da.SelectCommand;
                da.Fill(ds);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    student_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                else
                {
                    MessageBox.Show("Student details not found");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

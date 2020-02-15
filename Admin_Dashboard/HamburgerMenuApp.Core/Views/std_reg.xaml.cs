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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Configuration;

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
    }
}

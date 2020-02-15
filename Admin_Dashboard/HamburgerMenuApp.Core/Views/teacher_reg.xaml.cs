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
using System.Configuration;
using MySql.Data.MySqlClient;

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
        string constring = ConfigurationManager.AppSettings["Constring"];
        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            teacher_name.Text = string.Empty;
            staff_id.Text = string.Empty;
            //class_dept.Text = string.Empty;
            ph_no.Text = string.Empty;
            email.Text = string.Empty;
            address.Text = string.Empty;
            dob.Text = string.Empty;
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(constring);
                con.Open();
                string query = "insert into teacher_register(teach_id,password,teach_name,dept,desig,ph_no,email,address,gender,dob) values ('" + staff_id.Text + "','" + getpassword() + "','" + teacher_name.Text + "','" + department.Text + "','" + Designation.Text + "','" + ph_no.Text + "','" + email.Text + "','" + address.Text + "','" + gender.Text + "','" + dob.Text + "')";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Teacher Detail added sucessfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

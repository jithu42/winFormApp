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
    /// Interaction logic for leaves.xaml
    /// </summary>
    public partial class leaves : UserControl
    {
        public leaves()
        {
            InitializeComponent();
        }
        static public string constring
        {
            get { return ConfigurationManager.AppSettings["Constring"]; }
        }

        MysqlClass _mysql = null;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadgrid();
            clear();
        }

        private void Btn_search_clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        void clear()
        {
            loadgrid();
            from_date.Text = string.Empty;
            search_class_dept.SelectedIndex = 0;
            search_sem.SelectedIndex = 0;
        }

        private void Search_leaves_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = string.Empty;
                if (from_date.Text != string.Empty && search_class_dept.SelectedIndex != 0 && search_sem.SelectedIndex != 0)
                {
                    query = "Select * from leaves where dept='" + search_class_dept.Text + "' and sem ='" + search_sem.Text + "'and from_date ='" + from_date.Text + "'";
                }
                else if (from_date.Text != string.Empty && search_class_dept.SelectedIndex == 0 && search_sem.SelectedIndex == 0)
                {
                    query = "Select * from leaves where from_date='" + from_date.Text + "'";
                }
                else
                {
                    query = "Select * from leaves where dept='" + search_class_dept.Text + "' and sem ='" + search_sem.Text + "'";
                }
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    assign_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                else
                {
                    MessageBox.Show("Leave details not found", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void loadgrid()
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = "Select * from leaves order by id desc";
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables.Count > 0)
                {
                    assign_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}

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
    /// Interaction logic for qpaper.xaml
    /// </summary>
    public partial class qpaper : UserControl
    {
        public qpaper()
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
            clear();
            loadgrid();
            title.Focus();
        }

        private void Search_qpaper_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = string.Empty;
                query = "Select * from qpaper where dept='" + search_class_dept.Text + "' and sem ='" + search_sem.Text + "'";
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    qpaper_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                else
                {
                    MessageBox.Show("Question Paper details not found", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            loadgrid();
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
                string query = "update qpaper set title = '" + title.Text + "', _desc = '" + _desc.Text + "', dept = '" + class_dept.Text + "' , sem = '" + sem.Text + "', url = '" + url_add.Text + "' where id = '" + id.Content + "'";
                MessageBoxResult result = MessageBox.Show("Are you sure?, The Question paper record will be updated.", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    _mysql.Execute_query(query);
                    MessageBox.Show("The Question paper Record has been updated successfully.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
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
                string delquery = "delete from qpaper where id = '" + id.Content + "'";
                MessageBoxResult result = MessageBox.Show("Are you sure?, The Question paper record will be deleted.", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    _mysql.Execute_query(delquery);
                    MessageBox.Show("The Question paper Record has been deleted successfully.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void Btn_add_Click(object sender, RoutedEventArgs e)
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
                string query = "insert into qpaper(title,_desc,dept,sem,url) values ('" + title.Text + "','" + _desc.Text + "','" + class_dept.Text + "','" + sem.Text + "','" + url_add.Text + "')";
                _mysql.Execute_query(query);               
                MessageBox.Show("Question Paper Details added sucessfully", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);                
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
            title.Text = string.Empty;
            _desc.Text = string.Empty;
            url_add.Text = string.Empty;
            class_dept.SelectedIndex = 0;
            sem.SelectedIndex = 0;
            search_class_dept.SelectedIndex = 0;
            search_sem.SelectedIndex = 0;
            btn_del.IsEnabled = false;
            btn_update.IsEnabled = false;
            btn_add.IsEnabled = true;
        }

        public bool validate()
        {
            if (string.IsNullOrWhiteSpace(title.Text))
            {
                MessageBox.Show(Properties.Resources.validqtitle, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                Keyboard.Focus(title);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(_desc.Text))
            {
                MessageBox.Show(Properties.Resources.validdesc, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                Keyboard.Focus(_desc);
                return false;
            }
            else if (class_dept.SelectedIndex == 0)
            {
                MessageBox.Show(Properties.Resources.validdept, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                Keyboard.Focus(class_dept);
                return false;
            }
            else if (sem.SelectedIndex == 0)
            {
                MessageBox.Show(Properties.Resources.validsem, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                Keyboard.Focus(sem);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(url_add.Text))
            {
                MessageBox.Show(Properties.Resources.validurladd, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                Keyboard.Focus(url_add);
                return false;
            }
            return true;
        }

        public void loadgrid()
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = "Select * from qpaper order by id desc";
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables.Count > 0)
                {
                    qpaper_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void Qpaper_grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView dataRow = (DataRowView)qpaper_grid.SelectedItem;
            id.Content = dataRow.Row.ItemArray[0].ToString();
            title.Text = dataRow.Row.ItemArray[1].ToString();
            _desc.Text = dataRow.Row.ItemArray[2].ToString();
            var _dept_class = class_dept.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == dataRow.Row.ItemArray[3].ToString());
            int _class_index = class_dept.SelectedIndex = class_dept.Items.IndexOf(_dept_class);
            var _sem = sem.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == dataRow.Row.ItemArray[4].ToString());
            int _sem_index = sem.SelectedIndex = sem.Items.IndexOf(_sem);
            url_add.Text = dataRow.Row.ItemArray[5].ToString();
            btn_del.IsEnabled = true;
            btn_update.IsEnabled = true;
            btn_add.IsEnabled = false;
        }
    }
}

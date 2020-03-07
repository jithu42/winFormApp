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
    /// Interaction logic for events.xaml
    /// </summary>
    public partial class events : UserControl
    {
        public events()
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
            title.Focus();
        }

        private void Event_grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView dataRow = (DataRowView)event_grid.SelectedItem;
            id.Content = dataRow.Row.ItemArray[0].ToString();
            title.Text = dataRow.Row.ItemArray[3].ToString();
            _desc.Text = dataRow.Row.ItemArray[4].ToString();
            org.Text = dataRow.Row.ItemArray[5].ToString();
            var _dept_class = class_dept.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == dataRow.Row.ItemArray[1].ToString());
            int _class_index = class_dept.SelectedIndex = class_dept.Items.IndexOf(_dept_class);
            var _sem = sem.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == dataRow.Row.ItemArray[2].ToString());
            int _sem_index = sem.SelectedIndex = sem.Items.IndexOf(_sem);
            from_date.Text = dataRow.Row.ItemArray[6].ToString();
            to_date.Text = dataRow.Row.ItemArray[7].ToString();
            btn_del.IsEnabled = true;
            btn_update.IsEnabled = true;
            btn_add.IsEnabled = false;
        }

        private void Search_event_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = string.Empty;
                query = "Select * from events where dept='" + search_class_dept.Text + "' and sem ='" + search_sem.Text + "'";
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    event_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                else
                {
                    MessageBox.Show("Event details not found", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                string query = "update events set event_name = '" + title.Text + "', _desc = '" + _desc.Text + "', dept = '" + class_dept.Text + "' , sem = '" + sem.Text + "', organised_by = '" + org.Text + "', from_date = '" + from_date.Text + "', to_date = '" + to_date.Text + "' where id = '" + id.Content + "'";
                MessageBoxResult result = MessageBox.Show("Are you sure?, The Event record will be updated.", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    _mysql.Execute_query(query);
                    MessageBox.Show("The Event Record has been updated successfully.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
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
                string delquery = "delete from events where id = '" + id.Content + "'";
                MessageBoxResult result = MessageBox.Show("Are you sure?, The Event record will be deleted.", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    _mysql.Execute_query(delquery);
                    MessageBox.Show("The Event Record has been deleted successfully.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
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
                string query = "insert into events(dept,sem,event_name,_desc,organised_by,from_date,to_date) values ('" + class_dept.Text + "','" + sem.Text + "','" + title.Text + "','" + _desc.Text + "','" + org.Text + "','" + from_date.Text + "','" + to_date.Text + "')";
                _mysql.Execute_query(query);
                MessageBox.Show("Event Details added sucessfully", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        public bool validate()
        {
            if (string.IsNullOrWhiteSpace(title.Text))
            {
                MessageBox.Show("Enter the Title");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(_desc.Text))
            {
                MessageBox.Show("Enter the Description");
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
                string query = "Select * from events order by id desc";
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables.Count > 0)
                {
                    event_grid.ItemsSource = ds.Tables[0].DefaultView;
                }
                _mysql.CloseConnection();
                _mysql = null;
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
            org.Text = string.Empty;
            from_date.Text = string.Empty;
            to_date.Text = string.Empty;
            class_dept.SelectedIndex = 0;
            sem.SelectedIndex = 0;
            search_class_dept.SelectedIndex = 0;
            search_sem.SelectedIndex = 0;
            btn_del.IsEnabled = false;
            btn_update.IsEnabled = false;
            btn_add.IsEnabled = true;
        }
    }
}

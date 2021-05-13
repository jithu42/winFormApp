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
    /// Interaction logic for classtimetable.xaml
    /// </summary>
    public partial class classtimetable : UserControl
    {
        public classtimetable()
        {
            InitializeComponent();
        }

        bool loaded { get; set; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loaded = true;
            class_tt.IsEnabled = false;
            sub_tt.IsEnabled = false;
            btn_class.IsEnabled = true;
            btn_sub.IsEnabled = true;
            btn_add.Visibility = Visibility.Visible;
            btn_view.Visibility = Visibility.Hidden;
            //btn_del.Visibility = Visibility.Hidden;
        }

        MysqlClass _mysql = null;

        static public string constring
        {
            get { return ConfigurationManager.AppSettings["Constring"]; }
        }

        private void LoadCSVOnDataGridView(string fileName)
        {
            try
            {
                ReadCSV csv = new ReadCSV(fileName);

                try
                {
                    datagrid.ItemsSource = csv.readCSV.DefaultView;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SubLoadCSVOnDataGridView(string fileName)
        {
            try
            {
                ReadCSV csv = new ReadCSV(fileName);

                try
                {
                    datagrid1.ItemsSource = csv.readCSV.DefaultView;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            string _class = class_dept.Text;
            string _sem = sem.Text;
            string _day;
            bool subNotFound = false;
            bool error = false;
            string _time;
            int count = 0;

            if (!validate())
            {
                return;
            }

            if (IsAlreadyRegistered(_class, _sem))
            {
                MessageBox.Show("TimeTable already exists for " + class_dept.Text + " " + sem.Text, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            DataTable TT = new DataTable();
            TT = ((DataView)datagrid.ItemsSource).ToTable();


            DataTable sub = new DataTable();
            sub = ((DataView)datagrid1.ItemsSource).ToTable();

            if (TT != null)
            {
                foreach (DataRow row in TT.Rows)
                {
                    if (subNotFound)
                    {
                        break;
                    }

                    _day = string.Empty;

                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        if (subNotFound)
                        {
                            break;
                        }
                        if (i > 0)
                        {
                            count = 0;
                            foreach (DataRow subrow in sub.Rows)
                            {
                                if (row.ItemArray[i].ToString().ToUpper() == subrow.ItemArray[0].ToString().ToUpper())
                                {
                                    _time = _getTime(TT, i);
                                    error = addTimeTable(_day, _time, _class, _sem, subrow.ItemArray[0].ToString(), subrow.ItemArray[1].ToString());
                                    break;
                                }
                                if (row.ItemArray[i].ToString().ToUpper() == string.Empty)
                                {
                                    _time = _getTime(TT, i);
                                    error = addTimeTable(_day, _time, _class, _sem, string.Empty, string.Empty);
                                    break;
                                }
                                count = count + 1;
                                if (count == sub.Rows.Count)
                                {
                                    subNotFound = true;
                                }
                            }
                        }
                        else
                        {
                            _day = row.ItemArray[0].ToString();
                        }
                    }
                }
            }
            if (subNotFound)
            {
                MessageBox.Show("Error in TimeTable Grid. Subjects not found in Subject Table.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                deleteTimeTable();
                clear();
            }
            else
            {
                MessageBox.Show("TimeTable has been successfully added.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                clear();
            }
        }

        public string _getTime(DataTable timeTable, int index)
        {
            string value = string.Empty;
            try
            {
                foreach (DataColumn col in timeTable.Columns)
                {
                    value = timeTable.Columns[index].ToString();
                    break;
                }
                return value;
            }
            catch
            {
                return string.Empty;
            }
        }

        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void Btn_class_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.InitialDirectory = @"C:\";
            openFileDlg.DefaultExt = ".csv";
            openFileDlg.Filter = "Comma Separated Values (.csv)|*.csv";
            Nullable<bool> result = openFileDlg.ShowDialog();            
            if (result == true)
            {
                class_tt.Text = openFileDlg.FileName;
                LoadCSVOnDataGridView(class_tt.Text);
            }
        }

        private void Btn_sub_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.InitialDirectory = @"C:\";
            openFileDlg.DefaultExt = ".csv";
            openFileDlg.Filter = "Comma Separated Values (.csv)|*.csv";
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                sub_tt.Text = openFileDlg.FileName;
                SubLoadCSVOnDataGridView(sub_tt.Text);
            }
        }

        private void Btn_view_Click(object sender, RoutedEventArgs e)
        {
            if(!validateView())
            {
                return;
            }
            loadTTGrid();
        }

        private void View_mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!loaded)
                return;

            if(view_mode.SelectedIndex == 2)
            {
                btn_view.Visibility = Visibility.Visible;
                //btn_del.Visibility = Visibility.Visible;
                btn_class.IsEnabled = false;
                btn_sub.IsEnabled = false;
            }
            else if (view_mode.SelectedIndex == 1 || view_mode.SelectedIndex == 0)
            {
                btn_view.Visibility = Visibility.Hidden;
                //btn_del.Visibility = Visibility.Hidden;
                btn_class.IsEnabled = true;
                btn_sub.IsEnabled = true;
            }
        }

        bool validate()
        {
            if (view_mode.SelectedIndex == 0)
            {
                MessageBox.Show("Select the TimeTable Mode", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                Keyboard.Focus(view_mode);
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
            else if (datagrid.ItemsSource == null)
            {
                MessageBox.Show("TimeTable details is not loaded.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
            else if (datagrid1.ItemsSource == null)
            {
                MessageBox.Show("Subject details is not loaded.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
            return true;
        }
        public void clear()
        {
            class_dept.SelectedIndex = 0;
            sem.SelectedIndex = 0;
            view_mode.SelectedIndex = 0;
            datagrid.ItemsSource = null;
            datagrid1.ItemsSource = null;
            class_tt.Text = string.Empty;
            sub_tt.Text = string.Empty;
        }

        public bool addTimeTable(string day, string time, string cls, string sem, string sub, string staffid)
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = "insert into timetable(day,time,course,sem,subject,staffid) values ('" + day + "','" + time + "','" + cls + "','" + sem + "','" + sub + "','" + staffid + "')";
                _mysql.Execute_query(query);
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
            return true;
        }

        private bool IsAlreadyRegistered(string cls, string sem)
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = "Select * from timetable where course='" + cls + "' and sem ='" + sem +"'";
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

        private void deleteTimeTable()
        {
            try
            {
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string delquery = "delete from timetable where course ='" + class_dept.Text + "' and sem ='" + sem.Text +"'";
                _mysql.Execute_query(delquery);
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn_del_Click(object sender, RoutedEventArgs e)
        {
            if(!validateView())
            {
                return;
            }
            MessageBoxResult result = MessageBox.Show("Are you sure?, The TimeTable record for " + class_dept.Text + " " + sem.Text + " will be deleted.", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                if (IsAlreadyRegistered(class_dept.Text, sem.Text))
                {
                    deleteTimeTable();
                    MessageBox.Show("The TimeTable Record has been deleted successfully.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No TimeTable Record has been deleted.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {

            }
        }

        static List<string> day = new List<string>();
        static List<string> classtime = new List<string>();
        static List<string> subject = new List<string>();
        static List<string> staffid = new List<string>();

        public void loadTTGrid()
        {
            try
            {
                clearlist();
                if (_mysql == null)
                {
                    _mysql = new MysqlClass(constring);
                }
                string query = "Select * from timetable where course='" + class_dept.Text + "' and sem ='" + sem.Text + "'";
                DataSet ds = _mysql.ExecuteQueryReturnDataset(query);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataTable table in ds.Tables)
                    {
                        foreach (DataRow dr in table.Rows)
                        {
                            if(!day.Contains(dr.ItemArray[1].ToString()))
                            {
                                day.Add(dr.ItemArray[1].ToString());
                            }
                            if (!classtime.Contains(dr.ItemArray[2].ToString()))
                            {
                                classtime.Add(dr.ItemArray[2].ToString());
                            }
                            if (!subject.Contains(dr.ItemArray[5].ToString()) && dr.ItemArray[5].ToString() != string.Empty)
                            {
                                subject.Add(dr.ItemArray[5].ToString());
                            }
                        }
                    }
                    if (classtime.Count != 0)
                    {
                        createDataTable(ds);
                    }
                    else
                    {
                        MessageBox.Show("No TimeTable Data found.", "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                _mysql.CloseConnection();
                _mysql = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "St. Anne's Admin DashBoard", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void createDataTable(DataSet ds)
        {
            DataTable tableHead = new DataTable();
            tableHead.Columns.Add("Day", typeof(string));
            DataRow toInsert = tableHead.NewRow();

            int index = 0;
            string temp = string.Empty;
            var itemArray = new object[classtime.Count + 1];

            for (int i = 0; i<classtime.Count;i++)
            {
                tableHead.Columns.Add(classtime[i], typeof(string));
            }

            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    if (temp != dr.ItemArray[1].ToString())
                    {
                        if(itemArray == null)
                        {
                            itemArray = new object[classtime.Count + 1];
                        }
                        temp = dr.ItemArray[1].ToString();
                        index = 0;
                        itemArray[index] = dr.ItemArray[1].ToString();
                    }
                    if (itemArray.Length-1 != index)
                    {
                        itemArray[index + 1] = dr.ItemArray[5].ToString();
                        index++;
                    }
                    if (itemArray.Length - 1 == index)
                    {
                        toInsert.ItemArray = itemArray;
                        tableHead.Rows.Add(toInsert.ItemArray);
                        itemArray = null;
                    }
                }
            }

            datagrid.ItemsSource = tableHead.DefaultView;
        }

        public void clearlist()
        {
            day.Clear();
            classtime.Clear();
            subject.Clear();
            staffid.Clear();
        }

        public bool validateView()
        {
            if (class_dept.SelectedIndex == 0)
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
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
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
            string _class;
            string _sem;
            string _day;
            bool subNotFound = false;
            string _time;
            int count = 0;
            DataTable TT = new DataTable();
            TT = ((DataView)datagrid.ItemsSource).ToTable();


            DataTable sub = new DataTable();
            sub = ((DataView)datagrid1.ItemsSource).ToTable();

            if (TT != null)
            {
                foreach (DataRow row in TT.Rows)
                {
                    _day = string.Empty;

                    for(int i=0;i<row.ItemArray.Length;i++)
                    {
                        if (i > 0)
                        {
                            count = 0;
                            foreach (DataRow subrow in sub.Rows)
                            {
                                if (row.ItemArray[i].ToString().ToUpper() == subrow.ItemArray[0].ToString().ToUpper())
                                {
                                    _time = _getTime(TT, i);
                                    break;
                                }
                                count = count + 1;
                                if(count == subrow.ItemArray.Length - 1)
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

        }

        private void View_mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!loaded)
                return;

            if(view_mode.SelectedIndex == 2)
            {
                btn_view.Visibility = Visibility.Visible;
                btn_class.IsEnabled = false;
                btn_sub.IsEnabled = false;
            }
            else if (view_mode.SelectedIndex == 1 || view_mode.SelectedIndex == 0)
            {
                btn_view.Visibility = Visibility.Hidden;
                btn_class.IsEnabled = true;
                btn_sub.IsEnabled = true;
            }
        }
    }
}

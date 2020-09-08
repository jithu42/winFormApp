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

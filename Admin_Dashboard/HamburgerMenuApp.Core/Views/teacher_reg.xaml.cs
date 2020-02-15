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
    /// Interaction logic for teacher_reg.xaml
    /// </summary>
    public partial class teacher_reg : UserControl
    {
        public teacher_reg()
        {
            InitializeComponent();
        }

        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("test");
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

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
using System.Windows.Shapes;

namespace StudentTracker.ExtraWindows
{
    /// <summary>
    /// Interaction logic for RemoveSemesterWindow.xaml
    /// </summary>
    public partial class RemoveSemesterWindow : Window
    {
        MainWindow mainWindow;
        public RemoveSemesterWindow(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            SemesterComboBox.ItemsSource = main.Semesters;
            SemesterComboBox.DisplayMemberPath = "Name";
        }

        private void SemesterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveButton.IsEnabled = SemesterComboBox.SelectedIndex != -1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}

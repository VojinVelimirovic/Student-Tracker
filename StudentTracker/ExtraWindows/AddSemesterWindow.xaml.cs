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
using StudentTracker.Classes;

namespace StudentTracker.ExtraWindows
{
    /// <summary>
    /// Interaction logic for AddSemesterWindow.xaml
    /// </summary>
    public partial class AddSemesterWindow : Window
    {
        MainWindow mainWindow;
        public AddSemesterWindow(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {

            foreach (Semester s in mainWindow.Semesters) {
                if (NameBox.Text == s.Name)
                {
                    MessageBox.Show($"'{NameBox.Text}' already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            DialogResult = true;
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddButton.IsEnabled = !string.IsNullOrWhiteSpace(NameBox.Text);
        }
    }
}

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
    /// Interaction logic for RemoveSubjectWindow.xaml
    /// </summary>
    public partial class RemoveSubjectWindow : Window
    {
        MainWindow mainWindow;
        public RemoveSubjectWindow(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            SemComboBox.ItemsSource = main.Semesters;
            SemComboBox.DisplayMemberPath = "Name";
        }

        private void SemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SemComboBox.SelectedIndex != -1) {
                if (SemComboBox.SelectedItem is Semester selectedSemester) {
                    SubComboBox.IsEnabled = true;
                    SubComboBox.ItemsSource = selectedSemester.Subjects;
                    SubComboBox.DisplayMemberPath = "Name";
                }
            }
        }

        private void SubComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SubComboBox.SelectedIndex != -1) {
                RemoveSubject.IsEnabled = true;
            }
        }

        private void RemoveSubject_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}

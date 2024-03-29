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
    /// Interaction logic for AddSubjectWindow.xaml
    /// </summary>
    public partial class AddSubjectWindow : Window
    {
        MainWindow mainWindow;
        public AddSubjectWindow(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            SemComboBox.ItemsSource = main.Semesters;
            SemComboBox.DisplayMemberPath = "Name";
            if (!main.Semesters.Any()) {
                MessageBox.Show("Create a semester first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                Close();
            }
        }

        private void SemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameBoxSubject.Text)) {
                AddSubject.IsEnabled = true;
            }
        }

        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {
            if (SemComboBox.SelectedItem is Semester selectedSemester) {
                foreach (Subject s in selectedSemester.Subjects) {
                    if (NameBoxSubject.Text == s.Name) {
                        MessageBox.Show($"'{NameBoxSubject.Text}' already exists within the chosen semester", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            DialogResult = true;
        }

        private void NameBoxSubject_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameBoxSubject.Text) && SemComboBox.SelectedIndex!=-1)
            {
                AddSubject.IsEnabled = true;
            }
        }
    }
}

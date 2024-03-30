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
    /// Interaction logic for AddResultWindow.xaml
    /// </summary>
    public partial class AddResultWindow : Window
    {
        MainWindow mainWindow;
        public AddResultWindow(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            SemComboBox.ItemsSource = main.Semesters;
            SemComboBox.DisplayMemberPath = "Name";
            datePicker.SelectedDate = DateTime.Now.Date;
            if (!main.Semesters.Any())
            {
                MessageBox.Show("Create a semester and subject first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                Close();
            }
        }

        private void SemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SemComboBox.SelectedIndex != -1)
            {
                if (SemComboBox.SelectedItem is Semester selectedSemester)
                {
                    SubComboBox.IsEnabled = true;
                    SubComboBox.ItemsSource = selectedSemester.Subjects;
                    SubComboBox.DisplayMemberPath = "Name";
                }
            }
        }

        private void SubComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SubComboBox.SelectedIndex != -1) {
                NameBox.IsEnabled = true;
                ScoredBox.IsEnabled = true;
                TotalBox.IsEnabled = true;
                datePicker.IsEnabled = true;
            }
        }

        private void ScoredBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string newText = (sender as TextBox)?.Text + e.Text;

            if (!System.Text.RegularExpressions.Regex.IsMatch(newText, @"^\d*\.?\d*$"))
            {
                e.Handled = true;
            }
        }

        private void TotalBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string newText = (sender as TextBox)?.Text + e.Text;

            if (!System.Text.RegularExpressions.Regex.IsMatch(newText, @"^\d*\.?\d*$"))
            {
                e.Handled = true;
            }
        }

        private void UpdateButtonState() {
            bool allTextBoxesNotEmpty = !string.IsNullOrWhiteSpace(NameBox.Text) &&
                                        !string.IsNullOrWhiteSpace(ScoredBox.Text) &&
                                        !string.IsNullOrWhiteSpace(TotalBox.Text) &&
                                        datePicker.SelectedDate.HasValue;
            if (allTextBoxesNotEmpty)
            {
                AddResultButton.IsEnabled = true;
            }
            else {
                AddResultButton.IsEnabled = false;
            }
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateButtonState();
        }

        private void ScoredBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateButtonState();
        }

        private void TotalBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateButtonState();
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonState();
        }

        private bool IsValidDate(DateTime? selectedDate)
        {
            if (selectedDate == null)
            {
                MessageBox.Show("Date must be selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (selectedDate < new DateTime(1970, 1, 1))
            {
                MessageBox.Show("Date can't be before 01/01/1970", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (selectedDate.Value.Month < 1 || selectedDate.Value.Month > 12)
            {
                MessageBox.Show("Invald month", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (selectedDate.Value.Day < 1 || selectedDate.Value.Day > 31)
            {
                MessageBox.Show("Invalid day", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (selectedDate.Value.Day > DateTime.DaysInMonth(selectedDate.Value.Year, selectedDate.Value.Month))
            {
                MessageBox.Show("Invalid day", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void AddResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubComboBox.SelectedItem is Subject selectedSubject)
            {
                foreach (TestResult t in selectedSubject.TestResults)
                {
                    if (NameBox.Text == t.Name)
                    {
                        MessageBox.Show($"'{NameBox.Text}' already exists within the chosen subject", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            if (double.TryParse(TotalBox.Text, out double total) && double.TryParse(ScoredBox.Text, out double scored))
            {
                if (scored > total)
                {
                    MessageBox.Show("Points scored must be lower than Max points", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if(IsValidDate(datePicker.SelectedDate))
                DialogResult = true;
        }
    }
}

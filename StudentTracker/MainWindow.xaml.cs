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
using StudentTracker.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using StudentTracker.ExtraWindows;
using Newtonsoft.Json;
using System.IO;


namespace StudentTracker
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string AppStateFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appState.json");

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PopulateTreeView()
        {
            HasSemesters = Semesters.Any();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveAppState();
            base.OnClosing(e);
        }

        private bool hasSemesters;
        public bool HasSemesters
        {
            get { return hasSemesters; }
            set
            {
                if (hasSemesters != value)
                {
                    hasSemesters = value;
                    OnPropertyChanged(nameof(HasSemesters));
                }
            }
        }

        public ObservableCollection<Semester> Semesters { get; set; }
        private double totalAverage;
        public double TotalAverage
        {
            get { return totalAverage; }
            set
            {
                if (totalAverage != value)
                {
                    totalAverage = value;
                    OnPropertyChanged(nameof(TotalAverage));
                }
            }
        }
        private double zoomFactor = 1.1;
        private int zoomOut = 0;
        private int maxZoomOut = 3;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            LoadAppState();
            ZoomInButton.IsEnabled = false;
            PopulateTreeView();
            UpdateAverage();
        }

        private void SaveAppState()
        {
            try
            {
                string json = JsonConvert.SerializeObject(Semesters);
                File.WriteAllText(AppStateFilePath, json);
                Console.WriteLine("Serialization successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving application state: {ex.Message}");
            }
        }

        private void LoadAppState()
        {
            try
            {
                if (File.Exists(AppStateFilePath))
                {
                    string json = File.ReadAllText(AppStateFilePath);
                    Semesters = JsonConvert.DeserializeObject<ObservableCollection<Semester>>(json);
                }
                else
                {
                    Semesters = new ObservableCollection<Semester>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading application state: {ex.Message}");
                Semesters = new ObservableCollection<Semester>();
            }
        }

        private void AddSemester_Click(object sender, RoutedEventArgs e)
        {
            Semester semester = new Semester("");

            AddSemesterWindow form = new AddSemesterWindow(this);

            if(form.ShowDialog() == true) {
                semester.Name = form.NameBox.Text;
                Semesters.Add(semester);
            }
            PopulateTreeView();
        }

        private void RemoveSemester_Click(object sender, RoutedEventArgs e)
        {
            Semester semester = null;

            RemoveSemesterWindow form = new RemoveSemesterWindow(this);
            if (form.ShowDialog() == true && form.SemesterComboBox.SelectedItem is Semester selectedSemester)
            {
                semester = selectedSemester;
                Semesters.Remove(semester);
            }
            PopulateTreeView();
        }

        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {
            Semester semester = null;
            Subject subject = new Subject("");

            AddSubjectWindow form = new AddSubjectWindow(this);
            try
            {
                if (form.ShowDialog() == true && form.SemComboBox.SelectedItem is Semester selectedSemester)
                {
                    semester = selectedSemester;
                    subject.Name = form.NameBoxSubject.Text;
                    semester.Subjects.Add(subject);
                    semester.UpdateSemester();
                    UpdateAverage();
                }
            }
            catch (Exception) { }
        }

        private void RemoveSubject_Click(object sender, RoutedEventArgs e)
        {
            Semester semester = null;
            Subject subject = new Subject("");

            RemoveSubjectWindow form = new RemoveSubjectWindow(this);

            if (form.ShowDialog() == true && form.SemComboBox.SelectedItem is Semester selectedSemester && form.SubComboBox.SelectedItem is Subject selectedSubject) {
                semester = selectedSemester;
                subject = selectedSubject;
                semester.Subjects.Remove(subject);
                semester.UpdateSemester();
                UpdateAverage();
            }

        }

        private void AddResult_Click(object sender, RoutedEventArgs e)
        {
            Semester semester = null;
            Subject subject = new Subject("");
            TestResult testResult = new TestResult("");
            AddResultWindow form = new AddResultWindow(this);

            try
            {
                if (form.ShowDialog() == true && form.SubComboBox.SelectedItem is Subject selectedSubject && form.SemComboBox.SelectedItem is Semester selectedSemester)
                {
                    semester = selectedSemester;
                    subject = selectedSubject;
                    semester.Subjects.Remove(subject);
                    testResult.Name = form.NameBox.Text;
                    testResult.ScoredPoints = Double.Parse(form.ScoredBox.Text);
                    testResult.TotalPoints = Double.Parse(form.TotalBox.Text);
                    testResult.Date = form.datePicker.SelectedDate.Value.Date;
                    subject.TestResults.Add(testResult);
                    subject.UpdateSubject();
                    semester.Subjects.Add(subject);
                    semester.UpdateSemester();
                    UpdateAverage();
                }
            }
            catch (Exception) { }
        }

        private void RemoveTestResult_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            try
            {
                if (button != null)
                {
                    TestResult testResultToRemove = button.DataContext as TestResult;

                    if (testResultToRemove != null)
                    {
                        var expansionStates = new Dictionary<Semester, bool>();
                        foreach (Semester semester in semesterTreeView.Items)
                        {
                            if (semesterTreeView.ItemContainerGenerator.ContainerFromItem(semester) is TreeViewItem semesterItem)
                            {
                                expansionStates[semester] = semesterItem.IsExpanded;
                            }
                        }

                        foreach (Semester semester in Semesters)
                        {
                            foreach (Subject subject in semester.Subjects)
                            {
                                subject.TestResults.Remove(testResultToRemove);
                                subject.UpdateSubject();
                                semester.UpdateSemester();
                                UpdateAverage();
                            }
                        }

                        semesterTreeView.Items.Refresh();

                        foreach (Semester semester in semesterTreeView.Items)
                        {
                            if (expansionStates.TryGetValue(semester, out bool isExpanded))
                            {
                                if (semesterTreeView.ItemContainerGenerator.ContainerFromItem(semester) is TreeViewItem semesterItem)
                                {
                                    semesterItem.IsExpanded = isExpanded;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Semesters.Clear();
            UpdateAverage();
            PopulateTreeView();
        }

        private void UpdateAverage() 
        {
            double sum = 0.0;
            int semesterCnt = 0;
            foreach (Semester s in Semesters) {
                foreach (Subject sub in s.Subjects) {
                    if (sub.Grade != 5)
                    {
                        sum += sub.Grade;
                        semesterCnt++;
                    }
                }
            }
            if (sum != 0.0)
            {
                TotalAverage = sum / semesterCnt;
            }
            else {
                TotalAverage = 0.0;
            }
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            ScaleTransform transform = (ScaleTransform)Resources["zoomTransform"];
            transform.ScaleX *= zoomFactor;
            transform.ScaleY *= zoomFactor;
            zoomOut--;
            RegulateZoom();
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            ScaleTransform transform = (ScaleTransform)Resources["zoomTransform"];
            transform.ScaleX /= zoomFactor;
            transform.ScaleY /= zoomFactor;
            zoomOut++;
            RegulateZoom();
        }

        private void RegulateZoom() {
            if (zoomOut >= maxZoomOut)
            {
                ZoomOutButton.IsEnabled = false;
            }
            else {
                ZoomOutButton.IsEnabled = true;
            }
            if (zoomOut != 0)
            {
                ZoomInButton.IsEnabled = true;
            }
            else
            {
                ZoomInButton.IsEnabled = false;
            }
        }
    }
}

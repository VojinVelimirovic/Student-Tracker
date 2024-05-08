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
using StudentTracker.Helpers;
using Notification.Wpf;
using System.Threading;
using System.IO;


namespace StudentTracker
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string AppStateFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appState.json");

        private NotificationManager notificationManager;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            LoadAppState();
            ZoomInButton.IsEnabled = false;
            PopulateTreeView();
            UpdateAverage();
            notificationManager = new NotificationManager();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PopulateTreeView()
        {
            HasSemesters = Semesters.Any();
            HasSubjects = false;
            foreach(Semester semester in Semesters) 
            {
                if (semester.Subjects.Any())
                {
                    HasSubjects = true;
                    break;
                }
            }
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

        private bool hasSubjects;
        public bool HasSubjects
        {
            get { return hasSubjects; }
            set
            {
                if (hasSubjects != value)
                {
                    hasSubjects = value;
                    OnPropertyChanged(nameof(HasSubjects));
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
        private int maxZoomOut = 4;

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
                ShowToastNotification(new ToastNotification("Success", "Successfully added item(s)", NotificationType.Success));
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
                ShowToastNotification(new ToastNotification("Success", "Successfully removed item(s)", NotificationType.Success));
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
                    ShowToastNotification(new ToastNotification("Success", "Successfully added item(s)", NotificationType.Success));
                }
                PopulateTreeView();
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
                ShowToastNotification(new ToastNotification("Success", "Successfully removed item(s)", NotificationType.Success));
            }
            PopulateTreeView();
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
                    ShowToastNotification(new ToastNotification("Success", "Successfully added item(s)", NotificationType.Success));
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

                        ShowToastNotification(new ToastNotification("Success", "Successfully removed item(s)", NotificationType.Success));
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
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to remove all the data?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Semesters.Clear();
                UpdateAverage();
                PopulateTreeView();
                ShowToastNotification(new ToastNotification("Success", "Successfully removed item(s)", NotificationType.Success));
            }
            else
            {
                return;
            }
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
            //The zoom in/out functions only work on all the subitems in a TreeView if they have been expanded to at some point in the program, so i am
            //expanding them all and then returning them to their original states
            Dictionary<TreeViewItem, bool> originalExpandedState = new Dictionary<TreeViewItem, bool>();
            SaveOriginalExpandedState(semesterTreeView, originalExpandedState);
            ExpandCollapseTreeViewItems(semesterTreeView, true);
            AdjustFontSize(zoomFactor);
            zoomOut--;
            RegulateZoom();
            ExpandCollapseTreeViewItems(semesterTreeView, false);
            RestoreOriginalExpandedState(semesterTreeView, originalExpandedState);
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<TreeViewItem, bool> originalExpandedState = new Dictionary<TreeViewItem, bool>();
            SaveOriginalExpandedState(semesterTreeView, originalExpandedState);
            ExpandCollapseTreeViewItems(semesterTreeView, true);
            AdjustFontSize(1 / zoomFactor);
            zoomOut++;
            RegulateZoom();
            ExpandCollapseTreeViewItems(semesterTreeView, false);
            RestoreOriginalExpandedState(semesterTreeView, originalExpandedState);
        }

        private void AdjustFontSize(double factor)
        {
            semesterTreeView.UpdateLayout();
            foreach (var textBlock in FindVisualChildren<TextBlock>(semesterTreeView))
            {
                textBlock.FontSize *= factor;
            }

            foreach (var button in FindVisualChildren<Button>(semesterTreeView))
            {
                button.Height *= factor;
                button.Width *= factor;
            }
        }

        private void ExpandCollapseTreeViewItems(TreeView treeView, bool expand)
        {
            foreach (var item in treeView.Items)
            {
                var treeViewItem = treeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeViewItem != null)
                {
                    if (expand)
                    {
                        treeViewItem.IsExpanded = true;
                    }
                    else
                    {
                        treeViewItem.IsExpanded = false;
                    }
                }
            }
        }

        private void SaveOriginalExpandedState(TreeView treeView, Dictionary<TreeViewItem, bool> originalExpandedState)
        {
            originalExpandedState.Clear();
            foreach (var item in treeView.Items)
            {
                var treeViewItem = treeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeViewItem != null)
                {
                    originalExpandedState[treeViewItem] = treeViewItem.IsExpanded;
                }
            }
        }

        private void RestoreOriginalExpandedState(TreeView treeView, Dictionary<TreeViewItem, bool> originalExpandedState)
        {
            foreach (var item in treeView.Items)
            {
                var treeViewItem = treeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeViewItem != null)
                {
                    if (originalExpandedState.ContainsKey(treeViewItem))
                    {
                        treeViewItem.IsExpanded = originalExpandedState[treeViewItem];
                    }
                }
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                int count = VisualTreeHelper.GetChildrenCount(depObj);
                for (int i = 0; i < count; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
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

        private void SemesterItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(textBlock);
                while (parent != null && !(parent is TreeViewItem))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
                TreeViewItem treeViewItem = parent as TreeViewItem;
                if (treeViewItem != null)
                {
                    if (treeViewItem.IsExpanded)
                    {
                        treeViewItem.IsExpanded = false;
                    }
                    else
                    {
                        treeViewItem.IsExpanded = true;
                    }
                }
            }
        }

        private void semesterTreeView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SemesterItem_MouseLeftButtonDown(sender, e);
        }

        public void ShowToastNotification(ToastNotification toastNotification)
        {
            notificationManager.Show(toastNotification.Title, toastNotification.Message, toastNotification.Type, "MainWindowNotificationArea");
        }

        private void SemesterItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                textBlock.Foreground = Brushes.LightSkyBlue;
            }
        }

        private void SemesterItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                textBlock.Foreground = Brushes.DarkSlateGray;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.OemPlus)
            {
                if(ZoomInButton.IsEnabled)
                    ZoomIn_Click(this, new RoutedEventArgs());
            }
            else if(Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.OemMinus)
            {
                if(ZoomOutButton.IsEnabled)
                    ZoomOut_Click(this, new RoutedEventArgs());
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Delete)
            {
                if(ClearButton.IsEnabled)
                    ClearButton_Click(this, new RoutedEventArgs());
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.N)
            {
                if (AddSemester.IsEnabled)
                    AddSemester_Click(this, new RoutedEventArgs());
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.D)
            {
                if (RemoveSemester.IsEnabled)
                    RemoveSemester_Click(this, new RoutedEventArgs());
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.A)
            {
                if (AddSubject.IsEnabled)
                    AddSubject_Click(this, new RoutedEventArgs());
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.R)
            {
                if (RemoveSubject.IsEnabled)
                    RemoveSubject_Click(this, new RoutedEventArgs());
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.E)
            {
                if (AddResult.IsEnabled)
                    AddResult_Click(this, new RoutedEventArgs());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Classes
{
    public class Subject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string name;
        private ObservableCollection<TestResult> testResults;
        private double scoredPoints;
        private double totalPoints;
        private double percentage;
        private int grade;

        public string Name { get => name; set => name = value; }
        public ObservableCollection<TestResult> TestResults
        {
            get => testResults ?? (testResults = new ObservableCollection<TestResult>());
            set
            {
                if (testResults != value)
                {
                    testResults = value;
                    OnPropertyChanged(nameof(TestResults));
                }
            }
        }
        public double ScoredPoints { get => scoredPoints; set => scoredPoints = value; }
        public double TotalPoints { get => totalPoints; set => totalPoints = value; }
        public double Percentage { get => percentage; set => percentage = value; }
        public int Grade { get => grade; set => grade = value; }

        public Subject(string name)
        {
            Name = name;
            TestResults = new ObservableCollection<TestResult>();
            ScoredPoints = 0;
            TotalPoints = 0;
            Grade = 5;
            Percentage = 0;
        }

        public void UpdateSubject() {
            TestResults = new ObservableCollection<TestResult>(testResults.OrderBy(tr => tr.Date));
            ScoredPoints = 0;
            TotalPoints = 0;
            foreach (TestResult t in testResults) {
                ScoredPoints += t.ScoredPoints;
                TotalPoints += t.TotalPoints;
            }
            if (TotalPoints == 0)
            {
                Percentage = 0;
            }
            else {
                Percentage = 100 * ScoredPoints / TotalPoints;
            }
            if (Percentage < 51)
            {
                Grade = 5;
            }
            else if (Percentage >= 51 && Percentage < 61)
            {
                Grade = 6;
            }
            else if (Percentage >= 61 && Percentage < 71)
            {
                Grade = 7;
            }
            else if (Percentage >= 71 && Percentage < 81)
            {
                Grade = 8;
            }
            else if (Percentage >= 81 && Percentage < 91)
            {
                Grade = 9;
            }
            else {
                Grade = 10;
            }
        }
    }
}

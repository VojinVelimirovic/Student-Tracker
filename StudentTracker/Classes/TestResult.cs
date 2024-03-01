using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Classes
{
    public class TestResult : INotifyPropertyChanged
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
        private double scoredPoints;
        private double totalPoints;
        DateTime date;
        public string Name { get => name; set => name = value; }
        public double ScoredPoints { get => scoredPoints; set => scoredPoints = value; }
        public double TotalPoints { get => totalPoints; set => totalPoints = value; }
        public DateTime Date { get => date; set => date = value; }

        public TestResult(string name)
        {
            Name = name;
            ScoredPoints = 0;
            TotalPoints = 0;
            date = DateTime.Today.Date;
        }
    }
}

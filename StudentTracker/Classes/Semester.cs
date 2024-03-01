using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Classes
{
    public class Semester:INotifyPropertyChanged
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
        private ObservableCollection<Subject> subjects;
        private double averageGrade;

        public string Name { get => name; set => name = value; }
        public ObservableCollection<Subject> Subjects
        {
            get => subjects ?? (subjects = new ObservableCollection<Subject>());
            set
            {
                if (subjects != value)
                {
                    subjects = value;
                    OnPropertyChanged(nameof(Subjects));
                }
            }
        }
        public double AverageGrade { get => averageGrade; set => averageGrade = value; }

        public Semester(string name)
        {
            Name = name;
            AverageGrade = 0.0;
            Subjects = new ObservableCollection<Subject>();
        }

        public void UpdateSemester() {
            AverageGrade = 0.0;
            double sum = 0.0;
            int subCounter = 0;

            foreach (Subject s in Subjects)
            {
                if (s.Grade != 5)
                {
                    sum += s.Grade;
                    subCounter++;
                }
            }

            if (subCounter > 0)
            {
                AverageGrade = sum / subCounter;
            }
            else
            {
                AverageGrade = 0.0;
            }

            OnPropertyChanged(nameof(AverageGrade));
        }
    }
}

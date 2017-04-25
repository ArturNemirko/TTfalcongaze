using Logic;
using Logic.DAL;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TTFalcongaze.ViewModel
{
    class StudentViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<StudentModel> students;

        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructor
        public StudentViewModel(ObservableCollection<StudentModel> students, bool isNewStudent, StudentModel student)
        {
            this.students = students;
            IsNewStudent = isNewStudent;
            SaveCommand = new Command(arg => SaveMethod());
            
            if (student != null)
                Student = (StudentModel)student.Clone();
            else
                Student = new StudentModel();
        }

        #endregion

        private StudentModel student;

        #region Properties
        public StudentModel Student
        {
            get
            {
                return student;
            }
            set
            {
                if(value != student)
                {
                    student = value;
                    OnPropertyChanged("Student");
                }
            }
        }
        public bool IsNewStudent { get; set; }
        #endregion

        #region Commands
        public ICommand SaveCommand { get; set; }

        #endregion

        #region Methods
        private void SaveMethod()
        {
            
            if (IsNewStudent)
            {
                
                if (ValidationDataStudent())
                {
                    if (students.Count == 0)
                    {
                        Student.Id = 0;
                    }
                    else
                    {
                        Student.Id = students.Last().Id + 1;
                    }
                    students.Add(Student);
                    MessageBox.Show(StringsResources.StudentSuccessfullyAddedMessage, StringsResources.ProjectName, MessageBoxButton.OK);
                    Student = new StudentModel();
                }
               
            }
            else
            {
                if (ValidationDataStudent())
                {
                    var st = students.FirstOrDefault(x => x.Id == Student.Id);
                    if (st != null)
                    {
                        st.FirstName = Student.FirstName;
                        st.LastName = Student.LastName;
                        st.Gender = Student.Gender;
                        st.Age = Student.Age;
                    }
                    MessageBox.Show(StringsResources.ChangesSaved, StringsResources.ProjectName, MessageBoxButton.OK);
                }
            }


        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool ValidationDataStudent()
        {
            string pattern = StringsResources.PatternSearchNumber;

            if (String.IsNullOrEmpty(Student.FirstName) || Regex.IsMatch(Student.FirstName, pattern))
            {
                MessageBox.Show(StringsResources.ErrorNoDataFName, StringsResources.ProjectName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (String.IsNullOrEmpty(Student.LastName) || Regex.IsMatch(Student.LastName, pattern))
            {
                MessageBox.Show(StringsResources.ErrorNoDataLName, StringsResources.ProjectName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Student.Gender != 0 && Student.Gender != 1)
            {
                MessageBox.Show(StringsResources.ErrorNoDataGender, StringsResources.ProjectName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Student.Age <= 16 || Student.Age >= 100 && Student.Age != 0)
            {
                MessageBox.Show(StringsResources.ErrorNoDataAge, StringsResources.ProjectName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        #endregion
    }
}

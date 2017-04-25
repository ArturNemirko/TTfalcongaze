using Logic;
using Logic.DAL;
using Logic.Entities;
using Microsoft.Win32;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TTFalcongaze.ViewModel.Ioc;
using Logic.Logger;

namespace TTFalcongaze.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private readonly IXMLRepository _xmlRepository = IocKernel.Get<XMLRepository>();

        #region Constructor
        public MainViewModel()
        {
            Logger.InitLogger();
            OpenDocumentCommand = new Command(arg => OpenDocumentMethod());
            SaveAsDocumentCommand = new Command(arg => SaveAsDocumentMethod());
            SaveDocumentCommand = new Command(arg => SaveDocumentMethod());
            ShowWindowAddingStudentCommand = new Command(arg => ShowWindowAddingStudentMethod());
            ShowWindowEditStudentCommand = new Command(arg => ShowWindowEditStudentMethod(), CanExecuteAddClientCommand);
            DeleteStudentCommand = new Command(arg => DeleteStudentMethod(), CanExecuteAddClientCommand);
            Students = new ObservableCollection<StudentModel>();
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        private ObservableCollection<StudentModel> students;

        public ObservableCollection<StudentModel> Students
        {
            get
            {
                return students;
            }
            set
            {
                if (value != students)
                {
                    students = value;
                    OnPropertyChanged("Students");
                }
            }
        }

        public StudentModel CurrentStudent { get; set; }

        public ObservableCollection<StudentModel> CurrentsStudents { get; set; }
        #endregion

        #region Commands
        public ICommand OpenDocumentCommand { get; set; }

        public ICommand SaveDocumentCommand { get; set; }

        public ICommand SaveAsDocumentCommand { get; set; }

        public ICommand ShowWindowAddingStudentCommand { get; set; }

        public ICommand ShowWindowEditStudentCommand { get; set; }

        public ICommand DeleteStudentCommand { get; set; }

        #endregion

        #region Methods

        private void OpenDocumentMethod()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                Students = _xmlRepository.ReadXML(ofd.FileName);
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                MessageBox.Show(ex.InnerException.Message, StringsResources.ProjectName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveDocumentMethod()
        {
            _xmlRepository.SaveXML(Students);
        }

        private void SaveAsDocumentMethod()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.ShowDialog();
            _xmlRepository.SaveXML(Students, sfd.FileName);
        }
        private void ShowWindowAddingStudentMethod()
        {
            ViewShower.Show(new StudentViewModel(Students, true, null));
        }

        private void ShowWindowEditStudentMethod()
        {
            ViewShower.Show(new StudentViewModel(Students, false, CurrentStudent));
        }

        private void DeleteStudentMethod()
        {
            if(MessageBox.Show(StringsResources.RemovalStudent, StringsResources.Removal, MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                Students.Remove(CurrentStudent);
        }

        public bool CanExecuteAddClientCommand(object parameter)
        {
            if (CurrentStudent == null)
                return false;
            return true;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}

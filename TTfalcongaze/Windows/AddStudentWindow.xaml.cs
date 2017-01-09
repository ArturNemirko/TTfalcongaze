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
using Logic;
using Logic.DAL;
using Logic.Entities;

namespace TTfalcongaze
{
    /// <summary>
    /// Interaction logic for AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        private readonly UnitOfWork _unitOfWork = UnitOfWork.getInstance();
        private Student student;
        private const byte minAge = 16;
        public AddStudentWindow()
        {
            InitializeComponent();
            student = new Student();
            List<int> ages = new List<int>();
            for(int i= minAge; i<=100; ++i)
                ages.Add(i);

            List<string> sex = new List<string>();
            sex.Add(StringsResources.Male);
            sex.Add(StringsResources.Female);
            
            AgeComboBox.ItemsSource = ages;
            SexComboBox.ItemsSource = sex;

            this.DataContext = student;
        }

        private void SexComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = SexComboBox.SelectedItem;
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(StudentFirstNameTextBox.Text))
            {
                MessageBox.Show(StringsResources.ErrorNoDataFName, StringsResources.ProjectName, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (String.IsNullOrEmpty(StudentLastNameTextBox.Text))
            {
                MessageBox.Show(StringsResources.ErrorNoDataLName, StringsResources.ProjectName, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SexComboBox.SelectedIndex < 0)
            {
                MessageBox.Show(StringsResources.ErrorNoDataGender, StringsResources.ProjectName, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (student.Age < minAge)
                student.Age = minAge;
            student.Gender = Convert.ToByte(SexComboBox.SelectedIndex);
            _unitOfWork.Students.Create(student);
            _unitOfWork.Save();
            MessageBox.Show(StringsResources.StudentSuccessfullyAddedMessage, StringsResources.ProjectName, MessageBoxButton.OK);
            Close();
        }
    }
}

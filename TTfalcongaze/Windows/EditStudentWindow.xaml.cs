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

namespace TTfalcongaze.Windows
{
    /// <summary>
    /// Interaction logic for EditStudentWindow.xaml
    /// </summary>
    public partial class EditStudentWindow : Window
    {
        private readonly UnitOfWork _unitOfWork = UnitOfWork.getInstance();
        private Student student;

        private const byte minAge = 16;
        public EditStudentWindow()
        {
            InitializeComponent();
        }

        public EditStudentWindow(int id)
        {
            InitializeComponent();
            student = _unitOfWork.Students.GetElement(id);
            MainGrid.DataContext = student;

            List<int> ages = new List<int>();
            for (int i = minAge; i <= 100; ++i)
                ages.Add(i);

            AgeComboBox.ItemsSource = ages;
            if (student.Age != null)
                AgeComboBox.SelectedIndex = student.Age - minAge;

            List<string> sex = new List<string>();
            sex.Add(StringsResources.Male);
            sex.Add(StringsResources.Female);

            SexComboBox.ItemsSource = sex;
            SexComboBox.SelectedIndex = student.Gender;


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
            student.Gender = Convert.ToByte(SexComboBox.SelectedIndex);
            _unitOfWork.Students.Update(student);
            _unitOfWork.Save();
            MessageBox.Show(StringsResources.DataSaved, StringsResources.ProjectName, MessageBoxButton.OK,
                MessageBoxImage.Information);
            Close();
        }
    }
}

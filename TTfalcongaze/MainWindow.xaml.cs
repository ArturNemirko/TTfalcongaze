using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml;
using Logic;
using Logic.DAL;
using Logic.Entities;
using TTfalcongaze.Windows;

namespace TTfalcongaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UnitOfWork _unitOfWork = UnitOfWork.getInstance();

        public MainWindow()
        {
            InitializeComponent();
            UpdateList();
            StudentsListBox.SelectedIndex = 0;
        }

        private void StudentsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StudentsListBox.SelectedIndex >= 0)
            {
                DeleteMenuItem.IsEnabled = true;

                if (StudentsListBox.SelectedItems.Count > 1)
                    EditStudentMenuItem.IsEnabled = false;
                else
                {
                    EditStudentMenuItem.IsEnabled = true;
                    StudentGrid.DataContext = StudentsListBox.SelectedItem as Student;
                }
            }
            else
            {
                StudentGrid.DataContext = null;
            }
        }
        

        private void AddStudent_OnClick(object sender, RoutedEventArgs e)
        {
            Window window = new AddStudentWindow();
            window.ShowDialog();
            UpdateList();
        }

        private void EditStudentMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Window window = new EditStudentWindow((StudentsListBox.SelectedItem as Student).Id);
                window.ShowDialog();
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException();
            }
            UpdateList();

        }

        private void DeleteMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StudentsListBox.SelectedItems.Count > 1)
                {
                    if (MessageBox.Show(String.Format(StringsResources.RemovalOfSeveralStudents, StudentsListBox.SelectedItems.Count), StringsResources.Removal, MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        List<int> id = new List<int>();
                        foreach (var item in StudentsListBox.SelectedItems)
                        {
                            id.Add((item as Student).Id);
                        }
                        _unitOfWork.Students.Delete(id);
                        _unitOfWork.Save();
                    }
                }
                else if (MessageBox.Show(StringsResources.RemovalStudent, StringsResources.Removal, MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _unitOfWork.Students.Delete((StudentsListBox.SelectedItem as Student).Id);
                    _unitOfWork.Save();
                }
                else
                    return;
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException();
            }
            UpdateList();
        }

        private void UpdateList()
        {
            EditStudentMenuItem.IsEnabled = false;
            DeleteMenuItem.IsEnabled = false;

            StudentsListBox.ItemsSource = _unitOfWork.Students.GetAll();
            if (StudentsListBox.Items.Count == 0)
            {
                NotElementTextBlock.Visibility = Visibility.Visible;
                StudentsListBox.Visibility = Visibility.Hidden;
            }
            else
            {
                NotElementTextBlock.Visibility = Visibility.Hidden;
                StudentsListBox.Visibility = Visibility.Visible;
            }

        }
    }
}

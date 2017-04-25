using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Logic.Entities
{
    [Serializable]
    [XmlType("Student")]
    public class StudentModel : ICloneable, IDataErrorInfo, INotifyPropertyChanged
    {

        private int id;
        private string firstName;
        private string lastName;
        private byte age;
        private byte gender;

        #region Properties
        [XmlAttribute]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        [XmlElement("Last")]
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (value != lastName)
                {
                    lastName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public byte Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value != age)
                {
                    age = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public byte Gender
        {
            get
            {
                return gender;
            }
            set
            {
                if (value != gender)
                {
                    gender = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string pattern = StringsResources.PatternSearchNumber;
                string error = String.Empty;
                switch (columnName)
                {
                    case "Age":
                        if ((Age < 16) || (Age > 100))
                        {
                            error = StringsResources.ErrorNoDataAge;
                        }
                        break;
                    case "FirstName":
                        if(String.IsNullOrEmpty(FirstName) || Regex.IsMatch(FirstName, pattern))
                        {
                            error = StringsResources.ErrorNoDataFName;
                        }
                        break;
                    case "LastName":
                        if (String.IsNullOrEmpty(LastName) || Regex.IsMatch(LastName, pattern))
                        {
                            error = StringsResources.ErrorNoDataLName;
                        }
                        break;
                    case "Gender":
                        if (Gender != 0 && Gender != 1)
                        {
                            error = StringsResources.ErrorNoDataGender;
                        }
                        break;
                }
                return error;
            }
        }

        public StudentModel() { }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object Clone()
        {
            return new StudentModel() { Id = this.Id, FirstName = this.FirstName, LastName = this.LastName, Age = this.Age, Gender = this.Gender };
        }


    }
}

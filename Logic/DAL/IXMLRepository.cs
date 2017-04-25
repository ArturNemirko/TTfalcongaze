using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DAL
{
    public interface IXMLRepository
    {
        ObservableCollection<StudentModel> ReadXML(string path);
        void SaveXML(ObservableCollection<StudentModel> students);
        void SaveXML(ObservableCollection<StudentModel> students, string path);
    }
}

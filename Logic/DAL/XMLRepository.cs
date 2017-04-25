using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Logic.DAL
{
    public class XMLRepository : IXMLRepository
    {
        private string pathXML;

        private XmlSerializer formatter;

        public XMLRepository()
        {
            XmlRootAttribute myRoot = new XmlRootAttribute();
            myRoot.ElementName = StringsResources.Students;

            formatter = new XmlSerializer(typeof(ObservableCollection<StudentModel>), myRoot);
        }

        public ObservableCollection<StudentModel> ReadXML(string path)
        {
            pathXML = path;
            ObservableCollection<StudentModel> students = null;
            try
            {
                using (FileStream fs = new FileStream(pathXML, FileMode.OpenOrCreate))
                {
                    students = formatter.Deserialize(fs) as ObservableCollection<StudentModel>;
                }                                            
            }
            catch(Exception ex)
            {
                throw new Exception(StringsResources.ExceptionReadXML, ex);
            }
            return students;
        }

        public void SaveXML(ObservableCollection<StudentModel> students)
        {
            if (pathXML != null)
                SaveXML(students, pathXML);
            else
                SaveXML(students, StringsResources.ConnectionStudentsFile);
        }

        public void SaveXML(ObservableCollection<StudentModel> students, string path)
        {
            File.Delete(path);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, students, ns);
            }
        }


    }
}

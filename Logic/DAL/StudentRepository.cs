using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Entities;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace Logic.DAL
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly XmlDocument xDoc = new XmlDocument();
        private readonly XmlElement xRoot;


        private int lastID = 0;

        public StudentRepository(XmlDocument xDoc)
        {
            this.xDoc = xDoc;
            xRoot = xDoc.DocumentElement;
            LastIdInRoot();
        }

        public void Create(Student item)
        {
            XmlElement xRoot = xDoc.DocumentElement;

            XmlElement studentElem = xDoc.CreateElement(StringsResources.Student);
            XmlAttribute studentIDAttr = xDoc.CreateAttribute(StringsResources.StudentID);

            XmlElement studentFirstNameElem = xDoc.CreateElement(StringsResources.StudentFirstName);
            XmlElement studentLastNameElem = xDoc.CreateElement(StringsResources.StudentLastName);
            XmlElement studentAgeElem = xDoc.CreateElement(StringsResources.StudentAge);
            XmlElement studentGenderElem = xDoc.CreateElement(StringsResources.StudentGender);

            XmlText idText = xDoc.CreateTextNode((++lastID).ToString());
            XmlText firstNameText = xDoc.CreateTextNode(item.FirstName);
            XmlText lastNameText = xDoc.CreateTextNode(item.LastName);
            XmlText ageText = xDoc.CreateTextNode(item.Age.ToString());
            XmlText genderText = xDoc.CreateTextNode(item.Gender.ToString());

            studentIDAttr.AppendChild(idText);
            studentFirstNameElem.AppendChild(firstNameText);
            studentLastNameElem.AppendChild(lastNameText);
            studentAgeElem.AppendChild(ageText);
            studentGenderElem.AppendChild(genderText);

            studentElem.Attributes.Append(studentIDAttr);
            studentElem.AppendChild(studentFirstNameElem);
            studentElem.AppendChild(studentLastNameElem);
            studentElem.AppendChild(studentAgeElem);
            studentElem.AppendChild(studentGenderElem);

            xRoot.AppendChild(studentElem);
        }

        public void Delete(List<int> id)
        {
            foreach (var item in id)
            {
                XmlNode node = GetNode(item);
                if (node != null)
                    xRoot.RemoveChild(node);
            }
            FreeID();
        }
        public void Delete(int id)
        {
            XmlNode node = GetNode(id);
            if (node != null)
                xRoot.RemoveChild(node);      
            FreeID();     
        }

        private void FreeID()
        {
            int freeID = 0;
            foreach (XmlNode xnode in xRoot)
            {
                Student student = new Student();
                try
                {
                    if (xnode.Attributes.Count > 0)
                    {
                        XmlNode attr = xnode.Attributes.GetNamedItem(StringsResources.StudentID);
                        if (attr != null && Convert.ToInt32(attr.Value) > freeID)
                        {
                            attr.Value = freeID.ToString();
                        }
                        ++freeID;
                    }
                }
                catch (FormatException)
                {
                    throw new FormatException();
                }
            }
            LastIdInRoot();
        }

        public IEnumerable<Student> GetAll()
        {
            List<Student> students = new List<Student>();
            foreach (XmlNode xnode in xRoot)
            {
                Student student = DeserializeXmlNodeToStudent(xnode);
                if (student != null)
                    students.Add(student);
            }
            return students;
        }

        public Student GetElement(int id)
        {
            return DeserializeXmlNodeToStudent(id);
        }

        public void Update(Student student)
        {
            var xmlNode = GetNode(student.Id);
            try
            {
                foreach (XmlNode childnode in xmlNode.ChildNodes)
                {
                    if (childnode.Name == StringsResources.StudentFirstName)
                    {
                        childnode.InnerText = student.FirstName;
                        continue;
                    }
                    if (childnode.Name == StringsResources.StudentLastName)
                    {
                        childnode.InnerText = student.LastName;
                        continue;
                    }
                    if (childnode.Name == StringsResources.StudentAge)
                    {
                        childnode.InnerText = student.Age.ToString();
                        continue;
                    }
                    if (childnode.Name == StringsResources.StudentGender)
                    {
                        childnode.InnerText = student.Gender.ToString();
                    }
                }
            }
            catch (FormatException)
            {
                throw new FormatException();
    }
}

        protected void LastIdInRoot()
        {
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem(StringsResources.StudentID);
                    if (attr != null)
                    {
                        lastID = Convert.ToInt32(attr.Value);
                    }
                }
            }
            
        }

        protected XmlNode GetNode(int id)
        {
            foreach (XmlNode xnode in xRoot)
            {
                Student student = new Student();
                try
                {
                    if (xnode.Attributes.Count > 0)
                    {
                        XmlNode attr = xnode.Attributes.GetNamedItem(StringsResources.StudentID);
                        if (attr != null && Convert.ToInt32(attr.Value) == id)
                        {
                            return xnode;
                        }
                    }
                }
                catch (FormatException)
                {
                    throw new FormatException();
                }
            }
            return null;
        }

        protected Student DeserializeXmlNodeToStudent(int id)
        {
            XmlNode xmlNode = GetNode(id);
            if (xmlNode != null)
                return DeserializeXmlNodeToStudent(xmlNode);
            return null;
        }

        protected Student DeserializeXmlNodeToStudent(XmlNode xmlNode)
        {
            Student student = new Student();
            try
            {
                if (xmlNode.Attributes.Count > 0)
                {
                    XmlNode attr = xmlNode.Attributes.GetNamedItem(StringsResources.StudentID);
                    if (attr != null)
                    {
                        student.Id = Convert.ToInt32(attr.Value);
                    }
                }
                foreach (XmlNode childnode in xmlNode.ChildNodes)
                {
                    if (childnode.Name == StringsResources.StudentFirstName)
                    {
                        student.FirstName = childnode.InnerText;
                        continue;
                    }
                    if (childnode.Name == StringsResources.StudentLastName)
                    {
                        student.LastName = childnode.InnerText;
                        continue;
                    }
                    if (childnode.Name == StringsResources.StudentAge)
                    {
                        student.Age = Convert.ToByte(childnode.InnerText);
                        continue;
                    }
                    if (childnode.Name == StringsResources.StudentGender)
                    {
                        student.Gender = Convert.ToByte(childnode.InnerText);
                    }
                }
            }
            catch (FormatException)
            {
                throw new FormatException();
            }
            return student;
        }
    }
}

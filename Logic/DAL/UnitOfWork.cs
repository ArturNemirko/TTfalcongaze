using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Logic.DAL
{
    public class UnitOfWork : IDisposable
    {
        private static UnitOfWork instance;
        private static object syncRoot = new Object();

        private UnitOfWork()
        {
            xDoc.Load(StringsResources.ConnectionStudentsFile);
        }

        public static UnitOfWork getInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new UnitOfWork();
                }
            }
            return instance;
        }

        private XmlDocument xDoc = new XmlDocument();
        private StudentRepository studentRepository;

        public StudentRepository Students
        {
            get
            {
                if (studentRepository == null)
                    studentRepository = new StudentRepository(xDoc);
                return studentRepository;
            }
        }

        public void Save()
        {
            xDoc.Save(StringsResources.ConnectionStudentsFile);
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

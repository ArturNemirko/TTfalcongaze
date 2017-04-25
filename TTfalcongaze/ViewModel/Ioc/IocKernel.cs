using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTFalcongaze.ViewModel.Ioc
{
    public static class IocKernel
    {
        private static StandardKernel _kernel;

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public static void Initialize(INinjectModule modul)
        {
            if (_kernel == null)
            {
                _kernel = new StandardKernel(modul);
            }
        }
    }
}

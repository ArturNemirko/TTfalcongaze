using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTFalcongaze.ViewModel.Ioc
{
    class ViewModelLocator
    {
        public MainViewModel MainViewModel
        {
            get { return IocKernel.Get<MainViewModel>(); } 
        }
    }
}

using Logic.DAL;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTFalcongaze.ViewModel.Ioc
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<XMLRepository>().To<XMLRepository>().InSingletonScope(); 

            Bind<MainViewModel>().ToSelf().InTransientScope(); 
        }
    }
}

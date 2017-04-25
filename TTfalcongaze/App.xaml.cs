using Logic.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TTFalcongaze.View;
using TTFalcongaze.ViewModel;
using TTFalcongaze.ViewModel.Ioc;

namespace TTFalcongaze
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IocKernel.Initialize(new IocConfiguration());

            var mw = new MainWindow
            {
                DataContext = new MainViewModel()
            };
            mw.Show();
        }
    }
}

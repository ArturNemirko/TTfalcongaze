using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TTFalcongaze.View;

namespace TTFalcongaze.ViewModel
{
    class ViewShower
    {
        public static void Show(object p_dataContext)
        {
            UserControl control = new StudentUserControl();

            if (control != null)
            {
                Window wnd = new Window();
                control.DataContext = p_dataContext;
                StackPanel sp = new StackPanel();
                sp.Children.Add(control);
                wnd.Content = sp;
                wnd.Show();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BolsaSalesianos
{
    public static class Switcher
    {
        public static Window window;
        public static Frame frame;

        public static void SwitchPage(Page newPage)
        {
            window.Content = newPage;
        }

        /* 
         * Intercambia la pagina actual por una nueva en la ventana actual.
         */
        public static void SwitchFramePage(Page newPage)
        {
            frame.Content = newPage;
        }

        /* 
         * Intercambia la ventana actual por una nueva.
         */
        public static void SwitchWindow(Window newWindow)
        {
            window.Close();
            window = newWindow;
            window.Show();
        }
    }
}

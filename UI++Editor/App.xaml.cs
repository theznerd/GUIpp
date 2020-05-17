using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace UI__Editor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            string[] cla = Environment.GetCommandLineArgs();
            if(cla.Where(i => i.ToLower().Contains("tomanytalking")).Count() > 0)
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=KN4tfKQqtVs&lc=Ugwq5c0-yg7KoHwX-WN4AaABAg");
                Current.Shutdown(0);
            }
            InitializeComponent();
        }
    }
}

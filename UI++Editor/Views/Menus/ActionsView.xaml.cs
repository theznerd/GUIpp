using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI__Editor.Interfaces;

namespace UI__Editor.Views.Menus
{
    /// <summary>
    /// Interaction logic for ActionsView.xaml
    /// </summary>
    public partial class ActionsView : UserControl
    {
        public ActionsView()
        {
            InitializeComponent();
        }

        private void ActionsTreeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(null != ActionsTreeView.SelectedItem)
            {
                (ActionsTreeView.SelectedItem as IElement).TVSelected = false;
            }
        }

        private void SubActionsTreeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (null != SubActionsTreeView.SelectedItem)
            {
                (SubActionsTreeView.SelectedItem as IElement).TVSelected = false;
            }
        }
    }
}

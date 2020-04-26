using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.ViewModels.Menus.Actions;

namespace UI__Editor.Controllers
{
    public static class XMLToViewModel
    {
        public static IAction GetActionViewModel(XmlElement xmlElement)
        {
            // Write code to return view model based on element name
            return new AppTreeViewModel();
        }
    }
}

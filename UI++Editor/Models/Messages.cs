using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class Messages : IElement, IRootElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Status Messages"; } }
        public string RootElementType { get; } = "Messages";
        public ObservableCollection<Message> MessageCollection { get; set; }

        public Messages()
        {
            MessageCollection = new ObservableCollection<Message>()
            {
                new Message()
                {
                    Id = "COMPUTEREXISTS",
                    Content = "A computer with this name already exists. Please try another name."
                },
                new Message()
                {
                    Id = "COMPUTEREXISTSWARNING",
                    Content = "A computer with this name already exists but you may continue."
                },
                new Message()
                {
                    Id = "USERDOESNOTEXIST",
                    Content = "A user with this name does not exist. Please try another name."
                },
                new Message()
                {
                    Id = "USERDOESNOTEXISTWARNING",
                    Content = "A user with this name does not exist but you may continue."
                },
                new Message()
                {
                    Id = "AUTHENTICATING",
                    Content = "Authenticating…"
                },
                new Message()
                {
                    Id = "PREFLIGHTFAILED",
                    Content = "Please check and correct the failed checks above."
                },
                new Message()
                {
                    Id = "PREFLIGHTPASSED",
                    Content = "All checks successfully passed."
                },
                new Message()
                {
                    Id = "PREFLIGHTPASSEDWITHWARNING",
                    Content = "All checks successfully passed although there are warnings."
                }
            };
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Messages", null);

            foreach (Message m in MessageCollection)
            {
                XmlNode importNode = d.ImportNode(m.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}

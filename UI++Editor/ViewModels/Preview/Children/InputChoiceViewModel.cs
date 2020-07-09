using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using UI__Editor.EventAggregators;
using UI__Editor.Interfaces;
using UI__Editor.Models;

namespace UI__Editor.ViewModels.Preview.Children
{
    class InputChoiceViewModel : PropertyChangedBase, IChild, IPreview, IHandle<ChangeUI>
    {
        private IEventAggregator _EventAggregator;
        public IEventAggregator EventAggregator
        {
            get { return _EventAggregator; }
            set
            {
                _EventAggregator = value;
                EventAggregator.Subscribe(this);
            }
        }

        public bool PreviewRefreshButtonVisible { get { return false; } }
        public bool PreviewBackButtonVisible { get { return false; } }
        public bool PreviewCancelButtonVisible { get { return false; } }
        public bool PreviewAcceptButtonVisible { get { return false; } }
        public bool HasCustomPreview { get { return false; } }
        public string WindowHeight { get; set; }

        public InputChoiceViewModel()
        {
            EventAggregator = new EventAggregator();
        }

        public string Font
        {
            get
            {
                return Globals.DisplayFont;
            }
        }

        private string _Question;
        public string Question
        {
            get { return _Question; }
            set
            {
                _Question = value;
                NotifyOfPropertyChange(() => Question);
            }
        }

        private int _DropDownSize;
        public int DropDownSize
        {
            get { return _DropDownSize; }
            set
            {
                _DropDownSize = value;
                NotifyOfPropertyChange(() => DropDownSize);
                NotifyOfPropertyChange(() => DropDownHeight);
            }
        }

        private bool _Sort;
        public bool Sort
        {
            get { return _Sort; }
            set
            {
                _Sort = value;
                NotifyOfPropertyChange(() => Sort);
                NotifyOfPropertyChange(() => Choices);
            }
        }

        public string DropDownHeight
        {
            get
            {
                if(DropDownSize > 0)
                {
                    return (DropDownSize * 26).ToString();
                }
                else
                {
                    return "130";
                }
            }
        }

        public ObservableCollection<IChildElement> SubChildren { get; set; }

        public List<string> Choices
        {
            get
            {
                List<string> returnList = new List<string>();
                foreach(IChildElement subchild in SubChildren)
                {
                    if(subchild is Choice)
                    {
                        returnList.Add((subchild as Choice).Option);
                    }
                    if(subchild is ChoiceList)
                    {
                        if(null != (subchild as ChoiceList).OptionList)
                        {
                            returnList.AddRange((subchild as ChoiceList).OptionList.Split(','));
                        }
                    }
                }
                if (Sort)
                {
                    returnList.Sort();
                    return returnList;
                }
                else
                {
                    return returnList;
                }
            }
        }

        public void Handle(ChangeUI message)
        {
            if(message.Type == "PreviewChange")
            {
                NotifyOfPropertyChange(() => Choices);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
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

namespace UI__Editor.Views.Preview
{
    /// <summary>
    /// Interaction logic for InfoView.xaml
    /// </summary>
    public partial class ErrorInfoView : UserControl
    {
        public ErrorInfoView()
        {
            InitializeComponent();
        }

        private void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            string css = "body { margin:0; padding:0; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: 11px; }";
            string script = "document.body.style.overflow ='hidden'";
            WebBrowser wb = (WebBrowser)sender;
            wb.InvokeScript("execScript", new Object[] { script, "JavaScript" });
            mshtml.HTMLDocument CurrentDocument = (mshtml.HTMLDocument)wb.Document;
            mshtml.IHTMLStyleSheet styleSheet = CurrentDocument.createStyleSheet("", 0);
            styleSheet.cssText = css;
        }
    }
}

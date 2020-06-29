using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UI__Editor.Controllers
{
    public static class BrowserBehavior
    {
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
        "Html",
        typeof(string),
        typeof(BrowserBehavior),
        new FrameworkPropertyMetadata(OnHtmlChanged));

        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static string GetHtml(WebBrowser d)
        {
            return (string)d.GetValue(HtmlProperty);
        }

        public static void SetHtml(WebBrowser d, string value)
        {
            d.SetValue(HtmlProperty, value);
        }

        static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser wb = d as WebBrowser;
            if (wb != null && !string.IsNullOrEmpty(e.NewValue as string))
            {
                string body = "<html><head><style>body {font-family: " + Globals.DisplayFont + ";}</style><body>";
                body += (e.NewValue as string);
                body += "</body></html>";
                wb.NavigateToString(body);
            }
            else
            {
                wb.NavigateToString(" ");
            }
                
        }
    }
}

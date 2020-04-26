using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace UI__Editor.Converters
{
    // "Borrowed" from https://stackoverflow.com/questions/19560466/how-to-make-wpf-treeview-style-as-winforms-treeview
    public class LastItemTreeViewLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TreeViewItem item = (TreeViewItem)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
            return ic.ItemContainerGenerator.IndexFromContainer(item) == ic.Items.Count - 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }

    class FirstItemTreeViewHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((double)value) - 9;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }

    class FirstItemTreeViewLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TreeViewItem item = (TreeViewItem)value;
            TreeView ParentTreeView = ParentOfType<TreeView>(item);
            TreeViewItem tvi = (TreeViewItem)ParentTreeView.ItemContainerGenerator.ContainerFromIndex(0);
            return item == tvi;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public T ParentOfType<T>(DependencyObject element) where T : DependencyObject
        {
            if (element == null)
                return default(T);
            else
                return Enumerable.FirstOrDefault<T>(Enumerable.OfType<T>((IEnumerable)GetParents(element)));
        }

        public IEnumerable<DependencyObject> GetParents(DependencyObject element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            while ((element = GetParent(element)) != null)
                yield return element;
        }

        private DependencyObject GetParent(DependencyObject element)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            if (parent == null)
            {
                FrameworkElement frameworkElement = element as FrameworkElement;
                if (frameworkElement != null)
                    parent = frameworkElement.Parent;
            }
            return parent;
        }
    }
}

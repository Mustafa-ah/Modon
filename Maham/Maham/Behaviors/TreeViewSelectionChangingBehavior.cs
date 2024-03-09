using System;
using Maham.Models;
using Syncfusion.XForms.TreeView;
using Xamarin.Forms;

namespace Maham.Behaviors
{
    public class TreeViewSelectionChangingBehavior : BehaviorBase<SfTreeView>
    {
        SfTreeView TreeView;
        protected override void OnAttachedTo(SfTreeView treeView)
        {
            TreeView = treeView;
            TreeView.SelectionChanging += TreeView_SelectionChanging;
            base.OnAttachedTo(treeView);
        }
        private void TreeView_SelectionChanging(object sender, Syncfusion.XForms.TreeView.ItemSelectionChangingEventArgs e)
        {
            if (TreeView.SelectionMode == Syncfusion.XForms.TreeView.SelectionMode.Single)
            {
                if (e.AddedItems.Count > 0)
                {
                    var item = e.AddedItems[0] as Entity;
                    item.LabelColor = Color.Red;
                }
                if (e.RemovedItems.Count > 0)
                {
                    var item = e.RemovedItems[0] as Entity;
                    item.LabelColor = Color.Black;
                }
            }
        }
        protected override void OnDetachingFrom(SfTreeView bindable)
        {
            TreeView.SelectionChanging -= TreeView_SelectionChanging;
            base.OnDetachingFrom(bindable);
        }
    }
}

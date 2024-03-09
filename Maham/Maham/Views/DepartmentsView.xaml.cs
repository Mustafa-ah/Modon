using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maham.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maham.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DepartmentsView : Rg.Plugins.Popup.Pages.PopupPage
    {
        DepartmentsViewModel ViewModel;
        public DepartmentsView(bool AsUser, string userId, bool forEdit)
        {
            InitializeComponent();

            ViewModel = this.BindingContext as DepartmentsViewModel;

            ViewModel.AsUser = AsUser;
            ViewModel.UserId = userId;
            ViewModel.ForEdit = forEdit;

            ViewModel.GetData();
        }
    }
}
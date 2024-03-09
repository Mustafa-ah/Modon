using Maham.Service.Model.Response.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maham.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeControlPage
    {
        public static readonly BindableProperty SearchtextProperty =
           BindableProperty.Create(nameof(Searchtext), typeof(string), typeof(EmployeeControlPage), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty ListSourceProperty =
           BindableProperty.Create(nameof(ListSource), typeof(ObservableCollection<ResponsiblesDDL>), typeof(EmployeeControlPage), null, BindingMode.TwoWay);
        public static readonly BindableProperty ItemCheckedCommandProperty =
            BindableProperty.Create(nameof(ItemCheckedCommand), typeof(ICommand), typeof(EmployeeControlPage));

        public string Searchtext
        {
            get
            {
                return (string)GetValue(SearchtextProperty);
            }
            set
            {
                if (value != null)
                    SetValue(SearchtextProperty, value);
            }
        }
        public ObservableCollection<ResponsiblesDDL> ListSource
        {
            get
            {
                return (ObservableCollection<ResponsiblesDDL>)GetValue(ListSourceProperty);
            }
            set
            {
                if (value != null)
                {
                    SetValue(ListSourceProperty, value);
                }
            }
        }
        public ICommand ItemCheckedCommand
        {
            get { return (ICommand)GetValue(ItemCheckedCommandProperty); }
            set { SetValue(ItemCheckedCommandProperty, value); }
        }

        public EmployeeControlPage()
        {
            InitializeComponent();
          
        }
    }
}
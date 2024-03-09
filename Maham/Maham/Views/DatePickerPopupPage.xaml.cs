using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Maham.Views
{
    public partial class DatePickerPopupPage : PopupPage
    {
        private TaskCompletionSource<DateTime> _TaskCompletion;// = new TaskCompletionSource<DateTime>();

        DateTime initDate;

        public DatePickerPopupPage(DateTime dateTime)
        {
            InitializeComponent();

            initDate = dateTime;
        }

        private void SetPicker()
        {
            DatePicker.IsEnabled = true;
            DatePicker.IsVisible = true;
            DatePicker.Focus();
        }



        public async Task<DateTime> ShowDatePicker()
        {
            _TaskCompletion = new TaskCompletionSource<DateTime>();

            await this.Navigation.PushPopupAsync(this);

            SetPicker();

            return await _TaskCompletion.Task;
        }

        void DatePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            //if (_TaskCompletion != null)
            //{
            //    _TaskCompletion.SetResult(e.NewDate);

            //     _TaskCompletion = null;
            //}
            //this.Navigation.PopPopupAsync();
        }


        void CancelBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            SetResult(initDate);
            this.Navigation.PopPopupAsync();
        }

        void DatePicker_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            SetResult(DatePicker.Date);
            this.Navigation.PopPopupAsync();
        }

        private void SetResult( DateTime dateTime)
        {
            if (_TaskCompletion != null)
            {
                _TaskCompletion.SetResult(dateTime);

                _TaskCompletion = null;
            }
        }
    }
}

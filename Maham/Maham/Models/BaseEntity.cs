using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Maham.Models
{
    public class BaseEntity : INotifyPropertyChanged
    {

        public string id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}

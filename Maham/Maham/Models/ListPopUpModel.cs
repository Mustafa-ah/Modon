using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Models
{
    public class ListPopUpModel : BaseEntity
    {

       
        public int id { get; set; }
        public string name { get; set; }
        public string nameAr { get; set; }
        public string checkImage { get; set; }
        public string image2 { get; set; }
        
        public bool IsActive { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }
    }
}

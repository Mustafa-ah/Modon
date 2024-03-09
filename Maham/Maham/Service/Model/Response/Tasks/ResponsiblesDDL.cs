using System;
using System.Collections.Generic;
using System.Text;
using Maham.Bases;

namespace Maham.Service.Model.Response.Tasks
{
    public class ResponsiblesDDL : BaseModel
    {
        public string ArabicText { get; set; }
        public string Text { get; set; }
        public Value2 Value2 { get; set; }

        private bool _isCheckedemployee;
        public bool IsCheckedemployee
        {
            get => _isCheckedemployee;
            set
            {
                _isCheckedemployee = value;
                OnPropertyChanged();
            }
        }

    }
    public class EmergencyCallsDDL
    {
        public string ArabicText { get; set; }
        public string Text { get; set; }
        public Value3 Value3 { get; set; }
        public bool IsCheckedemployee { get; set; }

    }
}

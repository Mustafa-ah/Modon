using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Maham.Models
{
    public class Entity : BaseEntity
    {
        public Guid Value { get; set; }
        public string Text { get; set; }
        public List<Entity> Childern { get; set; }
        public bool IsChecked { get; set; }
        public bool Active { get; set; }

        private Color labelColor = Color.Black;
        public Color LabelColor
        {
            get { return labelColor; }
            set
            {
                labelColor = value;
                OnPropertyChanged();
            }
        }


    }
}

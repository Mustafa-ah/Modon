using System;
using Maham.Bases;
using Xamarin.Forms;

namespace Maham.Service.Model.Response.Priorities
{
    public class PriorirtiesDetailsResponse : BaseModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string progress { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int status_Id { get; set; }
        public string status { get; set; }
        public int priority_Id { get; set; }
        public string priority { get; set; }
        public int assignedTo_ID { get; set; }
        public string assignedTo { get; set; }
        public string sector { get; set; }
        public string fK_UrgentSupportID { get; set; }
        public double average_progress { get; set; }
        
        //public ImageSource _urgentSupport;
        //public ImageSource UrgentSupport
        //{
        //    get
        //    {
        //        try
        //        {
        //            int x = Convert.ToInt32(fK_UrgentSupportID);
        //            if (fK_UrgentSupportID == null)
        //            {
        //                OnPropertyChanged();
        //                return _urgentSupport = "";

        //            }
        //            else if (x > 0)
        //            {
        //                OnPropertyChanged();
        //                return _urgentSupport = "RedAlarm.png";

        //            }
        //            else
        //            {
        //                OnPropertyChanged();
        //                return _urgentSupport = "";

        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            OnPropertyChanged();
        //            return _urgentSupport = "";

        //        }

        //    }
        //}

        //public ImageSource StatusImage
        //{
        //    get
        //    {
        //        switch (status_Id)
        //        {
        //            case 12:
        //                return "Returned.png";
        //            case 11:
        //                return "Closed.png";
        //            case 1:
        //                return "DidnotStart.png";
        //            case 2:
        //                return "InProgress.png";
        //            case 4:
        //                return "Completed.png";
        //            default:
        //                return "DidnotStart.png";
        //        }
        //    }
        //}

        //public Color ColumnColor
        //{
        //    get
        //    {
        //        switch (priority_Id)
        //        {
        //            //high
        //            case 3:
        //                return (Color)App.Current.Resources["HighColor"];
        //            //normal
        //            case 2:
        //                return (Color)App.Current.Resources["NormalColor"];
        //            //low
        //            case 1:
        //                return (Color)App.Current.Resources["LowColor"];
        //            //critical
        //            case 4:
        //                return (Color)App.Current.Resources["CriticalColor"];

        //            default:
        //                return (Color)App.Current.Resources["CriticalColor"];
        //        }
        //    }
        //}

        //public Color ProgressColor
        //{
        //    get
        //    {
        //        double progressValued = Convert.ToDouble(progress.Trim());
        //        int progressValue = Convert.ToInt32(progressValued);
        //        if (status_Id == 1)
        //        {
        //            return (Color)App.Current.Resources["lightgrey5"];
        //        }
        //        else if (status_Id == 2)
        //        {
        //            return (Color)App.Current.Resources["lightblue3"];

        //        }
        //        else if (status_Id == 4)
        //        {
        //            return (Color)App.Current.Resources["Greenlight2"];

        //        }
        //        else if (status_Id == 11)
        //        {
        //            return (Color)App.Current.Resources["Greenlight1"];


        //        }
        //        else if (status_Id == 12)
        //        {
        //            return (Color)App.Current.Resources["BlueDark"];

        //        }

        //        else if (status_Id == 3)
        //        {
        //            return (Color)App.Current.Resources["redcolor"];

        //        }
        //        else
        //        {
        //            //Default
        //            return (Color)App.Current.Resources["BlueDark"];
        //        }
        //    }
        //}

        //public bool IsClosed
        //{
        //    get
        //    {
        //        if (status_Id == 11)
        //        {
        //            return true;
        //        }

        //        return false;
        //    }

        //}

        //public Color TextColor
        //{
        //    get
        //    {
        //        double progressValued = Convert.ToDouble(progress.Trim());
        //        int progressValue = Convert.ToInt32(progressValued);
        //        if (status_Id == 3)
        //        {
        //            return Color.FromHex("#d0021b");
        //        }


        //        else
        //        {
        //            //Default
        //            return Color.FromHex("#2196F3");
        //        }
        //    }
        //}
    }
}

using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using Maham.Bases;
using Xamarin.Forms;

namespace Maham.Models
{
    public class TaskItem : BaseModel
    {
        private string _status;
        private int _priority_Id;
        private string _priority;
        private int _assignedTo_ID;
        private string _assignedTo;
        private string _sector;
        private string _fK_UrgentSupportID;
        private double _average_progress;
        private int _flag_id;
        private string _flag;
        private Guid _id;
        private string _title;
        private string _progress;
        private string _description;
        private string _startDate;
        private string _endDate;
        private bool _isClosed;


        private int _status_Id;

        public int Status_Id
        {
            get { return _status_Id; }
            set
            {
                _status_Id = value;
                OnPropertyChanged();
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }
        public int Priority_Id
        {
            get { return _priority_Id; }
            set
            {
                _priority_Id = value;
                OnPropertyChanged();
            }
        }

        public string Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                OnPropertyChanged();
            }
        }

        public int AssignedTo_ID
        {
            get { return _assignedTo_ID; }
            set
            {
                _assignedTo_ID = value;
                OnPropertyChanged();
            }
        }

        public string AssignedTo
        {
            get { return _assignedTo; }
            set
            {
                _assignedTo = value;
                OnPropertyChanged();
            }
        }

        public string Sector
        {
            get { return _sector; }
            set
            {
                _sector = value;
                OnPropertyChanged();
            }
        }
        public string FK_UrgentSupportID
        {
            get { return _fK_UrgentSupportID; }
            set
            {
                _fK_UrgentSupportID = value;
                OnPropertyChanged();
            }
        }
        public double Average_progress
        {
            get { return _average_progress; }
            set
            {
                _average_progress = value;
                OnPropertyChanged();
            }
        }
        public int Flag_id
        {
            get { return _flag_id; }
            set
            {
                _flag_id = value;
                OnPropertyChanged();
            }
        }

        public string Flag
        {
            get { return _flag; }
            set
            {
                _flag = value;
                OnPropertyChanged();
            }
        }
        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string Progress
        {
            get { return _progress; }
            set
            {
               
                _progress = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public string StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public string EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }
        public ImageSource _urgentSupport;
        public ImageSource UrgentSupport
        {
            get
            {
                try
                {
                    int x = Convert.ToInt32(_fK_UrgentSupportID);
                    if (_fK_UrgentSupportID == null)
                    {
                        OnPropertyChanged();
                        return _urgentSupport = "";

                    }
                    else if (x > 0)
                    {
                        OnPropertyChanged();
                        return _urgentSupport = "RedAlarm.png";

                    }
                    else
                    {
                        OnPropertyChanged();
                        return _urgentSupport = "";

                    }
                }
              
                catch (Exception exception)
                {
                   
                    var properties = new Dictionary<string, string>
                       {
                             { "TaskItem", "urgent support" },
                       };
                    Crashes.TrackError(exception, properties);
                    OnPropertyChanged();
                    return _urgentSupport = "";
                }

            }
        }

        public ImageSource StatusImage
        {
            get
            {
                switch (Status_Id)
                {
                    case 12:
                        return "Returned.png";
                    case 11:
                        return "Closed.png";
                    case 1:
                        return "DidnotStart.png";
                    case 2:
                        return "InProgress.png";
                    case 4:
                        return "Completed.png";
                    default:
                        return "DidnotStart.png";
                }
            }
        }

        public Color ColumnColor
        {
            get
            {
                switch (Priority_Id)
                {
                    //high
                    case 3:
                        return (Color)App.Current.Resources["HighColor"];
                    //normal
                    case 2:
                        return (Color)App.Current.Resources["NormalColor"];
                    //low
                    case 1:
                        return (Color)App.Current.Resources["LowColor"];
                    //critical
                    case 4:
                        return (Color)App.Current.Resources["CriticalColor"];

                    default:
                        return (Color)App.Current.Resources["CriticalColor"];
                }
            }
        }

        public Color ProgressColor
        {
            get
            {
                double progressValued = Convert.ToDouble(_progress.Trim());
                int progressValue = Convert.ToInt32(progressValued);
                if (_status_Id == 1)
                {
                    return (Color)App.Current.Resources["lightgrey5"];
                }
                else if (_status_Id == 2)
                {
                    return (Color)App.Current.Resources["lightblue3"];

                }
                else if (_status_Id == 4)
                {
                    return (Color)App.Current.Resources["Greenlight2"];

                }
                else if (_status_Id == 11)
                {
                    return (Color)App.Current.Resources["Greenlight1"];


                }
                else if (_status_Id == 12)
                {
                    return (Color)App.Current.Resources["BlueDark"];

                }

                else if (_status_Id == 3)
                {
                    return (Color)App.Current.Resources["redcolor"];

                }
                else
                {
                    //Default
                    return (Color)App.Current.Resources["BlueDark"];
                }
            }
        }

        public bool IsClosed
        {
            get
            {
                if (_status_Id == 11)
                {
                    return true;
                }

                return false;
            }

        }


        public Color TextColor
        {
            get
            {
                double progressValued = Convert.ToDouble(_progress.Trim());
                int progressValue = Convert.ToInt32(progressValued);
                if (_status_Id == 3)
                {
                    return Color.FromHex("#d0021b");
                }


                else
                {
                    //Default
                    return Color.FromHex("#2196F3");
                }
            }
        }
        public TaskItem()
        {

        }
        public TaskItem(int status_Id, string status, int priority_Id, string priority, int assignedTo_ID
            , string assignedTo, string sector, string fK_UrgentSupportID, double average_progress, int flag_id, string flag, Guid id, string title
            , string progress, string description, string startDate, string endDate, bool isClosed)
        {
            status_Id = Status_Id;
            status = Status;
            priority_Id = Priority_Id;
            priority = Priority;
            assignedTo_ID = AssignedTo_ID;
            assignedTo = AssignedTo;
            sector = Sector;
            fK_UrgentSupportID = FK_UrgentSupportID;
            average_progress = Average_progress;
            flag_id = Flag_id;
            flag = Flag;
            id = Id;
            title = Title;
            progress = Progress;
            description = Description;
            startDate = StartDate;
            endDate = EndDate;
            isClosed = IsClosed;
        }
    }
}

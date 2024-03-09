using Prism.Navigation;
using System;
using Maham.Bases;
using Maham.Models;
using Xamarin.Forms;

namespace Maham.ViewModels
{
    public class TaskViewModel 
    {
        private TaskItem _taskItem;
        public NotPrioritiesPageViewModel ParentViewModel { get; }
        public TaskViewModel(TaskItem taskItem, NotPrioritiesPageViewModel parentViewModel)
        {
            this._taskItem = taskItem;
            ParentViewModel = parentViewModel;
        }

        public int Status_Id { get { return _taskItem.Status_Id; } }

        public string Status { get { return _taskItem.Status; } }

        public int Priority_Id { get { return _taskItem.Priority_Id; } }

        public string Priority { get { return _taskItem.Priority; } }

        public int AssignedTo_ID { get { return _taskItem.AssignedTo_ID; } }
        public string AssignedTo { get { return _taskItem.AssignedTo; } }
        public string Sector { get { return _taskItem.Sector; } }

        public string FK_UrgentSupportID { get { return _taskItem.FK_UrgentSupportID; } }

        public double Average_progress { get { return _taskItem.Average_progress; } }

        public int Flag_id { get { return _taskItem.Flag_id; } }

        public string Flag { get { return _taskItem.Flag; } }
        public Guid Id { get { return _taskItem.Id; } }


        public string Title { get { return _taskItem.Title; } }

        public double Progress
        {
            get
            {

                if (double.TryParse(_taskItem.Progress, out double result))
                    return result;
                return 0;
            }
        }

        public string Description { get { return _taskItem.Description; } }

        public string StartDate { get { return _taskItem.StartDate; } }

        public string EndDate { get { return _taskItem.EndDate; } }

        public ImageSource UrgentSupport { get { return _taskItem.UrgentSupport; } }

        public ImageSource StatusImage { get { return _taskItem.StatusImage; } }

        public Color ColumnColor { get { return _taskItem.ColumnColor; } }

        public Color? ProgressColor
        { 
            get
            {
                if (_taskItem.ProgressColor == null)
                {
                    return Color.White;
                }
                return _taskItem.ProgressColor;
            }
        }
        public bool IsClosed { get { return _taskItem.IsClosed; } }
        public Color TextColor { get { return _taskItem.TextColor; } }


        public TaskItem TaskItem
        {
            get => _taskItem;
        }

    }
}

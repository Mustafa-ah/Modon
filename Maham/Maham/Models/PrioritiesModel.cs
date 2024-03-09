using System;
using System.Collections.Generic;
using System.Text;
using Maham.Bases;

namespace Maham.Models
{
    public class PrioritiesModel : BaseModel
    {

        #region Private Property

        private string _sectionName;
        private int _sectionId;
        private int _tasksCount;
        private string _notStarted;
        private string _inProgress;
        private string _returned;
        private string _completed;
        private string _delayed;

        #endregion

        #region Public Property
        public string SectionName
        {
            get { return _sectionName; }
            set
            {
                _sectionName = value;
                OnPropertyChanged();
            }
        }
        public int SectionId
        {
            get { return _sectionId; }
            set
            {
                _sectionId = value;
                OnPropertyChanged();
            }
        }


        public int TasksCount
        {
            get { return _tasksCount; }
            set
            {
                _tasksCount = value;
                OnPropertyChanged();
            }
        }


        public string NotStarted
        {
            get { return _notStarted; }
            set
            {
                _notStarted = value;
                OnPropertyChanged();
            }
        }

        public string InProgress
        {
            get { return _inProgress; }
            set
            {
                _inProgress = value;
                OnPropertyChanged();
            }
        }

        public string Returned
        {
            get { return _returned; }
            set
            {
                _returned = value;
                OnPropertyChanged();
            }
        }

        public string Completed
        {
            get { return _completed; }
            set
            {
                _completed = value;
                OnPropertyChanged();
            }
        }


        public string Delayed
        {
            get { return _delayed; }
            set
            {
                _delayed = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Maham.Bases;

namespace Maham.Models
{
    public class NotPrioritiesTabContentApi : BaseModel
    {
        private string _dateNum;

        public string DateNum
        {
            get { return _dateNum; }
            set
            {
                _dateNum = value;
                OnPropertyChanged();
            }
        }

        private string _datename;

        public string DateName
        {
            get { return _datename; }
            set
            {
                _datename = value;
                OnPropertyChanged();
            }
        }
        private string _sectionName;

        public string SectionName
        {
            get { return _sectionName; }
            set
            {
                _sectionName = value;
                OnPropertyChanged();
            }
        }
        private string _fieldType;

        public string FieldType
        {
            get { return _fieldType; }
            set
            {
                _fieldType = value;
                OnPropertyChanged();
            }
        }

        private Guid _sectionID;

        public Guid SectionID
        {
            get { return _sectionID; }
            set
            {
                _sectionID = value;
                OnPropertyChanged();
            }
        }
        private string _taskCount;

        public string TaskCount
        {
            get { return _taskCount; }  
            set
            {
                _taskCount = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<TaskItem> _tasks;

        public ObservableCollection<TaskItem> Tasks
        {
            get
            {
                if (_tasks == null)
                    return _tasks = new ObservableCollection<TaskItem>();
                return _tasks;
            }
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        public NotPrioritiesTabContentApi()
        {

        }
        
        public NotPrioritiesTabContentApi( string dateNum,string dateName,string sectionName,string taskCount,ObservableCollection<TaskItem> tasks)
        {
            DateNum = dateNum;
            DateName = dateName;
            SectionName = sectionName;
            TaskCount = taskCount;
            Tasks = tasks;
        }
    }
}

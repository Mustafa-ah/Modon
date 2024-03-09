using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Maham.Models;
using MvvmHelpers;
using Xamarin.Forms;

namespace Maham.ViewModels
{
    public class TabContentViewModel : ObservableRangeCollection<TaskViewModel>, INotifyPropertyChanged
    {
        public NotPrioritiesPageViewModel ParentViewModel { get; }
        public ObservableRangeCollection<TaskViewModel> TabContentTasks = new ObservableRangeCollection<TaskViewModel>();
        public TabContentViewModel(NotPrioritiesPageViewModel parentViewModel, NotPrioritiesTabContentApi notPrioritiesTabContentApi, bool expanded = false)
        {
            ParentViewModel = parentViewModel;  
            this.NotPrioritiesTabContentApi = notPrioritiesTabContentApi;
            this._expanded = expanded;

            foreach (TaskItem TaskItem in notPrioritiesTabContentApi.Tasks)
            {
                TabContentTasks.Add(new TaskViewModel(TaskItem, ParentViewModel));
            }
            if (expanded)
                this.AddRange(TabContentTasks);
        }
        
        private bool _expanded;
        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Expanded"));
                    OnPropertyChanged(new PropertyChangedEventArgs("StateIcon"));
                    if (_expanded)
                    {
                        this.AddRange(TabContentTasks);
                    }
                    else
                    {
                        this.Clear();
                    }
                }
            }
        }
        public string StateIcon
        {
            get
            {
                if (Expanded)
                {
                    return "ExpandArrowUp.png";
                }
                else
                { return "ExpandArrowDown.png"; }
            }
        }
        public string DateNum { get { return NotPrioritiesTabContentApi.DateNum; } }
        public string DateName { get { return NotPrioritiesTabContentApi.DateName; } }
        public string SectionName { get { return NotPrioritiesTabContentApi.SectionName; } }
        public Guid SectionID { get { return NotPrioritiesTabContentApi.SectionID; } }
        public string FieldType { get { return NotPrioritiesTabContentApi.FieldType; } }
        public string TaskCount { get { return NotPrioritiesTabContentApi.TaskCount; } set { } }
        private Element _page;
        public Element Page
        {
            get { return _page; }
            set
            {
                
                _page = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Page)));
            }
        }
        
        public ObservableCollection<TaskItem> Tasks { get { return NotPrioritiesTabContentApi.Tasks; } }
        public NotPrioritiesTabContentApi NotPrioritiesTabContentApi { get; set; }
    }
}

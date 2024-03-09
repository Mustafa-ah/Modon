using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Text;
using Maham.Constants;
using Maham.Service.Model.Response;
using Maham.Extentions;
using Maham.Service.Model.Request.Tasks;
using Maham.Extentions;

namespace Maham.Models
{
    public class FilterTask
    {
        
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int StatusID { get; set; }
        public string Status { get; set; }

        public int SectorID { get; set; }
        public string SectorName { get; set; }

        public int ProjectID { get; set; }
        public string ProjectName { get; set; }

        public int AssigneeID { get; set; }
        public string AssigneeName { get; set; }

        public int PriorityID { get; set; }
        public string Priority { get; set; }

        public int TabId { get; set; }
        public bool IsActive { get; set; }

        public Value2 ResponsibleID { get; set; }
        public string  ResponsibleNaem { get; set; }

        public List<Entity> Entities { get; set; }
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }

        public int SourceId { get; set; }
        public string SourceName { get; set; }

        public string SearchTitle { get; set; }

        public FilterTask()
        {
            //StartDate = AppConstants.MinDate;
            //EndDate = AppConstants.MinDate;

            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = StartDate.AddMonths(1).AddDays(-1);

            ResponsibleID = new Value2() { ID = Guid.Empty.ToString(), RoleID = "0", Type = -1 };

            Entities = new List<Entity>();
        }

        public FilterDto GetFilterDto()
        {
            FilterDto _Filter = new FilterDto
            {
                FromDate = StartDate,
                ToDate = EndDate,
                StatusId = StatusID,
                PriorityId = PriorityID,
                ResponsibleID = ResponsibleID == null ? new Value2() { ID = Guid.Empty.ToString(), RoleID = "0", Type = -1 } : ResponsibleID,
                SourceId = SourceId,
                SearchTask = SearchTitle
            };

            return _Filter;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Maham.Service.Model.Response.Priorities;


namespace Maham.Service.Implmentation.Priorities
{
   public interface IPrioritiesService
    {
        Task<ObservableCollection<PrioritiesResponse>> GetPriorities();
    }
}

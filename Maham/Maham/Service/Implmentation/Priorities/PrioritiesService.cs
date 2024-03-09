using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Maham.Constants;
using Maham.Service.Model.Response.Priorities;
using Maham.Setting;
using Maham.ViewModels;

namespace Maham.Service.Implmentation.Priorities
{
    public class PrioritiesService : IPrioritiesService
    {
        private ITaskyApi _taskyApi;

        public Task<ObservableCollection<PrioritiesResponse>> GetPriorities()
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            return null;// api.GetPriorities(Settings.AccessToken,Settings.UserId);
        }
    }
}

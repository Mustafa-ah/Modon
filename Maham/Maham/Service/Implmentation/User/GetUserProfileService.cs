using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maham.Constants;
using Maham.Service.Model.Request.User;
using Maham.Service.Model.Response.User;
using Maham.Setting;
using Maham.ViewModels;
using Maham.Service.Model.Response;

namespace Maham.Service.Implmentation.User
{
    public class GetUserProfileService : IGetUserProfileService
    {
        private ITaskyApi _taskyApi;
        public GetUserProfileService(ITaskyApi taskyApi)
        {
            _taskyApi = taskyApi;
        }

        public Task<Result> GetProfile(string token, GetUserProfileRequest request)
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            return null;// api.GetProfileUser("",token);
        }

       
    }
}

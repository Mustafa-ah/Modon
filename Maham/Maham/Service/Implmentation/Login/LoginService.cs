using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maham.Constants;
using Maham.Service.Model.Request.Login;
using Maham.Service.Model.Response.Login;
using Maham.Setting;
using Maham.ViewModels;

namespace Maham.Service.Implmentation.Login
{
    public class LoginService : ILoginService
    {
        private ITaskyApi _taskyApi;
        public LoginService(ITaskyApi taskyApi)
        {
            _taskyApi = taskyApi;
        }
        public Task<LoginResponse> Login(LoginRequest request)
        {// hena beyzwod goz basse url // lasa hazbto  
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            return api.Authenticate(request);
        }
    }
}

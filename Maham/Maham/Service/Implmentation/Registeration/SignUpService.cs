using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maham.Constants;
using Maham.Service.Model.Request.Registeration;
using Maham.Service.Model.Response.Registeration;
using Maham.Setting;
using Maham.ViewModels;

namespace Maham.Service.Implmentation.Registeration
{
    public class SignUpService : ISignUpService
    {
        private ITaskyApi _taskyApi;
        public SignUpService(ITaskyApi taskyApi)
        {
            _taskyApi = taskyApi;
        }
        public Task<RegisterationResponse> Registeration(RegisterationRequest request)
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            return null;// api.SignUp(request);
        }
    }
}

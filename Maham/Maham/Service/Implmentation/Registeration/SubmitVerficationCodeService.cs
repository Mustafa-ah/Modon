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
    public class SubmitVerficationCodeService : ISubmitVerficationCodeService
    {
        private ITaskyApi _taskyApi;
        public SubmitVerficationCodeService(ITaskyApi taskyApi)
        {
            _taskyApi = taskyApi;
        }
        public Task<SubmitVerficationCodeResponse> SubmitVerficationCode(SubmitVerficationCodeRequest request)
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            return null;// api.SubmitverificationCode("","");
        }
    }
}

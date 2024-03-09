using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maham.Service.Model.Request.User;
using Maham.Service.Model.Response;
using Maham.Service.Model.Response.User;

namespace Maham.Service.Implmentation.User
{
    public interface IGetUserProfileService
    {
        Task<Result> GetProfile(string token,GetUserProfileRequest request);
    }
}

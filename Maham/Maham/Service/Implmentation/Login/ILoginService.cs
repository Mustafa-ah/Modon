using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maham.Service.Model.Request.Login;
using Maham.Service.Model.Response.Login;

namespace Maham.Service.Implmentation.Login
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }
}

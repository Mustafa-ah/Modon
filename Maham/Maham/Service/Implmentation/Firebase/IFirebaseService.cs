using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maham.Service.Model.Response;

namespace Maham.Service.Implmentation.Firebase
{
    public interface IFirebaseService
    {
        Task<FirebaseServiceResponse> AddFirebaseToken(string token,string fireBasetoken, string UserID);
    }
}

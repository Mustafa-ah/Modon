using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maham.Constants;
using Maham.Service.Model.Response;

namespace Maham.Service.Implmentation.Firebase
{
    public class FirebaseService : IFirebaseService
    {
        private ITaskyApi _taskyApi;

        public FirebaseService(ITaskyApi taskyApi)
        {
            _taskyApi = taskyApi;
        }

        public Task<FirebaseServiceResponse> AddFirebaseToken(string token, string fireBasetoken, string UserID)
        {
            throw new NotImplementedException();
        }
    }
}

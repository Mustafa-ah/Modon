using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maham.Service.Model.Request.Registeration;
using Maham.Service.Model.Response.Registeration;

namespace Maham.Service.Implmentation.Registeration
{
   public interface ISendVerficationCodeService
    {
        Task<SendVerficationCodeResponse> SendverficationCodes(SendVerficationCodeRequest request);
    }
}

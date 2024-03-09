using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response
{
    public class Result
    {
        public string Code { get; set; }
        public dynamic Data { get; set; }
        public long Count { get; set; }
        public long Sum { get; set; }
        public bool Success { get; set; }
        public List<dynamic> ListData { get; set; }

        public Result(bool Success)
        {
            this.Success = Success;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Maham.Service.Model.Response
{
    public class ResultData<T>
    {
        public ResultData()
        {
        }
        public string Code { get; set; }
        //public dynamic Data { get; set; }
        public long Count { get; set; }
        public bool Success { get; set; }
        public List<T> Data { get; set; }

        public ResultData(bool Success)
        {
            this.Success = Success;
        }
    }
}

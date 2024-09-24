using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Constants
{
  public static  class AppConstants
    {
        public const string BasicURL = "https://maham.modon.gov.sa";
        // public const string BasicURL = "http://15.185.62.3:50073";
        //public const string BasicURL = "http://dev.stingrayltd.com:50260";
        //public const string BasicURL = "http://192.168.1.2:5003";
        //public const string BasicURL = "http://197.50.225.151:2020";
        //public const string BasicURL = "http://209.182.216.32:2020/";
        public const string EmailValidationRule = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        public const string MinDate = "0001-01-01 12:00 AM";
        public const string AppName = "Maham/TaskDocs";
        public const int MaxFileSize = 4;
    }
}

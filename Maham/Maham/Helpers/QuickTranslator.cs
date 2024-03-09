using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Helpers
{
    public static class QuickTranslator
    {
        public static string TranslateStatus(int statusId)

        {

            string status = "";

            try

            {

                Dictionary<int, string> valuePairs = new Dictionary<int, string>()

                {

                    {4,"اكتملت"},

                    {3,"متأخرة"},

                    {12,"مرتجعة"},

                    {11,"مغلقة"},

                    { 2,"جارية"},

                    {1," لم تبدأ"}

                };


                if (valuePairs.ContainsKey(statusId))

                {

                    status = valuePairs[statusId];

                }

                return status;

            }

            catch

            {

                return status;

            }

        }
    }
}

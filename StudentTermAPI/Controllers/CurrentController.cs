using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace StudentTermAPI.Controllers
{
    /* Common starting route */
    [Route("term")]
    public class CurrentController: Controller
    {
        /* Route for feature 1 */
        [HttpGet("current")]
        public JsonResult GetTerms()
        {
            string month = DateTime.Now.ToString("MMMM");

            return new JsonResult(CurrentDataStore.Current.Terms.Where(
                t => t.Months.Contains(month)));
        }

        /* Route for feature 2 */
        [HttpGet("{date:datetime:regex(\\d{{4}}-\\d{{2}}-\\d{{2}})}")]
        public JsonResult GetString(DateTime date)
        {
            int monthInt = date.Month;
            string monthString = DateTimeFormatInfo.CurrentInfo.GetMonthName(monthInt);

            return new JsonResult(CurrentDataStore.Current.Terms.Where(
                t => t.Months.Contains(monthString)));
        }

        /* Route for feature 3 */
        [HttpGet("{termcode:int}")]
        public JObject GetTerm(int termcode)
        {
            int mod = termcode % 10;
            string month = "";
            switch (mod)
            {
                case 1:
                    month = "Winter";
                    break;
                case 4:
                    month = "Spring";
                    break;
                case 6:
                    month = "Summer";
                    break;
                case 8:
                    month = "Fall";
                    break;
                default:
                    return new JObject(new JProperty("error", "invalid month"));
            }

            int yearOffset = termcode / 10;
            int yearResult = 1900 + yearOffset;

            if (yearResult < 1957 || yearResult > 2018)
            {
                return new JObject(new JProperty("error", "invalid year"));
            }

            return new JObject(new JProperty("term", month));
        }
    }
}

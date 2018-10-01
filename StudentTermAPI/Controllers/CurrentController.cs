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
        [HttpGet("{date:datetime:regex(\\d{{2}}-\\d{{2}}-\\d{{4}})}")]
        public object GetString(DateTime date)
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

            string currentYearString = DateTime.Now.ToString("yyyy");
            Int32 currentYearInt = Int32.Parse(currentYearString);

            if (yearResult < 1957 || yearResult > currentYearInt)
            {
                return new JObject(new JProperty("error", "invalid year"));
            }

            return new JObject(new JProperty("term", month));
        }

        /* Route for feature 4 */
        [HttpGet("{termstring}")]
        public JObject GetCode(string termstring)
        {
            int term = 0;
            string yearStr = "";

            /* ex: Fall2018, spring1970, Winter2000, summer2017, etc.
             * with this format, length can only be 8 or 10.
             * If 8, then first 4 characters can only spell out "Fall", followed by 4 numbers (year)
             * Else if 10, then first 6 characters can only spell out "Winter", or "Spring" or "Summer", followed by 4 numbers (year)
             */
            if (termstring.Length == 8)
            {
                if(termstring.Substring(0, 4).ToLower().Equals("fall"))
                {
                    term = 8;
                    yearStr = termstring.Substring(4);
                }
            } 

            else if (termstring.Length == 10)
            {
                string termStr = termstring.Substring(0, 6).ToLower();
                switch (termStr)
                {
                    case "winter":
                        term = 1;
                        break;
                    case "spring":
                        term = 4;
                        break;
                    case "summer":
                        term = 6;
                        break;
                    default:
                        return new JObject(new JProperty("error", "invalid"));
                }
                yearStr = termstring.Substring(6);
            }

            if (yearStr.All(char.IsDigit))
            {
                int num = 0;
                try
                {
                    num = Int32.Parse(yearStr);
                }
                catch (Exception e)
                {
                    return new JObject(new JProperty("error", "\nFormat: Fall2018, Winter2000, Spring1999, Summer1967, etc"));
                }

                string currentYearString = DateTime.Now.ToString("yyyy");
                Int32 currentYearInt = Int32.Parse(currentYearString);

                if (num >= 1957 && num <= currentYearInt)
                {
                    return new JObject(new JProperty("termcode", (num-1900) * 10 + term + ""));
                }
            }
            return new JObject(new JProperty("error", "invalid"));
        }
    }
}

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
            // last digit in the term code is the month (1 = Winter, 4, = Spring, 6 = Summer, 8 = Fall)
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
                    // if it's not in 1, 4, 6, or 8, this is an invalid term month.
                    return new JObject(new JProperty("error", "invalid month"));
            }

            // the year is the first 3 numbers, added with 1900. i.e. 118 + 1900, which is 2018.
            int yearOffset = termcode / 10;
            int yearResult = 1900 + yearOffset;

            // check if it's within the range of 1957 (when Stony first opened, and current year)
            string currentYearString = DateTime.Now.ToString("yyyy");
            Int32 currentYearInt = Int32.Parse(currentYearString);

            // if it's not within this range, return invalid year.
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

            /* ex: Fall-2018, spring-1970, Winter-2000, summer-2017, etc.
             * split the String by delimiter "-"
             * check if array[0] = Fall or Spring or Winter or Summer
             * if not, return false, else continue
             * check if array[1] is parseable as integer
             * if not return false, else check if valid year
            */
            string [] arr = termstring.Split("-");
            yearStr = arr[1];
            arr[0] = arr[0].ToLower();
            switch (arr[0])
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
                case "fall":
                    term = 8;
                    break;
                default:
                    return new JObject(new JProperty("error", "invalid"));
            }

            if (yearStr.All(char.IsDigit))
            {
                int num = Int32.Parse(yearStr);
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

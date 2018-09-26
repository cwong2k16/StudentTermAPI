﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudentTermAPI.Controllers
{
    [Route("term")]
    public class CurrentController: Controller
    {
        [HttpGet("current")]
        public JsonResult GetTerms()
        {
            string month = DateTime.Now.ToString("MMMM");

            return new JsonResult(CurrentDataStore.Current.Terms.Where(
                t => t.Months.Contains(month)));
        }

        [HttpGet("{date:datetime:regex(\\d{{4}}-\\d{{2}}-\\d{{2}})}")]
        public JsonResult GetString(DateTime date)
        {
            int monthInt = date.Month;
            string monthString = DateTimeFormatInfo.CurrentInfo.GetMonthName(monthInt);

            return new JsonResult(CurrentDataStore.Current.Terms.Where(
                t => t.Months.Contains(monthString)));
        }

    }
}

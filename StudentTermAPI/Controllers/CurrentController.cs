using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudentTermAPI.Controllers
{
    [Route("term/current")]
    public class CurrentController: Controller
    {
        [HttpGet()]
        public JsonResult GetTerms()
        {
            string month = DateTime.Now.ToString("MMMM");

            return new JsonResult(CurrentDataStore.Current.Terms.FirstOrDefault(
                c=>c.Months.Contains(month)));
        }

    }
}

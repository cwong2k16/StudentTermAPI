using StudentTermAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTermAPI
{
    public class CurrentDataStore
    {
        public static CurrentDataStore Current { get; set; } = new CurrentDataStore();
        public List<CurrentModel> Terms { get; set; }

        public CurrentDataStore()
        {
            Terms = new List<CurrentModel>()
            {
                new CurrentModel()
                {
                    Months = new List<string>()
                    {
                        "September",
                        "October",
                        "November",
                        "December"
                    },
                    Term = "Fall"
                },
                new CurrentModel()
                {
                    Months = new List<string>()
                    {
                        "January"
                    },
                    Term = "Winter"
                },
                new CurrentModel()
                {
                    Months = new List<string>()
                    {
                        "February",
                        "March",
                        "April",
                        "May"
                    },
                    Term = "Spring"
                },
                new CurrentModel()
                {
                    Months = new List<string>()
                    {
                        "June",
                        "July",
                        "August",
                    },
                    Term = "Summer"
                },
            };
        }
    }
}

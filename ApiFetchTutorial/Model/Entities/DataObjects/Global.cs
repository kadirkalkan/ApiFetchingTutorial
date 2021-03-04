using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFetchTutorial.Model.Entities.DataObjects
{
    public class Global
    {
        public long NewConfirmed { get; set; }
        public long TotalConfirmed { get; set; }
        public long NewDeaths { get; set; }
        public long NewRecovered { get; set; }
        public long TotalRecovered { get; set; }
        public DateTime Date { get; set; }
    }
}

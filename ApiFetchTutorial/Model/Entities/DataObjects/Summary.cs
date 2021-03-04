using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFetchTutorial.Model.Entities.DataObjects
{
    public class Summary
    {
        public Guid ID { get; set; }
        public string Message { get; set; }
        public Global Global { get; set; }
        public List<CountryData> Countries { get; set; }

    }

}

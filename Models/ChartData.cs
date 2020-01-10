using System.Collections.Generic;

namespace RiseRestApi.Models
{
    public class ChartData
    {
        public ChartData()
        {
            CollectionNames = new HashSet<string>();
            SeriesData = new HashSet<Series>();
        }

        public class Series
        {
            public string Name { get; set; }
            public IList<object> Data { get; set; }
        }

        public ICollection<string> CollectionNames { get; set; }
        public ICollection<Series> SeriesData { get; set; }
    }
}

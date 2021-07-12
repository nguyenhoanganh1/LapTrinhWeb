using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EL
{
    public class Report
    {

        public string group;
        public double sum;
        public long count;
        public double min;
        public double max;
        public double avg;

        public Report(string group, double sum, long count, double min, double max, double avg)
        {
            this.group = group;
            this.sum = sum;
            this.count = count;
            this.min = min;
            this.max = max;
            this.avg = avg;
        }
        public Report()
        {

        }
        public Report (List<Report> tonko)
        {
            List<Report> report = tonko;
        }

            
        
    }
	
}

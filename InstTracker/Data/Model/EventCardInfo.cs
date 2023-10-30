using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstTracker.Data.Model
{
    public class EventCardInfo
    {
        public string Name { get; set; }
        public DateTime NextHit { get; set; }
        public TimeSpan TimeRemaining { get; set; }
    }
}
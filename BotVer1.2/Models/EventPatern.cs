using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotVer1._2.Models
{
    public class EventPatern: Composite
    {
        public string Id { get; private set; }
        public DateTime? OpenDate { get; private set; }
        public EventPatern(string name, string id, DateTime? openDate) : base(name) { Id = id; OpenDate = openDate; }

        public override string SetTextNode()
        {
            //DateTime dt = (DateTime)this.OpenDate;
            //TimeSpan t = new TimeSpan(2, 0, 0);
            //dt += t;
            //return dt.ToString("dd/MM HH:mm") + "   " + this.Name;
            return Convert.ToDateTime(OpenDate).ToLocalTime().ToString("dd/MM HH:mm") + "   " + this.Name;
        }
    }
}

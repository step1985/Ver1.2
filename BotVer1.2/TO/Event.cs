using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace BotVer1._2.TO
{
    public class Event : IComparable
    {
        DateTime? openDate;

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty(PropertyName = "timezone")]
        public string Timezone { get; set; }

        [JsonProperty(PropertyName = "venue")]
        public string Venue { get; set; }

        [JsonProperty(PropertyName = "openDate")]
        public DateTime? OpenDate { get; set; }
        //{
        //    get { return openDate; }
        //    set
        //    {
        //        openDate = value;
        //        eventNode.Text = OpenDate + " " + Name;
        //    }
        //}
        public override bool Equals(object obj)
        {
            if (obj is Event && obj != null)
            {
                //Event temp = (Event)obj;
                if (this.Id == (obj as Event).Id)
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            return new StringBuilder().AppendFormat("{0}", "Event")
                        .AppendFormat(" : Id={0}", Id)
                        .AppendFormat(" : Name={0}", Name)
                        .AppendFormat(" : CountryCode={0}", CountryCode)
                        .AppendFormat(" : Venue={0}", Venue)
                        .AppendFormat(" : Timezone={0}", Timezone)
                        .AppendFormat(" : OpenDate={0}", OpenDate)
                        .ToString();
        }


        int IComparable.CompareTo(object obj)
        {
            Event temp = (Event)obj;
            if (this.OpenDate > temp.OpenDate)
                return 1;
            if (this.OpenDate < temp.OpenDate)
                return -1;
            return 0;
        }
    }
}

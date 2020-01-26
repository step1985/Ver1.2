using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotVer1._2.Models
{
    public class CountryPatern: Composite
    {
        public CheckBox checkBox;
        public string CompetitionId { get; private set; }
        public CountryPatern(string name, string competitionId, CheckBox checkBox) : base(name) 
        { 
            CompetitionId = competitionId; 
            this.checkBox = checkBox; 
            this.checkBox.Text = this.Name;
            this.checkBox.Checked = true;
        }
    }
}

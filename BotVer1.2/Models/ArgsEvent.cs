using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotVer1._2.Models
{
    public class ArgsEvent: EventArgs
    {
        public string TextNode { get; private set; }
        public int Index { get; private set; }
        public bool Flag { get; private set; }
        public ArgsEvent(string text, int index, bool flag)
        {
            TextNode = text;
            Index = index;
            Flag = flag;
        }
    }
}

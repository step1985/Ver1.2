using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotVer1._2.Models;

namespace BotVer1._2.Views
{
    public interface IView
    {
        void UpdateTreeview(Country country, ArgsEvent e);
    }
}

using System;
using System.Windows.Forms;
using BotVer1._2.TO;

namespace BotVer1._2.Models
{
    public class Bet
    {
        public string BetId { get; set; }
        public DateTime? PlacedDate { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public double Size { get; set; }

        public TreeNode betNode = new TreeNode();

        public bool isMatched = false;

        public Bet(string betId, DateTime? placedDate, string status, double price, double size)
        {
            BetId = betId;
            PlacedDate = placedDate;
            Status = status;
            Price = price;
            Size = size;

            betNode.Text = Convert.ToDateTime(PlacedDate).ToLocalTime().ToString("dd/MM HH:mm") + "  " + Price + "@" + Size + " " + isMatched;
        }
    }
}

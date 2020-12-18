using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RDTSimulation.Enums;

namespace RDTSimulation
{
    public class Helper
    {
        public static List<Point> unreliableChannelNodes = new List<Point>();
        public static Point senderLocaliton = new Point(250,443);
        public static Point receiverLocation = new Point(714,443);
    }
}

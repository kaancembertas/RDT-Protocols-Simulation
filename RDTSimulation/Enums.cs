using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDTSimulation
{
    public class Enums
    {
        public enum STATUS
        {
            SENDING,
            SENT,
            PENDING
        };

        public enum DIRECTION
        {
            SENDER,
            RECEIVER
        };

        public enum PACKET_TYPE
        {
            DATA,
            ACK,
            NAK,
        }
    }
}

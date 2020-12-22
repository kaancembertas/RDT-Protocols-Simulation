using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDTSimulation
{
    class Endpoint
    {
        public List<Packet> packets;
        public List<Packet> receivedPackets;
        public int expectedSeq = 0;

        public Endpoint()
        {
            packets = new List<Packet>();
            receivedPackets = new List<Packet>();
        }

    }
}

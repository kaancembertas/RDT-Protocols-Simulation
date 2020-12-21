using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RDTSimulation.Enums;

namespace RDTSimulation
{
    public class Packet : Button , IComparable<Packet>
    {
        public STATUS status;
        public char data;
        public DIRECTION direction;
        public PACKET_TYPE packetType;
        public int id { get; }
        private List<Point> unreliableChannelNodes = Helper.unreliableChannelNodes;
        public bool isCorrupted = false;
        public bool isLoss = false;
        public int sequenceNumber { get; set; }

        public Packet(int id, char data, DIRECTION direction, PACKET_TYPE packetType)
        {
            this.status = STATUS.PENDING;
            this.Location = direction == DIRECTION.SENDER ? Helper.receiverLocation : Helper.senderLocaliton;
            this.FlatStyle = FlatStyle.Flat;
            this.Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);
            this.Width = 50;
            this.Height = 50;
            this.BackColor = Color.Gainsboro;
            this.data = data;
            this.direction = direction;
            this.Enabled = false;
            this.packetType = packetType;
            this.id = id;
            this.sequenceNumber = id;

            if (packetType == PACKET_TYPE.DATA)
            {
                this.Text = "P" + id;
            }
            else if (packetType == PACKET_TYPE.ACK)
            {
                this.Text = "ACK #" + sequenceNumber;
            }
            else if (packetType == PACKET_TYPE.NAK)
            {
                this.Text = "NAK #" + sequenceNumber;
            }
        }

        public void setSequenceNumber(int sequenceNumber)
        {
            this.sequenceNumber = sequenceNumber;
            if (this.packetType == PACKET_TYPE.DATA)
            {
                this.Text = "P" + id + "\n#" + this.sequenceNumber;
            }
            else if (packetType == PACKET_TYPE.ACK)
            {
                this.Text = "ACK #" + sequenceNumber;
            }
            else if (packetType == PACKET_TYPE.NAK)
            {
                this.Text = "NAK #" + sequenceNumber;
            }
        }

        public void moveRight()
        {
            this.Location = new Point(this.Location.X + Config.MOVE_OFFSET, this.Location.Y);
        }

        public void moveDown()
        {
            this.Location = new Point(this.Location.X, this.Location.Y + Config.MOVE_OFFSET);
        }

        public void moveLeft()
        {
            this.Location = new Point(this.Location.X - Config.MOVE_OFFSET, this.Location.Y);
        }

        public void moveUp()
        {
            this.Location = new Point(this.Location.X, this.Location.Y - Config.MOVE_OFFSET);
        }

        public bool updatePacketLocation()
        {
            if (direction == DIRECTION.RECEIVER)
            {
                if (Location.Y < unreliableChannelNodes[3].Y)
                {
                    return true;
                }

                if (Location.X >= unreliableChannelNodes[2].X - Width)
                {
                    moveUp();
                    return false;
                }

                if (Location.Y >= unreliableChannelNodes[1].Y - Height)
                {
                    moveRight();
                    return false;
                }

                moveDown();

                return false;
            }
            else if (direction == DIRECTION.SENDER)
            {
                if (Location.Y < unreliableChannelNodes[0].Y)
                {
                    return true;
                }

                if (Location.X <= unreliableChannelNodes[1].X)
                {
                    moveUp();
                    return false;
                }

                if (Location.Y >= unreliableChannelNodes[2].Y - Height)
                {
                    moveLeft();
                    return false;
                }

                moveDown();
                return false;
            }

            return false;
        }

        public void setHasBitError(bool val)
        {
            isCorrupted = val;
            if (val)
            {
                ForeColor = Color.Red;
            }
            else
            {
                ForeColor = Color.Black;
            }
        }

        public void setIsLoss(bool val)
        {
            isLoss = val;
            if(val)
            {
                ForeColor = Color.Red;
            }
            else
            {
                ForeColor = Color.Black;
            }
        }

        public int CompareTo(Packet other)
        {
            return this.id.CompareTo(other.id);
        }
    }
}

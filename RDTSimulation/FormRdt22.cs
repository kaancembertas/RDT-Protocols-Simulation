using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RDTSimulation.Enums;

namespace RDTSimulation
{
    public partial class FormRdt22 : Form
    {
        private List<Point> unreliableChannelNodes = Helper.unreliableChannelNodes;
        private List<Packet> dataPackets = new List<Packet>();
        private List<Packet> responsePackets = new List<Packet>();
        private List<Packet> receivedResponses = new List<Packet>();

        private List<Packet> receiverReceivedPackets = new List<Packet>();
        double bitErrorRate;
        int packetCount;

        public int senderExpectedSequence = 0;
        public int receiverExpectedSequence = 0;

        public FormRdt22()
        {
            InitializeComponent();
        }

        private void FormRdt22_Load(object sender, EventArgs e)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            Paint += new PaintEventHandler(
                     (object sender1, PaintEventArgs paint) =>
                     {
                         for (int i = 0; i < unreliableChannelNodes.Count - 1; i++)
                         {
                             paint.Graphics.DrawLine(blackPen, unreliableChannelNodes[i], unreliableChannelNodes[i + 1]);
                         }
                     }
                );

            lblReceiverExpected.Text = "Expected Sequence: #" + receiverExpectedSequence;
            lblSenderExpected.Text = "Expected Sequence: #" + senderExpectedSequence;
        }

        private void updateSenderExpected()
        {
            senderExpectedSequence = senderExpectedSequence == 0 ? 1 : 0;
            lblSenderExpected.Text = "Expected Sequence: #" + senderExpectedSequence;
        }

        private void updateReceiverExpected()
        {
            receiverExpectedSequence = senderExpectedSequence == 0 ? 1 : 0;
            lblReceiverExpected.Text = "Expected Sequence: #" + receiverExpectedSequence;
        }

        private bool isReceiverReceivedPacketDubblicate(Packet packet)
        {
            foreach (Packet p in receiverReceivedPackets)
                if (p.id == packet.id) return true;
            return false;
        }

        private void randomPacketError(Packet packet)
        {
            Random rnd = new Random();


            if (rnd.NextDouble() <= this.bitErrorRate)
            {
                packet.setHasBitError(true);
            }
        }

        private Packet getLastPacketSent()
        {
            Packet packet;
            List<Packet> newList = new List<Packet>(dataPackets);
            newList.Sort();
            for (int i = (newList.Count - 1); i >= 0; i--)
            {
                packet = newList[i];
                if (packet.status == STATUS.SENT) return packet;
            }
            return null;
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text.Length == 0) return;
            if (txtBer.Text.Length == 0) return;

            this.bitErrorRate = double.Parse(txtBer.Text);
            reset();
            btnSendMessage.Enabled = false;
            string message = txtMessage.Text;
            packetCount = message.Length;

            txtMessage.Text = "";
            txtBer.Text = "";
            lblSenderData.Text = "Data: " + message;

            for (int i = 0; i < message.Length; i++)
            {
                char c = message[i];
                Packet packet = new Packet(i, c, DIRECTION.RECEIVER, PACKET_TYPE.DATA);

                if (i != 0)
                    randomPacketError(packet);

                dataPackets.Add(packet);
                this.Controls.Add(packet);
                packet.SendToBack();
            }

            simulationTimer.Start();
            dataPackets[0].status = STATUS.SENDING;
            dataPackets[0].setSequenceNumber(senderExpectedSequence);

        }

        private void reset()
        {
            senderExpectedSequence = 0;
            receiverExpectedSequence = 0;
            lblReceiverExpected.Text = "Expected Sequence: #" + receiverExpectedSequence;
            lblSenderExpected.Text = "Expected Sequence: #" + senderExpectedSequence;
            dataPackets.Clear();
            responsePackets.Clear();
            lblSenderData.Text = "Data: ";
            lblReceiverData.Text = "Data: ";
            receiverPacketContainer.Controls.Clear();
            receiverReceivedPackets.Clear();
        }

        private bool isResponseDublicate(Packet packet)
        {
            foreach (Packet response in receivedResponses)
            {
                if (response.Equals(packet)) return true;
            }
            return false;
        }

        private void onReceiverGotPacket(Packet packet)
        {
            Packet response;
            packet.status = STATUS.SENT;
            if (!packet.isCorrupted)
            {
                if (isReceiverReceivedPacketDubblicate(packet))
                {
                    MessageBox.Show("Receiver got dublicate packet!","Discard it..\n P" + packet.id);
                    Controls.Remove(packet);
                }
                else
                {
                    receiverReceivedPackets.Add(packet);
                    receiverPacketContainer.Controls.Add(packet);
                    lblReceiverData.Text += packet.data;
                }
                response = new Packet(packet.id, packet.data, DIRECTION.SENDER, PACKET_TYPE.ACK);

            }
            else
            {
                response = receivedResponses[receivedResponses.Count - 1];
                response.Location = Helper.receiverLocation;
            }

            response.setSequenceNumber(receiverExpectedSequence);
            updateReceiverExpected();
            randomPacketError(response);


            this.Controls.Add(response);
            if (!responsePackets.Contains(response))
                responsePackets.Add(response);

            response.status = STATUS.SENDING;
        }

        private void onSenderGotPacket(Packet packet)
        {
            packet.status = STATUS.SENT;
            this.Controls.Remove(packet);
            updateSenderExpected();

            // All packets were transmitted
            if (packet.packetType == PACKET_TYPE.ACK &&
                (packet.id + 1) == packetCount &&
                !packet.isCorrupted)
            {
                simulationTimer.Stop();
                btnSendMessage.Enabled = true;
                return;
            }

            if (packet.isCorrupted)
            {
                Packet lastPacket = getLastPacketSent();
                Packet samePacket = new Packet(lastPacket.id, lastPacket.data, lastPacket.direction, lastPacket.packetType);
                dataPackets.Add(samePacket);
                samePacket.setHasBitError(false);
                samePacket.BringToFront();
                Controls.Add(samePacket);
                samePacket.status = STATUS.SENDING;
                samePacket.BringToFront();
                samePacket.setSequenceNumber(senderExpectedSequence);
            }
            else if (!isResponseDublicate(packet))
            {
                Packet packetToSend = dataPackets[packet.id + 1];
                packetToSend.setSequenceNumber(senderExpectedSequence);
                packetToSend.status = STATUS.SENDING;
                receivedResponses.Add(packet);
            }
            else
            {
                MessageBox.Show("Dublicate ACK!", "Sender did not accept the dublicate packet! \n ACK FOR P" + packet.id);
                // Sending data packet again!
                Packet dataPacket = getLastPacketSent();
                dataPacket.setHasBitError(false);
                dataPacket.Location = Helper.senderLocaliton;
                Controls.Add(dataPacket);
                dataPacket.BringToFront();
                dataPacket.status = STATUS.SENDING;
                dataPacket.setSequenceNumber(senderExpectedSequence);
            }
        }

        private void simulationTimer_Tick(object sender, EventArgs e)
        {
            foreach (Packet packet in dataPackets)
            {
                if (packet.status == STATUS.SENDING)
                {
                    bool isPacketSent = packet.updatePacketLocation();
                    if (isPacketSent)
                    {
                        onReceiverGotPacket(packet);
                    }

                }
            }

            foreach (Packet packet in responsePackets)
            {
                if (packet.status == STATUS.SENDING)
                {
                    bool isPacketSent = packet.updatePacketLocation();
                    if (isPacketSent)
                    {
                        onSenderGotPacket(packet);
                    }
                }
            }

        }

        private void FormRdt22_FormClosed(object sender, FormClosedEventArgs e)
        {
            simulationTimer.Stop();
        }
    }
}

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
    public partial class FormRdt21 : Form
    {
        private List<Point> unreliableChannelNodes = Helper.unreliableChannelNodes;
        private List<Packet> dataPackets = new List<Packet>();
        private List<Packet> responsePackets = new List<Packet>();

        private List<Packet> receiverReceivedPackets = new List<Packet>();
        double bitErrorRate;
        int packetCount;

        public int senderExpectedSequence = 0;
        public int receiverExpectedSequence = 0;

        public FormRdt21()
        {
            InitializeComponent();
        }

        private void FormRdt21_Load(object sender, EventArgs e)
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

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text.Length == 0) return;
            if (txtBer.Text.Length == 0) return;

            this.bitErrorRate = double.Parse(txtBer.Text);
            reset();
            btnSendMessage.Enabled = false;
            string message = txtMessage.Text;
            txtMessage.Text = "";
            txtBer.Text = "";
            packetCount = message.Length;
            lblSenderData.Text = "Data: " + message;

            for (int i = 0; i < message.Length; i++)
            {
                char c = message[i];
                Packet packet = new Packet(i, c, DIRECTION.RECEIVER, PACKET_TYPE.DATA);
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

        private void onReceiverGotPacket(Packet packet)
        {
            Packet response;
            packet.status = STATUS.SENT;
            if (!packet.isCorrupted)
            {
                if (isReceiverReceivedPacketDubblicate(packet))
                {
                    MessageBox.Show("Receiver got dublicate packet! Discard it..\n P" + packet.id);
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
                response = new Packet(packet.id, packet.data, DIRECTION.SENDER, PACKET_TYPE.NAK);
            }

            response.setSequenceNumber(receiverExpectedSequence);
            updateReceiverExpected();

            if(!isReceiverReceivedPacketDubblicate(packet))
                randomPacketError(response);

            this.Controls.Add(response);
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
                Packet lastPacket = dataPackets[packet.id];
                Packet samePacket = new Packet(lastPacket.id, lastPacket.data, lastPacket.direction, lastPacket.packetType);
                dataPackets.Add(samePacket);
                samePacket.setHasBitError(false);
                samePacket.BringToFront();
                Controls.Add(samePacket);
                samePacket.status = STATUS.SENDING;
                samePacket.BringToFront();
                samePacket.setSequenceNumber(senderExpectedSequence);
            }
            else if (packet.packetType == PACKET_TYPE.NAK)
            {
                // Sending data packet again!
                Packet dataPacket = dataPackets[packet.id];
                dataPacket.setHasBitError(false);
                dataPacket.Location = Helper.senderLocaliton;
                Controls.Add(dataPacket);
                dataPacket.BringToFront();
                dataPacket.status = STATUS.SENDING;
                dataPacket.setSequenceNumber(senderExpectedSequence);
            }
            else if (packet.packetType == PACKET_TYPE.ACK)
            {
                Packet packetToSend = dataPackets[packet.id + 1];
                packetToSend.setSequenceNumber(senderExpectedSequence);
                packetToSend.status = STATUS.SENDING;
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

        private void FormRdt21_FormClosed(object sender, FormClosedEventArgs e)
        {
            simulationTimer.Stop();
        }
    }
}

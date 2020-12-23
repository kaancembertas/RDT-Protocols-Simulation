using RDTSimulation;
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
    public partial class FormRdt30GoBN : Form
    {
        private List<Point> unreliableChannelNodes = Helper.unreliableChannelNodes;
        private List<Packet> drawablePackets = new List<Packet>();

        Endpoint sender;
        Endpoint receiver;

        Timer timer;
        double bitErrorRate;
        int packetCount;
        int windowSize;
        int windowBaseIndex = 0;

        public FormRdt30GoBN()
        {
            InitializeComponent();
        }

        private void FormRdt30GoBN_Load(object sender, EventArgs e)
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

            this.sender = new Endpoint();
            this.receiver = new Endpoint();
            this.timer = new Timer();
            this.timer.Tick += new EventHandler(onTimeout);
        }

        private async void onTimeout(object sender, EventArgs e)
        {
            this.timer.Stop();
            txtSenderLog.AppendText("\nTimeout!");

            //Sending window elements again
            for (int i = windowBaseIndex; i < windowBaseIndex+windowSize; i++)
            {
                if (i >= this.sender.packets.Count) break;

                Packet packet = this.sender.packets[i];
                if(receiver.receivedPackets.Contains(packet))
                {
                    packet = new Packet(packet.id, packet.data, packet.direction, packet.packetType);
                    Controls.Add(packet);
                    drawablePackets.Add(packet);
                }
                packet.Location = Helper.senderLocaliton;
                packet.status = STATUS.SENDING;
                packet.setIsLoss(false);
                txtSenderLog.AppendText("\nStart (re)sending P" + packet.id);
                await Task.Delay(190);
            }
            this.timer.Start();
        }

        private void reset()
        {
            drawablePackets.Clear();
            receiverPacketContainer.Controls.Clear();
            this.sender.packets.Clear();
            this.sender.receivedPackets.Clear();
            this.receiver.packets.Clear();
            this.receiver.receivedPackets.Clear();
            lblSenderData.Text = "Data: ";
            lblReceiverData.Text = "Data: ";
            receiver.expectedSeq = 0;
            sender.expectedSeq = 0;
            txtSenderLog.Text = "Sender Log:\n";
            txtReceiverLog.Text = "Receiver Log: \n";
            windowBaseIndex = 0;
        }
        private void randomPacketLoss(Packet packet)
        {
            Random rnd = new Random();

            if (rnd.NextDouble() < this.bitErrorRate)
            {
                packet.setIsLoss(true);
            }
        }

        private void startSendingPacket(Packet packet)
        {
            if (!drawablePackets.Contains(packet))
                drawablePackets.Add(packet);
            packet.status = STATUS.SENDING;
            Controls.Add(packet);
        }

        private bool isAckDublicate(Packet packet)
        {
            foreach (Packet p in sender.receivedPackets)
                if (packet.id == p.id) return true;
            return false;
        }
        
        private bool isPacketDublicate(Packet packet)
        {
            foreach (Packet p in receiver.receivedPackets)
                if (packet.id == p.id) return true;
            return false;
        }

        private async void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text.Length == 0) return;
            if (txtBer.Text.Length == 0) return;
            if (txtWindowSize.Text.Length == 0) return;
            reset();

            this.bitErrorRate = double.Parse(txtBer.Text);
            this.windowSize = int.Parse(txtWindowSize.Text);

            string message = txtMessage.Text;
            packetCount = message.Length;

            // Form Controls
            txtBer.Text = "";
            txtMessage.Text = "";
            txtWindowSize.Text = "";
            btnSendMessage.Enabled = false;

            lblSenderData.Text = message;
            // ------------------

            for (int i = 0; i < message.Length; i++)
            {
                char c = message[i];
                Packet packet = new Packet(i, c, DIRECTION.RECEIVER, PACKET_TYPE.DATA);

                // Adding to the lists
                this.sender.packets.Add(packet);
                this.drawablePackets.Add(packet);
                this.Controls.Add(packet);
                packet.SendToBack();
            }

            simulationTimer.Start();

            for (int i = 0; i < windowSize; i++)
            {
                if (i == packetCount) break;

                Packet packet = this.sender.packets[i];
                if (i != 0) randomPacketLoss(packet);
                packet.status = STATUS.SENDING;
                txtSenderLog.AppendText("\nStart sending P" + packet.id);
                await Task.Delay(190);
            }
            this.timer.Interval = 6000;
            this.timer.Start();
        }

        private void onReceiverGotPacket(Packet packet)
        {
            packet.status = STATUS.SENT;
            if(isPacketDublicate(packet))
            {
                txtReceiverLog.AppendText("\nDiscard dublicate P"+packet.id+" Send Ack #"+packet.id);
                Packet ack = receiver.packets[packet.id];
                ack.Location = Helper.receiverLocation;
                ack.setIsLoss(false);
                startSendingPacket(ack);
                return;
            }

            if (packet.id == receiver.expectedSeq)
            {
                receiver.expectedSeq++;

                Packet response = new Packet(packet.id, packet.data, DIRECTION.SENDER, PACKET_TYPE.ACK);
                lblReceiverData.Text += packet.data;
                receiverPacketContainer.Controls.Add(packet);
                receiver.receivedPackets.Add(packet);
                receiver.packets.Add(response);
                txtReceiverLog.AppendText("\nRcv P"+packet.id+", send ACK #"+response.id);
                randomPacketLoss(response);
                startSendingPacket(response);
            }
            else
            {
                Packet lastAck = receiver.packets[receiver.packets.Count - 1];
                Packet copiedAck = new Packet(lastAck.id, lastAck.data, DIRECTION.SENDER, lastAck.packetType);
                txtReceiverLog.AppendText("\nRcv P" + packet.id + ",discard (re)send ACK #" + copiedAck.id);


                copiedAck.Location = Helper.receiverLocation;
                startSendingPacket(copiedAck);
            }

        }

        private void onSenderGotPacket(Packet packet)
        {
            Controls.Remove(packet);
            packet.status = STATUS.SENT;

            if (packet.id + 1 == packetCount)
            {
                simulationTimer.Stop();
                btnSendMessage.Enabled = true;
                txtSenderLog.AppendText("\nAll packets transferred!");
                this.timer.Stop();
                return;
            }

            if (isAckDublicate(packet))
            {
                packet.status = STATUS.SENT;
                txtSenderLog.AppendText("\nIgnore dublicate ACK #" + packet.id);
                return;
            }

            if(packet.id == sender.expectedSeq)
            {
                sender.receivedPackets.Add(packet);
                sender.expectedSeq++;
                windowBaseIndex++;
                this.timer.Stop();
                if (packet.id + windowSize < packetCount)
                {
                    Packet packetToSend = sender.packets[packet.id + windowSize];
                    randomPacketLoss(packetToSend);
                    packetToSend.status = STATUS.SENDING;
                    txtSenderLog.AppendText("\nRcv ACK #" + packet.id + ", Start sending P" + packetToSend.id);
                }
                this.timer.Start();
            }
            else
            {
                txtSenderLog.AppendText("\nDiscard ACK #" + packet.id);
            }

        }

        private void simulationTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < drawablePackets.Count; i++)
            {
                Packet packet = drawablePackets[i];
                if (packet.status == STATUS.SENDING)
                {
                    int channelMiddleX = unreliableChannelNodes[3].X - unreliableChannelNodes[0].X;

                    if (packet.isLoss)
                    {
                        if (packet.direction == DIRECTION.RECEIVER && packet.Location.X >= channelMiddleX)
                        {
                            packet.status = STATUS.PENDING;
                            return;
                        }

                        if (packet.direction == DIRECTION.SENDER && packet.Location.X <= channelMiddleX)
                        {
                            packet.status = STATUS.PENDING;
                            return;
                        }
                    }

                    bool isPacketSent = packet.updatePacketLocation();
                    if (isPacketSent && packet.direction == DIRECTION.RECEIVER)
                    {
                        onReceiverGotPacket(packet);
                    }
                    else if (isPacketSent && packet.direction == DIRECTION.SENDER)
                    {
                        onSenderGotPacket(packet);
                    }
                }
            }
        }

        private void FormRdt30GoBN_FormClosed(object sender, FormClosedEventArgs e)
        {
            simulationTimer.Stop();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RDTSimulation.Enums;

namespace RDTSimulation
{
    public partial class FormRdt10 : Form
    {
        public FormRdt10()
        {
            InitializeComponent();
        }

        private List<Packet> packets = new List<Packet>();


        private void FormRdt10_Load(object sender, EventArgs e)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);
            List<Point> unreliableChannelNodes = Helper.unreliableChannelNodes;

            Paint += new PaintEventHandler(
                     (object sender1, PaintEventArgs paint) =>
                     {
                         for (int i = 0; i < unreliableChannelNodes.Count - 1; i++)
                         {
                             paint.Graphics.DrawLine(blackPen, unreliableChannelNodes[i], unreliableChannelNodes[i + 1]);
                         }
                     }
                );

        }
        private void onPacketSent(Packet packet)
        {
            packet.status = STATUS.SENT;
            receiverPacketContainer.Controls.Add(packet);
            lblReceiverData.Text += packet.data;
        }


        private bool isAllPacketsSent()
        {
            foreach (Packet packet in packets)
            {
                if (packet.status != STATUS.SENT)
                    return false;
            }

            return true;
        }

        private void simulationTimer_Tick(object sender, EventArgs e)
        {
            if (isAllPacketsSent())
            {
                btnSendMessage.Enabled = true;
                simulationTimer.Stop();
                return;
            }

            foreach (Packet packet in packets)
            {
                if (packet.status == STATUS.SENDING)
                {
                    bool isPacketSent = packet.updatePacketLocation();
                    if (isPacketSent)
                        onPacketSent(packet);
                }
            }
        }

        private void reset()
        {
            packets.Clear();
            lblSenderData.Text = "Data: ";
            lblReceiverData.Text = "Data: ";
            receiverPacketContainer.Controls.Clear();
        }

        private async void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text.Length == 0) return;

            reset();
            btnSendMessage.Enabled = false;
            string message = txtMessage.Text;
            txtMessage.Text = "";
            lblSenderData.Text = "Data: " + message;

            for (int i = 0; i < message.Length; i++)
            {
                char c = message[i];
                Packet packet = new Packet(i, c, DIRECTION.RECEIVER, PACKET_TYPE.DATA);

                packets.Add(packet);
                this.Controls.Add(packet);
                packet.SendToBack();
            }

            simulationTimer.Start();

            foreach (Packet packet in packets)
            {
                packet.status = STATUS.SENDING;
                await Task.Delay(500);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}

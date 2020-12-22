namespace RDTSimulation
{
    partial class FormRdt30StopAndWait
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.receiverPacketContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.receiverTransportLayer = new System.Windows.Forms.GroupBox();
            this.txtReceiverLog = new System.Windows.Forms.RichTextBox();
            this.lblReceiverData = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSenderLog = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSenderData = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtWindowSize = new System.Windows.Forms.TextBox();
            this.txtBer = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.simulationTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.receiverTransportLayer.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(421, 651);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(161, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Unreliable Channel";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(878, 447);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(159, 20);
            this.label9.TabIndex = 8;
            this.label9.Text = "extract (packet, data)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(878, 467);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "rdt_recv(packet)";
            // 
            // receiverPacketContainer
            // 
            this.receiverPacketContainer.Location = new System.Drawing.Point(3, 16);
            this.receiverPacketContainer.Name = "receiverPacketContainer";
            this.receiverPacketContainer.Size = new System.Drawing.Size(439, 263);
            this.receiverPacketContainer.TabIndex = 0;
            // 
            // receiverTransportLayer
            // 
            this.receiverTransportLayer.Controls.Add(this.txtReceiverLog);
            this.receiverTransportLayer.Controls.Add(this.receiverPacketContainer);
            this.receiverTransportLayer.Location = new System.Drawing.Point(560, 162);
            this.receiverTransportLayer.Name = "receiverTransportLayer";
            this.receiverTransportLayer.Size = new System.Drawing.Size(719, 282);
            this.receiverTransportLayer.TabIndex = 13;
            this.receiverTransportLayer.TabStop = false;
            this.receiverTransportLayer.Text = "Transport Layer";
            // 
            // txtReceiverLog
            // 
            this.txtReceiverLog.HideSelection = false;
            this.txtReceiverLog.Location = new System.Drawing.Point(448, 16);
            this.txtReceiverLog.Name = "txtReceiverLog";
            this.txtReceiverLog.ReadOnly = true;
            this.txtReceiverLog.Size = new System.Drawing.Size(265, 260);
            this.txtReceiverLog.TabIndex = 1;
            this.txtReceiverLog.Text = "Receiver Log:";
            // 
            // lblReceiverData
            // 
            this.lblReceiverData.AutoSize = true;
            this.lblReceiverData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblReceiverData.Location = new System.Drawing.Point(6, 48);
            this.lblReceiverData.Name = "lblReceiverData";
            this.lblReceiverData.Size = new System.Drawing.Size(49, 20);
            this.lblReceiverData.TabIndex = 0;
            this.lblReceiverData.Text = "data: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSenderLog);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblSenderData);
            this.groupBox2.Location = new System.Drawing.Point(23, 201);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(531, 243);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transport Layer";
            // 
            // txtSenderLog
            // 
            this.txtSenderLog.HideSelection = false;
            this.txtSenderLog.Location = new System.Drawing.Point(332, 19);
            this.txtSenderLog.Name = "txtSenderLog";
            this.txtSenderLog.ReadOnly = true;
            this.txtSenderLog.Size = new System.Drawing.Size(193, 218);
            this.txtSenderLog.TabIndex = 1;
            this.txtSenderLog.Text = "Sender Log:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(6, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "packet = make_pkt(data)";
            // 
            // lblSenderData
            // 
            this.lblSenderData.AutoSize = true;
            this.lblSenderData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSenderData.Location = new System.Drawing.Point(6, 69);
            this.lblSenderData.Name = "lblSenderData";
            this.lblSenderData.Size = new System.Drawing.Size(49, 20);
            this.lblSenderData.TabIndex = 0;
            this.lblSenderData.Text = "data: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(6, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "data = deliver_data()";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lblReceiverData);
            this.groupBox3.Location = new System.Drawing.Point(560, 58);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(477, 88);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Application Layer";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(19, 447);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "udt_send(packet)";
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(143, 97);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(266, 23);
            this.btnSendMessage.TabIndex = 3;
            this.btnSendMessage.Text = "Send Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtWindowSize);
            this.groupBox1.Controls.Add(this.txtBer);
            this.groupBox1.Controls.Add(this.btnSendMessage);
            this.groupBox1.Controls.Add(this.txtMessage);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(23, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 157);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application Layer";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(59, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Windows Size:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(42, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Bit Error Rate (0-1)";
            // 
            // txtWindowSize
            // 
            this.txtWindowSize.Enabled = false;
            this.txtWindowSize.Location = new System.Drawing.Point(143, 71);
            this.txtWindowSize.Name = "txtWindowSize";
            this.txtWindowSize.Size = new System.Drawing.Size(265, 20);
            this.txtWindowSize.TabIndex = 2;
            this.txtWindowSize.Text = "1";
            // 
            // txtBer
            // 
            this.txtBer.Location = new System.Drawing.Point(143, 45);
            this.txtBer.Name = "txtBer";
            this.txtBer.Size = new System.Drawing.Size(265, 20);
            this.txtBer.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(142, 19);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtMessage.Size = new System.Drawing.Size(266, 20);
            this.txtMessage.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Enter Message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(679, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Receiver Side";
            // 
            // simulationTimer
            // 
            this.simulationTimer.Interval = 1;
            this.simulationTimer.Tick += new System.EventHandler(this.simulationTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(151, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "Sender Side";
            // 
            // FormRdt30StopAndWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1291, 729);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.receiverTransportLayer);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormRdt30StopAndWait";
            this.Text = "Rdt 3.0 Stop and Wait Simulation";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRdt30StopAndWait_FormClosed);
            this.Load += new System.EventHandler(this.FormRdt30StopAndWait_Load);
            this.receiverTransportLayer.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FlowLayoutPanel receiverPacketContainer;
        private System.Windows.Forms.GroupBox receiverTransportLayer;
        private System.Windows.Forms.Label lblReceiverData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSenderData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer simulationTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtWindowSize;
        private System.Windows.Forms.RichTextBox txtSenderLog;
        private System.Windows.Forms.RichTextBox txtReceiverLog;
    }
}
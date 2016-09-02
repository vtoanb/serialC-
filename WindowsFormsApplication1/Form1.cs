using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
// Use threading to read uart without interrupt application
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Display available comport to commbo box
            string[] portname = SerialPort.GetPortNames();
            comboBox1.DataSource = portname;
           
            
        }

        public void readUart()
        {
            while (true)
            {
                string dataline = serialPort1.ReadLine();
                // Put read data to button text to test
                SetText(dataline);
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Start new thread to read uart
            Thread serialReadThread = new Thread(new ThreadStart(readUart));
            serialPort1.Close();
            connect.Text = "CONNECTING...";
            serialPort1.PortName = comboBox1.SelectedItem.ToString();
            serialPort1.Open();
            serialReadThread.Start();
            connect.Text = "CONNECTED";
            connect.BackColor = Color.Green;
        }

        // Safe edit text for button
        delegate void SetTextCallback(string text);


        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.button1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.button1.Text = text;
            }
        }
    }
}

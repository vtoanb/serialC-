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
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
           
            
        }

        private void DataReceivedHandler(
                       object sender,
                       SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadLine().Replace('\r','-');
            string[] parsed = indata.Split('-');

            // Set color for button according to received message
            switch (parsed[0]) { 
                case "L1":
                    if (parsed[1] == "1")
                    {
                        button1.BackColor = Color.YellowGreen;
                    }
                    else if (parsed[1] == "0")
                    {
                        button1.BackColor = Color.White;
                    }

                    break;
                case "L2":
                    if (parsed[1] == "1")
                    {
                        button2.BackColor = Color.YellowGreen;
                    }
                    else if (parsed[1] == "0")
                    {
                        button2.BackColor = Color.White;
                    }
                    break;
                case "L3":
                    if (parsed[1] == "1")
                    {
                        button3.BackColor = Color.YellowGreen;
                    }
                    else if (parsed[1] == "0")
                    {
                        button3.BackColor = Color.White;
                    }
                    break;
                case "L4":
                    if (parsed[1] == "1")
                    {
                        button4.BackColor = Color.YellowGreen;
                    }
                    else if (parsed[1] == "0")
                    {
                        button4.BackColor = Color.White;
                    }
                    break;
                case "L5":
                    if (parsed[1] == "1")
                    {
                        button5.BackColor = Color.YellowGreen;
                    }
                    else if (parsed[1] == "0")
                    {
                        button5.BackColor = Color.White;
                    }
                    break;
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
            if (connect.Text == "CONNECT" && comboBox1.SelectedItem.ToString().Contains("COM"))
            {
                serialPort1.Close();
                connect.Text = "CONNECTING...";
                serialPort1.PortName = comboBox1.SelectedItem.ToString();
                serialPort1.Open();
                connect.Text = "DISCONNECT";
                connect.BackColor = Color.Green;
            }
            else if (connect.Text == "DISCONNECT") {
                serialPort1.Close();
                connect.Text = "CONNECT";
                connect.BackColor = Color.White;
            }
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

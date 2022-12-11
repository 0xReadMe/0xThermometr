using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace Arduino
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (buttonConnect.Text == "Подключиться")
            {
                try
                {
                    mySerialPort.PortName = comboBoxPorts.Text;
                    mySerialPort.Open();
                    comboBoxPorts.Enabled = false;
                    buttonConnect.Text = "Отключиться";
                }
                catch(Exception exc)
                {
                    MessageBox.Show("Ошибка подключения " + exc);
                }
            }

            else if (buttonConnect.Text == "Отключиться")
            {
                mySerialPort.Close();
                buttonConnect.Text = "Подключиться";
                comboBoxPorts.Enabled = true;
            }
        }

        private void buttonUpdatePorts_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBoxPorts.Text = "";
            comboBoxPorts.Items.Clear();

            if (ports.Length != 0)
            {
                comboBoxPorts.Items.AddRange(ports);
                comboBoxPorts.SelectedIndex = 0;
            }
        }

        private void mySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = mySerialPort.ReadLine();
            string[] dataArr = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            label1.Text = dataArr[0];
            label4.Text = dataArr[1];
            chart1.Series[0].Points.AddY(dataArr[0]);
            chart1.Series[1].Points.AddY(dataArr[1]);
        }
    }
}

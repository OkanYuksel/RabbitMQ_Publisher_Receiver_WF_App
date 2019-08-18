using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ_Publisher_Receiver_WF_App.RabbitMQ_Helper_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RabbitMQ_Publisher_Receiver_WF_App
{
    public partial class Form1 : Form
    {
        private static readonly string _queueName = "Sample Queue";
        private static RabbitMQ_Operations _operationHelper = null;
        private RabbitMQService _rabbitMQService = null;
        private Boolean isProcessesStartedSuccesfully = false;
        private int messageCounter = 0;

        public Form1()
        {
            InitializeComponent();
            isProcessesStartedSuccesfully = processStarter();
        }
        public Boolean processStarter()
        {
            Boolean isSuccess = false;
            try
            {
                _rabbitMQService = new RabbitMQService();
                _operationHelper = new RabbitMQ_Operations();

                isSuccess = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Start Operations Were Failed. Exception : " + ex.ToString());
            }
            return isSuccess;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                button2.Enabled = true;
                if (isProcessesStartedSuccesfully)
                {
                    _operationHelper.CreateQueue(_queueName, _rabbitMQService);
                }
                else
                {
                    MessageBox.Show("Start Operations Were Failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.ToString());
            }
            finally
            {
                button1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                button2.Enabled = false;
                if (isProcessesStartedSuccesfully)
                {
                    _operationHelper.Publisher(_queueName, "Sample message!.." + messageCounter, _rabbitMQService);
                    messageCounter += 1;
                    addItemToListView(_operationHelper.Receiver(_queueName, _rabbitMQService));
                }
                else
                {
                    MessageBox.Show("Start Operations Were Failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.ToString());
            }
            finally
            {
                button2.Enabled = true;
            }
        }

        public void addItemToListView(String message)
        {
            listBox1.Items.Add(message);
        }
    }
}

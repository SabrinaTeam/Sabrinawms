using BLL;
using MODEL;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FrmInvoicePrint : Form
    {
       
        private MessageQueue queue;

        public List<mesEmployee> emps;
        public List<mesOrg> Orgs;
        mesEmployeeManager empm = new mesEmployeeManager();
        CompletedToMesManager cmm = new CompletedToMesManager();
        mesOrgManager orgm = new mesOrgManager();
        public string SAAUlr = "http://192.168.4.251:5000/api/process";
        public string SAATest = "http://192.168.4.251:5001/api/reportPlace";
        public string TOPUlr = "http://192.168.7.240:5000/api/process";
        public string TOPTest = "http://192.168.7.240:5001/api/reportPlace";
        bool isRead = false;
        public string orgName = "SAA";
        public string processID = "8";

        DataTable invoiceDataSource = new DataTable();

        ConnectionFactory factory = new ConnectionFactory();
        IConnection connection;
        IModel channel;
        public string HostName = "172.16.1.219";
        public string UserName = "sabrina";
        public string Password = "sabrina";
        public bool autoRecovery = true;
        private static FrmInvoicePrint frm;
        public FrmInvoicePrint()
        {
            factory.HostName = this.HostName;
            factory.UserName = this.UserName;
            factory.Password = this.Password;
            factory.AutomaticRecoveryEnabled = this.autoRecovery;
            factory.RequestedHeartbeat = 10;
            

            InitializeComponent();
        }
        public static FrmInvoicePrint GetSingleton()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new FrmInvoicePrint();
            }
            return frm;
        }

        private void FrmInvoicePrint_Resize(object sender, EventArgs e)
        {

        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            this.button2.Enabled = false;
            this.isRead = true;

            ThreadStart childref = new ThreadStart(receivedkibaQueue);
            // Console.WriteLine("In Main: Creating the Child thread");
            Thread childThread = new Thread(childref);
            childThread.IsBackground = true;
            childThread.Start();
            //  Console.ReadKey();

        }
        //  public static string queueName = "kibaQueue";
        public void receivedkibaQueue()
        {
            ConnectionFactory factory = new ConnectionFactory();
            IConnection connection;
            IModel channel;
            factory.HostName = "172.16.1.219";
            factory.UserName = "sabrina";
            factory.Password = "sabrina";
            factory.AutomaticRecoveryEnabled = true;
            factory.RequestedHeartbeat = 10;
            string queueName = "10";
            string exchangeName = "SAA";
            connection = factory.CreateConnection();

            if (connection.IsOpen)
            {
                channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: exchangeName, ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueBind(queueName, exchangeName, ExchangeType.Direct, null);
                channel.BasicQos(0, 1, false);
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    string orders = ByteArrayToObject(body).ToString();
                  //  var message = Encoding.UTF8.GetString(body);
                    SetText(orders);
                    channel.BasicAck(ea.DeliveryTag, true);
                };

                channel.BasicConsume(queue: queueName, noAck: false, consumer: consumer);
            } 


        }

        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        // Convert a byte array to an Object
        private Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);

            return obj;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.isRead = false;
            this.button2.Enabled = true;
        }

        private delegate void SetTextCallback(string text);
        //在给textBox1.text赋值的地方调用以下方法即可
        private void SetText(string text)
        {
            // InvokeRequired需要比较调用线程ID和创建线程ID
            // 如果它们不相同则返回true
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text += text + "\r\n";
            }
        }

        private void butSendTest_Click(object sender, EventArgs e)
        {
            ThreadStart childref = new ThreadStart(sendPush);
            Thread childThread = new Thread(childref);
            childThread.IsBackground = true;
            childThread.Start();
        }
        public void sendPush()
        {
            string order = this.textBox1.Text;
            bool pushResult = false;
            while (!pushResult)
            {
                pushResult = PushQueues(order);
            }

        }
         
        public bool PushQueues(string order)
        {
            //  ConnectionFactory factory = new ConnectionFactory();
            //   IConnection connection;
            // IModel channel;
            // factory.HostName = "172.16.1.219";
            //  factory.UserName = "sabrina";
            //  factory.Password = "sabrina";
            //  factory.AutomaticRecoveryEnabled = true;
            //  factory.RequestedHeartbeat = 10;
            //   string queueName =this.orgName + ;
            //   string exchangeName = "kibaQueue";

            string Org = "SAA";
            string queueName = Org + "-" + this.processID;
            string exchangeName = Org;
            this.connection = factory.CreateConnection();


            bool boolresult = false;
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            /*
             * 创建一个名为 myQueue1 的消息队列，如果名称相同不会重复创建，参数解释：
             * 参1：myQueue1, 消息队列名称；
             * 参2：false, 是否持久化，持久化的队列会存盘，服务器重启后任然存在；
             * 参3：false, 是否为排他队列，排他队列表示仅对首次声明它的连接可见，并在连接断开时自动删除。这种队列适用于一个客户端同时发送和读取消息的应用场景。
             * 参4：false, 是否自动删除，自动删除的前提是：至少有一个消费者连接到这个队列，之后所有与这个队列连接的消费者都断开时，才会自动删除。
             * 参5：设置队列的其他一些参数，如 x-rnessage-ttl、x-expires、x-rnax-length、x-rnax-length-bytes、x-dead-letter-exchange、x-deadletter-routing-key、x-rnax-priority 等。
             */

            // exchangeName 交换器名称  ExchangeType 交换机类型(Direct,Panout,Topic,Headers)
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);//创建一个名称为kibaqueue的消息队列

            // queueName 队列名，  durable 持久化  exclusive 是否排他  autoDelete 自动删除
            channel.QueueDeclare(queueName, false, false, false, null);//创建一个名称为kibaqueue的消息队列

            // queueName 队列名  exchangeName 交换器名   ExchangeType.Direct 路由KEY
            channel.QueueBind(queueName, exchangeName, ExchangeType.Direct, null);
            channel.ConfirmSelect();
            byte[] message = ObjectToByteArray(order);
            IBasicProperties properties = channel.CreateBasicProperties();

            //持久化方式一 旧
            //properties.SetPersistent(true);

            // 持久化方式二  新
            properties.DeliveryMode = 2;

            // 推送消息
            channel.BasicPublish("", queueName, properties, message);
            try
            {
                if (channel.WaitForConfirms())
                {
                    boolresult = true;
                }
                else
                {
                    boolresult = false;
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return boolresult;
        }
    }
}

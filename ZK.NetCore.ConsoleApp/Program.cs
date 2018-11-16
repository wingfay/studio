using System;
using System.Collections.Generic;
using System.Text;
using ZK.NetCore.Util;
using Amqp;
using Amqp.Framing;
using System.Threading;

namespace ZK.NetCore.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //int iteration = 100 * 1000;

            //string s = "";
            //CodeTimer.Time("String Concat", iteration, () => { s += "a"; });

            //StringBuilder sb = new StringBuilder();
            //CodeTimer.Time("StringBuilder", iteration, () => { sb.Append("a"); });


            //string address = "amqp://guest:guest@127.0.0.1:5672";
            //if (args.Length > 0)
            //{
            //    address = args[0];
            //}

            //// uncomment the following to write frame traces
            ////Trace.TraceLevel = TraceLevel.Frame;
            ////Trace.TraceListener = (l, f, a) => Console.WriteLine(DateTime.Now.ToString("[hh:mm:ss.fff]") + " " + string.Format(f, a));

            //Console.WriteLine("Running request client...");
            //new Client(address).Run();

            var x = 10;
            while(x-->0)
            {
                System.Console.WriteLine(x);
            }

            System.Console.ReadKey();

        }

        class Client
        {
            readonly string address;
            string replyTo;
            Connection connection;
            Session session;
            ReceiverLink receiver;
            SenderLink sender;
            int offset;

            public Client(string address)
            {
                this.address = address;
                this.replyTo = "client-" + Guid.NewGuid().ToString();
            }

            public void Run()
            {
                while (true)
                {
                    try
                    {
                        this.Cleanup();
                        this.Setup();

                        this.RunOnce();

                        this.Cleanup();
                        break;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Reconnect on exception: " + exception.Message);

                        Thread.Sleep(5000);
                    }
                }
            }

            void Setup()
            {
                this.connection = new Connection(new Address(address));
                this.session = new Session(connection);

                Attach recvAttach = new Attach()
                {
                    Source = new Source() { Address = "request_processor" },
                    Target = new Target() { Address = this.replyTo }
                };

                this.receiver = new ReceiverLink(session, "request-client-receiver", recvAttach, null);
                this.receiver.Start(300);
                this.sender = new SenderLink(session, "request-client-sender", "request_processor");
            }

            void Cleanup()
            {
                var temp = Interlocked.Exchange(ref this.connection, null);
                if (temp != null)
                {
                    temp.Close();
                }
            }

            void RunOnce()
            {
                Message request = new Message("hello " + this.offset);
                request.Properties = new Properties() { MessageId = "command-request", ReplyTo = this.replyTo };
                request.ApplicationProperties = new ApplicationProperties();
                request.ApplicationProperties["offset"] = this.offset;
                sender.Send(request, null, null);
                Console.WriteLine("Sent request {0} body {1}", request.Properties, request.Body);

                while (true)
                {
                    Message response = receiver.Receive();
                    receiver.Accept(response);
                    Console.WriteLine("Received response: {0} body {1}", response.Properties, response.Body);

                    if (string.Equals("done", response.Body))
                    {
                        break;
                    }

                    this.offset = (int)response.ApplicationProperties["offset"];
                }
            }
        }

        ///// <summary>
        ///// This example assumes a topic and a subscirption named "sub1" is precreated.
        ///// Example.Entity should be set to the topic name.
        ///// </summary>
        //class TopicExample : Example
        //{
        //    public override void Run()
        //    {
        //        this.SendReceiveAsync(10).GetAwaiter().GetResult();
        //    }

        //    async Task SendReceiveAsync(int count)
        //    {
        //        Trace.WriteLine(TraceLevel.Information, "Establishing a connection...");
        //        Connection connection = await Connection.Factory.CreateAsync(this.GetAddress());

        //        Trace.WriteLine(TraceLevel.Information, "Creating a session...");
        //        Session session = new Session(connection);

        //        Trace.WriteLine(TraceLevel.Information, "Creating a sender link...");
        //        SenderLink sender = new SenderLink(session, "topic-sender-link", this.Entity);

        //        Trace.WriteLine(TraceLevel.Information, "Sending {0} messages...", count);
        //        for (int i = 0; i < count; i++)
        //        {
        //            Message message = new Message();
        //            message.Properties = new Properties() { MessageId = "topic-test-" + i };
        //            message.BodySection = new Data() { Binary = Encoding.UTF8.GetBytes("message #" + i) };
        //            await sender.SendAsync(message);
        //        }

        //        Trace.WriteLine(TraceLevel.Information, "Closing sender...");
        //        await sender.CloseAsync();

        //        Trace.WriteLine(TraceLevel.Information, "Receiving messages from subscription...");
        //        ReceiverLink receiver = new ReceiverLink(session, "receiver-link", this.Entity + "/Subscriptions/sub1");
        //        for (int i = 0; i < count; i++)
        //        {
        //            Message message = await receiver.ReceiveAsync();
        //            if (message == null)
        //            {
        //                break;
        //            }

        //            receiver.Accept(message);
        //        }

        //        Trace.WriteLine(TraceLevel.Information, "Closing receiver...");
        //        await receiver.CloseAsync();

        //        Trace.WriteLine(TraceLevel.Information, "Shutting down...");
        //        await session.CloseAsync();
        //        await connection.CloseAsync();
        //    }
        //}
    }
}

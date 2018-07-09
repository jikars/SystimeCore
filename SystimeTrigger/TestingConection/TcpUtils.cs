using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SystimeTrigger.TestingConection
{
    internal class TcpUtils
    {
        private String Ip { get; set; }

        private int Port { get; set; }
        private bool IsConnectionSuccessful = false;
        private ManualResetEvent TimeoutObject { get; set; }

        internal TcpUtils(String ip, int port)
        {
            Ip = ip;
            Port = port;
            TimeoutObject = new ManualResetEvent(false);
        }


        //internal Boolean ConectionAvailable()
        //{
        //    TcpClient tcP = new TcpClient();
        //    try
        //    {
        //        tcP.ReceiveTimeout = 200;
        //        tcP.Connect(Ip, Port);
        //        return tcP.Connected;
        //    }
        //    catch (SocketException)
        //    {
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        tcP.Close();
        //    }
        //}






        public Boolean ConectionAvailable()
        {
            TcpClient tcpclient = new TcpClient();

            try
            {
                TimeoutObject.Reset();
                tcpclient.BeginConnect(Ip, Port,
                    new AsyncCallback(CallBackMethod), tcpclient);

                if (TimeoutObject.WaitOne(200, false))
                    return IsConnectionSuccessful;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                tcpclient?.Close();
            }

        }


        private void CallBackMethod(IAsyncResult asyncresult)
        {
            TcpClient tcpclient = null;
            try
            {
                IsConnectionSuccessful = false;
                tcpclient = asyncresult.AsyncState as TcpClient;

                if (tcpclient.Client != null)
                {
                    tcpclient.EndConnect(asyncresult);
                    IsConnectionSuccessful = true;
                }
            }
            catch (Exception)
            {
                IsConnectionSuccessful = false;
            }
            finally
            {
                tcpclient?.Close();
                TimeoutObject.Set();
            }
        }
    }
}

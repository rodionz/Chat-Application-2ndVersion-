﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using CommonTypes;
using System.IO;

namespace ClientBL
{
    public class UserLogic
    {

        public  delegate void Exseptions();
        public delegate void ClientBLEvents(MessageData mDAta);
        public static event Exseptions NoServer;
        public static event ClientBLEvents MessageRecieved;
        public static List<UserData> listofUserfortheUsers;
        public static bool GlobalValidIpandPort;
        public static  NetworkAction LolacAction;
        public static MessageData LockalmesData;
        public static TcpClient client;
        public static NetworkStream localNetStream;
        public static BinaryFormatter bFormatt;





        public static void MainClienFinction(MessageData mesData)

        {
            Task t1 = Task.Run(() => ConnecttoServer(mesData));
        }




        public static void IPAndPortValidation(MessageData premesData)

        {
            MessageData returning;

            TcpClient preclient = new TcpClient();

            try
            {
                preclient.Connect(premesData.Userdat.IPadress, premesData.Userdat.Portnumber);

                using (NetworkStream netStream = preclient.GetStream())
                {
                    BinaryFormatter bFormat = new BinaryFormatter();
                    bFormat.Serialize(netStream, premesData);
                    returning = (MessageData)bFormat.Deserialize(netStream);
                    listofUserfortheUsers = returning.listofUsers;
                    GlobalValidIpandPort = true;
                }
            }

            catch (SocketException SE)
            {
                NoServer();
            }

            finally
            {
                preclient.Close();
            }

        }




        public static void ConnecttoServer(MessageData mData)
        {
            TcpClient client = new TcpClient();
            MessageData returning;
            client.Connect(IPAddress.Parse(mData.Userdat.IPadress), mData.Userdat.Portnumber);
            NetworkStream usernetstream;
            BinaryFormatter Bformat = new BinaryFormatter();



            while (true)
            {


                switch (LolacAction)
                {
                    case NetworkAction.Connection:
                        usernetstream = client.GetStream();
                        Bformat.Serialize(usernetstream, mData);
                        //returning = (MessageData)Bformat.Deserialize(usernetstream);
                        LolacAction = NetworkAction.None;
                        break;

                    case NetworkAction.Sendmessage:
                        usernetstream = client.GetStream();
                        Bformat.Serialize(usernetstream, LockalmesData);
                        LolacAction = NetworkAction.ReceiveMesg;
                        //returning = (MessageData)Bformat.Deserialize(innersetrem);
                        //LolacAction = NetworkAction.None;
                        //MessageRecieved(LockalmesData);
                        break;

                    case NetworkAction.ReceiveMesg:
                        usernetstream = client.GetStream();
                        //somethin wrong here
                        returning = (MessageData)Bformat.Deserialize(usernetstream);

                        LolacAction = NetworkAction.None;
                        //MessageRecieved(LockalmesData);
                        break;

                    case NetworkAction.None:
                        break;

                }



            }
            //});
        }

        //public static void MainClienFinction(MessageData mesData, NetworkAction action)

        //{
        //    Task t2 = Task.Run(() => ConnecttoServer(mesData));
        //}


        //public static void  IPAndPortValidation(MessageData premesData)

        //{
        //    MessageData returning;

        //    TcpClient preclient = new TcpClient();

        //    try
        //    {
        //        preclient.Connect(premesData.Userdat.IPadress, premesData.Userdat.Portnumber);

        //        using (NetworkStream netStream = preclient.GetStream())
        //        {
        //            bFormatt = new BinaryFormatter();
        //            bFormatt.Serialize(netStream, premesData);
        //            returning = (MessageData)bFormatt.Deserialize(netStream);
        //            listofUserfortheUsers = returning.listofUsers;
        //            GlobalValidIpandPort = true;
        //        }
        //    }

        //    catch (SocketException SE)
        //    {
        //        NoServer();
        //    }

        //    finally
        //    {
        //        preclient.Close();
        //    }

        //}




        //public static void ConnecttoServer (MessageData mData,TcpClient client)
        //{

        //    client.Connect(IPAddress.Parse (mData.Userdat.IPadress), mData.Userdat.Portnumber );
        //    bFormatt = new BinaryFormatter();
        //    localNetStream = client.GetStream();
        //    Task listenig = Task.Run(() => StarListentoIncomingMessages(localNetStream));
        //    //bFormatt.Serialize(localNetStream, mData);


        //}

        //private static void StarListentoIncomingMessages(NetworkStream userNetstrem)
        //{
        //    while(true)
        //    {
        //        if (userNetstrem.DataAvailable)
        //        {
        //            bFormatt = new BinaryFormatter();
        //            LockalmesData = (MessageData)bFormatt.Deserialize(localNetStream);
        //            MessageRecieved(LockalmesData);
        //        }
        //    }
        //}


        //private static void SendMessage(MessageData mesData, TcpClient clien3)
        //{

        //    //clien3.Connect(IPAddress.Parse(mesData.Userdat.IPadress), mesData.Userdat.Portnumber);
        //   NetworkStream sendmessagstrem = clien3.GetStream();
        //   bFormatt = new BinaryFormatter();
        //    bFormatt.Serialize(localNetStream, mesData);
        //}





        //public void Disconnect()
        //{

        //}



        //public static void ColorwasChanged(UserData uData)
        //{


        //}

        //public static void FontwasChanged(UserData uData)
        //{

        //}
    }
}

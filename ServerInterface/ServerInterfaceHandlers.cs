﻿using System;
using System.Windows.Forms;
using CommonTypes;





namespace ServerInterface
{

    public partial class ServerInterfaceClass : Form
    {

      
        public  void NewUserEvenHandler(MessageData mymesdata)

        {
         
            if (CurrentUsersListbox.InvokeRequired || HistoryListbox.InvokeRequired)
            {
               Action<MessageData> stc = NewUserEvenHandler;
                this.Invoke(stc, new object[] { mymesdata });

            }

            else
            {
              
                CurrentUsersListbox.Items.Add(mymesdata.Userdat.Username +" IP: " + mymesdata.Userdat.IPadress + 
                    " Port: " + mymesdata.Userdat.Portnumber);
                HistoryListbox.Items.Add(mymesdata.Textmessage + mymesdata.Time.ToLongTimeString()  );
            }
        }

    
        public void MessagesentHandler(MessageData mData)
        {
            DateTime current = DateTime.Now;


            if (ChatListBox.InvokeRequired || CurrentUsersListbox.InvokeRequired || HistoryListbox.InvokeRequired )
            {
                Action<MessageData> dicon = MessagesentHandler;
                this.Invoke(dicon, new object[] { mData });
            }



            else
            {     if (mData.action == NetworkAction.ConectionREsponse)
                {
                    ChatListBox.Items.Add("Server says: " + mData.Textmessage);
                }

            // In the case of unexpected user disconnection
                   else if (mData.action == NetworkAction.UserDisconnection)
                {
                    ChatListBox.Items.Add("Server says: " + mData.Userdat.Username + " was disconnected");
                    CurrentUsersListbox.Items.Remove(CurrentUsersListbox.Items.Contains(mData.Userdat.Username));
                    HistoryListbox.Items.Add(mData.Userdat.Username + " was disconnected" + current.ToLongTimeString());
                }


            else if (mData.action == NetworkAction.SeverDisconnection)
                {

                    ChatListBox.Items.Add(mData.Textmessage);
                }
                else
                {
                    ChatListBox.Items.Add(mData.Userdat.Username + " says: " + mData.Textmessage);
                }
            }
        }

        public  void NoServerHandler()
        {
        
            if (ServerPanel.InvokeRequired)
            {
                Action dicon = NoServerHandler;
                this.Invoke(dicon, new object[] {  });

            }

            else
            {
                CurrentUsersListbox.Items.Clear();
                HistoryListbox.Items.Clear();
                ChatListBox.Items.Clear();
                RedLightPanel.Visible = true;
                GreenLightPanel.Visible = false;
                StartServerButton.Enabled = true;
                StopServerButton.Enabled = false;
            }
        }


        // For usual user disconnection
        public void DisconnectUserHAndler(UserData uData)
        {
            if (ServerPanel.InvokeRequired)
            {
                Action <UserData> dicon = DisconnectUserHAndler;
                Invoke(dicon, new object[] {uData });
            }


            else
            {
                DateTime current = DateTime.Now;
                    
             for(int i = 0;  i< CurrentUsersListbox.Items.Count; i++)

                {
                    if (CurrentUsersListbox.Items[i].ToString().Contains(uData.Username))
                    {
                        CurrentUsersListbox.Items.Remove(CurrentUsersListbox.Items[i]);
                        break;
                    }

                }
                            
                
                HistoryListbox.Items.Add(uData.Username + " was disconnected " + current.ToLongTimeString());
                ChatListBox.Items.Add("Server says: " + uData.Username + " was disconnected " );

            }
            
        }

}
}

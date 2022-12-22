using System.Windows.Forms;
using DLMS;
using System;

namespace ucCustomControl
{
    public delegate void AddObjects(TreeNode[] nodes);

    // This delegate enables asynchronous calls for setting
    // the text property on a TextBox control.
    public delegate void SetTextCallback(string text, Control TextBox);

    class MsgDisplay
    {
        static TextBox T;
        static ListBox L;
        static TreeView TR;
        static TextBox Tcomm;
        static TextBox TReadResp;
        static TextBox TWriteResp;

        public static TextBox ReadResponseViewer
        {
            get { return TReadResp; }
            set { TReadResp = value; }
        }
        public static TextBox WriteResponseViewer
        {
            get { return TWriteResp; }
            set { TWriteResp = value; }
        }

        public static TextBox MessageViewer
        {
            get
            {
                return T;
            }

            set
            {
                T = value;
            }
        }

        public static ListBox ObisCodesList
        {
            get
            {
                return L;
            }

            set
            {
                L = value;
            }
        }

        public static TreeView ObisCodesTree
        {
            get { return TR; }
            set { TR = value; }
        }
        public static TextBox CommunicationViewer
        {
            get { return Tcomm; }
            set { Tcomm = value; }
        }

        public static void AddObisCodetoList(byte[] obis)
        {
            ObisCodesList.Items.Add(DLMS_Common.ArrayToHexString(obis));
        }
        public static void AddObisCodetoList(string obis)
        {
            ObisCodesList.Items.Add(obis);
        }

        public static void AddObjectstoTree(byte[] obj)
        {
            ObisCodesTree.Nodes.Add(new TreeNode(DLMS_Common.ArrayToHexString(obj)));
        }

        public static TreeNode AddObjecttoTree(string obj)
        {
            return ObisCodesTree.Nodes.Add(obj);
        }

        public static void AddNodesInTree(TreeNode[] TreeNodes)
        {
            ObisCodesTree.Nodes.AddRange((TreeNode[])TreeNodes);
        }

        public static void AddSubNodeInTree(TreeNode parent, TreeNode subNode)
        {
            parent.Nodes.Add(subNode);
        }


        public static void AppendMsg(string s)
        {
            #region Commented Code_Section

            //string prev_msg = MessageViewer.Text;

            ///// 4K Characters in Debug Message Box
            //if (!string.IsNullOrEmpty(prev_msg) && prev_msg.Length >= (4 * 1024))
            //{
            //    string txt = prev_msg;
            //    txt = txt.Substring(0, prev_msg.Length - 512);
            //    MessageViewer.Text = string.Format("{0}...<Terminated>", txt);
            //} 

            #endregion

            ///prev_msg = MessageViewer.Text;
            string txt_Message = ">> " + s + "\r\n\r\n";

            /// MessageViewer.Text = txt_Message + prev_msg;
            MsgDisplay.AppendTextMessage(txt_Message, MessageViewer);

            /// MessageViewer.SelectedText = txt_Message;   /// MessageViewer.Text.Length;
            /// MessageViewer.Focus();
        }


        public static void AppendCommResp(string identifier, string s, DateTime timeStamp)
        {
            /// CommunicationViewer.Text = string.Format("{0} {1:dd/MM/yyyy HH:mm:ss:FF} Response> {2} \r\n\r\n{3}", identifier, timeStamp, s, CommunicationViewer.Text);

            string txt_message = String.Format("{0} {1:dd/MM/yyyy HH:mm:ss:FF} Response> {2} \r\n\r\n",
                                                identifier, timeStamp, s);
            MsgDisplay.AppendTextMessage(txt_message, CommunicationViewer);
        }

        public static void AppendCommReq(string identifier, string s, DateTime timeStamp)
        {
            #region Commented Code_Section

            /// Max 8K Character In Communication Block
            /// if (CommunicationViewer.Text != null && CommunicationViewer.Text.Length >= (8 * 1024))
            /// {
            ///     string txt = CommunicationViewer.Text;
            ///     txt = txt.Substring(0, CommunicationViewer.Text.Length - 512);
            ///     CommunicationViewer.Text = string.Format("{0}...<Terminated>", txt);
            /// } 

            #endregion

            string txt_message = string.Format("{0} {1:dd/MM/yyyy HH:mm:ss:FF} Request> {2}\r\n{3}\r\n\r\n", identifier, timeStamp, s,
                "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            MsgDisplay.AppendTextMessage(txt_message, CommunicationViewer);
        }


        #region Support_Function

        // This method demonstrates a pattern for making thread-safe
        // calls on a Windows Forms control. 
        //
        // If the calling thread is different from the thread that
        // created the TextBox control, this method creates a
        // SetTextCallback and calls itself asynchronously using the
        // Invoke method.
        //
        // If the calling thread is the same as the thread that created
        // the TextBox control, the Text property is set directly. 

        public static void AppendTextMessage(string text, Control textBox)
        {
            try
            {
                /// InvokeRequired required compares the thread ID of the
                /// calling thread to the thread ID of the creating thread.
                /// If these threads are different, it returns true.
                if (textBox.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(MsgDisplay.AppendTextMessage);
                    textBox.Invoke(d, new object[] { text, textBox });
                }
                else
                {
                    string prev_msg = textBox.Text;

                    /// 30K Characters in Debug Message Box
                    if (!string.IsNullOrEmpty(prev_msg) && prev_msg.Length >= (30 * 1024))
                    {
                        string txt = prev_msg;
                        txt = txt.Substring(0, prev_msg.Length - 512);
                        prev_msg = string.Format("{0}...<Terminated>", txt);
                    }

                    if (!string.IsNullOrEmpty(prev_msg))
                        textBox.Text = text + prev_msg + "\r\n";
                    else
                        textBox.Text += text + "\r\n";
                }
            }
            catch (Exception)
            {
                textBox.Text = "Error Display Message";
            }
        }

        #endregion

    }
}

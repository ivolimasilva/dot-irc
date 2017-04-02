using Common;
using Server.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Server
{
    public partial class ServerHome : Form
    {
        private FileSystemWatcher watcher = new FileSystemWatcher();
        private List<User> users = new List<User>();

        public ServerHome()
        {
            users = Files.Load(Files.filename);

            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(this.ServerHome_Close);

            startListView();

            #region File watcher
            watcher.Path = ".";
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite;
            watcher.Filter = "users.xml";

            // Add event handlers.
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Changed += new FileSystemEventHandler(OnChanged);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
            #endregion

        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            XDocument file;
            using (var mutex = new Mutex(false, "Users"))
            {
                mutex.WaitOne();
                file = XDocument.Load(e.FullPath);
                mutex.ReleaseMutex();
            }

            users.Clear();
            users =
                file.Root
                .Elements("User")
                .Select(_user => new User((string)_user.Element("Username"),
                    (string)_user.Element("Name"),
                    (string)_user.Element("Password"),
                    (int)_user.Element("Port"),
                    (bool)_user.Element("Online"))).ToList();

            startListView();
            lblAccountNo.Text = "Total Accounts: " + users.Count;
            lblOnlineAccounts.Text = "Accounts Online:" + users.FindAll(user => user.online).ToList().Count;
        }

        public void startListView()
        {
            listView.Items.Clear();
            
            foreach (User user in users)
            {
                ListViewItem item = new ListViewItem(user.username);
                if (user.online)
                {
                    item.SubItems.Add("online");
                }
                else
                {
                    item.SubItems.Add("offline");
                }

                if (listView.InvokeRequired)
                    listView.BeginInvoke((MethodInvoker)delegate ()
                    {
                        listView.Items.Add(item);
                    });
                else
                {
                    listView.Items.Add(item);
                }
            }
            lblAccountNo.Text = "Total Accounts: " + users.Count;

            // listView.OwnerDraw = true;
            // listView.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(list_DrawColumnHeader);
            // listView.DrawItem += new DrawListViewItemEventHandler(list_DrawItem);
            // listView.DrawSubItem += new DrawListViewSubItemEventHandler(listView_DrawSubItem);
        }

        private void list_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;

                using (var headerFont = new Font("Calibri", 11, FontStyle.Bold))
                {
                    //e.Graphics.FillRectangle(Brushes.Gray, e.Bounds);
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);
                }
            }
        }

        private void ServerHome_Close(object sender, EventArgs e)
        {
            foreach (User user in users)
            {
                user.online = false;
            }
            Files.Save(Files.filename, users);
            watcher.Dispose();      
        }

        private void ServerHome_Load(object sender, EventArgs e)
        {
            users = Files.Load(Files.filename);
            startListView();
        }
    }
}

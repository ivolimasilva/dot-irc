using Common;
using Server.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            InitializeComponent();

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

            users = Files.Load("users.xml");

            startListView();
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
        }

        private void startListView()
        {
            foreach (User user in users)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(user.username);
                if (user.online)
                    item.SubItems.Add("online");
                else item.SubItems.Add("offline");

                if (listView.InvokeRequired)
                    listView.BeginInvoke((MethodInvoker)delegate ()
                    {
                        listView.Items.Add(item);
                    });
            }
        }
    }
}

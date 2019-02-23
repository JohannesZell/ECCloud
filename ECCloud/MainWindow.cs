using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECCloud
{
    public partial class MainWindow : Form
    {
        private bool status;
        private Connector con;
        private int connectionStatus = 0; //0 = not connected - 1 = connected
        private Timer timer;

        public MainWindow()
        {
            InitializeComponent();
            con = new Connector();
            PopulateTreeView1();
            PopulateTreeView2();
            AllowDrop = true;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        public void setSessionID(int sessionID)
        {
            toolStripStatusLabel1.Text = Convert.ToString(sessionID);
            timer = new Timer();
            timer.Interval = 600000;
            timer.Tick += new EventHandler(TimerEventProcessor);
            timer.Start();
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            timer.Stop();
            setConnectionStatus(0);
            MessageBox.Show("Session abgelaufen, bitte neu anmelden!");
            //Zurueck zur Anmeldung wechseln
        }

        public void setConnectionStatus(int connectionStatus)
        {
            this.connectionStatus = connectionStatus;
            if(this.connectionStatus == 1)
            {
                toolStripConnection.Text = "Connected";
                toolStripConnection.ForeColor = Color.Green;
            }
            else
            {
                toolStripConnection.Text = "Not Connected";
                toolStripConnection.ForeColor = Color.Red;
            }
        }

        private void PopulateTreeView1()
        {
            TreeNode rootNode;

            DirectoryInfo info = new DirectoryInfo(@"C:\Users\johan\ECcloud\Local");
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }

        private void PopulateTreeView2()
        {
            TreeNode rootNode;

            DirectoryInfo info = new DirectoryInfo(@"C:\Users\johan\ECcloud\Remote");
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView2.Nodes.Add(rootNode);
            }
        }

        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";
                subSubDirs = subDir.GetDirectories();
                if (subSubDirs.Length != 0)
                {
                    GetDirectories(subSubDirs, aNode);
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            ListViewLocal.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                    {new ListViewItem.ListViewSubItem(item, "Directory"),
             new ListViewItem.ListViewSubItem(item,
                dir.LastAccessTime.ToShortDateString())};
                item.SubItems.AddRange(subItems);
                ListViewLocal.Items.Add(item);
            }
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                    { new ListViewItem.ListViewSubItem(item, "File"),
             new ListViewItem.ListViewSubItem(item,
                file.LastAccessTime.ToShortDateString())};

                item.SubItems.AddRange(subItems);
                ListViewLocal.Items.Add(item);
            }

            ListViewLocal.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            ListViewRemote.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                    {new ListViewItem.ListViewSubItem(item, "Directory"),
             new ListViewItem.ListViewSubItem(item,
                dir.LastAccessTime.ToShortDateString())};
                item.SubItems.AddRange(subItems);
                ListViewRemote.Items.Add(item);
            }
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                    { new ListViewItem.ListViewSubItem(item, "File"),
             new ListViewItem.ListViewSubItem(item,
                file.LastAccessTime.ToShortDateString())};

                item.SubItems.AddRange(subItems);
                ListViewRemote.Items.Add(item);
            }

            ListViewRemote.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // Proceed with the drag and drop, passing in the list item.                   
                DragDropEffects dropEffect = ListViewLocal.DoDragDrop(ListViewLocal.SelectedItems, DragDropEffects.Move);
            }
        }

        private void listView2_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // Proceed with the drag and drop, passing in the list item.                   
                DragDropEffects dropEffect = ListViewRemote.DoDragDrop(ListViewRemote.SelectedItems, DragDropEffects.Move);
            }
        }

        private void listView2_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
                e.Effect = e.AllowedEffect;
        }

        private void listView2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
            {
                if (e.Effect == DragDropEffects.Copy)
                {
                    foreach (ListViewItem current in (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)))
                    {
                        ListViewRemote.Items.Add((ListViewItem)current.Clone());
                    }
                }
                else
                {
                    foreach (ListViewItem current in (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)))
                    {
                        current.Remove();
                        ListViewRemote.Items.Add((ListViewItem)current);
                    }
                }
            }
        }

        private void listView1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
                e.Effect = e.AllowedEffect;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
            {
                if (e.Effect == DragDropEffects.Copy)
                {
                    foreach (ListViewItem current in (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)))
                    {
                        ListViewLocal.Items.Add((ListViewItem)current.Clone());
                    }
                }
                else
                {
                    foreach (ListViewItem current in (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)))
                    {
                        current.Remove();
                        ListViewLocal.Items.Add((ListViewItem)current);
                    }
                }
            }
        }

        private void startTimer()
        {
            timer.Start();
        }

        private void stopTimer()
        {
            if (timer != null)
            {
                timer.Stop();
            }
        }

        private void MainWindow_Deactivate(object sender, EventArgs e)
        {
            startTimer();
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            stopTimer();
        }
    }
}

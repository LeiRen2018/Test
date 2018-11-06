using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TreeDaTableHtml
{
    public partial class Form1 : Form
    {
        private enum Weather { 晴天=0,降温=1,寒潮=2}
        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            repositoryItemImageComboBox1.Items.AddEnum(typeof(Weather));
            repositoryItemImageComboBox1.Items[0].ImageIndex = 0;
            repositoryItemImageComboBox1.Items[1].ImageIndex = 1;
            repositoryItemImageComboBox1.Items[2].ImageIndex = 2;

            this.treeList1.AppendNode(new object[] { "2010.1.22","1", Weather.降温 }, null);
            this.treeList1.AppendNode(new object[] { "2010.1.23", "1", Weather.晴天 }, null);

            ImageList SelectTreeImageList = new ImageList();
            SelectTreeImageList.ColorDepth = ColorDepth.Depth32Bit;
            SelectTreeImageList.ImageSize = new Size(16, 16);
            SelectTreeImageList.Images.Add(Image.FromFile(@"E:\BaiduNetdiskDownload\BIM\BIM\Resources\icons\project_floder.png"));
            SelectTreeImageList.Images.Add(Image.FromFile(@"E:\BaiduNetdiskDownload\BIM\BIM\Resources\icons\project_floder.png"));
            treeList1.SelectImageList = SelectTreeImageList;
            LoadXmlTree(@"C:\Users\Administrator\Desktop\新建文本文档.xml");
        }

        public void LoadXmlTree(string xml)
        {
            System.Xml.Linq.XDocument xDoc = System.Xml.Linq.XDocument.Load(xml);

            TreeListViewItem item = new TreeListViewItem();
            string title = xDoc.Root.Attribute("name")?.Value ?? xDoc.Root.Name.LocalName;
            item.Text = title;
            item.ImageIndex = 0;
            item.SubItems.Add(xDoc.Root.Attribute("UniqueID")?.Value);
            item.SubItems.Add(xDoc.Root.Attribute("ItemType")?.Value);
            PopulateTree(xDoc.Root, item.Items);
            //treeListView1.Items.Add(item);
        }
        public void PopulateTree(XElement element, TreeListViewItemCollection items)
        {
            foreach (XElement node in element.Nodes())
            {
                TreeListViewItem item = new TreeListViewItem();
                string title = node.Name.LocalName.Trim();
                item.Text = title;
                if (title == "Device")
                {
                    var attr = node.Attribute("ItemType")?.Value;
                    switch (attr)
                    {
                        case "Channel": item.ImageIndex = 1; break;
                        case "RStreamer": item.ImageIndex = 3; break;
                        default: break;
                    }
                    item.SubItems.Add(node.Attribute("UniqueID")?.Value);
                    item.SubItems.Add(node.Attribute("ItemType")?.Value);
                }
                else
                {
                    item.ImageIndex = 2;
                    item.SubItems.Add(node.Attribute("Value")?.Value);
                }

                if (node.HasElements)
                {
                    PopulateTree(node, item.Items);
                }
                items.Add(item);
            }
        }

        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {

        }
    }
}

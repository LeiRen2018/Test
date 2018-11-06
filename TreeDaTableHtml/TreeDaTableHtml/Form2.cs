using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using DevExpress.Utils.Drawing;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList.ViewInfo;

namespace TreeDaTableHtml
{
    public partial class Form2 : Form
    {
        private enum Weather { 晴天 = 0, 降温 = 1, 寒潮 = 2 }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //CreateColumns(treeList1);
            //CreateNodes(treeList1);
            treeList1.DataSource = CreateTreeListTable();
            this.treeList1.KeyFieldName = "NodeCode";
            this.treeList1.ParentFieldName = "Others";
            treeList1.HierarchyFieldName = "NodeName";

            RepositoryItemTextEdit rite = new RepositoryItemTextEdit();
            
            //keydown事件            
            rite.DoubleClick += new EventHandler(DClick);

            treeList1.Columns["KeyFieldName"].ColumnEdit = rite;
            //treeList1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(gv_InitNewRow);
        }
        /// <summary>        
        /// /// 单元格回车        
        /// /// </summary>        
        /// /// <param name="sender"></param>        
        /// /// <param name="e"></param>        
        private void DClick(object o, EventArgs e)
        {
            treeList1.DataSource = CreateTreeListTable();
        }
        /// 构造一棵树型表结构
        private DataTable CreateTreeListTable()
        {
            DataTable dt = new DataTable();
            DataColumn dcOID = new DataColumn("KeyFieldName", Type.GetType("System.Int32"));
            DataColumn dcParentOID = new DataColumn("ParentFieldName", Type.GetType("System.Int32"));
            DataColumn dcNodeName = new DataColumn("NodeName", Type.GetType("System.String"));
            DataColumn dcNodeCode = new DataColumn("NodeCode", Type.GetType("System.Int32"));
            DataColumn dcOthers = new DataColumn("Others", Type.GetType("System.Int32"));
            dt.Columns.Add(dcOID);
            dt.Columns.Add(dcParentOID);
            dt.Columns.Add(dcNodeName);
            dt.Columns.Add(dcNodeCode);
            dt.Columns.Add(dcOthers);
            //以上代码完成了DataTable的构架，但是里面是没有任何数据的
            DataRow dr1 = dt.NewRow();
            dr1["KeyFieldName"] = 1;
            dr1["ParentFieldName"] = DBNull.Value;
            dr1["NodeName"] = "根节点名称";
            dr1["NodeCode"] = 1;
            dr1["Others"] = DBNull.Value;
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["KeyFieldName"] = 2;
            dr2["ParentFieldName"] = 1;
            dr2["NodeName"] = "节点子节点名称";
            dr2["NodeCode"] = 2;
            dr2["Others"] = 1;
            dt.Rows.Add(dr2);
            return dt;
        }
        private void CreateColumns(TreeList tl)
        {
            // 创建三个列。
            tl.BeginUpdate();
            tl.Columns.Add();
            tl.Columns[0].Caption = "Customer";
            tl.Columns[0].VisibleIndex = 0;
            tl.Columns.Add();
            tl.Columns[1].Caption = "Location";
            tl.Columns[1].VisibleIndex = 1;
            tl.Columns.Add();
            tl.Columns[2].Caption = "Phone";
            tl.Columns[2].VisibleIndex = 2;
            tl.Columns.Add();
            tl.Columns[3].Caption = "ID";
            tl.Columns[3].VisibleIndex = 3;
            tl.Columns.Add();
            tl.Columns[4].Caption = "IDName";
            tl.Columns[4].VisibleIndex = 4;
            tl.EndUpdate();
        }

        private void CreateNodes(TreeList tl)
        {
            tl.BeginUnboundLoad();

            for (int i = 0; i < 80000; i++)
            {
                // 创建根节点。
                TreeListNode parentForRootNodes = null;
                TreeListNode rootNode = tl.AppendNode(
                    new object[] { "Alfreds Futterkiste", "Germany, Obere Str. 57", "030-0074321",i,i },
                    parentForRootNodes);

                // 创建根节点的子节点
                tl.AppendNode(new object[] { "Suyama, Michael", "Obere Str. 55", "030-0074263", i, i },
                    rootNode);
                // 创造更多的节点
                // ...
            }
            tl.EndUnboundLoad();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            TreeListMultiSelection ListSelect = treeList1.Selection;
            if (ListSelect != null)
            {
                TreeListNode item = ListSelect[0];
                treeList1.DeleteNode(item);
                List<TreeListNode> a = new List<TreeListNode>();
                //treeList1.AppendNode(new object[] { "Suyama, Michael", "Obere Str. 55", "030-0074263" }, item);
            }
        }

        //添加
        private void ButAdd_Click(object sender, EventArgs e)
        {


            TreeListMultiSelection ListSelect = treeList1.Selection;
            string name= this.treeList1.FocusedNode[2].ToString() ;
            name = this.treeList1.FocusedNode.GetDisplayText(2).ToString();
            name = treeList1.FocusedNode.GetValue(2).ToString();
            int count = treeList1.Columns.Count;

            if (ListSelect != null)
            {
                TreeListNode item = ListSelect[0];
                string name1 = item.GetDisplayText("ParentFieldName");
                //for (int i = 0; i < count; i++)
                //{
                //    name = this.treeList1.FocusedNode[i].ToString();
                //    MessageBox.Show(name);
                //}

                //获取列名称
                //TreeListColumn column = treeList1.FocusedColumn;
                //if (column.FieldName == "自动计算")
                //{
                //    if (item.GetValue(1).ToString() == "土")
                //    {
                //        // 继续事件
                //        //e.Cancel = false;
                //    }
                //}
                TreeListNode ParentNode= treeList1.AppendNode(new object[] { "Suyama, Michael", "Obere Str. 55", "030-0074263" }, item);
                ParentNode.SetValue(treeList1.Columns[0], "显示的值");//添加第一节点显示的值 
                ParentNode.SetValue(treeList1.Columns[1], "显示的值");//添加第一节点显示的值 
                ParentNode.SetValue(treeList1.Columns[2], "显示的值");//添加第一节点显示的值 
                ParentNode.SetValue(treeList1.Columns[3], "显示的值");//添加第一节点显示的值 
            }
        }

        private void treeList1_CustomDrawNodeImages(object sender, CustomDrawNodeImagesEventArgs e)
        {
            string name = e.Node[0].ToString();
            if (name == "Suyama, Michael")
            {
                e.SelectImageIndex = 0; // e.SelectImageIndex为图片在ImageList中的index
            }
            else
                e.SelectImageIndex = 1;
            int level = e.Node.Level;
            if (level == 0)
                e.SelectImageIndex = 0;
            else if (level ==1)
                e.SelectImageIndex = 1;
            else if (level == 2)
                e.SelectImageIndex = 2;
            else if (level == 3)
                e.SelectImageIndex = 3;
            else if (level == 4)
                e.SelectImageIndex = 4;
            else if (level == 5)
                e.SelectImageIndex = 5;
            else if (level == 6)
                e.SelectImageIndex = 6;
            else if (level == 7)
                e.SelectImageIndex = 7;
        }

        //列表中单元格编辑
        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            TreeList currentTreeList = sender as TreeList;
            if (currentTreeList != null)
            {
                TreeListNode node = currentTreeList.FocusedNode;
                DevExpress.XtraTreeList.Columns.TreeListColumn column = currentTreeList.FocusedColumn;
                if (column.FieldName == "NodeName" && node.GetValue(0).ToString() != "")
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
                string a = treeList1.FocusedNode.GetValue(column.FieldName ).ToString();
               // MessageBox.Show(a);
            }
        }

        //背景色
        private void treeList1_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            return;
            if (e.Node.Selected)//如果该节点被选中，不改变颜色
            {
                e.Appearance.BackColor=Color.Gainsboro;
                e.Appearance.ForeColor = Color.Black;
                return;
            }
            string a1 = e.Node.Level.ToString();
            if (a1 == "0")
            {
                e.Appearance.BackColor = Color.FromArgb(100, 202, 154);
                //e.Appearance.ForeColor = Color.FromArgb(100, 202, 154);
            }
            else if (a1 == "1")
            {
                e.Appearance.BackColor = Color.FromArgb(131, 213, 174);
                //e.Appearance.ForeColor = Color.FromArgb(131, 213, 174);
            }
            else if(a1 == "2")
            {
                //e.Appearance.BackColor = Color.FromName("#B7E0CD");
                //e.Appearance.ForeColor = Color.FromName("#B7E0CD");207,234,221
                e.Appearance.BackColor = Color.FromArgb(183, 224, 205);
                //e.Appearance.ForeColor = Color.FromArgb(183, 224, 205);
            }
            else if (a1 == "3")
            {
                //e.Appearance.BackColor = Color.FromName("#B7E0CD");
                //e.Appearance.ForeColor = Color.FromName("#B7E0CD");
                e.Appearance.BackColor = Color.FromArgb(207, 234, 221);
                //e.Appearance.ForeColor = Color.FromArgb(207,234,221);
            }
        }

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            TreeList tree = sender as TreeList;
            if ( ModifierKeys == Keys.None
                && treeList1.State == TreeListState.Regular)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                TreeListHitInfo hitInfo = tree.CalcHitInfo(e.Location);
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    tree.SetFocusedNode(hitInfo.Node);
                }
                if (tree.FocusedNode != null)
                {
                    //popupMenu1.ShowPopup(p);
                }
            }
        }

        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeList1.ContextMenuStrip = null;
                TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
                TreeListNode node = hInfo.Node;
                treeList1.FocusedNode = node;
                if (node != null)
                {
                    treeList1.ContextMenuStrip = contextMenuStrip1;
                }
                string a = node.GetDisplayText("KeyFieldName");
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode clickedNode = this.treeList1.FocusedNode;
            string name = clickedNode.GetDisplayText("Customer");
            string formname = clickedNode.GetDisplayText("Phone");
            if (clickedNode != null)
                treeList1.AppendNode(new object[] { "Suyama, Michael", "Obere Str. 55", "030-0074263" }, clickedNode);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode clickedNode = this.treeList1.FocusedNode;
            string name = clickedNode.GetDisplayText("Customer");
            string formname = clickedNode.GetDisplayText("Phone");
             
            if (clickedNode != null)
                treeList1.DeleteNode(clickedNode);
        }

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode tree = this.treeList1.FocusedNode;
            if (tree == null)
                return;
            string name = tree.GetDisplayText("Customer");
            string formname = tree.GetDisplayText("Phone");
            TreeListNode node = treeList1.FocusedNode;
            DevExpress.XtraTreeList.Columns.TreeListColumn column = treeList1.FocusedColumn;
            if (column.FieldName == "NodeName" && node.GetValue(0).ToString() != "")
            {
                
            }
            TreeListNode praNode = tree.ParentNode;
        }

        private void treeList1_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {

        }

        private void treeList1_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            TreeList tree = sender as DevExpress.XtraTreeList.TreeList;
            IndicatorObjectInfoArgs args = e.ObjectArgs as IndicatorObjectInfoArgs;
            args.DisplayText = (tree.GetVisibleIndexByNode(e.Node) + 1).ToString();
        }

        private void treeList1_CustomDrawNodeButton(object sender, CustomDrawNodeButtonEventArgs e)
        {
           
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl t = sender as TabControl;
            string name = t.SelectedTab.Name;
        }

        //TreeList单元格内容改变
        private void treeList1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TreeList currentTreeList = sender as TreeList;
            if (currentTreeList != null)
            {
                TreeListNode node = currentTreeList.FocusedNode;
                DevExpress.XtraTreeList.Columns.TreeListColumn column = currentTreeList.FocusedColumn;
                string a = treeList1.FocusedNode.GetValue(column.FieldName).ToString();
                string name = node.GetDisplayText("NodeCode");
                node.SetValue(column.FieldName,"0");
            }
        }

        private void 我的ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem m = sender as ToolStripMenuItem;
            string name = m.Text;
        }

        private void treeList1_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl is DevExpress.XtraTreeList.TreeList)
            {
                TreeList tree = (TreeList)e.SelectedControl;
                TreeListHitInfo hit = tree.CalcHitInfo(e.ControlMousePosition);
                if (hit.HitInfoType == HitInfoType.Cell)
                {
                    object cellInfo = new TreeListCellToolTipInfo(hit.Node, hit.Column, null);
                    string toolTip = string.Format("{0} (Column: {1}, Node ID: {2})", hit.Node[hit.Column],
                    hit.Column.VisibleIndex, hit.Node.Id);
                    e.Info = new DevExpress.Utils.ToolTipControlInfo(cellInfo, toolTip);
                }
            }
        }
    }
}

using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeDaTableHtml
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            List<NameData> n = new List<NameData>();
            for (int i = 0; i < 50000; i++)
            {
                n.Add(new NameData
                {
                    id = i,
                    name = "张三",
                    sex = 1,
                    address = "东大街6号",
                    aihao = "看书",
                    phone = "11",
                    data = "1"
                });
            }
            gridControl1.DataSource = n;

            return;
            BindDataSource(InitDt());
        }
        private DataTable InitDt()
        {
            //DataTable dt = new DataTable("个人简历");
            //dt.Columns.Add("id", typeof(int));
            //dt.Columns.Add("name", typeof(string));
            //dt.Columns.Add("sex", typeof(int));
            //dt.Columns.Add("address", typeof(string));
            //dt.Columns.Add("aihao", typeof(string));
            //dt.Columns.Add("phone", typeof(string));
            //for(int i=0;i<100000;i++)
            //{ 
            //    dt.Rows.Add(new object[] { 1, "张三", 1, "东大街6号", "看书", "12345678910" });
            //    dt.Rows.Add(new object[] { 1, "王五", 0, "西大街2号", "上网,游戏", "" });
            //    dt.Rows.Add(new object[] { 1, "李四", 1, "南大街3号", "上网,逛街", "" });
            //    dt.Rows.Add(new object[] { 1, "钱八", 0, "北大街5号", "上网,逛街,看书,游戏", "" });
            //    dt.Rows.Add(new object[] { 1, "赵九", 1, "中大街1号", "看书,逛街,游戏", "" });
            //}
            //return dt;
            
            DataTable dt = new DataTable("个人简历");
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("sex", typeof(int));
            dt.Columns.Add("address", typeof(string));
            dt.Columns.Add("aihao", typeof(string));
            dt.Columns.Add("phone", typeof(string));
            dt.Columns.Add("data", typeof(decimal));
            dt.Rows.Add(new object[] { 1, "张三", 1, "东大街6号", "看书" ,"11",1});
            dt.Rows.Add(new object[] { 1, "王五", 0, "西大街2号", "上网,游戏", "22", 1 });
            dt.Rows.Add(new object[] { 1, "李四", 1, "南大街3号", "上网,逛街", "33", 1 });
            dt.Rows.Add(new object[] { 1, "钱八", 0, "北大街5号", "上网,逛街,看书,游戏", "44", 1 });
            dt.Rows.Add(new object[] { 1, "赵九", 1, "中大街1号", "看书,逛街,游戏", "55", 1 });
            return dt;

            
        }
        private void BindDataSource(DataTable dt)
        {
            //绑定DataTable  
            gridControl1.DataSource = dt;
            //gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
            RepositoryItemTextEdit rite = new RepositoryItemTextEdit();
            //keydown事件            
            rite.KeyDown+=new KeyEventHandler(rite_KeyDown);
            gridView1.Columns["name"].ColumnEdit = rite;
            gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_CellValueChanged);
            gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(gv_InitNewRow);
            
        }
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "sex")
            {
                switch (e.Value.ToString().Trim())
                {
                    case "1": e.DisplayText = "男";
                        break;
                    case "0": e.DisplayText = "女";
                        break;
                    default: e.DisplayText = "";
                        break;
                }
            }
        }
        
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int selectRow = gridView1.GetSelectedRows()[0];
            string id = this.gridView1.GetRowCellValue(selectRow, "name").ToString();
            gridView1.AddNewRow();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除所选数据？", "删除提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                int[] selectRows = gridView1.GetSelectedRows();
                //赋值
                //int tsNO = Convert.ToInt32(gridView1.GetRowCellValue(selectRows[0], "name"));
                gridView1.DeleteRow(gridView1.GetSelectedRows()[0]);
            }

        }

        /// <summary>        
        /// /// 单元格回车        
        /// /// </summary>        
        /// /// <param name="sender"></param>        
        /// /// <param name="e"></param>        
        void rite_KeyDown(object sender, KeyEventArgs e)
        {
            //用户点击 回车            
            if (e.KeyCode == Keys.Enter)
            {
                int selectRow = gridView1.GetSelectedRows()[0];
                DevExpress.XtraEditors.TextEdit te = sender as DevExpress.XtraEditors.TextEdit;
                //值为空时返回                
                if (te == null || string.IsNullOrEmpty(te.Text.Trim()))
                {
                    return;
                }
                
            }
        }
        private void SetCellEdit(int row)
        {
            Application.DoEvents();
            try
            {
                gridView1.BeginInit();
                gridControl1.Focus();
                gridView1.Focus();
                gridView1.SelectCell(row, gridView1.Columns["name"]);
                gridView1.FocusedColumn = gridView1.Columns["name"];
                gridView1.ShowEditor();
            }
            finally
            {
                gridView1.EndInit();
            }
        }

        /// 单元格 焦点离开 触发该事件 
        void gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "name")
            {
                if (e.Value == null)
                    return;
                int selectRow = gridView1.GetSelectedRows()[0];

                int i = e.RowHandle; //行数，千万别用for循环来遍历gridview的行数
                object sl = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "address"); //获取指定列SL数据
                //object dj = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DJ"); //获取指定列DJ数据
                if (sl != System.DBNull.Value)    //判读object类型是否为空
                {
                    this.gridView1.GetDataRow(i)["address"] = e.Value.ToString();
                }

            }
        }

        void gv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            SetCellEdit(e.RowHandle);
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
             
        }

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            

        }

        
        //节点双击事件
        private void DClick(object o, EventArgs e)
        {
            
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            int rows = this.gridView1.FocusedRowHandle;
            if (rows == 0)
                return;
            NameData nd = new NameData();
            nd.id = Convert.ToInt32(this.gridView1.GetRowCellValue(rows, "id").ToString());
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "name")
            {
                double a = 1;
                try
                {
                    a = a + 1;
                    a = Convert.ToDouble("2q");
                }
                catch
                {

                }
                
                try
                {
                    this.gridView1.SetRowCellValue(e.RowHandle, gridView1.Columns["sex"], "0");
                }
                catch
                {
                    this.gridView1.SetRowCellValue(e.RowHandle, gridView1.Columns["sex"], "0");
                }
            }
        }
    }
    public class NameData
    {
        //dt.Columns.Add("id", typeof(int));
        //    dt.Columns.Add("name", typeof(string));
        //    dt.Columns.Add("sex", typeof(int));
        //    dt.Columns.Add("address", typeof(string));
        //    dt.Columns.Add("aihao", typeof(string));
        //    dt.Columns.Add("phone", typeof(string));
        //    dt.Columns.Add("data", typeof(decimal));
        public  int id { get; set; }
        public string name { get; set; }
        public int sex { get; set; }
        public string address { get; set; }
        public string aihao { get; set; }
        public string phone { get; set; }
        public string data { get; set; }
    }
}

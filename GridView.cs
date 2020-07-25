using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace ResourceCreator
{
    // Need to subclass this 
    public partial class GridView : PictureBox, IDataChangeObserver
    {
        protected IDataChangeObserver m_DataChangeObserver;
        protected ArrayList m_List;
        protected int m_iRowHeight;
        protected Point mptLastPos;
        protected GridViewColumn m_hitColHeader;
        public int RowHeight { get { return m_iRowHeight; } }
        protected ComboBox m_CmbItem;
        
        public GridView()
        {
            InitializeComponent();
            m_List = new ArrayList();
            m_iRowHeight = 25;
            m_CmbItem = new ComboBox();
            m_CmbItem.Visible = false;
        }
        //
        public void SetDataChangeObserver(IDataChangeObserver observer)
        {
            m_DataChangeObserver = observer;
        }
        //
        private GridViewColumn GetColumn(int index)
        {
            if (index >= 0 && index < m_List.Count)
            {
                return (GridViewColumn)m_List[index];
            }

            return null;
        }
        //
        public void AddColumn(String text, int width)
        {
            GridViewColumn col = new GridViewColumn();
            col.Width = width;
            col.Height = this.Height;

            GridViewHeaderItem header = new GridViewHeaderItem(m_List.Count);
            header.Width = width;
            header.X = GetColX();
            header.Control.Text = text;
            header.Height = m_iRowHeight;
            col.SetHeaderItem(header);
            this.Controls.Add(header.GetControl());

            col.Y = 0;
            col.X = GetColX();
            col.Index = m_List.Count;
            m_List.Add(col);
        }
        //
        private int GetColX()
        {
            int x = -5;

            foreach (GridViewColumn col in m_List)
            {
                x += col.Width;
            }

            return x;
        }
        //
        public void RemoveAll()
        {
            foreach (GridViewColumn obj in m_List)
            {
                obj.List.Clear();
            }

            this.Controls.Clear();
            
            // Add Header items
            foreach (GridViewColumn obj in m_List)
            {
                this.Controls.Add(obj.Header.GetControl());
            }
            //
            this.Controls.Add(m_CmbItem);
            m_CmbItem.SelectedIndexChanged += new EventHandler(this.OnSelectedIndexChanged);
            //
            this.Invalidate();
        }
        //
        public GridViewItem AddItem(int row, int col, String text, ITEM_TYPE type)
        {
            GridViewColumn colObj = GetColumn(col);

            if (colObj != null)
            {
                GridViewItem item = colObj.AddItem(text, type);
                item.SetParent(this);
                this.Controls.Add(item.GetControl());
                return item;
            }

            return null;
        }
        //
        public GridViewItem AddItem(int row, int col, String text)
        {
            return AddItem(row, col, text, ITEM_TYPE.Label);
        }
        //
        public void AddItem(int row, int col, GridViewItem item)
        {

        }
        //
        public GridViewItem GetItem(int row, int col)
        {
            GridViewColumn colObj = GetColumn(col);

            if (colObj != null)
            {
                return colObj.GetItem(row);
            }

            return null;
        }
        //
        public GridViewColumn HitTest(Point point)
        {
            foreach (GridViewColumn col in m_List)
            {
                if (col.HitTest(point))
                    return col;
            }

            return null;

        }
        //
        public void SetItemText(int row, int col, String text)
        {
            GridViewItem item = GetItem(row, col);

            if (item != null)
            {
                item.Control.Text = text;
            }
        }
        //
        public String GetItemText(int row, int col)
        {
            GridViewItem item = GetItem(row, col);

            if (item != null)
            {
                return item.Control.Text;
            }

            return "";
        }
        //
        public virtual void OnClickItem(GridViewItem clickItem)
        {
            if (clickItem.VisibleOnClick)
            {
                if (clickItem is GridViewComboItem)
                {
                    ShowCombo((GridViewComboItem)clickItem);
                }
                else
                {
                    m_CmbItem.Visible = false;
                }
            }
        }
        //
        private void ShowCombo(GridViewComboItem item)
        {
            m_CmbItem.Tag = item;
            m_CmbItem.Items.Clear();
            m_CmbItem.Visible = true;
            m_CmbItem.Location = item.Control.Location;
            m_CmbItem.Size = new Size(item.Control.Width, 0);
            //item.SetControl(m_CmbItem);
            if (item.m_Data != null)
            {
                foreach (String str in item.m_Data)
                {
                    m_CmbItem.Items.Add(str);
                }
            }

            m_CmbItem.Text = item.Control.Text;
            m_CmbItem.Focus();
            m_CmbItem.Show();
        }
        //
        protected override void OnMouseDown(MouseEventArgs e)
        {
            mptLastPos = e.Location;
            m_hitColHeader = HitTest(e.Location);

            if (m_hitColHeader != null)
            {
                m_hitColHeader.Drag = true;
            }
        }
        //
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int x = e.X - mptLastPos.X;
            int y = e.Y - mptLastPos.Y;

            if (m_hitColHeader != null)
            {
                m_hitColHeader.X = mptLastPos.X;
                MoveAllCol(m_hitColHeader.Index + 1, x);
                ResizeCol(m_hitColHeader.Index - 1, m_hitColHeader.X);
                this.Refresh();
            }

            mptLastPos = e.Location;
        }
        //
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (m_hitColHeader != null)
            {
                m_hitColHeader.Drag = false;
                m_hitColHeader = null;
            }
            m_CmbItem.Visible = false;
        }
        //
        private void ResizeCol(int index, int x)
        {
            GridViewColumn colObj = GetColumn(index);
            if (colObj != null)
            {
                colObj.Width = x - colObj.X;
            }
        }
        //
        private void MoveAllCol(int startIndex, int tx)
        {
            if (startIndex == m_List.Count)
            {
                GridViewColumn colObj = GetColumn(startIndex-1);
                if (tx > 0)
                {
                    colObj.X += tx;
                    colObj.Width -= tx;
                }
                else if (colObj != null)
                {                   
                    colObj.X += tx;
                    colObj.Width -= tx;
                }


                return;
            }
            for (int i = startIndex; i < m_List.Count; i++)
            {
                GridViewColumn colObj = GetColumn(i);
                if (colObj != null)
                {
                    colObj.X += tx;
                }
            }
        }
        //
        public void Draw(Graphics dc)
        {
            foreach (GridViewColumn obj in m_List)
            {
                obj.Draw(dc);
            }

            if (m_CmbItem.Visible)
            {
                GridViewComboItem cmb = (GridViewComboItem)m_CmbItem.Tag;
                m_CmbItem.Location = cmb.Control.Location;
                m_CmbItem.Size = new Size(cmb.Control.Width, 0);
            }
            // DrawLine(dc);
        }
        //
        private void DrawLine(Graphics dc)
        {
            float[] dashValues = { 5, 2, 10, 4 };
            Pen blackPen = new Pen(Color.Black, 1);
            int x = 0;
            int y = 0;

            for (int i = 0; i < 100; i++)
            {
                //blackPen.DashPattern = dashValues;
                dc.DrawRectangle(blackPen, x, y, m_List.Count * 200, m_iRowHeight);
                y += m_iRowHeight;
            }
        }
        //
        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e.Graphics);
        }
        //
        void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewComboItem cmb = (GridViewComboItem)m_CmbItem.Tag;
            
            if (cmb != null)
            {
                if (cmb.Control.Text != m_CmbItem.Text)
                {
                    cmb.Control.Text = m_CmbItem.Text;
                    OnModify(cmb, e);
                }
            }
            
            //NotifyObserver(e);
        }

        #region IDataChangeObserver Members

        public void OnModify(object sender, EventArgs e)
        {
            GridViewItem item = (GridViewItem)sender;
            GridViewColumn col = GetColumn(item.Column.ColumnIndex);
           
            if (col != null)
            {
                col.UpdateData(item);
            }
            
            if (m_DataChangeObserver != null)
            {
                m_DataChangeObserver.OnModify(item.m_ItemData, e);
            }
        }

        #endregion

        private void GridView_MouseLeave(object sender, EventArgs e)
        {
            //m_CmbItem.Visible = false;
        }
    }
}

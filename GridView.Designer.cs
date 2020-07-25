using System;
using System.Drawing;
using System.Collections;
namespace ResourceCreator
{
    public class GridViewColumn : GridViewItem
    {
        protected ArrayList m_List;
        //protected int m_iPadding;
        protected GridViewHeaderItem m_Header;
        protected int m_iRowHeight;
        protected int m_iColHeaderHeight;
        // To Track Diff Colmn
        protected int m_iDepth;
        protected int m_iIndex;

        protected bool m_bDrag;
        public bool Drag { get { return m_bDrag; } set { m_bDrag = value; } }

        public ArrayList List { get { return m_List; } }
        public int Index { get { return m_iIndex; } set { m_iIndex = value; } }
        public GridViewHeaderItem Header { get { return m_Header; } }
        //public GridView m_Parent;
        Rectangle m_rcSeperatorBoundary;

        public GridViewColumn()
        {
            m_List = new ArrayList();
            m_iPadding = 2;
            m_iWidth = 100;
            m_iDepth = 5;
            m_rcSeperatorBoundary = new Rectangle();
            m_rcSeperatorBoundary.Width = m_iDepth;
            m_iRowHeight = 25;
            m_iColHeaderHeight = 20;
            m_iIndex = 0;
        }
        //
        public GridViewItem GetItem(int row)
        {
            if (row >= 0 && row < m_List.Count)
            {
                return (GridViewItem)m_List[row];
            }

            return null;
        }
        //
        public void AddItem(GridViewItem item, int row)
        {
            // Last Item
            if (row >= m_List.Count)
            {
                m_List.Add(item);
            }
            else
            {
                m_List[row] = item;
            }
        }
        //
        public void SetHeaderItem(GridViewHeaderItem item)
        {
            m_Header = item;
        }
        //
        public void AddItem(GridViewItem item)
        {
            m_List.Add(item);
        }
        //
        private GridViewItem CreateItem(ITEM_TYPE type)
        {
            switch (type)
            {
                case ITEM_TYPE.ComboBox:
                    return new GridViewComboItem();
                case ITEM_TYPE.TextBox:
                    return new GridViewTextItem();
                default:
                    return new GridViewLabelItem();
            }
        }
        //
        public GridViewItem AddItem(String text, ITEM_TYPE type)
        {
            GridViewItem item = CreateItem(type);
            item.Control.Text = text;
            // m_iRowHeight for Col header
            item.Y = m_iColHeaderHeight + m_iRowHeight * m_List.Count + m_iPadding;
            item.X = m_Header.X;
            item.SetColumn(m_Header);
            m_List.Add(item);
            return item;
        }
        //
        public override void Draw(Graphics dc)
        {
            float[] dashValues = { 5, 2, 10, 4 };
            Pen blackPen = new Pen(Color.Black, 1);
            
            //
            int position = 0;
            foreach (GridViewItem obj in m_List)
            {
                dc.DrawRectangle(blackPen, X, obj.Y, m_iWidth, m_iRowHeight);
                obj.X = X;
                obj.Width = m_iWidth;
                obj.Height = m_iRowHeight;
                //
                GetView(position, obj);
                obj.Draw(dc);
                position++;
            }

            m_Header.X = X;
            m_Header.Width = m_iWidth;

            m_Header.Draw(dc);


            m_rcSeperatorBoundary.X = m_iX - m_iDepth / 2;
            m_rcSeperatorBoundary.Y = m_iY;
            m_rcSeperatorBoundary.Width = m_iDepth;
            m_rcSeperatorBoundary.Height = m_iHeight;
            blackPen = new Pen(Color.Black, 1);
            dc.DrawRectangle(blackPen, m_rcSeperatorBoundary.X+2, m_rcSeperatorBoundary.Y, m_iDepth/2, m_iRowHeight-3);
        }
        //
        public override bool HitTest(Point point)
        {
            return m_rcSeperatorBoundary.Contains(point);
        }
        // Rethink like Android....
        public virtual void GetView(int position, GridViewItem convertView)
        {
            PropertyObject obj = (PropertyObject)convertView.m_ItemData;
            if (obj != null)
            {
                convertView.Control.Text = obj.Text;
            }
        }
        //
        public virtual void UpdateData(GridViewItem view)
        {
            PropertyObject obj = (PropertyObject)view.m_ItemData;
            if (obj != null)
            {
                //obj.Text = view.Control.Text;
                obj.TextNew = view.Control.Text;
            }
        }
    }

    //
    partial class GridView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.Size = new System.Drawing.Size(411, 275);
            this.MouseLeave += new System.EventHandler(this.GridView_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}

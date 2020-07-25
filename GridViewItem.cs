//----------------------------------------------------------------------
//
// Author	: Mohammad Saiful Alam
//
// Remarks
//
// Represents the converter manager class that converts XML format
//
// Email: saiful_vonair@yahoo.com, saiful.alam@bjitgroup.com
//
// Author			Date		Modification
//----------------------------------------------------------------------
//
//----------------------------------------------------------------------

using System;
using System.Windows.Forms;
using System.Drawing;

namespace ResourceCreator
{
    public interface IDataChangeObserver
    {
        void OnModify(object sender, EventArgs e); 
    }

    //
    public enum ITEM_TYPE 
    {
        Button = 0,
        ComboBox,
        Label,//Default
        ListBox,
        TextBox      
    };
    //
    public class GridViewItem
    {
        protected String m_Name;
        protected int m_iRow;
        protected int m_iCol;
        protected int m_iX;
        protected int m_iY;
        protected int m_iWidth;
        protected int m_iHeight;
        protected int m_iPadding;
        protected GridView m_Parent;
        protected ITEM_TYPE m_eType;
        // How the control will be shown
        protected bool m_bVisibleOnClick;
        public Object m_ItemData;
        protected GridViewHeaderItem m_Column;

        public int X { get { return m_iX; } set { m_iX = value; } }
        public int Y { get { return m_iY; } set { m_iY = value; } }
        public int Width { get { return m_iWidth; } set { m_iWidth = value; } }
        public int Height { get { return m_iHeight; } set { m_iHeight = value; } }
        public GridViewHeaderItem Column { get { return m_Column; } }
        public Control Control { get { return m_Window; } set { m_Window = value; } }
        public Object Tag { get { return m_ItemData; } set { m_ItemData = value; } }
        public bool VisibleOnClick { get { return m_bVisibleOnClick; } set { m_bVisibleOnClick = value; CreateControl(); } }

        public ITEM_TYPE Type { get { return m_eType; } set { m_eType = value; } }
        protected Control m_Window;

        public String[] m_Data;

        //public String m_DisplayText;

        public GridViewItem()
        {
            m_iX = 0;
            m_iY = 0;
            m_eType = ITEM_TYPE.Label;
            m_iPadding = 5;
            m_bVisibleOnClick = true;
            m_Data = null;
        }

        public void SetColumn(GridViewHeaderItem col)
        {
            m_Column = col;
        }

        public virtual bool HitTest(Point point)
        {
            Rectangle rc = new Rectangle(m_iX, m_iY, m_iWidth, m_iHeight);
            return rc.Contains(point);
        }

        public virtual void Draw(Graphics dc)
        {
            if(m_Window != null)
            {
                m_Window.Location = new Point(m_iX + m_iPadding, m_iY + 3);
                m_Window.Size = new Size(Width - m_iPadding - 3, m_iHeight - m_iPadding);
                m_Window.Show();
            }
        }

        public Control GetControl() { return m_Window; }
        public virtual void SetParent(GridView parent) { m_Parent = parent; }
        protected void NotifyObserver(EventArgs e)
        {
           m_Parent.OnModify(this, e);
        }

        protected virtual void CreateControl() { }
        //
        protected void On_Click(object sender, EventArgs e)
        {
            m_Parent.OnClickItem(this);
        }
    }
    //
    public class GridViewLabelItem : GridViewItem
    {
        public GridViewLabelItem()
        {
            m_Window = new Label();
            m_Window.Click += new System.EventHandler(this.On_Click);
            //m_Window.BackColor = Color.Blue;
            //m_Window.ForeColor = Color.White;
        }
    }
    //
    public class GridViewTextItem : GridViewItem
    {
        private TextBox m_MainObj;
        public GridViewTextItem()
        {
            m_MainObj = new TextBox();
            m_Window = m_MainObj;
            m_MainObj.BorderStyle = BorderStyle.None;
            m_MainObj.KeyUp += new System.Windows.Forms.KeyEventHandler(this.On_KeyUp);
            m_MainObj.Click += new System.EventHandler(this.On_Click);
            //m_MainObj.Leave += new System.EventHandler(this.On_Leave);
        }
       
        //
        private void On_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NotifyObserver(e);
            }
        }
        //
        //
        private void On_Leave(object sender, EventArgs e)
        {
           NotifyObserver(e);
        }
    }
    //
    public class GridViewComboItem : GridViewLabelItem
    {
        private ComboBox m_MainObj;
        public GridViewComboItem()
        {
        }

        public void SetControl(ComboBox control)
        {
            m_MainObj = control;
        }

        protected override void CreateControl()
        {
            base.CreateControl();
            // Always visible
            if (!m_bVisibleOnClick)
            {
                m_Parent.Controls.Remove(m_Window);
                m_MainObj = new ComboBox();
                m_Parent.Controls.Add(m_MainObj);
                m_Window = m_MainObj;
                m_MainObj.SelectedIndexChanged += new EventHandler(this.OnSelectedIndexChanged);
            }
        }

        void OnSelectedIndexChanged(object sender, EventArgs e)
        {
           NotifyObserver(e);
        }

    }
    //
    public class GridViewHeaderItem : GridViewItem
    {
        public int ColumnIndex { get {return m_iCol; } }
        public GridViewHeaderItem(int col)
        {
            m_iCol = col;
            m_iRow = -1;
            m_Window = new Label();
            m_Window.BackColor = Color.Silver;
            m_Window.ForeColor = Color.White;
        }
        //
        public override void Draw(Graphics dc)
        {
            if (m_Window != null)
            {
                m_Window.Location = new Point(m_iX + m_iPadding, m_iY + 1);
                m_Window.Size = new Size(Width - m_iPadding-1, m_iHeight - m_iPadding);
                m_Window.Show();
            }
        }
    }
}

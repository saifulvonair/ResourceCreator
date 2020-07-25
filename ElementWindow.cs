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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceHeader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Windows.Forms;

namespace ResourceCreator
{
    class ElementWindow : Composite
    {
        public enum ControlButton
        {

            Minimize = 0,
            Maximize,
            Close
        }
        //
        public class DrawElementControlButton : DrawElement
        {
            ControlButton mButtonType;

            public ControlButton ButtonType { get { return mButtonType; } set { mButtonType = value; } }

            public DrawElementControlButton()
            {
                mszSize = new Size(12,12);
            }

            public override void draw(Graphics dc)
            {
                base.draw(dc);
                float[] dashValues = { 5, 2, 10, 4 };
                Pen blackPen = new Pen(Color.Red, 1);
                //blackPen.DashPattern = dashValues;
                dc.DrawRectangle(blackPen, this.CurrentPositionX + 1, this.CurrentPositionY + 2, mszSize.Width, mszSize.Height);
                if (mButtonType == ControlButton.Minimize)
                {
                    dc.DrawRectangle(blackPen, this.CurrentPositionX+4, this.CurrentPositionY + Height - 1, this.Width -6, 1);
                }
                else if (mButtonType == ControlButton.Maximize)
                {
                    dc.DrawRectangle(blackPen, this.CurrentPositionX + 3, this.CurrentPositionY + 4, mszSize.Width-5, mszSize.Height-4);
                }
                else if (mButtonType == ControlButton.Close)
                {
                    dc.DrawLine(blackPen, this.CurrentPositionX + 3, this.CurrentPositionY + 4, this.CurrentPositionX + this.Width-2, this.CurrentPositionY + this.Height-2);
                }
            }
        }

        private int mTitleBarHeight;
        private int mTitleBarIconSize;
        private ArrayList m_listControls;
        private ElementTracker m_Tracker;
        protected Control m_Control;// = Control.FromHandle(m_Handle);

        public ElementTracker Tracker { get { return m_Tracker; } set { m_Tracker = value; } }
        public Control Container { get { return m_Control; } set { m_Control = value; } }
        public ElementWindow()
        {
            mszSize.Width = 400;
            mszSize.Height = 300;

            mptCurrentPosition.X = 0;
            mptCurrentPosition.Y = -1;
            mTitleBarHeight = 23;
            mTitleBarIconSize = 16;
            
            mbMoveable = false;

            mClrText = Color.White;

            mFontStyle = FontStyle.Bold;
            m_listControls = new ArrayList();
            //CreateControlButton();
            m_sName = "Dialog";
        }

        public void CreateControlButton()
        {
            int size = 0;
            DrawElementControlButton btn = new DrawElementControlButton();
            btn.ButtonType = ControlButton.Minimize;
            size = btn.Width;
            m_listControls.Add(btn);
            ////
            btn = new DrawElementControlButton();
            btn.ButtonType = ControlButton.Maximize;
            m_listControls.Add(btn);
            //
            btn = new DrawElementControlButton();
            btn.ButtonType = ControlButton.Close;
            m_listControls.Add(btn);
        }

        void UpdateControlButton()
        {
 
        }

        public DrawElement GetHitItem(Point point)
        {
            DrawElement hitItem = null;
            int count = mList.Count - 1;

            for (int i = count; i >= 0; i--)
            {
                hitItem = (DrawElement)mList[i];
                if (hitItem.hitTest(point))
                {
                    return hitItem;
                }
            }

            return null;
        }

        public override void draw(System.Drawing.Graphics dc)
        {
            int centerX = CurrentPositionX;
            int centerY = CurrentPositionY;
            SizeF fontSize;
            Brush brushTxt;
            Rectangle rect;
            Font font;
            Brush brushBack;
            rect = new Rectangle(this.CurrentPositionX-1,
                                      this.CurrentPositionY,
                                      this.Width-4,
                                      mTitleBarHeight);

            brushBack = new LinearGradientBrush(rect, Color.Blue, Color.White, LinearGradientMode.Horizontal);
            dc.FillRectangle(brushBack, rect);
            font = new Font(mFontFamily, mfFontSize, mFontStyle);
            fontSize = dc.MeasureString(Text, font);
            centerY = mTitleBarHeight / 2 - (int)fontSize.Height / 2;
            brushTxt = new SolidBrush(mClrText);
            dc.DrawString(mStrText,
                                 font,
                                 brushTxt,
                                 centerX + mTitleBarIconSize + 2,
                                 centerY + CurrentPositionY);
            font.Dispose();

            float[] dashValues = { 5, 2, 10, 4 };
            Pen blackPen = new Pen(Color.Black, 1);
            dc.DrawRectangle(blackPen, this.CurrentPositionX-1, this.CurrentPositionY, this.Width, this.Height);


            ElementPropertyWindow prop = (ElementPropertyWindow)m_Property;
            if (prop.GetValue("SystemMenu") == "true")
            {
                DrawControlButton(dc);
            }
            DrawChildControls(dc);
        }

        private void DrawControlButton(Graphics dc)
        {
            //int size = 0;
            Point imageLoc = Location;
            //Image image = getImage(0);

            //if (null != image)
            //{
            //    imageLoc.X = imageLoc.X + this.Width - image.Width;
            //    dc.DrawImage(image, imageLoc.X-3, imageLoc.Y-1);
            //}

            foreach (DrawElementControlButton aDrawable in m_listControls)
            {
               int size = aDrawable.Width + 5;

                if (aDrawable.ButtonType == ControlButton.Minimize)
                {
                    aDrawable.Location = new Point(this.Location.X + this.Width - size * 3 - 2, mptCurrentPosition.Y);

                }
                else if (aDrawable.ButtonType == ControlButton.Maximize)
                {
                    aDrawable.Location = new Point(this.Location.X + this.Width - size * 2 - 2, mptCurrentPosition.Y);

                }
                else if (aDrawable.ButtonType == ControlButton.Close)
                {
                    aDrawable.Location = new Point(this.Location.X + this.Width - size - 2, mptCurrentPosition.Y);

                }
                aDrawable.draw(dc);
            }
 
        }

        private void DrawChildControls(Graphics dc)
        {
            foreach (DrawElement aDrawable in mList)
            {
                if (aDrawable.Show)
                {
                    aDrawable.draw(dc);
                }
            }
        }

        public override void OnSizeChanged()
        {
            base.OnSizeChanged();
            if (m_Control != null)
            {
                m_Control.Width = Width + 8;
                m_Control.Height = Height + 8;
            }
        }

        public override bool onMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            //notifyPropertyChanged();
            return true;
        }

        public override bool onMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            //notifyPropertyChanged();
            return base.onMouseMove(e);
        }
    }
}

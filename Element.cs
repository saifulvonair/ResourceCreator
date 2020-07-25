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
using System.Drawing;
using System.IO;
using ResourceCreator;

namespace ResourceHeader
{
    public interface IDrawElementList
    {
        void Add(DrawElement obj);
        void Remove(DrawElement obj);
    }
    //
    public enum TextAlign
    {
        Left,
        Right,
        Center,
        AboveImage
    }
    //
    public class DrawElement : IDrawable
    {
        protected TextAlign m_eTextAlign;
        protected bool m_bDrawBorder;
        protected FactoryShape m_Factory;
        protected String m_sName;
        // Donot Modified it
        protected ElementProperty m_Property;

        public TextAlign TextAlign { get { return m_eTextAlign; } set { m_eTextAlign = value; } }
        public FactoryShape Factory { get { return m_Factory; } set { m_Factory = value; } }
        public ElementProperty Property { get { return m_Property; } set { m_Property = value; } }
        public String Name { get { return m_sName; } set { m_sName = value; } }

        public DrawElement()
        {
            m_eTextAlign = TextAlign.Left;
            m_bDrawBorder = true;
            m_Factory = null;
            m_Property = null;
	    }
        //
        protected void Get2dProperty(String val, ref int i, ref int j)
        {
            val = val.Trim();
            String[] posArr = val.Split(',');
            String str1 = posArr[0].Trim();
            String str2 = posArr[1].Trim();
            i = 0;
            j = 0;
            try
            {
                i = int.Parse(str1);
                j = int.Parse(str2);
            }
            catch (Exception e) { }

        }
        //
        public override void draw(System.Drawing.Graphics dc)
        {
            int centerX = 0;
            int centerY = 0;
            SizeF fontSize;
            Brush brushTxt;
            Rectangle rect;
            Font font;
            Point imageLoc = Location;
            String sText = mStrText;

            if (mbDrag)
                //return;
                rect = new Rectangle(this.CurrentPositionX,
                                      this.CurrentPositionY,
                                      this.ItemSize.Width,
                                      this.ItemSize.Height);

            font = new Font(mFontFamily, mfFontSize, mFontStyle);
            fontSize = dc.MeasureString(Text, font);

            // Need to verify this code
            if (fontSize.Width > Width)
            {
                int len = mStrText.Length;
                int sub = (int)(len * Width / fontSize.Width);
                sText = mStrText.Substring(0, sub );
                fontSize.Width = Width-1;
            }

            centerX = ItemSize.Width / 2 - (int)fontSize.Width / 2;

            centerY = ItemSize.Height / 2 - (int)fontSize.Height / 2;
            centerX += CurrentPositionX;
            centerY += CurrentPositionY;

            brushTxt = new SolidBrush(mClrText);
            float[] dashValues = { 5, 2, 10, 4 };
            Pen blackPen = new Pen(Color.Black, 1);
            //blackPen.DashPattern = dashValues;
            dc.FillRectangle(Brushes.Silver, this.CurrentPositionX, this.CurrentPositionY, this.Width, this.Height);
            dc.DrawRectangle(blackPen, this.CurrentPositionX, this.CurrentPositionY, this.Width, this.Height);

            dc.DrawString(  sText,
                             font,
                             brushTxt,
                             centerX,
                             centerY);
            font.Dispose();

          

        }

        public void UpdateState(String propName)
        {
            int x = 0, y = 0, w = 0, h = 0;

            if (propName == "Location")
            {
                Get2dProperty(Property.GetValue("Location"), ref x, ref y);
                CurrentPositionX = x;
                CurrentPositionY = y;
            }
            else if (propName == "Size")
            {
                Get2dProperty(Property.GetValue("Size"), ref w, ref h);
                Width = w;
                Height = h;
                OnSizeChanged();
            }
            else if(propName == "Text")
            {
                mStrText = Property.GetValue(propName);
            }
            //
            if (this is ElementWindow)
            {
                OnSizeChanged();
            }
        }
        // Serialize method to load data from disk
        public void load(StreamReader stream)
        {
            //mWidth = stream.ReadLine();
            //mHeight = stream.ReadLine();
        }

        // Serialize method to save into disk
        public void save(StreamWriter stream)
        {
            //stream.WriteLine(mWidth);
            //stream.WriteLine(mHeight);
        }
      
    }
}
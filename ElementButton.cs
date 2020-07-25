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

using ResourceHeader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ResourceCreator
{
    class ElementButton : DrawElement
    {
        public ElementButton()
        {
            m_eTextAlign = TextAlign.Center;
            m_bDrawBorder = false;
            m_sName = "Button";
        }

        public override void draw(System.Drawing.Graphics dc)
        {
            Image image = getImage(0);

            int centerX = 0;
            int centerY = 0;
            SizeF fontSize;
            Brush brushTxt;
            Rectangle rect;
            Font font;
            int imageSize = 0;
            Point imageLoc = Location;
            int paddingImage = 3;

            if (image != null)
            {
                imageSize = image.Width + 5;
            }
            Pen blackPen = new Pen(Color.Black, 1);
            //if (mbDrag)
            //    return;
           rect = new Rectangle(this.CurrentPositionX,
                                      this.CurrentPositionY,
                                      this.ItemSize.Width,
                                      this.ItemSize.Height);

            font = new Font(mFontFamily, mfFontSize, mFontStyle);
            fontSize = dc.MeasureString(Text, font);
            centerX = ItemSize.Width / 2 - (int)fontSize.Width / 2;
            
            centerY = ItemSize.Height / 2 - (int)fontSize.Height / 2;
            centerX += CurrentPositionX;
            centerY += CurrentPositionY;

            if (m_eTextAlign == TextAlign.Left)
            {
                imageLoc = Location;
                imageLoc.Y = centerY;
            }
            else
            {
                //imageLoc = Location;
                paddingImage = 0;
            }


            brushTxt = new SolidBrush(mClrText);


            if (null != image)
            {
                dc.DrawImage(image, imageLoc.X + paddingImage, imageLoc.Y - paddingImage);
            }
            else
            {
                float[] dashValues = { 5, 2, 10, 4 };

                Brush brushBack;
                brushBack = new LinearGradientBrush(rect, Color.SkyBlue, Color.Silver, LinearGradientMode.Vertical);
                dc.FillRectangle(brushBack, rect);
                //blackPen.DashPattern = dashValues;
                //dc.FillRectangle(Brushes.Silver, this.CurrentPositionX, this.CurrentPositionY, this.Width, this.Height);
                //dc.DrawRectangle(blackPen, this.CurrentPositionX, this.CurrentPositionY, this.Width, this.Height);
                DrawRoundRect(dc, blackPen, rect.X, rect.Y, rect.Width, rect.Height, 3);

            }
           
            
            dc.DrawString(mStrText,
                                 font,
                                 brushTxt,
                                 centerX,
                                 centerY);
            font.Dispose();

            //blackPen.DashPattern = dashValues;
            //dc.DrawRectangle(blackPen, this.CurrentPositionX, this.CurrentPositionY, this.Width, this.Height - 1);
        }
    }
}

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
using System.Collections;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using ResourceCreator;

namespace ResourceHeader
{
    public enum DrawState
    {
        Normal = 2,
        Selected = 4,
        Clicked = 8,
        FocusOnly = 16       
    };

    public interface IPropertyChangeObserver
    {
        void onPropertyChanged(IDrawable obj);
    }

    public class IDrawable 
    {
        protected IntPtr m_Handle;

        protected Point mptCurrentPosition;
        protected int miStartImageIndex;
        protected int miEndImageIndex;
        protected int miCurrentImageIndex;
        protected Size mszSize;        
        protected DrawState meState;
        protected Color mClrBorder;
        protected Color mClrBack;
        protected Color mClrText;
        protected Color mClrTextSelected;
        protected Color mClrTextNormal;
        protected String mStrText;
        protected bool mbShow;
        protected bool mbMoveable;
        protected bool mbDrag;
        protected bool mbResizable;
        protected Image mImgBack;
        protected int miCurrentPtIndex;
        protected float mfFontSize;
        protected Rectangle mrcArea;
        protected FontFamily mFontFamily;
        protected FontStyle mFontStyle;
        protected ArrayList mListImage;
        public IPropertyChangeObserver mPropertyChangeObserver;

      
        public Image ImgBack { get { return mImgBack; } set { mImgBack = value; } }
        public FontFamily FontFamily { get { return mFontFamily; } set { mFontFamily = value; } }
        public FontStyle FontStyle { get { return mFontStyle; } set { mFontStyle = value; } }

        public ArrayList ListImage { get { return mListImage; } set { mListImage = value; } }
        public IPropertyChangeObserver PropertyChangeObserver { set { mPropertyChangeObserver = value; } get { return mPropertyChangeObserver; } }

        public IntPtr Handle { get { return m_Handle; } set { m_Handle = value; } }

        public IDrawable()
        {
            m_Handle = IntPtr.Zero;
            mptCurrentPosition = new Point(0, 0);
            miStartImageIndex = 0;
            miEndImageIndex = 0;
            miCurrentImageIndex = 0;
            mszSize = new Size(76, 27);
            mfFontSize = 8;
            mClrBack = Color.SkyBlue;
            mClrText = Color.FromArgb(128, 105, 104);
            Show = true;
            miCurrentPtIndex = 0;
            mrcArea = new Rectangle(0, 0, 0, 0);
            mFontFamily = new FontFamily("Times New Roman");
            //mFontFamily = new FontFamily("meiryo");
            mFontStyle = FontStyle.Regular;
            mListImage = new ArrayList();
            mClrTextSelected = Color.FromArgb(139, 126, 111);
            mClrTextNormal = Color.FromArgb(9, 4, 5);
            mbMoveable = true;
            mbResizable = true;
        }
        public int CurrentPtIndex { get { return miCurrentPtIndex; } }
        public float FontSize { set { mfFontSize = value; } get { return mfFontSize; } }
        public bool Show 
        {
            get { return mbShow; }
            //set { mbShow = value; notifyPropertyChanged(); }
            set { mbShow = value; }
        }
        public bool Drag
        {
            get { return mbDrag;  }
            //set { mbDrag = value; notifyPropertyChanged(); }
            set { mbDrag = value;}
        }

        public DrawState DrawState
        {
            get{return meState;}
            set { meState = value; }
        }
        public virtual String Text 
        {
            get { return mStrText; }
            //set { mStrText = value; notifyPropertyChanged(); }
            set { mStrText = value; }
        }
        public Point Location { get { return mptCurrentPosition; } set { mptCurrentPosition = value; } }
        public int CurrentImageIndex { get { return miCurrentImageIndex; } set { miCurrentImageIndex = value; } }
        public int StartImageIndex { get { return miStartImageIndex; } set { miStartImageIndex = value; } }
        public int EndImageIndex { get { return miEndImageIndex; } set { miEndImageIndex = value; } }
        public int CurrentPositionX
        {
            get { return mptCurrentPosition.X; }
            set { mptCurrentPosition.X = value; }
        }
        public int CurrentPositionY
        {
            get { return mptCurrentPosition.Y; }
            set { mptCurrentPosition.Y = value; }
        }
        public Size ItemSize
        {
            get { return mszSize; }
            set { mszSize = value; }
        }
        public int Height
        {
            get { return mszSize.Height; }
            set { mszSize.Height = value; }
        }
        public int Width
        {
            get { return mszSize.Width; }
            set { mszSize.Width = value;  }
        }
        public virtual void OnSizeChanged() { }
        public Color ClrBack { get { return mClrBack; } set { mClrBack = value; } }
        public Color ClrBorder { get { return mClrBorder; } set { mClrBorder = value; } }
        public Color ClrText { get { return mClrText; } set { mClrText = value; } }
        public bool Moveable { get { return mbMoveable; } set { mbMoveable = value; } }
        public Image getImage(int aIndex)
        {
            if (aIndex >= 0 && aIndex < mListImage.Count)
                return (Image)mListImage[aIndex];
            return null;
        }
        public virtual bool hitTest(Point aPoint)
        {
            Rectangle rect = new Rectangle(mptCurrentPosition, mszSize);
            return rect.Contains(aPoint);
        }

        public Rectangle getBoundary()
        {
            Rectangle rect = new Rectangle(mptCurrentPosition, mszSize);
            return rect;
        }

        public virtual void draw(Graphics aGraphics)
        {
            if (mbDrag)
            {
                float[] dashValues = { 5, 2, 10, 4 };
                Pen blackPen = new Pen(Color.Red, 1);
                //blackPen.DashPattern = dashValues;
                aGraphics.DrawRectangle(blackPen, this.CurrentPositionX, this.CurrentPositionY, this.Width, this.Height - 1);
            }
        }
        public virtual void notifyPropertyChanged()
        {
            if (mPropertyChangeObserver != null)
                mPropertyChangeObserver.onPropertyChanged(this);
        }
        public virtual void onAdvance() { }
        public virtual void drawBackground(Graphics aGraphics) { }
        public virtual void onKeyPress(object aSender, String aKey) { }
        public virtual bool onMouseDown(MouseEventArgs e) { return false; }
        public virtual bool onMouseUp(MouseEventArgs e) { return false; }
        public virtual bool onMouseMove(MouseEventArgs e) { return false; }
        public virtual bool onMouseLeave(MouseEventArgs e) { return false; }
        public virtual bool onPositionChange(Point aPoint) 
        {
            CurrentPositionX += aPoint.X;
            CurrentPositionY += aPoint.Y;
            //notifyPropertyChanged();
            return false;
        }
        public void DrawRoundRect(Graphics g, Pen p, float x, float y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();

            gp.AddLine(x + radius, y, x + width - (radius * 2), y); // Line
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90); // Corner
            gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2)); // Line
            gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90); // Corner
            gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height); // Line
            gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90); // Corner
            gp.AddLine(x, y + height - (radius * 2), x, y + radius); // Line
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90); // Corner
            gp.CloseFigure();

            g.DrawPath(p, gp);
            gp.Dispose();
        }
    }
}

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
using ResourceHeader;

namespace ResourceCreator
{
    public class SceneCanvas : IDrawElementList
    {
        //Graphics mGraphics;
        ArrayList mList;
        static SceneCanvas mInstance = null;
        Size mSzCanvasSize;
        //Image mPicture;
        IntPtr mHandle;
        IDrawable mDrawable;
        PictureBox mCanvas;
        Rectangle mRectDirty;
        //ElementTracker mTracking;

        public IntPtr Handle { get { return mHandle; } }
        public PictureBox Canvas { get { return mCanvas; } set { mCanvas = value; } }

       // public ElementTracker Tracker { get { return mTracking; } }

        public Size Size 
        {
            get {return mSzCanvasSize; }
            set { mSzCanvasSize = value; }
        }

        SceneCanvas()
        {
            //mGraphics = null;
            mDrawable = null;
            mList = new ArrayList();
            mRectDirty = new Rectangle(0,0,0,0);
            //mTracking = ElementTracker.Instance;//new ElementTracker();
            //mTracking.CreateHotSpot();
        }

        public static SceneCanvas Self()
        {
            if (null == mInstance) {
                mInstance = new SceneCanvas();
            }
            return mInstance;        
        }

        public IDrawable Drawable
        {
            get { return mDrawable; }
            set { mDrawable = value; }
        }

        public void initialize(Size aSize,IntPtr aHandle)
        {   
            /*mPicture = new Bitmap(aSize.Width, aSize.Height);
            mGraphics = Graphics.FromImage(mPicture);
            */
            mHandle = aHandle;
            mSzCanvasSize = aSize;
        }

        public void updateArea(Rectangle aRect)
        {
            mRectDirty.X = mRectDirty.X|aRect.X;
            mRectDirty.Y = mRectDirty.Y | aRect.Y;
            mRectDirty.Width = mRectDirty.Width | aRect.Width;
            mRectDirty.Height = mRectDirty.Height | aRect.Height;
            ////
            //if (null != mCanvas)
            //{
            //    mCanvas.Invalidate(aRect);
            //}
        }

        
        public void Add(DrawElement aDrawable)
        {
            if (null != mList && null != aDrawable) {
                if (!mList.Contains(aDrawable)) {                                        
                    mList.Add(aDrawable);
                }
            }
        }

        public void Remove(DrawElement obj)
        {
            mList.Remove(obj);
        }
      
        public void removeAll()
        {
            mList.Clear();        
        }

      
        public void drawBackground(Graphics aGraphics)
        {
            Brush brushBack = new SolidBrush(Color.White);
            Pen penRect = new Pen(Color.Black);
            
            aGraphics.FillRectangle(brushBack,
                                    0,
                                    0,
                                    mSzCanvasSize.Width,
                                    mSzCanvasSize.Height);


            //aGraphics.DrawRectangle(penRect,
            //                        0,
            //                        0,
            //                        this.mSzCanvasSize.Width-1,
            //                        this.mSzCanvasSize.Height-1);

            brushBack.Dispose();
        }

        public void draw(Graphics aGraphics)
        {
            //drawBackground(aGraphics);            
            foreach (DrawElement aDrawable in mList)
            {
                if (aDrawable.Show)
                {
                    aDrawable.draw(aGraphics);
                }
            }

            //mTracking.draw(aGraphics);
        }

        public void onMouseDown(MouseEventArgs e)
        {
            //Tracker.onMouseDown(e);
            //Invalidate();
        }

        public void onMouseUp(MouseEventArgs e)
        {
            //Tracker.onMouseUp(e);
            //Invalidate();
        }

        public void onMouseMove(MouseEventArgs e)
        {
            //Tracker.onMouseMove(e);
            //Invalidate();
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

    }
}

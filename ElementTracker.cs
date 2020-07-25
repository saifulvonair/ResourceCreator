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
using ResourceHeader;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace ResourceCreator
{
    public interface IHotSpotObserver
    {
        void OnHotSpot(ResourceCreator.ElementTracker.HotSpot hotSpot);
    }
    //
    public class ElementTracker
    {
        public class HotSpot : IDrawable
        {
            public Rectangle mMainRect;
            public Point mHitPoint;
            protected int mHotWidth;
            protected int mHotHeight;
            protected int mCenterX;
            protected int mCenterY;
            protected int mMinSize = 8;


            public virtual void UpdateHotSpot()
            {
            }

            public HotSpot()
            {
                mHotHeight = mMinSize;
                mHotWidth = mMinSize;
                // This will be chabged in run time
                Width = mMinSize;
                Height = mMinSize;
            }

            public virtual void Transform(IDrawable obj, int x, int y) { }

            public override void draw(Graphics aGraphics)
            {
                //base.draw(aGraphics);
                if (mbDrag)
                {
                    float[] dashValues = { 5, 2, 10, 4 };
                    Pen blackPen = new Pen(Color.Black, 1);
                    aGraphics.DrawRectangle(blackPen, mCenterX, mCenterY, mHotWidth, mHotHeight);
                }
            }

            protected void UpdateLeft(IDrawable obj, int x, int y)
            {
                int dis = x - obj.CurrentPositionX;
                obj.Width -= dis;
                obj.CurrentPositionX = x;

                if (obj.Width < mMinSize)
                {
                    obj.Width = mMinSize;
                }
            }

            protected void UpdateBottom(IDrawable obj, int x, int y)
            {

                obj.Height = y - obj.CurrentPositionY;

                if (obj.Height < mMinSize)
                {
                    obj.Height = mMinSize;
                }
  
            }

            protected void UpdateTop(IDrawable obj, int x, int y)
            {
                int dis = y - obj.CurrentPositionY;
                obj.Height -= dis;

                if (obj.Height < mMinSize)
                {
                    obj.Height = mMinSize;
                }

                else
                {
                    obj.CurrentPositionY = y;
                }
            }

            protected void UpdateRight(IDrawable obj, int x, int y)
            {
                obj.Width = x - obj.CurrentPositionX;

                if (obj.Width < mMinSize)
                {
                    obj.Width = mMinSize;
                }
            }
        }
        //
        public class HotSpotRight : HotSpot
        {
            public HotSpotRight()
            {
                
            }
            public override void UpdateHotSpot()
            {
                base.UpdateHotSpot();
                this.mCenterX = mMainRect.Right;
                this.mCenterY = mMainRect.Y + mMainRect.Height / 2 - mHotHeight / 2;
                this.CurrentPositionX = mMainRect.X + mMainRect.Width;
                this.CurrentPositionY = mMainRect.Y;
                Height = mMainRect.Height;
            }

            public override void Transform(IDrawable obj, int x, int y)
            {
                base.Transform(obj, x, y);
                UpdateRight(obj, x, y);
            }
        }
        //
        public class HotSpotLeft : HotSpot
        {
            public HotSpotLeft()
            {

            }
            public override void UpdateHotSpot()
            {
                base.UpdateHotSpot();
                this.mCenterX = mMainRect.Left - mHotHeight;
                mCenterY = mMainRect.Y + mMainRect.Height / 2 - mHotHeight / 2;
                Height = mMainRect.Height;
                this.CurrentPositionX = mMainRect.X - mHotWidth;
                this.CurrentPositionY = mMainRect.Y;

            }

            public override void Transform(IDrawable obj, int x, int y)
            {
                base.Transform(obj, x, y);
                UpdateLeft(obj, x, y);
            }
        }
        //
        public class HotSpotTop : HotSpot
        {
            public HotSpotTop()
            {

            }

            public override void UpdateHotSpot()
            {
                base.UpdateHotSpot();
                this.mCenterX = mMainRect.X + mMainRect.Width/2 - mHotWidth / 2;
                this.mCenterY = mMainRect.Y - mHotHeight;
                Width = mMainRect.Width;
                this.CurrentPositionX = mMainRect.X;
                this.CurrentPositionY = mMainRect.Y - mHotHeight;
            }

            public override void Transform(IDrawable obj, int x, int y)
            {
                base.Transform(obj, x, y);
                UpdateTop(obj, x, y);
            }
        }
        //
        public class HotSpotBottom : HotSpot
        {
            public HotSpotBottom()
            {

            }

            public override void UpdateHotSpot()
            {
                base.UpdateHotSpot();
                this.mCenterX = mMainRect.X + mMainRect.Width/2 - mHotWidth/2;
                this.mCenterY = mMainRect.Bottom;
                Width = mMainRect.Width;
                this.CurrentPositionX = mMainRect.X;
                this.CurrentPositionY = mMainRect.Y + mMainRect.Height;
            }

            public override void Transform(IDrawable obj, int x, int y)
            {
                base.Transform(obj, x, y);
                UpdateBottom(obj, x, y);
            }
        }
        //
        public class HotSpotRightBottom : HotSpot
        {
            public HotSpotRightBottom()
            {

            }

            public override void UpdateHotSpot()
            {
                base.UpdateHotSpot();
                this.mCenterX = mMainRect.X + mMainRect.Width;
                this.mCenterY = mMainRect.Y + mMainRect.Height;
                this.CurrentPositionX = mMainRect.X + mMainRect.Width;
                this.CurrentPositionY = mMainRect.Y + mMainRect.Height;
            }

            public override void Transform(IDrawable obj, int x, int y)
            {
                base.Transform(obj, x, y);

                obj.Height = y - obj.CurrentPositionY;

                if (obj.Height < mMinSize)
                {
                    obj.Height = mMinSize;
                }

                obj.Width = x - obj.CurrentPositionX;

                if (obj.Width < mMinSize)
                {
                    obj.Width = mMinSize;
                }
            }

        }
        //
        public class HotSpotRightTop : HotSpot
        {
            public HotSpotRightTop()
            {

            }

            public override void draw(Graphics aGraphics)
            {
                //base.draw(aGraphics);
                if (mbDrag)
                {
                    float[] dashValues = { 5, 2, 10, 4 };
                    Pen blackPen = new Pen(Color.Black, 1);
                    aGraphics.DrawRectangle(blackPen, CurrentPositionX, CurrentPositionY, mHotWidth, mHotHeight);
                }
            }

            public override void UpdateHotSpot()
            {
                base.UpdateHotSpot();

                this.mptCurrentPosition.X = mMainRect.Right;
                this.mptCurrentPosition.Y = mMainRect.Top - mHotHeight;
            }

            public override void Transform(IDrawable obj, int x, int y)
            {
                base.Transform(obj, x, y);

                int dis = y - obj.CurrentPositionY;
                obj.Height -= dis;

                if (obj.Height < mMinSize)
                {
                    obj.Height = mMinSize;
                }

                else
                {
                    obj.CurrentPositionY = y;
                }

                //
                obj.Width = x - obj.CurrentPositionX;

                if (obj.Width < mMinSize)
                {
                    obj.Width = mMinSize;
                }
            }

        }
        //
        public class HotSpotLeftTop : HotSpot
        {
            public HotSpotLeftTop()
            {

            }

            public override void draw(Graphics aGraphics)
            {
                //base.draw(aGraphics);
                if (mbDrag)
                {
                    float[] dashValues = { 5, 2, 10, 4 };
                    Pen blackPen = new Pen(Color.Black, 1);
                    aGraphics.DrawRectangle(blackPen, CurrentPositionX, CurrentPositionY, mHotWidth, mHotHeight);
                }
            }

            public override void UpdateHotSpot()
            {
                base.UpdateHotSpot();
                this.mptCurrentPosition.X = mMainRect.Left - mHotWidth;
                this.mptCurrentPosition.Y = mMainRect.Top - mHotHeight;
            }

            public override void Transform(IDrawable obj, int x, int y)
            {
                base.Transform(obj, x, y);

                int dis = y - obj.CurrentPositionY;
                obj.Height -= dis;

                if (obj.Height < mMinSize)
                {
                    obj.Height = mMinSize;
                }

                else
                {
                    obj.CurrentPositionY = y;
                }

                //
                UpdateLeft(obj, x, y);
            }

        }
        //
        public class HotSpotLeftBottom : HotSpot
        {
            public HotSpotLeftBottom()
            {

            }
           
            public override void UpdateHotSpot()
            {
                base.UpdateHotSpot();
                this.mptCurrentPosition.X = mMainRect.Left - mHotWidth;
                this.mptCurrentPosition.Y = mMainRect.Bottom;
                this.mCenterX = this.mptCurrentPosition.X;
                this.mCenterY = this.mptCurrentPosition.Y;             
            }

            public override void Transform(IDrawable obj, int x, int y)
            {
                base.Transform(obj, x, y);
                UpdateLeft(obj, x, y);
                UpdateBottom(obj, x, y);
            }

        }
        //
        public bool m_AddToHistory = false;
        //
        private ArrayList mList;
        private HotSpot mSelected;
        private DrawElement mTrackObj;
        private Dictionary<HotSpot, Cursor> m_dicCursor;
        // My be Track object but my not if click on HotSpot
        private DrawElement mHitObj;
        private Point mptLastPos;
        private IHotSpotObserver m_HotSpotObserver;

        static private ElementTracker m_Instance;

        public DrawElement TrackObj { get { return mTrackObj; } set { mTrackObj = value; } }
        public DrawElement HitObj { get { return mHitObj; } set { mHitObj = value; } }

        // Single instance
        static public ElementTracker Instance 
        {
            get 
            {
                if (m_Instance == null)
                    m_Instance = new ElementTracker();
                return m_Instance;
            }
        }

        //
        public ElementTracker()
        {
            m_Instance = null;
            mList = new ArrayList();
            m_dicCursor = new Dictionary<HotSpot, Cursor>();
        }
        //
        public void AttachHotSpotObserver(IHotSpotObserver observer)
        {
            m_HotSpotObserver = observer;
        }
        //
        public Cursor GetHotSpotCursor(HotSpot hotSpot)
        {
            try
            {
                return m_dicCursor[hotSpot];
            }
            catch (Exception e) {  }

            return System.Windows.Forms.Cursors.Default;
        }


        void AddHotSpot(HotSpot hotSpot)
        {
            hotSpot.Drag = true;
            mList.Add(hotSpot);
        }

        public HotSpot CreateHotSpot()
        {
            HotSpot hotSpot = new HotSpotRight();
            m_dicCursor.Add(hotSpot, System.Windows.Forms.Cursors.SizeWE);
            AddHotSpot(hotSpot);

            hotSpot = new HotSpotLeft();
            m_dicCursor.Add(hotSpot, System.Windows.Forms.Cursors.SizeWE);
            AddHotSpot(hotSpot);
            
            hotSpot = new HotSpotTop();
            m_dicCursor.Add(hotSpot, System.Windows.Forms.Cursors.SizeNS);
            AddHotSpot(hotSpot);
            
            hotSpot = new HotSpotBottom();
            m_dicCursor.Add(hotSpot, System.Windows.Forms.Cursors.SizeNS);
            AddHotSpot(hotSpot);
            
            hotSpot = new HotSpotRightBottom();
            m_dicCursor.Add(hotSpot, System.Windows.Forms.Cursors.SizeNWSE);
            AddHotSpot(hotSpot);
            
            hotSpot = new HotSpotRightTop();
            m_dicCursor.Add(hotSpot, System.Windows.Forms.Cursors.SizeNESW);
            AddHotSpot(hotSpot);

            hotSpot = new HotSpotLeftTop();
            m_dicCursor.Add(hotSpot, System.Windows.Forms.Cursors.SizeNWSE);
            AddHotSpot(hotSpot);
            hotSpot = new HotSpotLeftBottom();
            m_dicCursor.Add(hotSpot, System.Windows.Forms.Cursors.SizeNESW);
            AddHotSpot(hotSpot);
            return hotSpot;
        }

        public void ShowHotSpot( Point point)
        {
            if (mTrackObj == null)
                return;

            foreach (HotSpot aDrawable in mList)
            {
                aDrawable.Show = true;
                aDrawable.mMainRect = mTrackObj.getBoundary();
                aDrawable.mHitPoint = point;
                aDrawable.UpdateHotSpot();
            }
        }

        public void UpdateHotSpot(Point point)
        {
            if (mTrackObj == null)
                return;

            foreach (HotSpot aDrawable in mList)
            {
                if (aDrawable.Show)
                {
                    aDrawable.mMainRect = mTrackObj.getBoundary();
                    aDrawable.mHitPoint = point;
                    aDrawable.UpdateHotSpot();
                }
            }
        }

        public HotSpot FindHotSpot(Point point)
        {
            mSelected = null;
            if (mTrackObj == null)
                return null;
            mSelected = HitTest(point);
            return mSelected;
        }

        public HotSpot HitTest(Point point)
        {
            foreach (HotSpot aDrawable in mList)
            {
                if (aDrawable.Show)
                {
                    if (aDrawable.hitTest(point))
                    {
                        return aDrawable;
                    }
                }
            }

            return null;
        }

        public bool hitTest(Point aPoint)
        {
            if(mTrackObj == null)
                return false;

            Rectangle rc = mTrackObj.getBoundary();

            rc.X = rc.X - 5;
            rc.Y = rc.Y - 5;
            rc.Width = rc.Width + 10;
            rc.Height = rc.Height + 10;

            return rc.Contains(aPoint);

        }
       
        public void ShowHotSpot(bool show, int hotSpot)
        {
            HotSpot hs = (HotSpot)mList[hotSpot];
            hs.Show = show;
        }

        public  void draw(Graphics aGraphics)
        {
            if (mTrackObj == null)
                return;

            Rectangle rc = mTrackObj.getBoundary();

            rc.X = rc.X - 5;
            rc.Y = rc.Y - 5;
            rc.Width = rc.Width + 10;
            rc.Height = rc.Height + 10;

            float[] dashValues = { 5, 2, 10, 4 };
            Pen pen = new Pen(Color.Red, 1);
            //aGraphics.DrawRectangle(pen, rc.Left, rc.Top, rc.Width, rc.Height);

            foreach (IDrawable aDrawable in mList)
            {
                if (aDrawable.Show)
                {
                    aDrawable.draw(aGraphics);
                }
            }
        }

        public bool  HandleMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (mTrackObj == null)
                return false;

            //mptLastPos = e.Location;
            ShowHotSpot(e.Location);
            FindHotSpot(e.Location);
            return false;

        }

        public bool HandleMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (mTrackObj == null)
                return false;

            int x = e.X;
            int y = e.Y;

            

            if (mSelected != null)
            {
                mSelected.Transform(mTrackObj, x, y);
                UpdateHotSpot(e.Location);
                m_AddToHistory = true;
                //mTrackObj.Property.SetValue("Location", mTrackObj.Location.X.ToString() + "," + mTrackObj.Location.Y.ToString());
                //mTrackObj.Property.SetValue("Size", mTrackObj.Width.ToString() + "," + mTrackObj.Height.ToString());
                mTrackObj.OnSizeChanged();
                return true;
            }

            return false;
        }

        public bool HandleMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            if (null != TrackObj)
            {
                // if Size has been changed then
                PropertyObject propObjSize = mTrackObj.Property.GetPropObj("Size");
                propObjSize.TextNew = mTrackObj.Width.ToString() + "," + mTrackObj.Height.ToString();
                PropertyObject propObjLoc = mTrackObj.Property.GetPropObj("Location");
                propObjLoc.TextNew = mTrackObj.Location.X.ToString() + "," + mTrackObj.Location.Y.ToString();
                PropertyObject propModified = null;

                if (propObjSize.TextNew != propObjSize.Text)
                {
                    propModified = propObjSize;
                }
                else if (propObjLoc.TextNew != propObjLoc.Text)
                {
                    propModified = propObjLoc;
                }

                if (propObjLoc != null)
                {
                    mTrackObj.Factory.OnModify(propModified, new EventArgs());
                    mTrackObj.OnSizeChanged();  
                }

            }

            m_AddToHistory = false;
            mSelected = null;
            return true;
        }

        public DrawElement FindTrackObj(Point point, DrawElement hitItem)
        {
            // If diff track obj than must change it
            // If in Hotspot
            // If nothing
            // IF in item
            if (FindHotSpot(point) != null)
            {
                return TrackObj;
            }

            return hitItem;
        }

        public void onMouseDown(MouseEventArgs e)
        {
            DrawElement hitItem = null;
            mptLastPos = e.Location;
            hitItem = FindTrackObj(e.Location, HitObj);
            TrackObj = hitItem;
            m_AddToHistory = false;
            if (TrackObj != null)
            {
                if (TrackObj.Moveable)
                {
                    TrackObj.Drag = true;
                    HandleMouseDown(e);
                }
                // Need to move in the manager class
                TrackObj.onMouseDown(e);
            }
        }

        public void onMouseMove(MouseEventArgs e)
        {
            // If not handle by hotspot then
            if (!HandleMouseMove(e))
            {
                updateMove(e);
                UpdateHotSpot(e.Location);
                HotSpot hotspot = HitTest(e.Location);
                m_HotSpotObserver.OnHotSpot(hotspot);
            }
        }

        public void onMouseUp(MouseEventArgs e)
        {
            if (null != TrackObj)
            {
                TrackObj.Drag = false;
            }

            HandleMouseUp(e);
            //NotifyObserver();
        }

        public void updateMove(MouseEventArgs e)
        {

            if (null != TrackObj)
            {
                if (TrackObj.Moveable && TrackObj.Drag == true)
                {
                    int x = e.X - mptLastPos.X;
                    int y = e.Y - mptLastPos.Y;
                    if(x != 0 || y != 0)
                    {
                        TrackObj.onPositionChange(new Point(x, y));
                        //mTrackObj.Property.SetValue("Location", mTrackObj.Location.X.ToString() + "," + mTrackObj.Location.Y.ToString());
                        mptLastPos = e.Location;
                        m_AddToHistory = true;
                    }
                }
            }
        }
    }
}

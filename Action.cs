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
using ResourceHeader;
using System.Drawing;
using System.Windows.Forms;

//
namespace ResourceCreator
{
    public class Action : IPropertyChangeObserver
    {
        protected DrawElement m_Model;
        protected ElementTracker m_Tracker;
        public DrawElement Model 
        {
            get { return m_Model; }
            set { m_Model = value; m_Model.PropertyChangeObserver = this; } 
        }
        public bool mbUndo = false;
        protected IController mController;
        protected ElementProperty m_ActionParam;
        public ElementProperty Param { get { return m_ActionParam; } }

        public Action(IController controller)
        {
            mController = controller;
            m_Model = null;
            m_Tracker = ElementTracker.Instance;
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
        public void SetParam(ElementProperty param)
        {
            m_ActionParam = param;
        }

        public virtual int execute()
        {
            m_Tracker.TrackObj = m_Model;
            m_Tracker.UpdateHotSpot(new Point(0, 0));
            m_Model.notifyPropertyChanged();
            return 0;
        }
        //
        public virtual int Undo() 
        {
            return execute();
        }
        //
        public virtual int Redo()
        {
            return execute();
        }
        //
        protected void RefreshView()
        {
            mController.dispatchRequest(Model);
        }

       
        #region IPropertyChangeObserver Members

        void IPropertyChangeObserver.onPropertyChanged(IDrawable obj)
        {
            RefreshView();
        }

        #endregion
    }
    //
    public class ActionProperty : Action
    {

        public ActionProperty(IController controller)
            :base(controller)
        {
           
        }

        public override int execute()
        {
            UpdatePosition();

            bool[] hotSpot = { true, true };

            if (Model is ElementWindow)
            {
                hotSpot[0] = false;
                hotSpot[1] = false;
            }

            ElementProperty.CopyProperty(Model.Property, m_ActionParam);
            //
            m_Tracker.ShowHotSpot(hotSpot[0], 1);
            m_Tracker.ShowHotSpot(hotSpot[1], 2);
            Model.Text = m_ActionParam.GetValue("Text");

            return base.execute();
        }
      
        //
        protected void UpdatePosition()
        {
            int x = 0, y =0,w =0,h =0;
            Get2dProperty(m_ActionParam.GetValue("Location"), ref x, ref y);
            Get2dProperty(m_ActionParam.GetValue("Size"), ref w, ref h);
            Model.CurrentPositionX = x;
            Model.CurrentPositionY = y;
            Model.Width = w;
            Model.Height = h;
            if (Model is ElementWindow)
            {
                m_Model.OnSizeChanged();
            }
        }
    }
    // This action should assign all default values. caption, pos, size,
    // location, style etc.. also know the concrete model, like button, window, text
    public class ActionAdd : ActionProperty
    {
        public IDrawElementList m_Parent;
        public Control m_ParentControl;

        public ActionAdd(IController controller)
            :base(controller)
        {

        }

        public override int execute()
        {
            //m_Window.add(m_ActionParam.Model);
            //ActionParamAdd param = (ActionParamAdd)m_ActionParam;
            if (m_Parent != null)
            {
                m_Parent.Add(Model);
            }

            m_Tracker.TrackObj = Model;
          
            return base.execute();
        }

        public override int Undo()
        {
            m_Parent.Remove(Model);
            m_Tracker.TrackObj = null;
            RefreshView();
            return 0;
        }

        public override int Redo()
        {
            return execute();
        }
    }   
    //
    public class ActionModifyProperty : Action
    {
        private String m_sOldVal;
        private String m_sNewVal;

        public PropertyObject m_PropObj;
        
        public ActionModifyProperty(IController controller)
            : base(controller)
        {
            m_sNewVal = "";
            m_sOldVal = "";
        }

        public override int execute()
        {
            // Save Old Value
            m_sOldVal = Model.Property.GetValue(m_PropObj.Name);
            m_sNewVal = m_PropObj.TextNew;

            bool[] hotSpot = { true, true };

            if (Model is ElementWindow)
            {
                hotSpot[0] = false;
                hotSpot[1] = false;
            }
            //
            m_Tracker.ShowHotSpot(hotSpot[0], 1);
            m_Tracker.ShowHotSpot(hotSpot[1], 2);
            //
            Model.Property.SetValue(m_PropObj.Name, m_PropObj.TextNew);
            Model.UpdateState(m_PropObj.Name);
            return base.execute();
        }

        public override int Undo()
        {
            Model.Property.SetValue(m_PropObj.Name, m_sOldVal);
            Model.UpdateState(m_PropObj.Name);
            return base.execute();
        }

        public override int Redo()
        {
            Model.Property.SetValue(m_PropObj.Name, m_sNewVal);
            Model.UpdateState(m_PropObj.Name);
            return base.execute();
        }
    }
}

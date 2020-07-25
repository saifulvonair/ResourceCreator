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
using System.Collections;
using ResourceCreator;
using System.Windows.Forms;

namespace ResourceHeader
{
    public class FactoryShape : IController, IDataChangeObserver
    {
        protected ArrayList mList;
        protected int m_iStartY;
        protected int m_iStrtX;
        protected String m_Name;//
        protected GridView m_GridView;
        protected IntPtr m_Handle;
        protected Control m_Control;// = Control.FromHandle(m_Handle);
        //
        //public ElementProperty m_Property;
        // Property
        public ArrayList List { get { return mList; } }
        public String Name { get { return m_Name; } set { m_Name = value; } }
        public GridView GridView {set{m_GridView = value;} get{return m_GridView;}}
        public IntPtr Handle 
        {
            set 
            { 
                m_Handle = value;
                m_Control = Control.FromHandle(m_Handle);
            } 
        }

        public FactoryShape()
        {
            mList = new ArrayList();
            m_Name = "";
            m_GridView = null;
            //m_Property = null;
        }
      
        private void Get2dProperty(String val, ref int i, ref int j )
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
        public virtual void PopulatePropertyList(ElementProperty prop)
        {   
            // Remove Observer
            m_GridView.SetDataChangeObserver(null);
            m_GridView.RemoveAll();

            PropertyObject propObj = null;
            GridViewItem itemSub = null;

            prop.InitPropertyMap();

            // Use var keyword to enumerate dictionary
            foreach (var pair in prop.List)
            {
                propObj = pair.Value;
                m_GridView.AddItem(0, 0, propObj.Name);
                itemSub = m_GridView.AddItem(0, 1, "", propObj.Type);
                itemSub.m_ItemData = propObj;
                // May be not necessary if use this in the object itself..
                //SetProValues(prop, propObj.Name, itemSub);
                itemSub.m_Data = propObj.List;
            }
            // Will be used as Adapter object's data
            m_GridView.Tag = prop;
            m_GridView.SetDataChangeObserver(this);

        }
        // MUST Overide this method to reflect concrete property
        public virtual void UpdateProperty(DrawElement obj)
        {
            if (obj == null)
            {
                return;
            }

            UpdatePropertyList(obj);
            m_GridView.Invalidate();
            m_Control.Invalidate();            
        }

        public void UpdatePropertyList(DrawElement obj)
        {
            // Depends on Object....
            //if (ElementTracker.Instance.TrackObj != obj)
            {
                obj.Factory.PopulatePropertyList(obj.Property);
                FactoryManager.getInstance().SetSelectedFactory(obj.Factory);
            }
        }
        //
        public virtual ResourceCreator.Action CreateAddAction(DrawElement obj, IDrawElementList parent)
        {
            ActionAdd action = new ActionAdd(this);
            action.m_Parent = parent;
            action.Model = obj;
            return action;
        }
        //
        public virtual DrawElement CreateControl() 
        {
            return null; 
        }
        // Delete all item from the list
        public void removeAll()
        {
	        // Finallly remove the deleted items
	        mList.Clear();
        }
       
        // Add item to the mList 
        public void Add(DrawElement item)
        {
	        if (item != null) 
            {
		        mList.Add(item);
                item.Factory = this;
		    }
        }
        // Remove item from the mList
        public void Remove(DrawElement item)
        {
	        if (item != null)
            {
		        mList.Remove(item);
		    }
        }
        // Return the total number of DrawElement
        public int getCount()
        {
	        return mList.Count;
        }

        #region IController Members

        public void dispatchRequest(object obj)
        {
            if (obj is DrawElement)
            {
                UpdateProperty((DrawElement)obj);
            }
        }

        #endregion

        #region IDataChangeObserver Members

        public void OnModify(object sender, EventArgs e)
        {
            if (sender is PropertyObject)
            {
                PropertyObject propObj = (PropertyObject)sender;
                ActionModifyProperty action = new ActionModifyProperty(this);
                action.m_PropObj = propObj;
                action.Model = ElementTracker.Instance.TrackObj;
                ControllerCommandHistory.Instance.ExcuteAction(action);

            }
       }

        #endregion
    }
}
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

using System.Collections;

namespace ResourceHeader
{
    class Composite : DrawElement, IDrawElementList
    {
        protected ArrayList mList;

        public ArrayList List { get { return mList; } }
        
        public Composite()
        {
            mList = new ArrayList();
        }
        // Add item to the mList 
        public void Add(DrawElement item)
        {
            if (!mList.Contains(item)) 
            {
		        mList.Add(item);
	        }
        }

        // Remove item from the mList
        public void Remove(DrawElement item)
        {
            mList.Remove(item);
        }

        public DrawElement GetLastItem()
        {
            if (mList.Count > 0)
                return (DrawElement)mList[mList.Count - 1];
            return null;
        }
    }
}

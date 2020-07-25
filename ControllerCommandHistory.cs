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

namespace ResourceCreator
{
    public interface IHistoryController
    {
        int ExcuteAction(Action action);
        //
        int HandleRedo();
        //
        int HandleUndo();

    }
    //
    public class ControllerCommandHistory : IHistoryController, IController
    {
        #region IController Members

        public void dispatchRequest(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        private int m_iNextUndo;
        private ArrayList mList;
        private Action m_Action;
        public Action LastAction { get { return m_Action; } }
        public ArrayList list { get { return mList; } }
        public int NextUndo { get { return m_iNextUndo; } }

        //
        static private ControllerCommandHistory m_Instance;
        //
        // Single instance
        static public ControllerCommandHistory Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new ControllerCommandHistory();
                return m_Instance;
            }
        }

        public ControllerCommandHistory()
        {
            mList = new ArrayList();
            m_iNextUndo = -1;
            m_Action = null;
            m_Instance = null;
        }
        //
        public void ClearHistory()
        {
            mList.Clear();
            m_iNextUndo = -1;
            m_Action = null;
        }
        //
        void trimHistoryList()
        {
	        /* We can redo any undone command until we execute a new 
	        * command. The new command takes us off in a new direction,
	        * which means we can no longer redo previously undone actions. 
	        * So, we purge all undone commands from the history list.*/

	        // Exit if no items in History list
	        if (mList.Count == 1) return;

	        // Exit if NextUndo points to last item on the list
	        if (m_iNextUndo == mList.Count - 1) return;

	        // Purge all items below the NextUndo pointer
	        for (int i = mList.Count - 1; i > m_iNextUndo; i--)
	        {	
		        mList.RemoveAt(i);
            }
        }
        //
        public int ExcuteAction(Action action)
        {
           trimHistoryList();

           int ret = action.execute();
           mList.Add(action);
           m_Action = action;
           // Move the 'next undo' pointer to point to the new command
           m_iNextUndo++;
           return ret;
        }
        //
        public int HandleRedo()
        {
            // If the NextUndo pointer points to the last item, no commands to redo
            if (m_iNextUndo == mList.Count -1) return -1;
            // Get the Command object to redo
            m_iNextUndo++;
            // Get the Command object to be undone
            Action action = (Action)mList[m_iNextUndo];
            int ret = action.Redo();
            return ret;
        }

        public int HandleUndo()
        {
            // If the NextUndo pointer is -1, no commands to undo
            if ( m_iNextUndo < 0) return -1;

            // Get the Command object to be undone
            Action action = (Action)mList[m_iNextUndo];

            m_iNextUndo--;
            return action.Undo();
        }

    }
}

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
using System.IO;
using ResourceCreator;


namespace ResourceHeader
{

    class FactoryManager
    {
        const string DATA_FILE = "datafile.dat";
        static private FactoryManager mInstance;
        private ArrayList mList;
        private FactoryShape m_SelectedTool;

        public ArrayList List { get { return mList; } }
        public FactoryManager()
        {
	        mList = new ArrayList();
            CreateFactory();
        }

        public void AttachDepency(GridView propView, IntPtr handle)
        {
            foreach (FactoryShape factory in mList)
            {
                factory.GridView = propView;
                factory.Handle = handle;
            }
 
        }

        // Delete all item from the list
        void removeAll()
        {
	        mList.Clear();
	    }

        // Return the single instance of this class
        public static FactoryManager getInstance(){
	        if (mInstance == null) {
		        mInstance = new FactoryManager();
	        }
	        return mInstance;
        }

        public void CreateFactory()
        {
            FactroryImage.getInstance().initialize();

            //
            FactoryShape factory = new FactoryShapeWindow();
            //factory.Name = "MainWindow";
            add(factory);
            factory = new FactoryShapeButton();
            add(factory);
      /*      factory = new FactoryShapeRadioButton();
            add(factory);
            factory = new FactoryShapeCheckBox();
            add(factory);
            factory = new FactoryShapeComboBox();
            add(factory);
            factory = new FactoryShapeTextBox();
            add(factory);
            factory = new FactoryShapeLabel();
            add(factory);
            factory = new FactoryShapeListBox();
            add(factory);
            factory = new FactoryShapeGridView();
            add(factory);
            */


            // Window
            SelectTool(0);
        }

        public FactoryShape GetFactory(int index)
        {
            if (index >= 0 && index < mList.Count)
                return (FactoryShape)mList[index];
            return null;
        }

        public FactoryShape GetFactory(String name)
        {
            int index = -1;
            FactoryShape factory = null;

            for(int i =0; i<mList.Count; i++)
            {
               factory = (FactoryShape)mList[i];
                if (name == factory.Name)
                {
                    index = i;
                    break;
                }   
            }

          return  GetFactory(index);
        }
        
        // Serialize method to load data from disk
        public void load(StreamReader stream){
            //int count = 0;
            //stream >> count;
            //FactoryShape *item = null;
            //int i = 0;
            //for (i = 0; i < count; i++) {
            //    // FIXME this only for testing 
            //    // and it will be depends on type of shape
            //    item = new FactoryShapeRectangle();
            //    if (item != null) {
            //        item->load(stream);
            //        add(item);
            //    }
            //}

        }

        // Serialize method to save into disk
        public void save(StreamWriter stream){
	        // No of Shape
            //stream << mCount;
            //FactoryShape *item = null;
            //int i = 0;
            //for (i = 0; i < mCount; i++) {
            //    item = get(i);
            //    if (item != null) {
            //        item->save(stream);
            //    }
            //}
        }

        //Serialise the data based on parameter loadData
        // if loadData = true then it will load data
        // if fale then it will save data to disk
        public void serialize(bool loadData){
            //FXFileStream  stream;		
            //bool fileOpenSuccess = false;
            //try {
            //    if (loadData) {
            //        // Load the data		
            //        fileOpenSuccess = stream.open(DATA_FILE,FXStreamLoad);
            //        if (fileOpenSuccess) {
            //            load(stream);
            //            stream.close();
            //        }
            //    } else {
            //        // Save the data
            //        fileOpenSuccess = stream.open(DATA_FILE,FXStreamSave);
            //        if (fileOpenSuccess) {				
            //            save(stream);
            //            stream.close();
            //        }
            //    }
            //}
            //catch(...) {

            //}
        }
        // Add item to the mList 
        void add(FactoryShape item){
	        if (item != null) 
            {
		        mList.Add(item);
	        }
        }

        // Return the total number of FactoryShape of FactoryManager
        int getCount(){
	        return mList.Count;
        }

        public FactoryShape SelectTool(int toolIndex)
        {
            m_SelectedTool = GetFactory(toolIndex);
            return m_SelectedTool;
        }

        public FactoryShape GetSelectedTool()
        {
            return m_SelectedTool;
        }

        public void SetSelectedFactory(FactoryShape factory)
        {
            m_SelectedTool = factory;
        }
      
        // Set the IDrawElementObserver observer
        //void setDrawElementObserver(IDrawElementObserver *observer){
        //    mDrawElementObserver = observer;
        //}

        // Notify the IDrawElementObserver to didplay 
        // selected item information
        void notifyDrawElementObserver(){
            //if (mDrawElementObserver != null) {
            //    mDrawElementObserver->updateUI(mHitItem);
            //}
        }

        // Notify the IDrawElementObserver to update UI
        void updateUI()
        {
            //if (mDrawElementObserver != null) {
            //    mDrawElementObserver->updateUI();
            //}
        }

        
    }
}
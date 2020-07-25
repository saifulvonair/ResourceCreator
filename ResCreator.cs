using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResourceHeader;
using System.Collections;
using ResourceConverter;
using System.Xml;

namespace ResourceCreator
{
    public partial class ResCreator : Form, IHotSpotObserver
    {
        SceneCanvas mSceneCanvas = SceneCanvas.Self();
        ControllerCommandHistory mHostory = ControllerCommandHistory.Instance;
        ActionAdd m_ActionWindow;
        ElementWindow m_Window;// = (DrawElementWindow)m_ActionWindow.mModel;

        public ResCreator()
        {
            InitializeComponent();
            ElementTracker.Instance.CreateHotSpot();
            //
            m_GridView.AddColumn("ProName", 150);
            m_GridView.AddColumn("Value", 233);
           // m_GridView.AddColumn("", 1);

            FactoryManager.getInstance().AttachDepency(m_GridView, m_Canvas.Handle);
            mSceneCanvas.Canvas = this.m_Canvas;
            CreateWindow();

            String fileName = Environment.CurrentDirectory + "\\Resource.xml";
            XmlDocument xDoc = new XmlDocument();

            try
            {
                xDoc.Load(fileName);

                XmlNodeList nodes = xDoc.GetElementsByTagName("Dialog");
                if (nodes.Count == 0)
                {
                    //CreateWindow();
                }
                foreach (XmlNode node in nodes)
                {
                    CreateElementWindow(node);
                }
            }
            catch(Exception e)
            {
            }

            m_listItems.Items.RemoveAt(0);
        }
        //
        public void CreateWindow()
        {
            FactoryShape factory = FactoryManager.getInstance().GetSelectedTool();
            ElementWindow model = (ElementWindow)factory.CreateControl();
            m_Window = (ElementWindow)model;
            model.Container = m_Canvas;

            bool[] hotSpot = { false, false };
            mSceneCanvas.Add(model);

            ElementProperty prop = FactoryActionParam.CreateParam(model);
            model.Property = prop;
            factory.PopulatePropertyList(prop);
            m_GridView.Tag = prop;
            m_ActionWindow = new ActionAdd(factory);
            m_ActionWindow.Model = model;
            // Assign a copy of Param.....
            m_ActionWindow.SetParam(prop);
            mHostory.ExcuteAction(m_ActionWindow);

            //mSceneCanvas.Canvas.Invalidate();
            PopulateControlList();
            ElementTracker.Instance.AttachHotSpotObserver(this);
            //m_Window.PropertyChangeObserver = this;
            model.OnSizeChanged();
        }
        //
        public void CreateElementWindow(XmlNode node)
        {
            FactoryShape factory = FactoryManager.getInstance().GetFactory(node.Name);
            ElementWindow model = (ElementWindow)factory.CreateControl();
            m_Window = (ElementWindow)model;
            model.Container = m_Canvas;
            
            bool[] hotSpot = { false, false };
            mSceneCanvas.Add(model);

            ElementProperty prop = FactoryActionParam.CreateParam(model);
            model.Property = prop;
            factory.PopulatePropertyList(prop);
            m_GridView.Tag = prop;
           
            foreach (XmlAttribute attr in node.Attributes)
            {
                prop.SetValue(attr.Name, attr.Value);
            }

            //FIXME use Factory for action...
            m_ActionWindow = new ActionAdd(factory);
            m_ActionWindow.Model = model;
            // Assign a copy of Param.....
            m_ActionWindow.SetParam(prop);
            mHostory.ExcuteAction(m_ActionWindow);

            //mSceneCanvas.Canvas.Invalidate();
            PopulateControlList();
            ElementTracker.Instance.AttachHotSpotObserver(this);
            //m_Window.PropertyChangeObserver = this;
            model.OnSizeChanged();

            foreach (XmlNode child in node.ChildNodes)
            {
                // We only need Control elements..
                if (child.Name == "Properties") continue;
                //
                CreateChildlement(child);
            }

        }
        //
        public void CreateChildlement(XmlNode node)
        {
            FactoryShape factoryShape = FactoryManager.getInstance().GetFactory(node.Name);
            DrawElement model = factoryShape.CreateControl();

            ElementProperty prop = FactoryActionParam.CreateParam(model);
            model.Property = prop;

            foreach (XmlAttribute attr in node.Attributes)
            {
                prop.SetValue(attr.Name, attr.Value);
            }


            try
            {
                XmlNodeList propertiesNodes = node.FirstChild.ChildNodes;
                foreach(XmlNode nodeProp in propertiesNodes)
                {
                    prop.SetValue(nodeProp.Name, nodeProp.InnerText);
                }

            }
            catch (Exception e)
            {

            }
            



            factoryShape.PopulatePropertyList(prop);
            m_GridView.Tag = prop;
            Action action = factoryShape.CreateAddAction(model, m_Window);
            action.SetParam(prop);
            //
            mHostory.ExcuteAction(action);
        }
        //
        public void WriteToXml()
        {
            ManagerConverter con = new ManagerConverter();
            ArrayList list = new ArrayList();
            list.Add(m_Window);
            con.SetParsers(list);

            String fileLoc = Environment.CurrentDirectory + "\\Resource.xml";
            String headerFileLoc = Environment.CurrentDirectory + "\\Resource.h";
            //MessageBox.Show(fileLoc);
            con.ConvertFile(fileLoc);
            //

            //ResourceIdWriter.ConvertFile(headerFileLoc, fileLoc);
        }
        //
        private void PopulateControlList()
        {
            ArrayList list = FactoryManager.getInstance().List;

            m_listItems.Items.Clear();

            foreach (FactoryShape factory in list)
            {
                this.m_listItems.Items.Add(factory.Name);
            }
        }
        //
        private void m_Canvas_Paint(object sender, PaintEventArgs e)
        {
            mSceneCanvas.draw(e.Graphics);
            ElementTracker.Instance.draw(e.Graphics);
       }
        //
        private void m_Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            mSceneCanvas.onMouseUp(e);
            ElementTracker.Instance.onMouseUp(e);
            m_Canvas.Invalidate();
        }
        //
        private void UpdatePropertyList(DrawElement obj)
        {
            // Depends on Object....
            //if (ElementTracker.Instance.TrackObj != obj)
            {
                obj.Factory.PopulatePropertyList(obj.Property);
                obj.Factory.UpdateProperty(obj);
                FactoryManager.getInstance().SetSelectedFactory(obj.Factory);
            }
        }
        //
        private void m_Canvas_MouseMove(object sender, MouseEventArgs e)
        {

            mSceneCanvas.onMouseMove(e);
            ElementTracker.Instance.onMouseMove(e);
            m_Canvas.Invalidate();
        }
        //
        private void m_Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            

            mSceneCanvas.onMouseDown(e);
            ElementWindow window = m_Window;//(DrawElementWindow)m_ActionWindow.mModel;

            if (ElementTracker.Instance.FindHotSpot(e.Location) == null)
            {
                ElementTracker.Instance.HitObj = window.GetHitItem(e.Location);
                if (ElementTracker.Instance.HitObj == null)
                {
                    ElementTracker.Instance.HitObj = window;
                    ElementTracker.Instance.ShowHotSpot(false, 1);
                    ElementTracker.Instance.ShowHotSpot(false, 2);
                }
                // Click on Diff Object
                UpdatePropertyList(ElementTracker.Instance.HitObj);
            }
            
            ElementTracker.Instance.onMouseDown(e);
            m_Canvas.Invalidate();
            
        }
        //
        private void mRedo_Click(object sender, EventArgs e)
        {
            mHostory.HandleRedo();
        }
        //
        private void mUndo_Click(object sender, EventArgs e)
        {
            mHostory.HandleUndo();
       }
        //
        private void ResCreator_Load(object sender, EventArgs e)
        {
          
            m_grpLeft.Height = this.Height - m_listItems.Top - 56;
            m_listItems.Left = m_grpLeft.Left + 5;
            m_listItems.Width = m_grpLeft.Width - 10;

            m_GridView.Left = m_listItems.Left;
            m_GridView.Top = m_listItems.Bottom + 4;

            m_GridView.Width = m_listItems.Width;
            m_GridView.Height = m_grpLeft.Height - m_listItems.Height - 60;
            //

            m_grpRight.Height = m_grpLeft.Height;
            m_grpRight.Width = this.Width - m_grpLeft.Width - 30;
            mHostory.ClearHistory();
            
        }
        //
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = m_listItems.SelectedIndex+1;
            FactoryShape factoryShape = FactoryManager.getInstance().SelectTool(index);
            DrawElement model = factoryShape.CreateControl();

            ElementProperty prop = FactoryActionParam.CreateParam(model);
            int id = m_Window.List.Count + 1;
            String strVal = factoryShape.Name + factoryShape.List.Count.ToString();
            model.Property = prop;
            prop.SetValue("Name", strVal.Trim());
            prop.SetValue("Id", id.ToString());
            prop.SetValue("Location", model.Location.X.ToString() + "," + model.Location.Y.ToString());
            prop.SetValue("Text", model.Text);
            prop.SetValue("Size", model.Width.ToString() + "," + model.Height.ToString());
            //
            factoryShape.PopulatePropertyList(prop);
            m_GridView.Tag = prop;
            //
            Action action = factoryShape.CreateAddAction(model, m_Window);
            action.SetParam(prop);
            mHostory.ExcuteAction(action);
            
        }


        #region IHotSpotObserver Members

        public void OnHotSpot(ElementTracker.HotSpot hotSpot)
        {
            this.m_Canvas.Cursor = ElementTracker.Instance.GetHotSpotCursor(hotSpot);
        }

        #endregion

        private void mCommit_Click(object sender, EventArgs e)
        {   
            WriteToXml();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ResCreator_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteToXml();
        }

        private void m_listItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

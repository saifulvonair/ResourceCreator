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

namespace ResourceCreator
{
    public class PropertyObject
    {
        private String m_Text;
        private String m_Name;
        private String[] m_list;
        private ITEM_TYPE m_eType;
        private String m_TextNew;
        //
        private String m_sDefaultText;
        //
        public String Text { get { return m_Text; } set { m_Text = value; } }
        public String DefaultText { get { return m_sDefaultText; } set { m_sDefaultText = value; } }
        public String Name { get { return m_Name; } set { m_Name = value; } }
        public ITEM_TYPE Type { get { return m_eType; } set { m_eType = value; } }
        public String[] List { get { return m_list; } set { m_list = value; } }
        public String TextNew { get { return m_TextNew; } set { m_TextNew = value; } }
        public PropertyObject(String name, String val, ITEM_TYPE type)
        {
            m_Text = val;
            m_Name = name;
            m_eType = type;
            m_list = null;
            m_sDefaultText = val;
        }
        //
        public bool IsModified()
        {
            return (m_sDefaultText != m_Text);
        }
    }
    //
    public class ElementProperty
    {
       // protected Dictionary<String, String[]> m_dicPropertyData;
        protected bool m_bModified;
        protected Dictionary<String, PropertyObject> m_dicListProperty;

        public virtual bool IsModified() { return false; }
        public Dictionary<String, PropertyObject> List { get { return m_dicListProperty; } }

        public virtual void InitDefaultProperty() 
        {
           
        }
        //
        public virtual void InitPropertyMap()
        {
            String[] sList = { "true", "false" };
            //m_dicPropertyData.Add("Enable", sList);
            //m_dicPropertyData.Add("Visible", sList);
            AddObjValue("Enable", sList);
            AddObjValue("Visible", sList);
        }
        //
        protected void AddObjValue(String name, String[] values)
        {
            PropertyObject obj = GetPropObj(name);
            obj.List = values;
        }
        //
        public ElementProperty()
        {
           
           m_bModified = false;
           m_dicListProperty = new Dictionary<string, PropertyObject>();

           m_dicListProperty.Add("Id", new PropertyObject("Id", "1", ITEM_TYPE.Label));
           m_dicListProperty.Add("Name", new PropertyObject("Name", "Button", ITEM_TYPE.TextBox));
           m_dicListProperty.Add("Text", new PropertyObject("Text", "Button", ITEM_TYPE.TextBox));
           m_dicListProperty.Add("Enable",new PropertyObject("Enable", "true", ITEM_TYPE.ComboBox));
           m_dicListProperty.Add("Location", new PropertyObject("Location", "0,0", ITEM_TYPE.TextBox));
           m_dicListProperty.Add("Size", new PropertyObject("Size", "76,27", ITEM_TYPE.TextBox));
           m_dicListProperty.Add("Visible", new PropertyObject("Visible", "true", ITEM_TYPE.ComboBox));
        }
        //
        public String GetValue(String name)
        {
            try
            {
                return m_dicListProperty[name].Text;
            }
            catch (Exception e) { }

            return "";
        }
        //
        public PropertyObject GetPropObj(String name)
        {
            try
            {
                return m_dicListProperty[name];
            }
            catch (Exception e) { }

            return null;
        }
        //
        public void SetValue(String name, String text)
        {
            PropertyObject obj = GetPropObj(name);

            if (obj != null)
            {
                obj.Text = text;
            }
        }
        //
        static public void CopyProperty(ElementProperty propOld, ElementProperty propNew)
        {
            PropertyObject propObjOld = null;
            PropertyObject propObjNew = null;
            int size = propOld.List.Count;

            List<PropertyObject> listOld = new List<PropertyObject>(propOld.List.Values);
            List<PropertyObject> listNew = new List<PropertyObject>(propNew.List.Values);


            for (int i = 0; i < size; i++)
            {
                propObjNew = listNew[i];
                propObjOld = listOld[i];
                propObjOld.Text = propObjNew.Text;
            }
        }
        //
        public bool IsPropertyModified(String name)
        {
            PropertyObject propObj = GetPropObj(name);

            if (propObj != null)
            {
                return propObj.IsModified();
            }

            return false;
        }
    }
    //
    public class ElementPropertyWindow : ElementProperty
    {
        public ElementPropertyWindow()
        {
            m_dicListProperty.Add("SystemMenu", new PropertyObject("SystemMenu", "true", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("BorderStyle", new PropertyObject("BorderStyle", "Sizable", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("HelpButton", new PropertyObject("HelpMenu", "true", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("Icon", new PropertyObject("Icon", "Default", ITEM_TYPE.TextBox));
            m_dicListProperty.Add("MaximizeBox", new PropertyObject("MaximizeBox", "true", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("MinimizeBox", new PropertyObject("MinimizeBox", "true", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("ShowIcon", new PropertyObject("ShowIcon", "true", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("TopMost", new PropertyObject("TopMost", "false", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("TopLevel", new PropertyObject("TopLevel", "false", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("WindowState", new PropertyObject("WindowState", "Normal", ITEM_TYPE.ComboBox));
        }
        //
        public override bool IsModified()
        {
            return base.IsModified();
        }
        //
        public override void InitDefaultProperty()
        {
            base.InitDefaultProperty();
            ////
            SetValue("Location", "0,0");
            SetValue("Name", "Window");
            SetValue("Text", "Window");
            SetValue("Size", "550,400");
        }
        //
        public override void InitPropertyMap()
        {
            base.InitPropertyMap();

            String[] sListTF = { "true", "false" };

            AddObjValue("SystemMenu", sListTF);
            AddObjValue("HelpButton", sListTF);
            AddObjValue("MaximizeBox", sListTF);
            AddObjValue("MinimizeBox", sListTF);
            AddObjValue("ShowIcon", sListTF);
            AddObjValue("TopMost", sListTF);
            AddObjValue("TopLevel", sListTF);

            String[] sListBorder = { "Sizable", "FixedSingle", "Fixed3D", "FixedDialog", "FixedToolWindow", "SizableToolWindow", "None" };
            AddObjValue("BorderStyle", sListBorder);

            String[] sListWndState = { "Normal", "Minimized", "Maximized" };
            AddObjValue("WindowState", sListWndState);
        }
        //
    }
    //
    public class ElementPropertyButton : ElementProperty
    {
        public ElementPropertyButton()
        {
            m_dicListProperty.Add("FlatStyle", new PropertyObject("FlatStyle", "Standard", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("Image",new PropertyObject("Image", "", ITEM_TYPE.Button));
            m_dicListProperty.Add("ImageAlign",new PropertyObject("ImageAlign", "MiddleCenter", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("TabStop", new PropertyObject("TabStop", "true", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("TabIndex", new PropertyObject("TabIndex", "1", ITEM_TYPE.TextBox));
        }
        //
        public override void InitDefaultProperty()
        {
            base.InitDefaultProperty();
        }

        public override void InitPropertyMap()
        {
            base.InitPropertyMap();
            String[] sListFlatStyle = { "Standard", "System", "Popup", "Flat" };
            AddObjValue("FlatStyle", sListFlatStyle);
            String[] sListImageAlign = { "MiddleCenter", "MiddleLeft", "MiddleRight", "BottomLeft", "BottomCenter", "BottomRight", "TopLeft", "TopCenter", "TopRight" };
            AddObjValue("ImageAlign", sListImageAlign);
        }
    }
    //
    public class ElementPropertyButtonBase : ElementProperty
    {
        public ElementPropertyButtonBase()
        {
            m_dicListProperty.Add("Appearance", new PropertyObject("Appearance", "Standard", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("AutoCheck", new PropertyObject("AutoCheck", "true", ITEM_TYPE.Button));
            m_dicListProperty.Add("CheckeAlign", new PropertyObject("CheckeAlign", "", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("Checked", new PropertyObject("Checked", "", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("TabStop", new PropertyObject("TabStop", "true", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("TabIndex", new PropertyObject("TabIndex", "1", ITEM_TYPE.TextBox));
            m_dicListProperty.Add("TextAlign", new PropertyObject("TextAlign", "MiddleCenter", ITEM_TYPE.ComboBox));

        }
        //
        public override void InitDefaultProperty()
        {
            base.InitDefaultProperty();
        }

        public override void InitPropertyMap()
        {
            base.InitPropertyMap();
            String[] sList = { "true", "false" };
            String[] sListImageAlign = { "MiddleCenter", "MiddleLeft", "MiddleRight", "BottomLeft", "BottomCenter", "BottomRight", "TopLeft", "TopCenter", "TopRight" };
            AddObjValue("TextAlign", sListImageAlign);
            AddObjValue("AutoCheck", sList);
            AddObjValue("Checked", sList);
            AddObjValue("TabStop", sList);
        }
    }
    //
    public class ElementPropertyRadioButton : ElementPropertyButtonBase
    {
        public ElementPropertyRadioButton()
        {
            SetValue("Name", "RadioButton");
            SetValue("Text", "RadioButton");
            PropertyObject propObj = GetPropObj("Text");
            propObj.DefaultText = "RadioButton";
        }
    }
    //
    public class ElementPropertyCheckBox : ElementPropertyButtonBase
    {
        public ElementPropertyCheckBox()
        {
            SetValue("Name", "CheckBox");
            SetValue("Text", "CheckBox");
            PropertyObject propObj = GetPropObj("Text");
            propObj.DefaultText = "CheckBox";
        }
    }
    //
    public class ElementPropertyComboBox : ElementProperty
    {
        public ElementPropertyComboBox()
        {
            SetValue("Name", "ComboBox");

            m_dicListProperty.Add("DropDownHeight", new PropertyObject("DropDownHeight", "Standard", ITEM_TYPE.TextBox));
            m_dicListProperty.Add("DropDownStyle", new PropertyObject("DropDownStyle", "DropDown", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("DropDownWidth", new PropertyObject("DropDownWidth", "76", ITEM_TYPE.TextBox));
            m_dicListProperty.Add("FlatStyle", new PropertyObject("FlatStyle", "", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("ForeColor", new PropertyObject("ForeColor", "", ITEM_TYPE.TextBox));
            m_dicListProperty.Add("ItemHeight", new PropertyObject("ItemHeight", "13", ITEM_TYPE.TextBox));
            m_dicListProperty.Add("Sorted", new PropertyObject("Sorted", "false", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("TabStop", new PropertyObject("TabStop", "true", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("TabIndex", new PropertyObject("TabIndex", "1", ITEM_TYPE.TextBox));
        }
        //
        public override void InitPropertyMap()
        {
            base.InitPropertyMap();
            String[] sList = { "true", "false" };
            String[] sDropDownStyle = { "DropDown", "DropDownList", "Simple" };
            String[] sListFlatStyle = { "Standard", "System", "Popup", "Flat" };

            AddObjValue("DropDownStyle", sDropDownStyle);
            AddObjValue("FlatStyle", sListFlatStyle);
            AddObjValue("Sorted", sList);
            AddObjValue("TabStop", sList);
        }
    }
    //
    public class ElementPropertyTextBox : ElementProperty
    {
        public ElementPropertyTextBox()
        {
            m_dicListProperty.Add("AcceptReturn", new PropertyObject("AcceptReturn", "false", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("BorderStyle", new PropertyObject("BorderStyle", "Fixed3D", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("CharacterCasing", new PropertyObject("CharacterCasing", "Normal", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("Multiline", new PropertyObject("Multiline", "false", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("PasswordChar", new PropertyObject("PasswordChar", "", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("ScrollBars", new PropertyObject("ScrollBars", "None", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("TextAlign", new PropertyObject("TextAlign", "Left", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("TabStop", new PropertyObject("TabStop", "true", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("TabIndex", new PropertyObject("TabIndex", "1", ITEM_TYPE.TextBox));
        }
        //
        public override void InitPropertyMap()
        {
            base.InitPropertyMap();
            String[] sList = { "true", "false" };
            String[] sListBorder = { "None", "FixedSingle", "Fixed3D" };
            String[] sCharacterCasing = { "Normal", "Upper", "Lower" };
            String[] sScrollBars = { "None", "Horzontal", "Vertical", "Both" };
            String[] sTextAlign = { "Left", "Right", "Center" };

            AddObjValue("AcceptReturn", sList);
            AddObjValue("BorderStyle", sListBorder);
            AddObjValue("CharacterCasing", sCharacterCasing);
            AddObjValue("Multiline", sList);
            AddObjValue("ScrollBars", sScrollBars);
            AddObjValue("TextAlign", sTextAlign);
            AddObjValue("TabStop", sList);
        }
    }
    //
    public class ElementPropertyLabel : ElementProperty
    {
        public ElementPropertyLabel()
        {
            SetValue("Name", "Label");
            m_dicListProperty.Add("BorderStyle", new PropertyObject("BorderStyle", "Fixed3D", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("FlatStyle", new PropertyObject("FlatStyle", "Standard", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("TabIndex", new PropertyObject("TabIndex", "1", ITEM_TYPE.TextBox));
        }
        //
        public override void InitDefaultProperty()
        {
            base.InitDefaultProperty();
            SetValue("Name", "Label");
        }
        //
        public override void InitPropertyMap()
        {
            base.InitPropertyMap();
            String[] sListBorder = { "None", "FixedSingle", "Fixed3D" };
            String[] sListFlatStyle = { "Standard", "System", "Popup", "Flat" };
            AddObjValue("FlatStyle", sListFlatStyle);
            AddObjValue("BorderStyle", sListBorder);
        }
    }
    //
    public class ElementPropertyListBox : ElementProperty
    {
        public ElementPropertyListBox()
        {
            m_dicListProperty.Add("BorderStyle", new PropertyObject("BorderStyle", "Fixed3D", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("ColumnWidth", new PropertyObject("ColumnWidth", "0", ITEM_TYPE.TextBox));
            m_dicListProperty.Add("HorizontalExtent", new PropertyObject("HorizontalExtent", "0", ITEM_TYPE.TextBox));
            m_dicListProperty.Add("HorizontalScrollbar", new PropertyObject("HorizontalScrollbar", "false", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("ItemHeight", new PropertyObject("ItemHeight", "13", ITEM_TYPE.TextBox));
            m_dicListProperty.Add("ScrollAlwaysVisible", new PropertyObject("ScrollAlwaysVisible", "false", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("SelectionMode", new PropertyObject("SelectionMode", "One", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("Sorted", new PropertyObject("Sorted", "false", ITEM_TYPE.ComboBox));
            m_dicListProperty.Add("TabIndex", new PropertyObject("TabIndex", "1", ITEM_TYPE.TextBox));
            m_dicListProperty.Add("TabStop", new PropertyObject("TabStop", "true", ITEM_TYPE.ComboBox));

        }
        //
        public override void InitDefaultProperty()
        {
            base.InitDefaultProperty();
            SetValue("Name", "ListBox");
            SetValue("Text", "ListBox");
            SetValue("Size", "150,100");
        }
        //
        public override void InitPropertyMap()
        {
            base.InitPropertyMap();

            String[] sList = { "true", "false" };
            String[] sSelectionMode = { "One", "MultiSimple","MultiExtended","None" };
            String[] sListBorder = { "None", "FixedSingle", "Fixed3D" };
            AddObjValue("BorderStyle", sListBorder);
            AddObjValue("HorizontalScrollbar", sList);
            AddObjValue("ScrollAlwaysVisible", sList);
            AddObjValue("SelectionMode", sSelectionMode);
            AddObjValue("Sorted", sList);
            AddObjValue("TabStop", sList);
        }
    }
    //
    public class ElementPropertyGridView : ElementProperty
    {
        public ElementPropertyGridView()
        {
            m_dicListProperty.Add("TabIndex", new PropertyObject("TabIndex", "1", ITEM_TYPE.TextBox));
            m_dicListProperty.Add("TabStop", new PropertyObject("TabStop", "true", ITEM_TYPE.ComboBox));
        }

        //
        public override void InitDefaultProperty()
        {
            base.InitDefaultProperty();
            SetValue("Name", "GridView");
            SetValue("Text", "GridView");
            SetValue("Size", "150,100");
        }
        //
        public override void InitPropertyMap()
        {
            base.InitPropertyMap();
            String[] sList = { "true", "false" };
            AddObjValue("TabStop", sList);
        }
    }
}   

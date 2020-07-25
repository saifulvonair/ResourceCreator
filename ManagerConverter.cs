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
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using ResourceHeader;
using ResourceCreator;

namespace ResourceConverter
{
    class ManagerConverter
    {
        private ArrayList m_list;
        private XmlDocument m_XmlDom;
        //
        public ManagerConverter()
        {
            m_list = null;
        }
        //
        public void SetParsers(ArrayList listOfParsers)
        {
            m_list = listOfParsers;
        }
        //
        public void ConvertFile(String filePath)
        {
            if (m_list == null)
                return;
            
            m_XmlDom = new XmlDocument();
            m_XmlDom.AppendChild(m_XmlDom.CreateElement("", "HostResource", ""));
            XmlElement nodeDialog = null;
            XmlElement nodeChild = null;
            XmlElement xmlRoot = m_XmlDom.DocumentElement;
            xmlRoot.SetAttribute("version", "1.0");
           

            foreach (ElementWindow dialog in m_list)
            {
                XmlElement node = m_XmlDom.CreateElement("", "Dialogs", "");
                nodeDialog = CreatePropertyNode(dialog, node);
                //WriteProperty(dialog, nodeDialog);
                WriteDialog(dialog, nodeDialog);
                xmlRoot.AppendChild(node);

                //foreach (DrawElement item in dialog.List)
                //{
                //    nodeChild = CreatePropertyNode(item, nodeDialog);
                //    WriteProperty(item, nodeChild);
                //}
            }

            Console.WriteLine(m_XmlDom.InnerXml);
            String str = Path.GetFileNameWithoutExtension(filePath);
            m_XmlDom.Save(str + ".xml");
        }
        //
        private XmlElement CreatePropertyNode(DrawElement item, XmlElement node)
        {
            XmlElement nodeChild = null;
            nodeChild = m_XmlDom.CreateElement(item.Name);
            node.AppendChild(nodeChild);
            return nodeChild;
        }
        //
        private void WriteProperty(DrawElement item, XmlElement node)
        {
            PropertyObject propObj = null;
            int index = 0;
            List<PropertyObject> listProp = new List<PropertyObject>(item.Property.List.Values);

            foreach (String prop in item.Property.List.Keys)
            {
                propObj = listProp[index];
                //if (propObj.IsModified())
                {
                    node.SetAttribute(propObj.Name, propObj.Text);
                }
                index++;
            }
        }
        //

        /// <summary>generates xml for a dialog resource item</summary>
        /// <param name="dlg">
        ///  The dialog to be converted</param>
        ///  <param name="vals">
        ///  String tokens</param>
        ///  <param name="ParentNode">
        ///  Parent xml node for the menu item.</param>
        ///
        ///
        private void WriteDialog(ElementWindow dlg, XmlElement parentNode)
        {
            SetItemAttribs(dlg, parentNode);
            // write each of the items
            foreach (DrawElement child in dlg.List)
            {
                XmlElement nodeChild = m_XmlDom.CreateElement("", child.Factory.Name, "");
                SetItemAttribs(child, nodeChild);
                parentNode.AppendChild(nodeChild);
            }
        }
        /// <summary>sets attributes of a control based on what properties were set</summary>
        ///
        private void SetItemAttribs(DrawElement item, XmlElement node)
        {
            XmlElement properties = m_XmlDom.CreateElement("", "Properties", "");
            node.AppendChild(properties);
            foreach (String strKey in item.Property.List.Keys)
            {
                if (strKey.Equals("Id"))
                {
                    node.SetAttribute(strKey, item.Property.GetValue(strKey));
                }
                else if (!string.IsNullOrEmpty(item.Property.GetValue(strKey)))
                {
                    XmlElement nodeChild = m_XmlDom.CreateElement("", strKey, "");
                    nodeChild.InnerText = item.Property.GetValue(strKey);
                    properties.AppendChild(nodeChild);
                }
            }
        }
    }
}

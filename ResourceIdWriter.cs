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
using System.Xml;
using System.Collections;
using System.IO;

namespace ResourceCreator
{
    class ResourceIdWriter
    {
        static public String m_sNewLine = "\r\n";
        //
        static public void ConvertFile(String resFile, String xmlFile)
        {
            XmlDocument xDoc = new XmlDocument();

            try
            {
                xDoc.Load(xmlFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            XmlNodeList nodes = xDoc.GetElementsByTagName("Dialog");
            XmlNode child = null;
            int count = nodes.Count;
            XmlAttribute att;
            ArrayList list = new ArrayList();

            for (int i = 0; i < count; i++)
            {
                child = nodes[i];
                att = child.Attributes[0];
                list.Add(child);
                AddChildNodeId(child, list);
                //list.Add(m_sNewLine);
            }

            nodes = xDoc.GetElementsByTagName("Image");
            count = nodes.Count;
            for (int i = 0; i < count; i++)
            {
                child = nodes[i];
                list.Add(child);                
            }


            ExportToFile(list, resFile);
        }
        //
        static void AddChildNodeId(XmlNode node, ArrayList list)
        {
            XmlNode child = null;
            int count = node.ChildNodes.Count;
            XmlAttribute att;

            for (int i = 0; i < count; i++)
            {
                child = node.ChildNodes[i];
                att = child.Attributes[0];

                if (list.IndexOf(att.Value) == -1)
                {
                    list.Add(child);
                }
            }
        }
        // Pass Output file name here
        static void ExportToFile(ArrayList list, String exportFileName)
        {

            StreamWriter sw = new StreamWriter(exportFileName);

            if (sw == null)
                return;

            try
            {
                sw.WriteLine("// Auto generated IDs DoNOT edit");
                sw.WriteLine("// Will be changed after build");
                sw.WriteLine("#pragma once");
                sw.WriteLine();

                String str = "";
                foreach (XmlNode node in list)
                {
                    str = node.Attributes[1].Value;
                    str = str.PadRight(50);

                    //if (m_sNewLine == strID)
                    //{
                    //    sw.WriteLine(m_sNewLine);
                    //}
                    //else if (strID == "IDC_STATIC")
                    //{
                    //    //sw.WriteLine("#define " + strID + "\t\t" + "-1");                       
                    //    sw.WriteLine("#define " + str + "-1");
                    //}
                    //else
                    {
                        //sw.WriteLine("#define " + strID + "\t\t" + id.ToString());
                        sw.WriteLine("#define " + str + node.Attributes[0].Value);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //WriteResMap(sw, list);

            sw.Close();
        }
        //
        //static void WriteResMap(StreamWriter sw, ArrayList list)
        //{
        //    sw.WriteLine("#include <map>");
        //    sw.WriteLine("using namespace std;");
        //    sw.WriteLine("static map<char*, int> m_ResMap;");

        //    sw.WriteLine("");
        //    sw.WriteLine("//");
        //    sw.WriteLine("static void LoadResID()");
        //    sw.WriteLine("{");


        //    String str = " ";
        //    foreach (XmlNode strID in list)
        //    {
        //        //if (m_sNewLine != strID)
        //        {
        //            str = "\tm_ResMap[\"" + strID.Value + "\"" + "]";
        //            str = str.PadRight(56);
        //            sw.WriteLine(str + "= " + strID.Value + ";");
        //        }
        //    }

        //    sw.WriteLine("}");

        //    sw.WriteLine("");
        //    sw.WriteLine("//");
        //    sw.WriteLine("static int GetResID(char* str)");
        //    sw.WriteLine("{");
        //    sw.WriteLine("\treturn m_ResMap[str];");
        //    sw.WriteLine("}");
        //}
    }
}

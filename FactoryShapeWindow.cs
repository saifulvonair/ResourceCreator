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

using ResourceHeader;
using System.Drawing;

namespace ResourceCreator
{
    class FactoryShapeWindow : FactoryShape
    {

        public FactoryShapeWindow()
        {
            m_Name = "Dialog";
        }

        //
        public override DrawElement CreateControl()
        {
            ElementWindow obj = new ElementWindow();
            //ElementPropertyWindow prop = new ElementPropertyWindow();
            //prop.InitDefaultProperty();
            //prop.InitPropertyMap();

            obj.Text = "Window";
            obj.Location = new Point(0, 0);
            obj.Width = 550;// Get From Prop
            obj.Height = 400;//Get From Prop
            obj.CreateControlButton();
            //obj.Property = prop;

            Image img = FactroryImage.getInstance().getImage(IMAGES.ControlBox_png);
            obj.ListImage.Add(img);

            Add(obj);

            //m_Property = prop;

            base.CreateControl();
            return obj;

        }
       
    }
}

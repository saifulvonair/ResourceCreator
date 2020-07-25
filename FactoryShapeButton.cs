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
    class FactoryShapeButton : FactoryShape
    {
       
        public FactoryShapeButton()
        {
            m_iStartY = 10;
            m_iStrtX = 10;
            m_Name = "Button";
        }
        //
        public override DrawElement CreateControl()
        {
            ElementButton obj = new ElementButton();
            obj.Text = "Button";
            obj.Location = new Point(m_iStrtX + 100, m_iStartY + 50);
            m_iStartY += 20;
            Add(obj);
            base.CreateControl();
            return obj;
        }      
    }
}

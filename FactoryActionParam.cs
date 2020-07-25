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

namespace ResourceCreator
{
    public class FactoryActionParam
    {
        public static ElementProperty CreateParam(DrawElement obj)
        {
            ElementProperty prop = null;

            if (obj is ElementWindow)
            {
                prop = new ElementPropertyWindow();
            }
            else if (obj is ElementButton)
            {
                prop = new ElementPropertyButton();
            }
            if (prop != null)
            {
                prop.InitDefaultProperty();
            }
            return prop;
        }

    }
}

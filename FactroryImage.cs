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
using System.Text;
using System.Media;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;


namespace ResourceCreator
{
    class FactroryImage
    {
        //
        #region private
        static ArrayList mpListImages;
        static bool mbInitialize = false;
        static FactroryImage mpCFactroryImage;
        #endregion

        public static String IMAGE_LOC = @"\\Contents\\Images";

        bool mbSuccess;
        public bool Success { get { return mbSuccess; } }
        private FactroryImage()
        {
            mpCFactroryImage = null;
            mbSuccess = false;
        }
        //
        public static FactroryImage getInstance()
        {
            if (mpCFactroryImage == null)
                mpCFactroryImage = new FactroryImage();
            return mpCFactroryImage;
        }

        //
        #region public
        
        public void initialize()
        {
            if(null == mpListImages)
	        {
		        mpListImages = new ArrayList();
	        }
	        mbInitialize = true;
            loadAllImage(0);
        }
        //
        public void uninitialize()
        {

        }      
        //
        public Image getImage(IMAGES aIndex)
        {
            Image lpImg = null;
	        //int lId = 0;
            if (mbInitialize)
            {
                if (aIndex > IMAGES.Start && aIndex < IMAGES.End)
                {
                    lpImg = (Image)mpListImages[(int)aIndex];
                }
            }
            return lpImg;
        }
        //
        public void loadAllImage(int aDpi)
        {
            try 
            {
                String strLoc = Application.StartupPath + IMAGE_LOC;
                String[] listFile = Directory.GetFiles(strLoc);
                foreach (String str in listFile)
                {
                    try
                    {
                        Image img = Image.FromFile(str);
                        if (null != img)
                        {
                            append(img);
                        }
                    }
                    catch { mbSuccess = false; }
                }
                mbSuccess = true;
            }
            catch { }           
        }

        public void append(Image aImage)
        {
            mpListImages.Add(aImage);
        }
        //
        #endregion
      }
}

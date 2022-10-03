using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace GEIMS.Client.UI
{
    public partial class ImageConversions : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreatePhoto();
        }

        #region Create photo function by wabcam
        /// <summary>
        /// Created By : Amruta
        /// </summary>
        void CreatePhoto()
        {
            try
            {
                string strPhoto = Request.Form["imageData"]; //Get the image from flash file
                byte[] photo = Convert.FromBase64String(strPhoto);
                int number = getRandomID();

                string filename = Session["PicsName"].ToString() + number + ".jpg";
                Session["ImageNumber"] = number;
                FileStream fs = new FileStream(Server.MapPath("../Logo/EmployeePhoto/") + filename, FileMode.OpenOrCreate, FileAccess.Write);

                BinaryWriter br = new BinaryWriter(fs);
                br.Write(photo);
                br.Flush();
                br.Close();
                fs.Close();

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Generate Random Numbers
        public int getRandomID()
        {
            Random r = new Random();
            return r.Next(10000, 99999);
        }
        #endregion
    }
}
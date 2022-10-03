using System;
using System.Data;
using System.Web.UI.WebControls;
using GEIMS.Common;
using GEIMS.BL;
using GEIMS.BO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using log4net;
using System.Web.UI;
using GEIMS.DataAccess;

namespace GEIMS.Client.UI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    ApplicationResult objResult = new ApplicationResult();
                    StudentFeesTemplateTBL objStudentFeeTemplate = new StudentFeesTemplateTBL();

                    objResult = objStudentFeeTemplate.StudentFeeTemplate_FeesNameWise("1", "1", "A", "14-15",0);
                    if (objResult != null)
                    {
                        for (int i = 0; i < objResult.resultDT.Columns.Count; i++)
                        {

                            BoundField boundfield = new BoundField();
                            boundfield.DataField = objResult.resultDT.Columns[i].ColumnName.ToString();
                            boundfield.HeaderText = objResult.resultDT.Columns[i].ColumnName.ToString();
                            //boundfield.SortExpression = objResult.resultDT.Columns[i].ColumnName.ToString();
                            gvStudentFees.Columns.Add(boundfield);
                            if (i == 0)
                            {
                                boundfield.Visible = false;
                            }
                        }
                        gvStudentFees.Visible = true;
                        gvStudentFees.DataSource = objResult.resultDT;
                        gvStudentFees.DataBind();
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }

        protected void gvStudentFees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ApplicationResult objResult = new ApplicationResult();
                    StudentFeesTemplateTBL objStudentFeeTemplate = new StudentFeesTemplateTBL();
                    objResult = objStudentFeeTemplate.StudentFeeTemplate_FeesNameWise("1", "1", "A", "14-15", 0);
                    int intcCount = objResult.resultDT.Columns.Count;
                    for (int j = 2; j < intcCount; j++)
                    {
                        CheckBox chk = new CheckBox();
                        chk.ID = "chk_" + j;
                        //chk.Text = (e.Row.DataItem as DataRowView).Row["chk"].ToString();
                        e.Row.Cells[j].Controls.Add(chk);

                    }
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    ApplicationResult objResult = new ApplicationResult();
                    StudentFeesTemplateTBL objStudentFeeTemplate = new StudentFeesTemplateTBL();
                    objResult = objStudentFeeTemplate.StudentFeeTemplate_FeesNameWise("1", "1", "A", "14-15", 0);
                    int intcCount = objResult.resultDT.Columns.Count;
                    for (int j = 2; j < intcCount; j++)
                    {
                        TemplateField tfield = new TemplateField();
                        CheckBox chk = new CheckBox();
                        chk.ID = "chkHeader_" + j;
                        chk.Text = objResult.resultDT.Columns[j].ColumnName.ToString();
                        e.Row.Cells[j].Controls.Add(chk);
                        tfield = new TemplateField();
                        // tfield.HeaderText = objResult.resultDT.Columns[j].ColumnName.ToString();
                        gvStudentFees.Columns.Add(tfield);

                    }

                }
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}
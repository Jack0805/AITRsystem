using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITRsystem
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonGoSurvey_Click(object sender, EventArgs e)
        {
            Response.Redirect("question.aspx");
        }

        protected void ButtonGoSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("staffsearch.aspx");
        }
    }
}
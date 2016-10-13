using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data.SqlClient;
using System.Configuration;
using System.Data;



namespace AITRsystem
{
    public partial class question : System.Web.UI.Page
    {
        DataTable QuestionDT = new DataTable();
        DataTable AnswerDT = new DataTable();
        DataTable LeadDT = new DataTable();
        DataTable InsertDT = new DataTable();
        SqlConnection myConnection;
        static Dictionary<string, string> dictionary = new Dictionary<string, string>();
        static string checklistitem = "";
       

        public string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

          
            
                return context.Request.ServerVariables["REMOTE_ADDR"];
           
        }

        RadioButtonList RBL = new RadioButtonList();
       
        
        DropDownList DDL = new DropDownList();
        CheckBoxList CBL = new CheckBoxList();
        List<TextBox> Textboxlist = new List<TextBox>();

        protected void Page_Load(object sender, EventArgs e)
        {
           
     
         
            Session["Backto"] = "";
            //Button1.Enabled = true;
            if (checklistitem != "")
            {
                LabelAlert.Visible = true;
                LabelAlert.Text = checklistitem;
                checklistitem = "";
            }
            else
            {

                LabelAlert.Visible = false;
            }
     
                Session["QuestionType"] = "";
                Session["belongto"] = "";
                
                Session["empty"] = false;
                
                /*foreach (KeyValuePair<string, string> pair in dictionary)
                {
                    Response.Write("<b>" + pair.Key.ToString() + ":" + pair.Value.ToString() + "</b><br/>");
                }*/


                if (Session["Leadto"] == null || (int)Session["Leadto"] == 1)
                {
                    Session["Leadto"] = 1;
                    
                    //Button1.Enabled = false;

                    ((Button)form1.FindControl("Button1")).Attributes.Add("disabled","true");
                }

                
                SqlCommand myCommandForQuestion;
                SqlCommand myCommandForQuestionBody;
                SqlCommand myCommandInsertUpdateDelete;


                String myConnectionString = "Data Source=SQL5021.myWindowsHosting.com,1433;Initial Catalog=DB_9AB8B7_DDA5384;User Id=DB_9AB8B7_DDA5384_admin;Password=mtMx5NRa;MultipleActiveResultSets=True";

                myConnection = new SqlConnection();
                myConnection.ConnectionString = myConnectionString;

                myCommandForQuestion = new SqlCommand("SELECT * FROM table_questions WHERE QID = @QID", myConnection);
                myCommandForQuestion.Parameters.AddWithValue("@QID", Session["Leadto"]);

                myCommandForQuestionBody = new SqlCommand("SELECT * FROM table_questions_body WHERE Belongsto = @Belongsto", myConnection);
                myCommandForQuestionBody.Parameters.AddWithValue("@Belongsto", Session["Leadto"]);

                myConnection.Open();
                SqlDataReader myReaderForQuestion = myCommandForQuestion.ExecuteReader();
                SqlDataReader myReaderForQuestionBody = myCommandForQuestionBody.ExecuteReader();


                QuestionDT.Load(myReaderForQuestion);
                AnswerDT.Load(myReaderForQuestionBody);

                
                if (QuestionDT.Rows.Count > 0 && AnswerDT.Rows.Count > 0 && (int)Session["Leadto"] != 1001)
                {
                    
                    Session["Backto"] = QuestionDT.Rows[0]["Backto"];

                    QuestionsLabel.Text = QuestionDT.Rows[0]["Qcontent"].ToString();
                    
                    Session["QuestionType"] = QuestionDT.Rows[0]["Qtype"].ToString();
                    LabelEnd.Text = "";
                    if (Session["QuestionType"].Equals("radiobutton"))
                    {
                        RBL.ID = "radiolist";
                        RBL.Font.Size = 14;
                     

                        RequiredFieldValidator rv = new RequiredFieldValidator();

                        rv.ControlToValidate = RBL.ID;
                        rv.ErrorMessage = "Please select at least on option above";
                        rv.ForeColor = System.Drawing.Color.Red;
                        rv.Display = System.Web.UI.WebControls.ValidatorDisplay.None;

                        for (int i = 0; i < AnswerDT.Rows.Count; i++)
                        {
                            RBL.Items.Add(AnswerDT.Rows[i]["Bcontent"].ToString());
                            
                        }

                        PlaceHolder.Controls.Add(RBL);
                        PlaceHolder1.Controls.Add(rv);
                    
                    }

                    else if (Session["QuestionType"].Equals("dropdownlist"))
                    {

                        DDL.ID = "dropdown";
                        //add bootstrap styles
                        DDL.Attributes.Add("class","form-control input-lg");
                        PlaceHolder.Controls.Add(DDL);
                        for (int i = 0; i < AnswerDT.Rows.Count; i++)
                        {
                            DDL.Items.Add(AnswerDT.Rows[i]["Bcontent"].ToString());
                        }
                    }

                    else if (Session["QuestionType"].Equals("textbox"))
                    {
      

                   
                        
                        for (int i = 0; i < AnswerDT.Rows.Count; i++)
                        {

                            RequiredFieldValidator rv = new RequiredFieldValidator();
                            RegularExpressionValidator rev = new RegularExpressionValidator();
                            
                            TextBox tbx = new TextBox();
                            rv.Display = System.Web.UI.WebControls.ValidatorDisplay.None;
                            rev.Display = System.Web.UI.WebControls.ValidatorDisplay.None;
                            tbx.ID = "aTextBox" + i.ToString();
                            rv.ControlToValidate = tbx.ID;
                            rev.ControlToValidate = tbx.ID;
                            rv.ErrorMessage = AnswerDT.Rows[i]["Bcontent"].ToString() + " is empty";
                            rev.ErrorMessage = AnswerDT.Rows[i]["Bcontent"].ToString() + " format is incorrect";
                            rev.ValidationExpression = AnswerDT.Rows[i]["RegExp"].ToString();
                            Textboxlist.Add(tbx);
                            PlaceHolder.Controls.Add(tbx);
                            PlaceHolder1.Controls.Add(rv);
                            PlaceHolder1.Controls.Add(rev);
                            ((TextBox)form1.FindControl("aTextBox" + i.ToString())).Attributes.Add("placeholder", AnswerDT.Rows[i]["Bcontent"].ToString());
                            ((TextBox)form1.FindControl("aTextBox" + i.ToString())).Attributes.Add("class", "form-control");
                            ((TextBox)form1.FindControl("aTextBox" + i.ToString())).Attributes.Add("style", "margin-bottom: 10px;");

                        }
                       
                    }

                    else if (Session["QuestionType"].Equals("checkbox"))
                    {
                  
                        PlaceHolder.Controls.Add(CBL);
                        for (int i = 0; i < AnswerDT.Rows.Count; i++)
                        {
                            CBL.Items.Add(AnswerDT.Rows[i]["Bcontent"].ToString());
                        }
                    }

                }

                else
                {
                    Session["DateNow"] = DateTime.Now.ToString();
 
                    NextButton.Text = "Do it again";
           
                    // regDate contains default value of respondents, which is "Anonymous".
                    string[] regData = { "Anonymous", "Anonymous", "Anonymous","Anonymous"};
                   
               
                
                    //insert respondent info to database, check if user is registered or not, if yes, give values. If not, just keep regData array like default above;
                    foreach (KeyValuePair<string, string> pairs in dictionary)
                    {
                        string tmp;
                        string[] items = {};
                        string key = pairs.Key;

                        if (dictionary.TryGetValue(key, out tmp))
                        {
                            items = tmp.Split(',');
                        }

                        if (key.Equals("12"))
                        {
                             //isReg = true;
                             regData[0] = items[0];
                             regData[1] = items[1];
                             regData[2] = items[2];
                             regData[3] = items[3];
                        }                   

                    }

                    //insert respondent info to database
                        myCommandInsertUpdateDelete = new SqlCommand("INSERT INTO Respondent (Fname, Lname, Birth, Phone, IP, Rdate) VALUES (@Fname, @Lname, @Birth, @Phone, @IP, @Rdate)", myConnection);
                        myCommandInsertUpdateDelete.Parameters.AddWithValue("@Fname", regData[0]);
                        myCommandInsertUpdateDelete.Parameters.AddWithValue("@Lname", regData[1]);
                        myCommandInsertUpdateDelete.Parameters.AddWithValue("@Birth", regData[2]);
                        myCommandInsertUpdateDelete.Parameters.AddWithValue("@Phone", regData[3]);
                        myCommandInsertUpdateDelete.Parameters.AddWithValue("@IP", GetIPAddress());
                        myCommandInsertUpdateDelete.Parameters.AddWithValue("@Rdate", Session["DateNow"]);
                        myCommandInsertUpdateDelete.ExecuteNonQuery();


                        myCommandInsertUpdateDelete = new SqlCommand("SELECT * FROM Respondent WHERE IP = @IP AND Rdate = @Rdate", myConnection);
                        myCommandInsertUpdateDelete.Parameters.AddWithValue("@IP", GetIPAddress());
                        myCommandInsertUpdateDelete.Parameters.AddWithValue("@Rdate", Session["DateNow"]);
                        SqlDataReader myReaderForUserID = myCommandInsertUpdateDelete.ExecuteReader();
                     
                        DataTable UserDT = new DataTable();
                        UserDT.Load(myReaderForUserID); // Reuse this datatable to get the ID of this user;
                        string RID;
                        RID = UserDT.Rows[0]["RID"].ToString();

                    // this time, insert answers to the answers table 
                        foreach (KeyValuePair<string, string> pairs in dictionary)
                        {
                            string tmp;
                            string[] items = { };
                            string key = pairs.Key;

                            if (dictionary.TryGetValue(key, out tmp) && !key.Equals("12"))
                            {
                                items = tmp.Split(',');

                                for (int howmany = 0; howmany < items.Count(); howmany++)
                                {
                                    myCommandInsertUpdateDelete = new SqlCommand("INSERT INTO RespondentAnswers (RID, QID, Answers) VALUES (@RID, @QID, @Answers)", myConnection);
                                    myCommandInsertUpdateDelete.Parameters.AddWithValue("@RID", RID);
                                    myCommandInsertUpdateDelete.Parameters.AddWithValue("@QID", key);
                                    myCommandInsertUpdateDelete.Parameters.AddWithValue("@Answers", items[howmany]);

                                    myCommandInsertUpdateDelete.ExecuteNonQuery();

                                }
                            }
                        }
                    
                    
                    LabelEnd.Text = "Thank you for attending our suvery! :)";

                    Session["Leadto"] = null;
                    Button1.Visible = false;
                    dictionary.Clear();

                }

                myConnection.Close();

        }

        protected void NextButton_Click(object sender, EventArgs e)
        {
          
                // Run code now that validation has been verified.

                //SqlConnection myConnection;
                SqlCommand myCommandForLeadto;
                String myConnectionString = "Data Source=SQL5021.myWindowsHosting.com,1433;Initial Catalog=DB_9AB8B7_DDA5384;User Id=DB_9AB8B7_DDA5384_admin;Password=mtMx5NRa;MultipleActiveResultSets=True";

                myConnection = new SqlConnection();
                myConnection.ConnectionString = myConnectionString;
          

                if (Session["QuestionType"].Equals("radiobutton"))
                {
                    foreach (ListItem myList in RBL.Items)
                    {
                        if (myList.Selected)
                        {
                            myCommandForLeadto = new SqlCommand("SELECT * FROM table_questions_body WHERE Bcontent = @Bcontent", myConnection);
                            myCommandForLeadto.Parameters.AddWithValue("@Bcontent", myList.Text);
                            myConnection.Open();
                            SqlDataReader myReaderForLeadto = myCommandForLeadto.ExecuteReader();
                            LeadDT.Load(myReaderForLeadto);

                            Session["Leadto"] = LeadDT.Rows[0]["Leadsto"];
                            Session["belongto"] = LeadDT.Rows[0]["Belongsto"];
                            dictionary.Add(Session["belongto"].ToString(), myList.Text);
      

                            myConnection.Close();
                        }
                    }
                }

                else if (Session["QuestionType"].Equals("dropdownlist"))
                {
                    foreach (ListItem myList in DDL.Items)
                    {
                        if (myList.Selected)
                        {
                            myCommandForLeadto = new SqlCommand("SELECT * FROM table_questions_body WHERE Bcontent = @Bcontent", myConnection);
                            myCommandForLeadto.Parameters.AddWithValue("@Bcontent", myList.Text);
                            myConnection.Open();
                            SqlDataReader myReaderForLeadto = myCommandForLeadto.ExecuteReader();
                            LeadDT.Load(myReaderForLeadto);

                            Session["Leadto"] = LeadDT.Rows[0]["Leadsto"];
                            Session["belongto"] = LeadDT.Rows[0]["Belongsto"];
                            dictionary.Add(Session["belongto"].ToString(), myList.Text);
                            myConnection.Close();
                        }
                    }
                }

                else if (Session["QuestionType"].Equals("textbox"))
                {
      

                    for (int i = 0; i < AnswerDT.Rows.Count; i++)
                    {
                        if (dictionary.ContainsKey(QuestionDT.Rows[0]["QID"].ToString()))
                        {
                            dictionary[QuestionDT.Rows[0]["QID"].ToString()] = dictionary[QuestionDT.Rows[0]["QID"].ToString()] + "," + Textboxlist[i].Text;
                        }

                        else
                        {

                            dictionary.Add(QuestionDT.Rows[0]["QID"].ToString(), Textboxlist[i].Text);
                        }

                    }

                    myCommandForLeadto = new SqlCommand("SELECT * FROM table_questions_body WHERE Bcontent = @Bcontent", myConnection);
                    myCommandForLeadto.Parameters.AddWithValue("@Bcontent", AnswerDT.Rows[0]["Bcontent"].ToString());
                    myConnection.Open();
                    SqlDataReader myReaderForLeadto = myCommandForLeadto.ExecuteReader();
                    LeadDT.Load(myReaderForLeadto);

                    Session["Leadto"] = LeadDT.Rows[0]["Leadsto"];
                    Session["belongto"] = LeadDT.Rows[0]["Belongsto"];
                }

                else if (Session["QuestionType"].Equals("checkbox"))
                {
                    // in our data table, there is a column that indicates the maximum selection for different checkbox questions
                    Session["counter"] = QuestionDT.Rows[0]["Limit"];



                    if (CBL.Items.Count > 1)
                    {
                        Session["ActualCounter"] = 0;
                        List<int> leadtonumber = new List<int> { };
                        List<string> item = new List<string> { };

                        for (int counter = 0; counter < CBL.Items.Count; counter++)
                        {

                            if (CBL.Items[counter].Selected)
                            {
                                Session["ActualCounter"] = (int)Session["ActualCounter"] + 1;
                                myCommandForLeadto = new SqlCommand("SELECT * FROM table_questions_body WHERE Bcontent = @Bcontent", myConnection);
                                myCommandForLeadto.Parameters.AddWithValue("@Bcontent", CBL.Items[counter].Text);
                                myConnection.Open();
                                SqlDataReader myReaderForLeadto = myCommandForLeadto.ExecuteReader();
                                LeadDT.Load(myReaderForLeadto);

                                leadtonumber.Add((int)LeadDT.Rows[0]["Leadsto"]);

                                item.Add(CBL.Items[counter].Text);

                                myConnection.Close();
                            }
                        }

                        if ((int)Session["ActualCounter"] > (int)Session["counter"])
                        {

                            string display = "Please select no more than " + Session["counter"] + " options, try again!";
                            checklistitem = display;
                        }
                        else if ((int)Session["ActualCounter"] == 0 && (int)Session["Leadto"] != 7)
                        {
                            string display = "Please select at least 1 option above";
                            checklistitem = display;
                        }
                        else
                        {
                            if (item.Count() < 1)
                            {
                                item.Add("No cars");
                                leadtonumber.Add(8);
                                Session["belongto"] = 7;
                            }
                            else
                            {
                                Session["belongto"] = LeadDT.Rows[0]["Belongsto"];
                            }
                            LabelAlert.Visible = false;
                            Session["Leadto"] = leadtonumber.Max();
                          
                            string mystring = string.Join(",", item.ToArray());
                            dictionary.Add(Session["belongto"].ToString(), mystring);
                        }
                    }
                }




                myConnection.Close();
                Response.Redirect("question.aspx");
                

            
        }

        // this button is the back button. Inside back button function, it will define how to go back to the previous question ID;
        protected void NextButton_Click1(object sender, EventArgs e)
        {
       

            
            if (dictionary.Keys.Last().Equals("1000"))
            {
                Session["Backto"] = 1000;
            }

            else if (dictionary.Keys.Last().Equals("10"))
            {
                Session["Backto"] = 10;
            }


            dictionary.Remove(Session["Leadto"].ToString());
            
            Session["Leadto"] = Session["Backto"];
            dictionary.Remove(Session["Leadto"].ToString());
            Response.Redirect("question.aspx");
            myConnection.Close();
        }
    
    }
}
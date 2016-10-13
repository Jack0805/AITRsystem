using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AITRsystem
{
    public partial class staffsearch : System.Web.UI.Page
    {
        String[] search = new String[14];
        SqlConnection myConnection;
        //string myCommandForSearchPart = "SELECT RID FROM RespondentAnswers WHERE Answers IN (@answers) group by RID having count(*) = @count";
        //string tempcheckanswer = "";
        string tempIDanswer = "";
        string mainQuery = "SELECT * FROM Respondent";
        static string tempInitialQuery = "";

        //static string testquery = "";

        DataTable searchID = new DataTable();
        int[] myint = new int[15];


        string[] AgeRange = { "Any", "18-25", "26-35", "Over 35" };
        string[] State = { "Any", "New South Wales", "Western Australia", "Queensland", "South Australia", "Tasmania", "Victoria" };
        string[] Gender = { "Male", "Female" };
        string[] Car = { "Benz", "Toyota", "Mazda", "Honda", "BMW", "Jeep", "No cars" };
        string[] Banks = { "ANZ", "Wespac", "Commonwealth", "St.Geroge", "ABC" };
        string[] BankService = { "Internet Banking", "Home Loan", "Credit card", "Share investment" };
        string[] ISP = { "TPG", "Optus", "Vodafone", "VxNet" };
        string[] ISPService = { "Fetch TV", "Netflix", "FoxTel" };
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            SqlCommand myCommand;
            //SqlCommand mysearchCommand;
            
            String myConnectionString = "Data Source=SQL5021.myWindowsHosting.com,1433;Initial Catalog=DB_9AB8B7_DDA5384;User Id=DB_9AB8B7_DDA5384_admin;Password=mtMx5NRa;MultipleActiveResultSets=True";
            for (int i = 0; i < 14; i++)
            {
                search[i] = "";
            }

            myConnection = new SqlConnection();
            myConnection.ConnectionString = myConnectionString;

            myConnection.Open();

            //for displaying searching critirias

            /*myCommandForSearch = new SqlCommand("SELECT * FROM table_questions_body", myConnection);

            SqlDataReader myReaderForSearch = myCommandForSearch.ExecuteReader();

            DataTable SearchDT = new DataTable();

            SearchDT.Load(myReaderForSearch);*/



                ((TextBox)form1.FindControl("TextBoxID")).Attributes.Add("placeholder", "Respondent ID");

                ((TextBox)form1.FindControl("TextBoxFname")).Attributes.Add("placeholder", "Respondent First Name");

                ((TextBox)form1.FindControl("TextBoxLname")).Attributes.Add("placeholder", "Respondent Last Name");

                ((TextBox)form1.FindControl("TextBoxPhone")).Attributes.Add("placeholder", "Respondent Phone");

                ((TextBox)form1.FindControl("TextBoxBirth")).Attributes.Add("placeholder", "Respondent Birthday");

                ((TextBox)form1.FindControl("TextBoxHomeSuburbOrPostcode")).Attributes.Add("placeholder", "Home Suburb or Postcode");

                ((TextBox)form1.FindControl("TextBoxWorkingSuburbOrPostcode")).Attributes.Add("placeholder", "Working Suburb or Postcode");

                ((TextBox)form1.FindControl("TextBoxEmail")).Attributes.Add("placeholder", "Email");


                if (!IsPostBack)
                {
                    for (int i = 0; i < AgeRange.Count(); i++)
                    {

                        DropDownListAgeRange.Items.Add(AgeRange[i]);
                    }

                    for (int i = 0; i < State.Count(); i++)
                    {

                        DropDownListSate.Items.Add(State[i]);
                    }

                    for (int i = 0; i < Gender.Count(); i++)
                    {

                        RadioButtonListGender.Items.Add(Gender[i]);
                    }

                    for (int i = 0; i < Car.Count(); i++)
                    {

                        CheckBoxListCar.Items.Add(Car[i]);
                    }

                    for (int i = 0; i < Banks.Count(); i++)
                    {

                        CheckBoxListBank.Items.Add(Banks[i]);
                    }

                    for (int i = 0; i < BankService.Count(); i++)
                    {

                        CheckBoxListBackservice.Items.Add(BankService[i]);
                    }

                    for (int i = 0; i < ISP.Count(); i++)
                    {

                        RadioButtonListisp.Items.Add(ISP[i]);
                    }

                    for (int i = 0; i < ISPService.Count(); i++)
                    {

                        CheckBoxListISPService.Items.Add(ISPService[i]);
                    }

                }

                //CheckBoxListBackservice.Enabled = false;




            
            // connection for reading the data from respondent data table.


                //myCommand = new SqlCommand("SELECT * FROM Respondent WHERE RID IN(12,13)", myConnection);

                mainQuery = mainQuery + tempInitialQuery;
                myCommand = new SqlCommand(mainQuery, myConnection);

                //Label1.Text = mainQuery;// this label used to show the query, so that it will be eaiser to see if the auto generate query is correct

                Label1.Text = "";

            SqlDataReader myReaderForAllRespondents = myCommand.ExecuteReader();
            DataTable respondentDT = new DataTable();

            respondentDT.Columns.Add("RID", System.Type.GetType("System.String"));
            respondentDT.Columns.Add("First Name", System.Type.GetType("System.String"));
            respondentDT.Columns.Add("Last Name", System.Type.GetType("System.String"));
            respondentDT.Columns.Add("Birth date", System.Type.GetType("System.String"));
            respondentDT.Columns.Add("Phone number", System.Type.GetType("System.String"));
            respondentDT.Columns.Add("IP", System.Type.GetType("System.String"));
            respondentDT.Columns.Add("Attended Datetime", System.Type.GetType("System.String"));

            //respondentDT.Load(myReaderForAllRespondents);

            DataRow AllrespondentRow;

            while (myReaderForAllRespondents.Read())
            {
                AllrespondentRow = respondentDT.NewRow();
                AllrespondentRow["RID"] = myReaderForAllRespondents["RID"].ToString();
                AllrespondentRow["First Name"] = myReaderForAllRespondents["Fname"].ToString();
                AllrespondentRow["Last Name"] = myReaderForAllRespondents["Lname"].ToString();
                AllrespondentRow["Birth date"] = myReaderForAllRespondents["Birth"].ToString();
                AllrespondentRow["Phone number"] = myReaderForAllRespondents["Phone"].ToString();
                AllrespondentRow["IP"] = myReaderForAllRespondents["IP"].ToString();
                AllrespondentRow["Attended Datetime"] = myReaderForAllRespondents["Rdate"].ToString();

                respondentDT.Rows.Add(AllrespondentRow);
            }
            if (respondentDT.Rows.Count == 0 || tempInitialQuery == " WHERE Rdate IS NOT NULL")
            {
                Label1.Text = "Your searching pattern mathes no results, please try again!";
            }

            else
            {
                GridView1.DataSource = respondentDT;
                GridView1.DataBind();
            }
            tempInitialQuery = ""; // empty this static string
            myConnection.Close();

        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {

            //int count = 0;
            //int anotherCount = 0;
            //string tempcheck = "";
            var nameList = new List<String> { };
            string[] checkboxanswer = new string[4];
       
            if (TextBoxID.Text != "")
            {
                search[0] = " AND RID = " + @"'" + TextBoxID.Text + @"'";
            }

            if (TextBoxFname.Text != "")
            {
                search[1] = " AND Fname = " + @"'" + TextBoxFname.Text + @"'";
            }

            if (TextBoxLname.Text != "")
            {
                search[2] = " AND Lname = " + @"'" + TextBoxLname.Text + @"'";

            }

            if (TextBoxPhone.Text != "")
            {
                search[3] = " AND Phone  = " + @"'" + TextBoxPhone.Text + @"'";

            }

            if (TextBoxBirth.Text != "")
            {
                search[4] = " AND Birth  = " + @"'" + TextBoxBirth.Text + @"'";
            }


            if (TextBoxHomeSuburbOrPostcode.Text != "")
            {

                nameList.Add(TextBoxHomeSuburbOrPostcode.Text);
            }

            if (TextBoxWorkingSuburbOrPostcode.Text != "")
            {

                nameList.Add(TextBoxWorkingSuburbOrPostcode.Text);
            }

            if (TextBoxEmail.Text != "")
            {

                nameList.Add(TextBoxEmail.Text);
            }
           

           
            foreach (ListItem item in DropDownListAgeRange.Items)
            {
                if (item.Selected)
                {
                    if (item.Text != "Any")
                    {

                        nameList.Add(item.Text);
                    }
                }
            }

            foreach (ListItem item in DropDownListSate.Items)
            {
                if (item.Selected)
                {
                    if (item.Text != "Any")
                    {
  
                        nameList.Add(item.Text);
                    }
                }
            }

            foreach (ListItem item in RadioButtonListGender.Items)
            {
                if (item.Selected)
                {

                    nameList.Add(item.Text);
                }
            }
               
          
            foreach (ListItem item in CheckBoxListBank.Items)
            {
                if (item.Selected)
                {
 
          

                    nameList.Add(item.Text);
               
                }

            }

            foreach (ListItem item in CheckBoxListCar.Items)
            {
                if (item.Selected)
                {

                

                    nameList.Add(item.Text);

                }

            }

            foreach (ListItem item in CheckBoxListBackservice.Items)
            {
                if (item.Selected)
                {

                 

                    nameList.Add(item.Text);

                }

            }


            foreach (ListItem item in RadioButtonListisp.Items)
            {
                if (item.Selected)
                {

        

                    nameList.Add(item.Text);

                }

            }

            foreach (ListItem item in CheckBoxListISPService.Items)
            {
                if (item.Selected)
                {

                   

                    nameList.Add(item.Text);

                }

            }

 
            
          
            myConnection.Open();
            SqlCommand myCommandForSearch;
            var sql = "SELECT RID FROM RespondentAnswers WHERE Answers IN ({0}) Group by RID Having count(*) = @count";
            myCommandForSearch = new SqlCommand(sql,myConnection);
            //myCommandForSearch = new SqlCommand("SELECT RID FROM RespondentAnswers WHERE Answers IN ('Male') Group by RID Having count(*) = 1", myConnection);
            
            var nameParameter = new List<string>();
            var index = 0;
            foreach (var name in nameList)
            {
                var paramName = "@nameParam" + index;
                myCommandForSearch.Parameters.AddWithValue(paramName, name);
                nameParameter.Add(paramName);
                index++;
            }
            myCommandForSearch.CommandText = String.Format(sql,string.Join(",", nameParameter));

            
            

            //myCommandForSearch.Parameters.AddWithValue("@answers", "'Male'");
            myCommandForSearch.Parameters.AddWithValue("@count", nameList.Count);
            //testquery = myCommandForSearchPart;

            if (nameList.Count > 0)
            {
                SqlDataReader myReaderForSearch = myCommandForSearch.ExecuteReader();
                searchID.Load(myReaderForSearch);
            }

            else
            {
                if (TextBoxID.Text != "")
                {
                    tempIDanswer = TextBoxID.Text;
                }
            }

            if (searchID.Rows.Count > 0 && TextBoxID.Text == "" && TextBoxFname.Text == "" && TextBoxLname.Text == "" && TextBoxPhone.Text == "" && TextBoxBirth.Text == "") // dont have to search by using the data in respondent table
            {
                for (int c = 0; c < searchID.Rows.Count; c++)
                {
                    myint[c] = (int)searchID.Rows[c]["RID"];
                    tempIDanswer = tempIDanswer + (int)searchID.Rows[c]["RID"] + ',';
                }

                tempIDanswer = tempIDanswer.Remove(tempIDanswer.Length - 1);
                tempInitialQuery = " WHERE RID IN (" + tempIDanswer + ")";
            }

            else // if need to use data both from respondent table and respondentAnswers table, or could be using a single join query to do this
            {
                /*for (int c = 0; c < searchID.Rows.Count; c++)
                {
                    myint[c] = (int)searchID.Rows[c]["RID"];

                    if (myint[c] == Convert.ToInt32(TextBoxID.Text))
                    {
                        tempIDanswer = TextBoxID.Text;
                        c = searchID.Rows.Count - 1;
                    }
                    else
                    {
                        tempIDanswer = tempIDanswer + (int)searchID.Rows[c]["RID"] + ',';
                    }
                }*/

                if (TextBoxID.Text == "")
                {
                    //tempIDanswer = tempIDanswer.Remove(tempIDanswer.Length - 1);
                    tempInitialQuery = " WHERE Rdate IS NOT NULL" + search[1] + search[2] + search[3] + search[4];
                }
                else
                {
                    tempInitialQuery = " WHERE RID IN (" + tempIDanswer + ")" + search[1] + search[2] + search[3] + search[4];
                }
            }

            //Session["isClicked"] = true;

            Response.Redirect("staffsearch.aspx");
        }

        protected void ButtonShowall_Click(object sender, EventArgs e)
        {
            Response.Redirect("staffsearch.aspx");
        }
    }
}
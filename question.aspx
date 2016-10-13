<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="question.aspx.cs" Inherits="AITRsystem.question" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Survey</title>
    <link href="css/bootstrap.min.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            
            <div class="col-md-6 col-md-offset-3">
                
                <div class="page-header">
                    <div class="col-md-6 col-md-offset-3">
                    <a href="index.aspx"><img src="AIT_LOGO_horizontal_RGB.jpg" alt="" style="margin-bottom:20px;"/></a>
                    
                    </div>
                    <h1>Welcome to AIT Survey System</h1>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-6 col-md-offset-3">
                    <div class="panel panel-default">
                        <div class="panel-heading"><asp:Label ID="QuestionsLabel" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"></asp:Label></div>
                            <div class="panel-body">    
                                <asp:PlaceHolder ID="PlaceHolder" runat="server"></asp:PlaceHolder>   
                               
    
                                <asp:TextBox ID="tytty" runat="server" Visible="false"></asp:TextBox>
                                <asp:Label ID="LabelEnd" runat="server" Text="Label"></asp:Label>


                                

                            </div>
                        </div>
                    </div>
                </div>

                
                <div class="col-md-6 col-md-offset-3">

                <div class="panel panel-default">
                    <div class="col-md-6 col-md-offset-3">
                    <div class="panel-body">
                        
                                
                                
                                <asp:Button ID="Button1" runat="server" Text="Back" onclick="NextButton_Click1" CausesValidation="False" type="button" class="btn btn-default btn-lg"/>
                                <asp:Button ID="NextButton" runat="server" Text="Next" onclick="NextButton_Click" type="button" class="btn btn-primary btn-lg"/>
                     </div>
                    </div>
                </div>
                <div class="col-md-6 col-md-offset-3">
                <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="BulletList" ShowSummary="true" ForeColor="Red"/>
                <asp:Label ID="LabelAlert" runat="server" ForeColor="Red" Text="alertTrigger"></asp:Label>
                </asp:PlaceHolder>

                </div>
               </div>

            </div>
    </form>
  

 
   <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js" type="text/javascript"></script>
   <script src="js/bootstrap.min.js" type="text/javascript"></script> 
</body>
</html>

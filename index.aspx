<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="AITRsystem.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome</title>
    <link href="css/bootstrap.min.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
                <div class="col-md-6 col-md-offset-3">
                
                <div class="page-header">
                    <div class="col-md-6 col-md-offset-3">
                    <img src="AIT_LOGO_horizontal_RGB.jpg" alt="" style="margin-bottom:20px;"/>
                    
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-md-offset-3">
            <asp:Button ID="ButtonGoSurvey" runat="server" Text="Doing Survey now" onclick="ButtonGoSurvey_Click" type="button" class="btn btn-primary btn-lg btn-block" style="margin-bottom:15px;"/>
            <asp:Button ID="ButtonGoSearch" runat="server" Text="Go to searching page" onclick="ButtonGoSearch_Click" type="button" class="btn btn-danger btn-lg btn-block" style="margin-bottom:15px;"/>
            </div>
    </div>
    </form>
</body>
</html>

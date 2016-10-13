<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="staffsearch.aspx.cs" Inherits="AITRsystem.staffsearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Searching</title>
    <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <script src="js/jquery-3.1.1.js" type="text/javascript"></script><!-- using Jquery to do some advanced validation for banks and isp -->
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="page-header">
                    
                    <a href="index.aspx"><img src="AIT_LOGO_horizontal_RGB.jpg" alt=""/></a>
                    
                    
                    <h1>Welcome to Adminin Searching Panel</h1>
                </div>
            <div class="col-md-6 col-md-offset-3">
            <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red" style="margin-bottom:10px;" Font-Size="X-Large"></asp:Label> <!-- for query string test use, will be deleted later -->
             </div>
            <asp:GridView ID="GridView1" runat="server" class="table table-striped"></asp:GridView>
            <div class="col-md-6 col-md-offset-3">
            <asp:Button ID="ButtonShowall" runat="server" Text="Show all" onclick="ButtonShowall_Click" type="button" class="btn btn-primary btn-lg btn-block" style="margin-bottom:15px;"/>
            <asp:Button ID="ButtonSearch" runat="server" Text="Search results" onclick="ButtonSearch_Click" type="button" class="btn btn-danger btn-lg btn-block" style="margin-bottom:15px;"/>
            </div>

                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">Respondent basic info</div>
                                <div class="panel-body">  
                                    <asp:TextBox ID="TextBoxID" runat="server" placeholder="place" style="margin-bottom:10px;" class="form-control"></asp:TextBox>
        
                                    <asp:TextBox ID="TextBoxFname" runat="server" placeholder="place" style="margin-bottom:10px;" class="form-control"></asp:TextBox>

                                    <asp:TextBox ID="TextBoxLname" runat="server" placeholder="place" style="margin-bottom:10px;" class="form-control"></asp:TextBox>

                                    <asp:TextBox ID="TextBoxBirth" runat="server" placeholder="place" style="margin-bottom:10px;" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                     </div>

                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">Age,State and gender</div>
                                <div class="panel-body"> 
                                    <asp:DropDownList ID="DropDownListAgeRange" runat="server" class="form-control input-md" style="margin-bottom:10px;"></asp:DropDownList>

                                    <asp:DropDownList ID="DropDownListSate" runat="server" class="form-control input-md" style="margin-bottom:10px;"></asp:DropDownList>
                                   
                                    <hr />
                                    <asp:RadioButtonList ID="RadioButtonListGender" runat="server" style="margin-bottom:10px;"></asp:RadioButtonList>
                                  
                                    
                                </div>
                            </div>
                        </div>


                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">Suburbs,postcode and email</div>
                                <div class="panel-body"> 

                                    <asp:TextBox ID="TextBoxHomeSuburbOrPostcode" runat="server" placeholder="place" class="form-control" style="margin-bottom:10px;"></asp:TextBox>

                                    <asp:TextBox ID="TextBoxWorkingSuburbOrPostcode" runat="server" placeholder="place" class="form-control" style="margin-bottom:10px;"></asp:TextBox>

                                    <asp:TextBox ID="TextBoxEmail" runat="server" placeholder="place" class="form-control" style="margin-bottom:10px;"></asp:TextBox>

                                     <asp:TextBox ID="TextBoxPhone" runat="server" placeholder="place" style="margin-bottom:10px;" class="form-control"></asp:TextBox>
                                </div>
                           </div>
                      </div>


                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">Car use (No more than 2 selections)</div>
                                <div class="panel-body"> 
                                    <asp:CheckBoxList ID="CheckBoxListCar" runat="server"></asp:CheckBoxList>
                              </div>
                           </div>
                      </div>

                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">Bank use (No more than 4 options)</div>
                                <div class="panel-body">
                                    <asp:CheckBoxList ID="CheckBoxListBank" runat="server"></asp:CheckBoxList>
                                </div>
                           </div>
                      </div>

                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">Bank Services use</div>
                                <div class="panel-body">
                                    <asp:CheckBoxList ID="CheckBoxListBackservice" runat="server" style="margin-bottom:30px;"></asp:CheckBoxList>
                                </div>
                          </div>
                     </div>

                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">ISP</div>
                                <div class="panel-body">
                                   
                                    <asp:RadioButtonList ID="RadioButtonListisp" runat="server">
                                    </asp:RadioButtonList>
                                </div>
                          </div>
                     </div>

                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">ISP services</div>
                                <div class="panel-body">
                                    <asp:CheckBoxList ID="CheckBoxListISPService" runat="server"></asp:CheckBoxList>
                                </div>
                          </div>
                     </div>
   
    </div>
    </form>

<script type="text/javascript"> // using some jquery to do advanced validation work, such as enable the bank services and isp services checkbox list depending on the bank and isp checkbox.
    $(function () {

        var str = "Wespac";
        var strr = "ANZ";
        var strrr = "Commonwealth";
        var nocar = "No cars";
        var isp = "Optus";
        var list = $('#<%= CheckBoxListBank.ClientID%> input');
        var anlist = $('#<%= CheckBoxListBackservice.ClientID%> input');
        var carlist = $('#<%= CheckBoxListCar.ClientID%> input');
        var isplist = $('#<%= RadioButtonListisp.ClientID%> input');
        var ispservice = $('#<%= CheckBoxListISPService.ClientID%> input');
        var count = 0;
        var countt = 0;


        isplist.each(function (index) {  // here, only Optus is selected, the next checkbox list, which is ISP services will be enabled, otherwise it will be disabled.
            
                ispservice.each(function (index) {

                        $(this).attr('disabled', 'disabled');

                });
            $(this).click(function () {

                var isChecked = $(this).is(":checked");

                if (isChecked) {

                    if ($(this).val() == isp) {

                        ispservice.each(function (index) {

                            $(this).prop("disabled", false);

                        });
                    }

                    else {

                        ispservice.each(function (index) {

                            $(this).attr('disabled', 'disabled');

                        });

                    }

                }
            });
        });





        carlist.each(function (index) { // in here, if user choose no cars, then other options will be disabled.

            $(this).click(function () {

                if (countt == 2) {  // to validate if users choose more than 4 options, and make an alert.

                    alert("Maximum 2");

                }

                var isChecked = $(this).is(":checked");

                if (isChecked) {
                    countt++;
                    if ($(this).val() == nocar) {

                        //alert($(this).val());

                        carlist.each(function (index) {

                            if ($(this).val() != nocar) {
                                $(this).attr('disabled', 'disabled');
                                $(this).prop("checked", false);
                            }
                        });

                    }
                }

                else {
                    countt--;
                    carlist.each(function (index) {

                        $(this).prop("disabled", false);

                    });

                }



            });
        });













        list.each(function (index) { // here, we determin whether to enable or disable BANK SERVICE options, if Wespac, or CommonWealth or ANZ is checked, we enabled the bank service. Otherwise, disable it.
            item = $(this);
            $(this).click(function () {

                if (count == 4) {  // to validate if users choose more than 4 options, and make an alert.

                    alert("Maximim 4");

                }

                var isChecked = $(this).is(":checked");

                if (isChecked) {


                    count++;
                    if ($(this).val() == str || $(this).val() == strr || $(this).val() == strrr) {
                        anlist.each(function (index) {
                            item = $(this);
                            //item.attr('disabled', 'disabled');
                            item.prop("disabled", false);

                        });
                    }

                }

                else {
                    count--;
                    if (count == 0 || $(this).val() == str || $(this).val() == strr || $(this).val() == strrr) {
                        anlist.each(function (index) {
                            item = $(this);
                            item.attr('disabled', 'disabled');
                            //item.prop("disabled", false);

                        });
                    }
                }
            });

            anlist.each(function (index) {
                item = $(this);
                item.attr('disabled', 'disabled');

            });

        });

    });

</script>

</body>
</html>

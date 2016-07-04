<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsuranceCalcForm.aspx.cs" Inherits="RelayAppInsuranceCalculator.InsuranceCalcForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="RelayAppForm.css"/>
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.css" />
</head>
<body style="font-family:Verdana, Arial;">
    <h1 style="text-align:center;">Motor Insurance Calculator</h1>
    <p style="margin:0px 0px 0px 250px; font-style:italic;">
    Possible reasons for Decline of Policy: (User will be notified if form submitted breaks any of the rules) <br />

    1. If the start date of the policy is before today. <br />
    2. If the youngest driver is under the age of 21 at the start date of the policy. <br />
    3. If the oldest driver is over the age of 75 at the start date of the policy. <br />
    4. If a driver has more than 2 claims. <br />
    5. If the total number of claims exceeds 3. <br />

    </p>
    <form id="form1" runat="server">
        <div>

            <br />
            <div style="border: 4px groove grey; max-width: 350px; margin: auto;">
                <h2>Start Date Of The Policy</h2>
                <asp:Label ID="CalendarLabel" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Calendar ID="Calendar1" runat="server" SelectedDate="2016-05-30" OnSelectionChanged="Calendar1_SelectionChanged" 
                    BackColor="White" BorderColor="#3366CC"  CellPadding="1" DayNameFormat="Shortest"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" 
                    Width="250px" Style="margin:0px 0px 0px 50px;">
                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" 
                    BorderWidth="1px" Font-Bold="True"
                    Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                </asp:Calendar>
            </div>
            <br />
            <div style="border: 4px groove grey; max-width: 700px; margin: auto;">
                <h2>Add Driver(s)</h2>
                <asp:Label ID="NameLabel" runat="server" Text="Name : " Width="200px"></asp:Label>
                <asp:TextBox ID="NameTextBox" runat="server" Width="150px" Style="margin-left: 0px; margin-bottom: 0px;"></asp:TextBox>
                <br />
                <asp:Label ID="DOBLabel" runat="server" Text="DOB (mm/dd/yyyy) : " Width="200px"></asp:Label>
                <asp:TextBox ID="DOBTextBox" runat="server" Width="150px"></asp:TextBox>
                <asp:CompareValidator ControlToValidate="DOBTextBox" ID="DOBValidator" runat="server" ErrorMessage="Date is not valid" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                <br />
                <asp:Label ID="OccupationLabel" runat="server" Text="Occupation : " Width="200px"></asp:Label>
                <asp:DropDownList ID="OccupationDropDown" runat="server" Width="150px">
                    <asp:ListItem Text="Chauffeur" Value="Chauffeur" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Accountant" Value="Accountant"></asp:ListItem>
                    <asp:ListItem Text="Banker" Value="Banker"></asp:ListItem>
                    <asp:ListItem Text="Doctor" Value="Doctor"></asp:ListItem>
                    <asp:ListItem Text="Software Engineer" Value="Software Engineer"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <h3>Add Claim(s) (if applicable)</h3>
                Date (mm/dd/yyyy)
                <asp:TextBox ID="ClaimTextBox1" runat="server"></asp:TextBox>
                <asp:CompareValidator ControlToValidate="ClaimTextBox1" ID="CompareValidator1" runat="server" ErrorMessage="Date is not valid" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                <br />
                Date (mm/dd/yyyy) 
            <asp:TextBox ID="ClaimTextBox2" runat="server"></asp:TextBox>
                <asp:CompareValidator ControlToValidate="ClaimTextBox2" ID="CompareValidator2" runat="server" ErrorMessage="Date is not valid" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                <br />
                Date (mm/dd/yyyy) 
            <asp:TextBox ID="ClaimTextBox3" runat="server"></asp:TextBox>
                <asp:CompareValidator ControlToValidate="ClaimTextBox3" ID="CompareValidator3" runat="server" ErrorMessage="Date is not valid" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                <br />
                Date (mm/dd/yyyy) 
            <asp:TextBox ID="ClaimTextBox4" runat="server"></asp:TextBox>
                <asp:CompareValidator ControlToValidate="ClaimTextBox4" ID="CompareValidator4" runat="server" ErrorMessage="Date is not valid" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                <br />
                Date (mm/dd/yyyy) 
            <asp:TextBox ID="ClaimTextBox5" runat="server"></asp:TextBox>
                <asp:CompareValidator ControlToValidate="ClaimTextBox5" ID="CompareValidator5" runat="server" ErrorMessage="Date is not valid" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                <br />
                <br />
                <asp:Button ID="AddDriver" runat="server" Text="Add Driver" OnClick="AddDriver_Click" Font-Bold="True" Font-Size="Large" Height="43px" Width="110px" />
            </div>

            <br />
            <div style="border: 4px groove grey; max-width: 700px; margin: auto;">
                <h2>Policy Summary</h2>
                <asp:Label ID="PolicySummaryLabel" runat="server" Text=" "></asp:Label>
                <br />
            </div>
            <br />
            <asp:Button ID="CalculatePremiumBtn" runat="server" OnClick="CalculatePremiumBtn_Click" Text="Calculate Premium" CssClass="centeredButton" Font-Bold="True" Font-Size="Large" Height="40px" Width="200px"/>
            <asp:Button ID="ClearPolicyBtn" runat="server" Text="Clear Policy" OnClick="ClearPolicyBtn_Click" Width="200px" CssClass="centeredButton" Font-Bold="True" Font-Size="Large" Height="40px"/>
            <br />
            <br />
            <div style="border: 4px groove grey; max-width: 700px; margin: auto;">
                <asp:Label ID="PremumResultLabel" runat="server" Font-Size="Medium" Font-Bold="True"></asp:Label>
            </div>
            <br />
        </div>

    </form>




</body>
</html>

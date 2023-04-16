<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="LabDarbas3_19.Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Parduotuvių analizė</title>
    <link href="Style.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <div class="first-half">
                <div class="first-inputs-container">
                    <div class="first-inputs">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:FileUpload ID="FileUpload2" runat="server" />
                        <asp:Button ID="Button1" runat="server" Text="ĮKELTI DUOMENIS" OnClick="Button1_Click" />
                    </div>
                </div>
                <div class="first-outputs-container">
                    <div class="first-outputs">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label>
                        <asp:Table ID="Table1" runat="server" CellPadding="5" CellSpacing="5" BackColor="#2C2C2C" BorderColor="#181818"></asp:Table>
                        <asp:Label ID="Label2" runat="server" Font-Bold="True"></asp:Label>
                        <asp:Table ID="Table2" runat="server" CellPadding="5" CellSpacing="5" BackColor="#2C2C2C" BorderColor="#181818"></asp:Table>
                    </div>
                </div>
            </div>
            <div class="second-half">
                <div class="second-inputs-container">
                    <div class="second-inputs">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <asp:Button ID="Button2" runat="server" Text="IŠANALIZUOTI" OnClick="Button2_Click" />
                    </div>
                </div>
                <div class="second-outputs-container">
                    <div class="second-outputs">
                        <asp:Label ID="Label3" runat="server" Font-Bold="True"></asp:Label>
                        <asp:Table ID="Table3" runat="server" CellPadding="5" CellSpacing="5" BackColor="#2C2C2C" BorderColor="#181818"></asp:Table>
                        <asp:Label ID="Label4" runat="server" Font-Bold="True"></asp:Label>
                        <asp:Table ID="Table4" runat="server" CellPadding="5" CellSpacing="5" BackColor="#2C2C2C" BorderColor="#181818"></asp:Table>
                        <asp:Label ID="Label5" runat="server" Font-Bold="True"></asp:Label>
                        <asp:Table ID="Table5" runat="server" CellPadding="5" CellSpacing="5" BackColor="#2C2C2C" BorderColor="#181818"></asp:Table>
                        <asp:Label ID="Label6" runat="server" Font-Bold="True"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

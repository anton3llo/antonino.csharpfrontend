<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="measurements.aspx.cs" Inherits="HendoHealth.measurements" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>HendoHealth</title>
    <link rel="stylesheet" href="Css/misure.css" />
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="modal-header" id="header">
            <!-- header -->
            <h1>I miei dipositivi</h1>
        </div>
        <div class="modal-body" id="body">
                
                <div class="row">
                    <asp:Button ID="blood" runat="server" OnClick="Blood_Click" Text="Blood Pressure" CssClass="btn-danger"/>
                </div>
                <div class="row">
                    <asp:Button ID="Oxygen" runat="server" OnClick="Oxygen_Click" Text="Pulse Oximeter" CssClass="btn-info"/>
                </div>
                <div class="row">
                    <asp:Button ID="glycemieBtn" runat="server" OnClick="GlycemieBtn_Click" Text="Glycemie" CssClass="btn-default"/>
                </div>
                <div class ="row">
                    <asp:Button ID="weightBtn" runat="server" OnClick="WeightBtn_Click" Text="Weight" CssClass="btn-success"/>
                </div>
                
                <div class="col" id="colData">
                    <div class="col col-md-10 col-md-offset-1">
                        <asp:GridView ID="bloodpressure" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-responsive">
                            <Columns>
                                <asp:BoundField DataField="Blood Pressure" HeaderText="Blood Pressure" />
                                <asp:BoundField DataField="Sistolic Pressure" HeaderText="Sistolic Pressure" />
                                <asp:BoundField DataField="Heart Rate" HeaderText="Heart Rate" />
                                <asp:BoundField DataField="Diastolic Pressure" HeaderText="Diastolic Pressure" />
                                <asp:BoundField DataField="Notes" HeaderText="Notes" />
                                <asp:BoundField DataField="Date" HeaderText="Date" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col col-md-10 col-md-offset-1">
                        <asp:GridView ID="bloodoxygen" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-responsive">
                            <Columns>
                                <asp:BoundField DataField="SpO2" HeaderText="SpO2" />
                                <asp:BoundField DataField="Pulse" HeaderText="Pulse" />
                                <asp:BoundField DataField="IP" HeaderText="IP" />
                                <asp:BoundField DataField="Note" HeaderText="Note" />
                                <asp:BoundField DataField="Date" HeaderText="Date" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col col-md-10 col-md-offset-1">
                        <asp:GridView ID="glycemie" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-responsive">
                            <Columns>
                                <asp:BoundField DataField="BG" HeaderText="BG" />
                                <asp:BoundField DataField="Dinner Situation" HeaderText="Dinner Situation" />
                                <asp:BoundField DataField="Drugs Situation" HeaderText="Drugs Situation" />
                                <asp:BoundField DataField="Note" HeaderText="Note" />
                                <asp:BoundField DataField="Date" HeaderText="Date" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col col-md-10 col-md-offset-1">
                        <asp:GridView ID="weight" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-responsive">
                            <Columns>
                                <asp:BoundField DataField="Weight" HeaderText="Weight" />
                                <asp:BoundField DataField="Body Fat" HeaderText="Body Fat" />
                                <asp:BoundField DataField="Water Mass" HeaderText="Water Mass" />
                                <asp:BoundField DataField="Muscle Mass" HeaderText="Muscle Mass" />
                                <asp:BoundField DataField="Bones Mass" HeaderText="Bones Mass" />
                                <asp:BoundField DataField="VGV" HeaderText="VGV" />
                                <asp:BoundField DataField="DCI" HeaderText="DCI" />
                                <asp:BoundField DataField="BMI" HeaderText="BMI" />
                                <asp:BoundField DataField="Note" HeaderText="Note" />
                                <asp:BoundField DataField="Date" HeaderText="Date" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
          
        <div class="modal-footer" id="footer">
            <!-- footer -->
            <asp:Button ID="logOut" runat="server" OnClick="logOut_Click" Text="Log Out"/>
        </div>
    </form>
</body>
</html>

﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxGridView runat="server" ID="GridView1" OnSubstituteFilter="GridView1_SubstituteFilter">
                <Columns>
                    <dx:GridViewDataSpinEditColumn FieldName="Id">
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataTextColumn FieldName="Title">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Notes">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFilterBar="Visible" />
            </dx:ASPxGridView>
        </div>
    </form>
</body>
</html>

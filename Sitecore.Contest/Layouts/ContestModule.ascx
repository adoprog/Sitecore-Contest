<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ContestModule.ascx.cs"
    Inherits="Sitecore.Contest.ContestModule" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="Sitecore.Web.UI.WebControls" %>
<div runat="server" id="module42_contest">
    <link rel="stylesheet" href="/sitecore modules/Contest/css/contest.css" type="text/css" />
    <asp:UpdatePanel ID="contestPanel" runat="server">
        <ContentTemplate>
            <div runat="server" id="divContest" class="contestBox">
                <div class="contestMainColumn">
                    <%= FieldRenderer.Render(CurrentContest.InnerItem, "Image", "w=320&h=175")%>
                </div>
                <div class="contestPlaceHolder">
                </div>
                <div class="contestMainColumn">
                    <div class="contestSubcolumnTop">
                        <%= FieldRenderer.Render(CurrentContest.InnerItem, "Question")%>
                    </div>
                    <div class="contestSubcolumn" style="vertical-align: top">
                        <asp:RadioButtonList ID="answersList" runat="server" Font-Size="Small">
                        </asp:RadioButtonList>
                    </div>
                    <div class="contestSubcolumn contestBottomRight">
                        <br />
                        <div style="width: 155px; text-align: left">
                            <asp:TextBox ID="txtName" runat="server" Width="125px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvName" Width="0px" ValidationGroup="contest" runat="server"
                                ControlToValidate="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div style="width: 155px; text-align: left">
                            <asp:TextBox ID="txtEmail" runat="server" Width="125px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" Width="0px" ValidationGroup="contest" runat="server"
                                ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="emailFormatValidator" Width="3px" ValidationGroup="contest"
                                runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ControlToValidate="txtEmail" ErrorMessage="*"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div runat="server" id="bottomWallpaper" style="width: 645px; height: 75px;">
                    <div class="contestBottomColumn">
                        <%= FieldRenderer.Render(CurrentContest.InnerItem, "Headline")%>
                        <%= FieldRenderer.Render(CurrentContest.InnerItem, "Abstract")%>
                    </div>
                    <div class="contestBottomColumn contestBottomRight">
                        <br />
                        <br />
                        <asp:ImageButton ID="btnSubmit" ValidationGroup="contest" runat="server" ImageUrl="~/sitecore modules/Contest/img/sendSvar.gif"
                            OnClick="btnSubmit_Click" />
                    </div>
                </div>
                <cc1:TextBoxWatermarkExtender ID="watermarkName" runat="server" TargetControlID="txtName"
                    WatermarkText="Name">
                </cc1:TextBoxWatermarkExtender>
                <cc1:TextBoxWatermarkExtender ID="watermarkEmail" runat="server" TargetControlID="txtEmail"
                    WatermarkText="Email">
                </cc1:TextBoxWatermarkExtender>
                <img src="/sitecore modules/Contest/img//deltagNwind.gif" alt="" style="position: relative; left: 20px;
                    top: -235px" />
            </div>
            <div runat="server" id="divThankYou" class="thankYouBox">
                <div class="contestThankYouMainColumn">
                    <div class="contestImageBox">
                        <%= FieldRenderer.Render(CurrentContest.InnerItem, "Image", "w=320&h=175")%>
                    </div>
                    <div class="contestBottomColumn">
                        <%= FieldRenderer.Render(CurrentContest.InnerItem, "ThankYouText")%>
                    </div>
                </div>
                <div class="contestPlaceHolder">
                </div>
                <div class="contestThankYouMainColumn">
                    <div runat="server" id="topWallpaper" class="contestSubcolumnTop">
                        <%= FieldRenderer.Render(CurrentContest.InnerItem, "OtherContestsText")%>
                    </div>
                    <asp:GridView ID="gridContests" runat="server" AutoGenerateColumns="False" Width="310px"
                        OnRowDataBound="gridContests_RowDataBound" GridLines="None" ShowHeader="False"
                        Style="line-height: 15px; margin-top: 5px">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblQuestion" runat="server" Font-Size="Smaller" ForeColor="White"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblHeadline" runat="server" Font-Size="Smaller" ForeColor="#660033"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDeltag" runat="server" ImageAlign="Right" ImageUrl="~/sitecore modules/Contest/img/deltag.gif"
                                        OnClick="imgDeltag_Click" />
                                </ItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <img src="/img/contest/deltagNwind.gif" style="position: relative; left: 10px; top: -235px" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

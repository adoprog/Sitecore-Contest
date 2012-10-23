<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContestTeaser.ascx.cs"
    Inherits="Sitecore.Contest.ContestTeaser" %>
<%@ Import Namespace="Sitecore.Web.UI.WebControls" %>
<link rel="stylesheet" href="/sitecore modules/Contest/css/contest.css" type="text/css" />
    <div id="teaserWallpaper" runat="server" class="contestTeaserBox">
        <div class="contestImageBox">
            <%= FieldRenderer.Render(CurrentContest.InnerItem, "Image", "w=320&h=175")%>
        </div>
        <div class="contestTeaserBottomColumn">
            <div style="float:left">
                <%= FieldRenderer.Render(CurrentContest.InnerItem, "Headline")%>
            </div>
            <div style="float:left">
                <asp:ImageButton ID="imgTeaserDeltag" runat="server" ImageAlign="Right" ImageUrl="~/sitecore modules/Contest/img/deltagTeaser.gif"
                    Style="margin-right: 10px; margin-top: 6px" OnClick="imgTeaserDeltag_Click" />
            </div>
        </div>
    <img src="/img/contest/deltagNwind.gif" style="position: relative; left: 10px; top: -217px" />
</div>

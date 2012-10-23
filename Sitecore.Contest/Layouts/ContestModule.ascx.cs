using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AlphaSolutions.myLiving.Web.Classes.Contest.Items;
using Sitecore.Data.Items;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Web.UI.Sheer;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sitecore.Layouts;
using Sitecore.SecurityModel;
using Sitecore;

namespace Sitecore.Contest
{
    public partial class ContestModule : System.Web.UI.UserControl
    {
        private const string ContestSublayoutId = "{A780E4DD-70A5-46CD-9B92-4EE01F0BB292}";
        private const string SubmittedAnswerTemplateId = "{E339FFD1-76A1-4E40-B55F-88B57F530C5E}";
        string RootItemId
        {
            get
            {
                return Sitecore.Configuration.Settings.GetSetting("ContestRootItemId");
            }
        }
        protected Database ContentDatabase
        {
            get
            {
                return Factory.GetDatabase("master");
            }
        }

        public ContestItem CurrentContest
        {
            get
            {
                if (!IsPostBack)
                {
                    if (Request["contestid"] != null)
                    {
                        ViewState[this.ClientID] = Request["contestid"];
                    }
                    else
                    {
                        if (GetContestsToShow().Count > 0)
                        {
                            ViewState[this.ClientID] = GetContestsToShow()[0].ID.ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }                
                return new ContestItem(ContentDatabase.Items[(string)ViewState[this.ClientID]]);
            }
        }

        protected string GetDataSource()
        {
            string renderings = Sitecore.Context.Item.Fields["__renderings"].Value;

            LayoutDefinition lDef = LayoutDefinition.Parse(renderings);
            DeviceItem dev = Sitecore.Context.Device;
            DeviceDefinition dDef = lDef.GetDevice(dev.ID.ToString());

            Item subl = Sitecore.Context.Database.GetItem(new ID(ContestSublayoutId));
            return dDef.GetRendering(subl.ID.ToString()).Datasource;
        }

        /// <summary>
        /// Gets contests, to show on "Thank You" page.
        /// </summary>
        /// <returns></returns>
        protected List<Item> GetContestsToShow()
        {
            Item settingsItem = ContentDatabase.Items[GetDataSource()];
            if (settingsItem == null)
                return null;
            Sitecore.Data.Fields.MultilistField contests = settingsItem.Fields["Contests"];

            List<Item> items = new List<Item>();
            foreach (Item item in contests.GetItems())
            {
                if (item.Template.Name == "Contest")
                {
                    items.Add(item);
                }
            }
            return items;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // set moduleheight if adjusted
            if (!String.IsNullOrEmpty(Attributes["sc_parameters"]))
            {
                System.Collections.Specialized.NameValueCollection renderingParameters = Sitecore.Web.WebUtil.ParseUrlParameters(Attributes["sc_parameters"]);
                if (!String.IsNullOrEmpty(renderingParameters.Get("modulespacing")))
                {
                    this.module42_contest.Attributes.Add("style", "margin-bottom:" + renderingParameters.Get("modulespacing") + "px;");
                }
            }
            if (CurrentContest == null)
            {
                divContest.Visible = false;
                divThankYou.Visible = false;
                return;
            }
            if (!IsPostBack)
            {
                bottomWallpaper.Style["background-image"] = CurrentContest.Wallpaper;
                topWallpaper.Style["background-image"] = CurrentContest.Wallpaper;
                FillAnswers(CurrentContest.InnerItem.Axes.GetDescendants());
                divContest.Visible = true;
                divThankYou.Visible = false;
            }
            gridContests.DataSource = GetContestsToShow();
            gridContests.DataBind();
        }

        /// <summary>
        /// Loads the answers into the radiolist
        /// </summary>
        /// <param name="answers">Items, that corresponds to answers.</param>
        protected void FillAnswers(Item[] answers)
        {
            answersList.Items.Clear();
            foreach (Item item in answers)
            {
                if (item.TemplateName == "Answer")
                    answersList.Items.Add(new ListItem(item.Fields["AnswerText"].ToString(), item.ID.ToString()));
            }
            answersList.SelectedIndex = 0;
        }

        protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            Item answer = ContentDatabase.Items[answersList.SelectedItem.Value];
            if (answer != null)
            {
                //Check, whether this answer is right. If yes - save the data.
                Sitecore.Data.Fields.CheckboxField isRightAnswer = answer.Fields["IsRightAnswer"];
                if (isRightAnswer.Checked)
                {
                    SaveSubmittedData();
                }
            }
            divContest.Visible = false;
            divThankYou.Visible = true;
            FillAnswers(CurrentContest.InnerItem.Axes.GetDescendants());
            txtEmail.Text = "";
            txtName.Text = "";
        }

        protected void gridContests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Alternate || e.Row.RowState == DataControlRowState.Normal))
            {
                Label lblQuestion = (Label)e.Row.FindControl("lblQuestion");
                Label lblHeadline = (Label)e.Row.FindControl("lblHeadline");

                Item item = (Item)e.Row.DataItem;

                string question = StripTags(item["Question"].ToString());
                string headline = StripTags(item["Headline"].ToString());
                                
                lblQuestion.Text = question.Length > 30 ? question.Substring(0, 30) + "..." : lblQuestion.Text = question;
                lblHeadline.Text = lblHeadline.Text.Length > 30 ? headline.Substring(0, 30) + "..." : lblHeadline.Text = headline;

                ImageButton deltag = (ImageButton)e.Row.FindControl("imgDeltag");
                deltag.CommandArgument = item.ID.ToString();
            }
        }
                
        /// <summary>
        /// Strip the tags to display rich text in required format. 
        /// </summary>
        /// <param name="text">Text, containing HTML markup</param>
        /// <returns>Plain text</returns>
        protected string StripTags(string text)
        {
            return Regex.Replace(text, "<.*?>", string.Empty);
        }

        protected void imgDeltag_Click(object sender, ImageClickEventArgs e)
        {
            ViewState[this.ClientID] = ((ImageButton)sender).CommandArgument;
            divContest.Visible = true;
            divThankYou.Visible = false;
            FillAnswers(CurrentContest.InnerItem.Axes.GetDescendants());
        }

        protected void SaveSubmittedData()
        {
            using (new SecurityDisabler())
            {
                TemplateItem submittedAnswerTemplate = ContentDatabase.Templates[new Sitecore.Data.ID(SubmittedAnswerTemplateId)];
                TemplateItem folderTemplate = ContentDatabase.Templates["{A87A00B1-E6DB-45AB-8B54-636FEC3B5523}"];
                Item rootItem = ContentDatabase.Items.GetItem(RootItemId);

                Item contestItem = rootItem.Axes.GetChild(CurrentContest.Name) == null ?
                    rootItem.Add(CurrentContest.Name, folderTemplate) :
                    rootItem.Axes.GetChild(CurrentContest.Name);

                Item submittedAnswer = contestItem.Axes.GetChild(txtEmail.Text.Replace(".", "DOT").Replace("@", "AD"));
                if (submittedAnswer == null)
                {
                    //As item name could not contain '.' and '@' symbols, we replace them.
                    submittedAnswer = contestItem.Add(txtEmail.Text.Replace(".", "DOT").Replace("@", "AD"), submittedAnswerTemplate);
                }
                using (new Sitecore.Data.Items.EditContext(submittedAnswer))
                {
                    //Set the display name, to show normal email address.
                    submittedAnswer[FieldIDs.DisplayName] = txtEmail.Text;
                    submittedAnswer["ContestId"] = CurrentContest.ID.ToString();
                    submittedAnswer["Answer"] = answersList.SelectedItem.Text;
                    submittedAnswer["Name"] = txtName.Text;
                    submittedAnswer["Email"] = txtEmail.Text;
                }
            }
        }
    }
    
}
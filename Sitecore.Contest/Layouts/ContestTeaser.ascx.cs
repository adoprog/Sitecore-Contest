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
using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore.Layouts;
using Sitecore.Configuration;
using AlphaSolutions.myLiving.Web.Classes.Contest.Items;
using System.Text.RegularExpressions;

namespace Sitecore.Contest
{
    public partial class ContestTeaser : System.Web.UI.UserControl
    {
        private const string ContestTeaserSublayoutId = "{7C64ACB6-3A57-4646-9395-02E659962418}";
     
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
                return new ContestItem(ContentDatabase.Items[GetDataSource()]);
            }
        }

        public string ContestsFolder
        {
            get
            {
                return Sitecore.Configuration.Settings.GetSetting("ContestsFolder");
            }
        }

        protected string GetDataSource()
        {
            string renderings = Sitecore.Context.Item.Fields["__renderings"].Value;

            LayoutDefinition lDef = LayoutDefinition.Parse(renderings);
            DeviceItem dev = Sitecore.Context.Device;
            DeviceDefinition dDef = lDef.GetDevice(dev.ID.ToString());

            Item subl = Sitecore.Context.Database.GetItem(new ID(ContestTeaserSublayoutId));
            return dDef.GetRendering(subl.ID.ToString()).Datasource;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                teaserWallpaper.Style["background-image"] = CurrentContest.Wallpaper;
                teaserWallpaper.Style["background-position"] = "Bottom";
            }            
        }

        protected void imgTeaserDeltag_Click(object sender, ImageClickEventArgs e)
        {
            Item targetItem = ContentDatabase.Items[GetDataSource()];
            Response.Redirect("~" + ContestsFolder + targetItem.Name + ".aspx?contestId=" + targetItem.ID.ToString());
        }

        protected string StripTags(string text)
        {
            return Regex.Replace(text, "<.*?>", string.Empty);
        }
    }
}
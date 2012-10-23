using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Sitecore.Data.Items;

namespace AlphaSolutions.myLiving.Web.Classes.Contest.Items
{
    public class ContestItem : CustomItemBase
    {
        public ContestItem(Item item)
            : base(item)
        {
        }
        
        public string Headline
        {
            get
            {
                return InnerItem["Headline"];
            }
            set
            {
                InnerItem["Headline"] = value;
            }
        }

        public string Abstract
        {
            get
            {
                return InnerItem["Abstract"];
            }
            set
            {
                InnerItem["Abstract"] = value;
            }
        }

        public string Image
        {
            get
            {
                return InnerItem["Image"];
            }
            set
            {
                InnerItem["Image"] = value;
            }
        }


        public string Question
        {
            get
            {
                return InnerItem["Question"];
            }
            set
            {
                InnerItem["Question"] = value;
            }
        }
         
        public string Wallpaper
        {
            get
            {
                string imageUrl = "";
                Sitecore.Data.Fields.ImageField imageField = InnerItem.Fields["Wallpaper"];
                if (imageField != null && imageField.MediaItem != null) 
                { 
                    Sitecore.Data.Items.MediaItem image = new Sitecore.Data.Items.MediaItem(imageField.MediaItem);
                    imageUrl = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image)); 
                }
                return imageUrl;
            }
        }

        public string ThankYouText
        {
            get
            {
                return InnerItem["ThankYouText"];
            }
            set
            {
                InnerItem["ThankYouText"] = value;
            }
        }

        public string OtherContestsText
        {
            get
            {
                return InnerItem["OtherContestsText"];
            }
            set
            {
                InnerItem["OtherContestsText"] = value;
            }
        }

        public DateTime Created
        {
            get
            {
                return InnerItem.Statistics.Created;
            }
        }

        public DateTime Updated
        {
            get
            {
                return InnerItem.Statistics.Updated;
            }
        }

        public string Creator
        {
            get
            {
                return InnerItem.Statistics.CreatedBy;
            }
        }
    
    }
}

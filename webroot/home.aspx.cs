using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Boeijenga.Business;
using Boeijenga.Common.Objects;
using Boeijenga.DataAccess;


public partial class home : BasePage
    {
      	
       ArrayList cartTable;
        string moreLabel = "";
        public string ApplicationNewsImagePath = System.Configuration.ConfigurationManager.AppSettings["news-imageuri"];
        public string ApplicationImagePath = System.Configuration.ConfigurationManager.AppSettings["searchresult-article-imageuri"];
   
        protected void Page_Load(object sender, EventArgs e)
        {

            AddHitCount();
            Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);
           Master.SetVisitPageList(Functions.GetPageName(Request.Url.ToString()));
            if (!IsPostBack)
            {
                SetCulture();
                LoadRecords();

                SetCulturalValue();
                LoadMainProducts();
                //lblHitCount.Text = "Total Hits: "+SetHitValue();
            }
            
            SetCulturalValue();

            
        }

        protected string GetArticleImagePath(string imageName)
        {

            return string.Format(ApplicationImagePath, imageName + "&");

        }

        private void AddHitCount()
        {
            if (Session["hitadded"] == null)
            {
                string sqlgetcount = "select coalesce(totalhits,'0')as MaxHits from hitcounter;";
                DataTable dt = DataAccessHelper.GetInstance().GetDataTable(sqlgetcount);
                double hit = Convert.ToDouble(dt.Rows[0]["MaxHits"].ToString());
                hit++;
                string sqlUpdateHitcount = "update hitcounter set totalhits='" + hit + "';";
                DataAccessHelper.GetInstance().ExecuteQuery(sqlUpdateHitcount);
                Session.Add("hitadded", "yes");
            }
        }

        protected string GetNewsImagePath(string imageName)
        {

            return string.Format(ApplicationNewsImagePath, imageName + "&");

        }
        /// <summary>
        /// Reacts to Master Page menu click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Master_MenuButton(object sender, EventArgs e)
        {
            Session["cultureName"] = Master.CurrentButton.ToString();
            SetCulture();
            LoadRecords();
            SetObjectValue();
            SetCulturalValue();
        }
        private void SetObjectProperty(bool visible)
        {
            Master.FindControl("lblLoginInfo").Visible = visible;             //logged in as:
            Master.FindControl("lblLogin").Visible = visible;                 //login name
        }
        private void SetObjectValue()
        {
            if (Session["userid"] != null)
            {
                SetObjectProperty(true);
            }
            else
            {
                SetObjectProperty(false);
            }
        }
        private void LoadRecords()
        {
            LoadSpotLight();
            ShowNews();
        }

        private void LoadMainProducts()
        {
            MainProduct sheetMusicOrgaanProduct = new Facade().GetSheetMusicOrgaanMainProducts();
            MainProduct sheetMusicOtherProduct = new Facade().GetSheetMusicOtherMainProducts();
            MainProduct bookMainProduct = new Facade().GetBookMainProducts();
            MainProduct cddvdProduct = new Facade().GetCDDVDMainProducts();


            lblBookPreview.Text = bookMainProduct.Title + "... &raquo;";
            btnBookOrgaanPreview.CommandArgument = bookMainProduct.ArticleCode;
            imgBookPreview.ImageUrl = GetArticleImagePath(bookMainProduct.ImageFile);
            if (sheetMusicOtherProduct.Title != null)
            {
                lblOtherSheetmusicPreview.Text = sheetMusicOtherProduct.Title + "... &raquo;";
            }
            else
                lblOtherSheetmusicPreview.Text = sheetMusicOtherProduct.Title;

            btnOtherSheetmusicPreview.CommandArgument = sheetMusicOtherProduct.ArticleCode;
            imgOtherSheetmusicPreview.ImageUrl = GetArticleImagePath(sheetMusicOtherProduct.ImageFile);

            lblSheetmusicOrgaanPreviewTitle.Text = sheetMusicOrgaanProduct.Title + "... &raquo;";
            btnSheetmusicOrgaanPreview.CommandArgument = sheetMusicOrgaanProduct.ArticleCode;
            imgSheetmusicOrgaanPreview.ImageUrl = GetArticleImagePath(sheetMusicOrgaanProduct.ImageFile);

            lblCDDVDPreview.Text = cddvdProduct.Title + "... &raquo;";
            btnCDDVDPreview.CommandArgument = cddvdProduct.ArticleCode;
            imgCDDVDPreview.ImageUrl = GetArticleImagePath(cddvdProduct.ImageFile);

           
        }
        private string GetCultureStr()
        {
            return Functions.GetCultureStr(Session["cultureName"].ToString());
        }

        private void LoadSpotLight()
        {
            string sqlSpotLight = @"
            select articlecode,(case when char_length(description" + GetCultureStr() + @")>200 then
	            substr(description" + GetCultureStr() + @",0,200) || '... <a class=""spotlight-morelink"" href=""javascript:void(0)"" href=""searchresult.aspx?articlecode=' || a.articlecode || '"">" + moreLabel + @"</a>' else description" + GetCultureStr() + @" end) as description,description"+ GetCultureStr()+@" as fulldescription,
	            title, subtitle,COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') ||'&nbsp ' ||(case when (c.dob is not null and c.dob<>'') 
	            then '('||c.dob||(case when (c.dod is not null and c.dod <>'') then '-'||c.dod else '*' end)||')' else '*' end) as composer,serie, grade, editor,
	            category,events,publisher,round(a.price+round(a.price*cat.vatpc/100,2),2) as Price,editionno,publicationno,
	            pages,publishdate,duration,period,language,isbn10,isbn13,articletype,
	            quantity,imagefile,pdffile,purchaseprice
            from article a
            left join category cat on cat.categoryid =( 
                            select case 
                              when (position(',' in category)-1)<0 then 
                               category 
                              else 
                               substr (category, 1,position(',' in category)-1)
                              end
                             from article 
                             where articlecode=a.articlecode
                            )
            left join composer c on a.composer=c.composerid
            where articlecode = (
	            select article 
	            from spotlight 
	            where spotlight=true 
	            order by spotlightdate 
	            desc limit 1 )";
            
            DataTable dtArticle = DataAccessHelper.GetInstance().GetDataTable(sqlSpotLight);

            if (dtArticle.Rows.Count>0)
            {
                lblTitle.Text = dtArticle.Rows[0]["title"].ToString();
                lblSubTitle.Text = dtArticle.Rows[0]["subtitle"].ToString();
                lblDescription.Text =  dtArticle.Rows[0]["description"].ToString();
                lblFullDescription.Text = dtArticle.Rows[0]["fulldescription"].ToString();
                lblComposer.Text = "<i>" + dtArticle.Rows[0]["composer"].ToString() + "</i>";  //Added new field in the spotlight
                //lblID.Text = func.GetArticleProperty(dtArticle.Rows[0]);      have to ask 17-01-08
                //Functions.HandleNullResources(lblPrice, (string)base.GetGlobalResourceObject("string", "lblPrice") + ": <b>€ " + string.Format("{0:F2}", double.Parse(dtArticle.Rows[0]["price"].ToString())) + "</b>");
                lblPriceInfo.Text = (string) base.GetGlobalResourceObject("string", "lblPrice");
                lblPrice.Text = "<b>€ " + string.Format("{0:F2}", double.Parse(dtArticle.Rows[0]["price"].ToString())) + "</b>";
              
                
                //lblPrice.Text = "Price: <b>€ " + string.Format("{0:F2}", double.Parse(dtArticle.Rows[0]["price"].ToString())) + "</b>";
                string filename = dtArticle.Rows[0]["imagefile"].ToString();
                string fileFullname = ConfigurationManager.AppSettings["resources"].ToString()
                    + "images\\" + filename;
                if (Functions.FileExist(fileFullname) && Functions.IsImageFile(fileFullname))
                {
                    imgSpotlight.ImageUrl = ConfigurationManager.AppSettings["web-resources"].ToString() + "images/" + filename;
                    imgSpotlight.Visible = true;
                }
                else
                {
                   // imgSpotlight.Visible = false;
                }
                btnDetail.CommandArgument = dtArticle.Rows[0]["articlecode"].ToString();
            }
        }
        private void ShowNews()
        {
            string sqlNews = @"
            Select upper(title) as title, newsimagefile,to_char(newsdate,'DD-Mon-YYYY') as date,
            (case when char_length(description)>220 then 
	            substr(description,0,220) || '...&nbsp;<a class=""morenewslink pinklink"" href=""javascript:void(0)"">" + moreLabel + @"</a>'
            else
	            description
            end
            ) as description,description as fulldescription
            from news 
            where shownews=true
            order by newsdate desc, subject asc limit 5";
            grdNews.DataSource = DataAccessHelper.GetInstance().GetDataTable(sqlNews);
            grdNews.DataBind();
          
        }


        protected void btnSpotlightDetail_Click(object sender, EventArgs e)
        {
			string articleCode = btnDetail.CommandArgument.ToString();
			if (!articleCode.Equals("0") && articleCode != null)
				Response.Redirect("searchresult.aspx?" + "articlecode=" + btnDetail.CommandArgument);
        }
        private void SetCulturalValue()
        {
            moreLabel = (string)base.GetGlobalResourceObject("string", "more");
            lblNewsMore.Text = (string)base.GetGlobalResourceObject("string", "lblNewsMore");
            ShowNews();
            LoadSpotLight();
       }
       
        protected void btnQuickBuy_Click(object sender, EventArgs e)
        {
            

            string articleCode = btnDetail.CommandArgument.ToString();

            ArrayList cartTable = new ArrayList();
            if (Session["order"] != null)
            {
                cartTable = (ArrayList)Session["order"];
            }
            if (!isOrderExists(articleCode))
            {
                Order order = new Order(articleCode, 1);
                order = new Facade().LoadOrderInfo(articleCode, order);
                order.publisherName = GetPublisherName(articleCode);
                cartTable.Add(order);
                Session["order"] = cartTable;
            }

            Response.Redirect("shoppingcart.aspx");
        }


        private bool isOrderExists(string articleCode)
        {
            bool exists = false;
            if (Session["order"] != null)
            {
                ArrayList cartTable = (ArrayList)Session["order"];
                IEnumerator enu = cartTable.GetEnumerator();
                while (enu.MoveNext())
                {
                    Order order = (Order)enu.Current;
                    if (order.articlecode.ToString().Equals(articleCode))
                    {
                        exists = true;
                        break;
                    }
                }
            }
            return exists;
        }


        private void SetCulture()
        {
            Master.SetCulture();

        }
		private string GetPublisherName(string articleCode)
		{
			string sqlQuey ="select case when a.articletype='b' then (COALESCE(p.firstname) || ' '|| COALESCE(p.middlename,'') ||' '|| COALESCE(p.lastname) ) " +
							"else (COALESCE(c.firstname) ||' '|| COALESCE(c.middlename,'')  ||' '|| COALESCE(c.lastname) )end  as publisher " +
							"from article a, publisher p, composer c " +
							"where p.publisherid=a.publisher " +
							"and a.composer = c.composerid  and  articlecode='" + articleCode + "'";
			DataTable dtArticle = DataAccessHelper.GetInstance().GetDataTable(sqlQuey);
			if (dtArticle.Rows.Count > 0)
				return dtArticle.Rows[0]["publisher"].ToString();
			else
				return "";
		}


     

       


        protected void detail_command(object sender, CommandEventArgs e)
        {
            string articleCode = e.CommandArgument.ToString();
            if(articleCode !=null)
            {
                Response.Redirect("searchresult.aspx?articlecode=" +articleCode);
            }
        }

        protected string GetDescriptionWithMoreIfRequired(string fullDescrip)
        {
            return DescriptionTextHandler.GetDescriptionWithMoreIfRequired(fullDescrip, (string)base.GetGlobalResourceObject("string", "more"));
        }
    }

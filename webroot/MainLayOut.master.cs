using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Globalization;
using System.Threading;
using Boeijenga.Business;
using Boeijenga.Common.Objects;
using Npgsql;

#region Public Delegates

// Expose delegates for Master Page Events
public delegate void MasterPageMenuClickHandler(object sender, System.EventArgs e);

#endregion Public Delegates

namespace bo01
{
    public partial class MainLayOut : System.Web.UI.MasterPage
    {
        string[] menuItems = { "Home", "Books", "Sheetmusic", "CD/DVD", "Publish", "About", "News", "Contact" };
        string[] toolTips = { "Home", "Books", "Sheetmusic", "CD/DVD", "Publish", "About", "News","Contact" };
        //string[] menuItems = {"about"};
        string[] urls = { "home.aspx", "searchresult.aspx?search=false&type=b&shop=true", "searchresult.aspx?search=false&type=s&shop=true", "searchresult.aspx?search=false&type=c,d&shop=true", "searchresult.aspx?search=false&shop=false", "about.aspx","News.aspx", "contact.aspx" };

        #region Public Properties

        private string _currentCulture;

        public string CurrentButton
        {
            get { return _currentCulture; }
            set { _currentCulture = value; }
        }

        #endregion Public Properties

        #region Public Events

        public event MasterPageMenuClickHandler MenuButton;

        #endregion Public Events


        public void SetVisitPageList(String pageName)
        {
            ArrayList visitPageList = new ArrayList();
            if (Session["visitPageList"] != null)
            {
                visitPageList = (ArrayList)Session["visitPageList"];
            }
            

            Session["visitPageList"] = Functions.initVisitPageList(visitPageList, pageName);
        }

        public void ShowStatus(string message, int timeout)
        {



            var script = @" jQuery(document).ready( function() {{ 

                        var status = '{0}';
                       showStatus(status,{1},true,false);


                }});";

           
            ScriptManager.RegisterStartupScript(this,this.GetType(),

                              "showStatus",

                              string.Format(script, message, timeout), true);

        }

        #region Page Events

        /// <summary>
        /// Handles Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BuildMenu(phMenu);
                EnableLoginTextBoxes(false);
                


            }
            InitBreadCumb();
        }

        private void InitBreadCumb()
        {
            if (Session["visitPageList"] != null)
            {
                ArrayList visitPageList = (ArrayList)Session["visitPageList"];
                for (int i = 0; i < visitPageList.Count; i++)
                {
                    String pageUrl = visitPageList[i].ToString();

                    if (i != visitPageList.Count - 1)
                    {


                        breadcumbContainer.Controls.Add(
                          new LiteralControl("<a class='visited-page-link' href='" + pageUrl + "'>" + Functions.GetPageTitle(pageUrl) + " &raquo;</a>   "));
                    }
                    else
                    {

                        breadcumbContainer.Controls.Add(
                          new LiteralControl(" <a class='active-page-link' href='" + pageUrl + "'>" + Functions.GetPageTitle(pageUrl) + " </a> "));

                    }
                }
            }
        }

        /// <summary>
        /// Handles Page PreRender Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {

            SetCulture();
            SetObjectValue();
            BuildMenu(phMenu);

        }

        #endregion Page Events

        #region Virtual Methods

        /// <summary>
        /// Invokes subscribed delegates to menu click event.
        /// </summary>
        /// <param name="e">Click Event arguments</param>
        protected virtual void OnMenuButton(EventArgs e)
        {
            if (MenuButton != null)
            {
                //Invokes the delegates.
                MenuButton(this, e);
            }
        }

        #endregion Virtual Methods

        private void SetObjectProperty(bool visible)
        {
            lblLoginInfo.Visible = visible;             //logged in as:
            lblLogin.Visible = visible;                 //login name
            Label4.Visible = visible;
        }

        private void EnableLoginTextBoxes(bool visible)
        {
            if (visible)
            {

                txtPassword.TextMode = TextBoxMode.Password;
            }
            else
            {

                txtPassword.TextMode = TextBoxMode.SingleLine;
            }
            txtUserName.Visible = visible;
          //  txtPassword.Visible = false;
            txtFakePassword.Visible = visible;
            btnLogin.Visible = visible;
        }
        private void EnableLoginRegistrationlink(bool visible)
        {

            lnkLogin.Visible = visible;
            lnkRegister.Visible = visible;
            Label5.Visible = visible;
        }


        private void EnableLanguageBar(bool visible)
        {

            flagNL.Visible = visible;
            flagEN.Visible = visible;
        }

        private void SetObjectValue()
        {
            if (Session["user"] != null)
            {
                User user = (User)Session["user"];
                SetObjectProperty(true);
                Label4.Visible = false;
                lnkRegister.Text = "";
                lnkLogin.Text = (string)base.GetGlobalResourceObject("string", "header_logout") + " &raquo;";
                lblLogin.Text =  user.FirstName + " &raquo;";
                lblLoginInfo.Text = (string)base.GetGlobalResourceObject("string", "header_logininfo");

               

            }
            else
            {
                SetObjectProperty(false);
                btnLogin.Text = (string)base.GetGlobalResourceObject("string", "header_login");
                lnkRegister.Text = (string)base.GetGlobalResourceObject("string", "header_register") + " &raquo;";
                lnkLogin.Text = (string)base.GetGlobalResourceObject("string", "header_login") + " &raquo;";

                linkBasket.Text = (string)base.GetGlobalResourceObject("string", "basket") + " &raquo;";
                lblBasketContent.Text = (string)base.GetGlobalResourceObject("string", "basketempty");
                lblBasketPayament.Text = (string)base.GetGlobalResourceObject("string", "payment") + " &raquo;";
                linkEmptyBasket.Text = (string)base.GetGlobalResourceObject("string", "emptybasket") + " &raquo;";
                lblBasketTotalPrice.Text = Double.Parse("0").ToString();
            }
            InitOrderSession();
            linkMyAccount.Text = (string)base.GetGlobalResourceObject("string", "myAccount") + " &raquo;";
           txtUserName.Attributes["title"] = (string)base.GetGlobalResourceObject("string", "lblUserName");
           txtFakePassword.Attributes["title"] = (string)base.GetGlobalResourceObject("string", "lblPassword");

            lblToatal.Text = (string)base.GetGlobalResourceObject("string", "total") + " : &euro;";
            rfvUserName.ErrorMessage =  rfvUserName.ToolTip ="*"+ (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
            revUserName.ErrorMessage = revUserName.ToolTip = "*" + (string)base.GetGlobalResourceObject("string", "emailValidMessage");
        }

        private void InitOrderSession()
        {
            if (Session["order"] != null)
            {

                ArrayList orderList = (ArrayList)Session["order"];

                int productCount = Functions.getProductCount(orderList);
                lblBasketContent.Text = productCount.ToString() + " products";
                lblBasketTotalPrice.Text = string.Format("{0:F2}", Functions.GetProductToatlPrice(orderList).ToString() ); ;
            }
            else
            {
                lblBasketContent.Text = (string)base.GetGlobalResourceObject("string", "basketempty");

                lblBasketTotalPrice.Text = Double.Parse("0").ToString();


            }
        }

        protected void flagNL_Click(object sender, ImageClickEventArgs e)
        {
            // Assign value to public property
            _currentCulture = "nl-NL";
            // Fire event to existing delegates
            OnMenuButton(e);
        }
        protected void flagEN_Click(object sender, ImageClickEventArgs e)
        {
            // Assign value to public property
            _currentCulture = "en-US";

            // Fire event to existing delegates
            OnMenuButton(e);
        }
        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                EnableLoginTextBoxes(true);
                SetObjectProperty(false);
                EnableLoginRegistrationlink(false);
                EnableLanguageBar(false);
            }
            else
            {
                _currentCulture = (Session["cultureName"] == null) ? "nl-NL" : Session["cultureName"].ToString();
                Session.Clear();
                Session["cultureName"] = _currentCulture;
                Response.Redirect("Home.aspx");
            }
        }

        protected void lnkRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
        protected void lnkHelp_Click(object sender, EventArgs e)
        {
            Response.Redirect("help.aspx");
        }
        protected void chicken_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("shoppingcart.aspx");
        }
        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }



        protected void btnLogin_Click(object sender, EventArgs e)
        {

            int customerid = 0;
            if (Functions.IsValidMail(txtUserName.Text))
            {
                //string loginQuery = "SELECT customerid,firstname,middlename,lastname FROM customer WHERE email = '" + txtUserName.Text.Trim() + "' AND password =  '" + txtPassword.Text.Trim() + "'";
                User user = new Facade().CheckLogIn(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                Session["user"] = user;//setting the session of user

                if (user == null)
                {
                    string errorMSg = (string)base.GetGlobalResourceObject("string", "invalidLogin");
                    this.ShowStatus(errorMSg, 5000);
                }
                else
                {
                    Response.Redirect("home.aspx");
                }
            }
        }



        public void SetCulture()
        {
            string cultureName = "";
            if (Session["cultureName"] != null)
            {
                cultureName = Session["cultureName"].ToString();
            }
            else
            {
                cultureName = HttpContext.Current.Request.UserLanguages[0];
                //cultureName = HttpContext.Current.Request.UserLanguages[];
                Session["cultureName"] = cultureName;
            }
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Page.Culture = cultureName;
           
        }

        public void BuildMenu(PlaceHolder phMenu)
        {
            phMenu.Controls.Clear();
            string langIndex = "en";
            if (Session["cultureName"] != null)
            {
                langIndex = Session["cultureName"].ToString().Substring(0, 2);
            }
            else
            {
                langIndex = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
            }

            phMenu.Controls.Add(new LiteralControl("<ul class='menu-ul'>"));

            for (int index = 0; index < menuItems.Length; index++)
            {
                phMenu.Controls.Add(new LiteralControl(GetMenuItem(index, langIndex)));

                if (index != menuItems.Length - 1)
                {
                    phMenu.Controls.Add(new LiteralControl(@"<li> <div class='menu-separetor' </div></li>"));
                }
            }

            phMenu.Controls.Add(new LiteralControl("</ul>"));

        }
        public string GetMenuItem(int mIndex, string cultureIndex)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<li>");
            sb.Append("<a");
            sb.Append(" href='" + urls[mIndex] + "'");
            sb.Append(" title='" + toolTips[mIndex] + "'");

            sb.Append(">" + menuItems[mIndex] + "</a>");
            sb.Append("</li>");
            return sb.ToString();
        }

        protected void link_basket_click(object sender, EventArgs e)
        {
            Response.Redirect("shoppingcart.aspx");
        }

        protected void linkEmptyBasket_click(object sender, EventArgs e)
        {
            Session["order"] = null;
            Response.Redirect(Request.Url.ToString(), true);
        }
        protected void linkMyAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("register.aspx");
        }

        protected void lblBasketPayament_click(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {

                Response.Redirect("confirm.aspx");
            }
            else
            {

                Response.Redirect("signup.aspx");
            }

        }
    }
}
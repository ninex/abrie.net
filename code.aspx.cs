using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace abrie.netWeb
{
    public partial class code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["_escaped_fragment_"] != null)
            {
                ProvideStatic(Request.QueryString["_escaped_fragment_"]);
            }
        }

        private void ProvideStatic(string fragment)
        {
            WCF.DataService service = new WCF.DataService();
            string view;
            if (string.IsNullOrEmpty(fragment))
            {
                view = System.IO.File.ReadAllText(Server.MapPath("~/assets/views/articleSummary.html"));
                WCF.Entities.Articles allarticles = service.GetAllArticles();
                if (allarticles != null)
                {
                    foreach (var article in allarticles)
                    {
                        articles.InnerHtml += view.Replace("${Title}", article.Title).Replace("{{html Summary}}", article.Summary).Replace("${Created}", article.Created.ToString("yyyy-MM-dd")).Replace("#!${Id}","code.aspx#!"+article.Id);
                    }
                }
            }
            else
            {
                view = System.IO.File.ReadAllText(Server.MapPath("~/assets/views/article.html"));
                WCF.Entities.Article article = service.GetArticle(fragment);
                if (article != null)
                {
                    articles.InnerHtml = view.Replace("${Title}", article.Title).Replace("{{html ArticleContent}}", article.ArticleContent);
                }
            }
        }
    }
}
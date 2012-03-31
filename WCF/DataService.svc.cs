using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using abrie.netWeb.WCF.Entities;
using System.ServiceModel.Syndication;

namespace abrie.netWeb.WCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DataService : IDataService
    {
        private const string myPwd = "magicpwd";

        private DataAccessors.IDataStore dataStore = null;

        public DataService()
        {
            //dataStore = new DataAccessors.LinqDataStore();
            Article.dataStore = dataStore;
            Comment.dataStore = dataStore;
        }

        #region IDataService Members

        /// <summary>
        /// Empty method to test communication to the server
        /// </summary>
        public void test() { }

        #region Article

        public void SubmitArticle(Article article, string pwd)
        {
            if (pwd != myPwd)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
                return;
            }
            Article.SubmitArticle(article);
        }
        public Article GetArticle(string id)
        {
            return Article.GetArticle(id);
        }
        public Articles GetAllArticles()
        {
            return Article.GetAllArticles();
        }
        public void RemoveArticle(string id, string pwd)
        {
            if (pwd != myPwd)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
                return;
            }
            Article.RemoveArticle(id.Replace("articles/",""));
        }

        #endregion

        #region Comment

        public void SubmitComment(Comment comment)
        {
            Comment.SubmitComment(comment);
        }
        public Comments GetArticleComments(string articleId)
        {
            return Comment.GetArticleComments(articleId);
        }
        public void RemoveComment(string id, string pwd)
        {
            if (pwd != myPwd)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
                return;
            }
            Comment.RemoveComment(id);
        }

        #endregion

        public Rss20FeedFormatter GetFeed()
        {
            SyndicationFeed feed = new SyndicationFeed("abrie.net feed", "This is a feed for code articles on abrie.net", new System.Uri("http://abrie.net/code.aspx"));
            feed.Authors.Add(new SyndicationPerson("abrie@abrie.net"));
            feed.Categories.Add(new SyndicationCategory("sandbox"));

            feed.Items = Article.ArticleFeed();

            return new Rss20FeedFormatter(feed);
        }

        #endregion
    }
}

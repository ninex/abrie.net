using System.ServiceModel;
using System.ServiceModel.Web;
using abrie.netWeb.WCF.Entities;

namespace abrie.netWeb.WCF
{
    [ServiceContract(Namespace = "http://abrie.net")]
    public interface IDataService
    {
        [WebInvoke(Method = "POST",
            UriTemplate = "test",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        void test();

        #region Article

        [WebInvoke(Method = "POST",
            UriTemplate = "article?pwd={pwd}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        void SubmitArticle(Article article, string pwd);

        [WebInvoke(Method = "GET",
            UriTemplate = "article/{id}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        Article GetArticle(string id);

        [WebInvoke(Method = "GET",
            UriTemplate = "articles",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        Articles GetAllArticles();

        [WebInvoke(Method = "DELETE",
            UriTemplate = "articles/{id}?pwd={pwd}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        void RemoveArticle(string id, string pwd);

        #endregion

        #region Comment

        [WebInvoke(Method = "POST",
            UriTemplate = "comment",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        void SubmitComment(Comment comment);

        [WebInvoke(Method = "GET",
            UriTemplate = "comments/{articleId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        Comments GetArticleComments(string articleId);

        [WebInvoke(Method = "DELETE",
            UriTemplate = "comment/{id}?pwd={pwd}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        void RemoveComment(string id, string pwd);

        #endregion

        #region Feed
        [WebInvoke(Method = "GET",
            UriTemplate = "feed")]
        [OperationContract]
        System.ServiceModel.Syndication.Rss20FeedFormatter GetFeed();
        #endregion
    }
}

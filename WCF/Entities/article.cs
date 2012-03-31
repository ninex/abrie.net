using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using abrie.netWeb.WCF.DataAccessors;
using System.ServiceModel.Syndication;

namespace abrie.netWeb.WCF.Entities
{
    [DataContract(Namespace = "http://abrie.net/article")]
    public class Article
    {
        public static IDataStore dataStore;

        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Title;
        [DataMember]
        public string ArticleContent;
        [DataMember]
        public DateTime Created;
        [DataMember]
        public string DemoLink;
        [DataMember]
        public string Summary;


        public static void SubmitArticle(Article article)
        {
            article.Created = DateTime.Now;
            if (dataStore != null)
            {
                dataStore.Store(article);
            }
            else
            {
                DataAccessors.Article dataArticle;
                using (contentLINQDataContext context = new contentLINQDataContext())
                {
                    if (!string.IsNullOrEmpty(article.Id))
                    {
                        dataArticle = (from articles in context.Articles
                                       where articles.Id == int.Parse(article.Id)
                                       select articles).SingleOrDefault();
                        if (dataArticle != null)
                        {
                            dataArticle.ArticleContent = article.ArticleContent;
                            dataArticle.DemoLink = article.DemoLink;
                            dataArticle.Title = article.Title;
                            dataArticle.Summary = article.Summary;
                            context.SubmitChanges();
                            return;
                        }
                    }

                    dataArticle = new DataAccessors.Article()
                    {
                        ArticleContent = article.ArticleContent,
                        Created = article.Created,
                        DemoLink = article.DemoLink,
                        Title = article.Title,
                        Summary = article.Summary
                    };
                    context.Articles.InsertOnSubmit(dataArticle);
                    context.SubmitChanges();
                }
            }
        }

        public static Article GetArticle(string id)
        {
            if (dataStore != null)
            {
                return dataStore.Get<Article>(id);
            }
            else
            {
                using (contentLINQDataContext context = new contentLINQDataContext())
                {
                    return (from article in context.Articles
                            where article.Id == int.Parse(id)
                            select new Article
                            {
                                ArticleContent = article.ArticleContent,
                                Created = article.Created.Value,
                                DemoLink = article.DemoLink,
                                Title = article.Title,
                                Id = article.Id.ToString(),
                                Summary = article.Summary
                            }).SingleOrDefault();
                }
            }
        }

        public static Articles GetAllArticles()
        {
            if (dataStore != null)
            {
                return new Articles(dataStore.GetAll<Article>().OrderByDescending(p => p.Created).ToList());
            }
            else
            {
                using (contentLINQDataContext context = new contentLINQDataContext())
                {
                    return new Articles((from article in context.Articles
                                         orderby article.Created descending
                                         select new Article
                                         {
                                             ArticleContent = article.ArticleContent,
                                             Created = article.Created.Value,
                                             DemoLink = article.DemoLink,
                                             Title = article.Title,
                                             Id = article.Id.ToString(),
                                             Summary = article.Summary
                                         }).ToList());
                }
            }
        }
        public static void RemoveArticle(string id)
        {
            if (dataStore != null)
            {
                dataStore.Delete<Article>(id);
            }
            else
            {
                using (contentLINQDataContext context = new contentLINQDataContext())
                {
                    DataAccessors.Article dataArticle = (from article in context.Articles
                                                         where article.Id == int.Parse(id)
                                                         select article).SingleOrDefault();
                    if (dataArticle != null)
                    {
                        context.Articles.DeleteOnSubmit(dataArticle);
                        context.SubmitChanges();
                    }
                }
            }
        }
        public static List<System.ServiceModel.Syndication.SyndicationItem> ArticleFeed()
        {
            using (contentLINQDataContext context = new contentLINQDataContext())
            {
                var feed = new List<SyndicationItem>((from article in context.Articles
                                                      orderby article.Created descending
                                                      select new SyndicationItem
                                                     {
                                                         Content = TextSyndicationContent.CreateHtmlContent(article.ArticleContent),
                                                         LastUpdatedTime = article.Created.Value,
                                                         Title = TextSyndicationContent.CreatePlaintextContent(article.Title),
                                                         Id = article.Id.ToString(),
                                                         Summary = TextSyndicationContent.CreateHtmlContent(article.Summary)
                                                     }).ToList());
                foreach (var item in feed)
                {
                    item.Links.Add(SyndicationLink.CreateAlternateLink(new Uri("http://abrie.net/code.aspx#" + item.Id)));
                }
                return feed;
            }
        }
    }

    [CollectionDataContract(Namespace = "http://abrie.net/articles")]
    public class Articles : List<Article>
    {
        public Articles() { }
        public Articles(List<Article> articles) : base(articles) { }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using abrie.netWeb.WCF.DataAccessors;

namespace abrie.netWeb.WCF.Entities
{
    [DataContract(Namespace = "http://abrie.net/comment")]
    public class Comment
    {
        public static IDataStore dataStore;

        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Submitter;
        [DataMember]
        public string Text;
        [DataMember]
        public DateTime Created;
        [DataMember]
        public string ArticleId;

        public static void SubmitComment(Comment comment)
        {
            comment.Created = DateTime.Now;
            if (dataStore != null)
            {
                dataStore.Store(comment, comment.Id);
            }
            else
            {
                using (contentLINQDataContext context = new contentLINQDataContext())
                {
                    DataAccessors.Comment dataComment = new DataAccessors.Comment()
                    {
                        Submitter = comment.Submitter,
                        Text = comment.Text,
                        Created = comment.Created,
                        ArticleId = int.Parse(comment.ArticleId)
                    };
                    context.Comments.InsertOnSubmit(dataComment);
                    context.SubmitChanges();
                }
            }
        }

        public static Comments GetArticleComments(string articleId)
        {
            if (dataStore != null)
            {
                return new Comments(dataStore.GetAll<Comment>().Where(p => p.ArticleId.Equals(articleId)).ToList());
            }
            else
            {
                using (contentLINQDataContext context = new contentLINQDataContext())
                {
                    return new Comments((from comment in context.Comments
                                         where comment.ArticleId == int.Parse(articleId)
                                         select new Comment
                                         {
                                             Submitter = comment.Submitter,
                                             Text = comment.Text,
                                             Created = comment.Created.Value,
                                             ArticleId = comment.ArticleId.ToString(),
                                             Id = comment.Id.ToString()
                                         }).ToList());
                }
            }
        }
        public static void RemoveComment(string id)
        {
            if (dataStore != null)
            {
                dataStore.Delete<Comment>(id);
            }
            else
            {
                using (contentLINQDataContext context = new contentLINQDataContext())
                {
                    DataAccessors.Comment dataComment = (from comment in context.Comments
                                                         where comment.Id == int.Parse(id)
                                                         select comment).SingleOrDefault();
                    if (dataComment != null)
                    {
                        context.Comments.DeleteOnSubmit(dataComment);
                        context.SubmitChanges();
                    }
                }
            }
        }
    }

    [CollectionDataContract(Namespace = "http://abrie.net/comments")]
    public class Comments : List<Comment>
    {
        public Comments() { }
        public Comments(List<Comment> comments) : base(comments) { }
    }
}
using ArticleHub_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArticleHub_backend.Controllers
{
    [RoutePrefix("article")]
    public class ArticleController : ApiController
    {
        ArticleHubEntities db = new ArticleHubEntities();
        [HttpPost, Route("add")]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Add([FromBody] Article article)
        {
            try
            {
                article.publication_date = DateTime.Now;
                db.Articles.Add(article);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { message = "Article added Sucessufully" });
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        [HttpGet, Route("get")]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Get()
        {
            try
            {
                var result = db.Articles
                    .Join(db.Categories,
                    article => article.categoryId,
                    category => category.id,
                    (article, category) => new
                    {
                        article.id,
                        article.title,
                        article.content,
                        article.status,
                        article.publication_date,
                        categoryId = category.id,
                        categoryName = category.name
                    })
                    .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                // Créez un message d'erreur et retournez-le avec un code HTTP 500
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        [HttpGet, Route("getPublic")]
        public HttpResponseMessage GetPublic()
        {
            try
            {
                var result = db.Articles
                    .Join(db.Categories,
                    article => article.categoryId,
                    category => category.id,
                    (article, category) => new
                    {
                        article.id,
                        article.title,
                        article.content,
                        article.status,
                        article.publication_date,
                        categoryId = category.id,
                        categoryName = category.name
                    })
                    .Where(a => a.status == "published")
                    .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                // Créez un message d'erreur et retournez-le avec un code HTTP 500
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        [HttpPost, Route("update")]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Update([FromBody] Article article)
        {
            try
            {
                Article artObj = db.Articles
                    .Find(article.id);
                if (artObj == null)
                {

                    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Article is does not found" });

                }
                artObj.title = article.title;
                artObj.content = article.content;
                artObj.categoryId = article.categoryId;
                artObj.publication_date = DateTime.Now;
                artObj.status = article.status;
                db.Entry(artObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { message = "Article update Sucessufully" });

            }
            catch (Exception e)
            {
                // Créez un message d'erreur et retournez-le avec un code HTTP 500
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        [HttpGet, Route("delete/{id}")]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Article artObj = db.Articles
                    .Find(id);
                if (artObj == null)
                {

                    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Article is does not found" });

                }
                db.Articles.Remove(artObj);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { message = "Article delete Sucessufully" });

            }
            catch (Exception e)
            {
                // Créez un message d'erreur et retournez-le avec un code HTTP 500
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

        }

    }

}

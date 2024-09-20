using ArticleHub_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArticleHub_backend.Controllers
{
    [RoutePrefix("category")]

    public class CategoryController : ApiController

    {
        ArticleHubEntities db = new ArticleHubEntities();
        [HttpPost, Route("add")]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Add([FromBody] Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { message = "Category added Sucessufully" });
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
                var result = db.Categories
                    .OrderBy(u => u.name)
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
        public HttpResponseMessage Update([FromBody] Category category)
        {
            try
            {
                Category catObj = db.Categories
                    .Find(category.id);
                if (catObj == null)
                {

                    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Category is does not found" });

                }
                catObj.name = category.name;
                db.Entry(catObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { message = "Category update Sucessufully" });

            }
            catch (Exception e)
            {
                // Créez un message d'erreur et retournez-le avec un code HTTP 500
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

        }



    }


}

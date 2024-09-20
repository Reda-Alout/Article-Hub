using ArticleHub_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArticleHub_backend.Controllers
{
    [RoutePrefix("appUser")]
    public class AppuserController : ApiController
    {
        ArticleHubEntities db = new ArticleHubEntities();
        [HttpPost, Route("login")]
        public HttpResponseMessage Login([FromBody] Appuser appuser)
        {
            try
            {
                Appuser userObj = db.Appusers
                    .Where(u => (u.email == appuser.email && u.password == appuser.password)).FirstOrDefault();
                if (userObj != null)
                {
                   // System.Diagnostics.Debug.WriteLine("24");
                    if (userObj.status == "true")
                    {
                        //System.Diagnostics.Debug.WriteLine("27");
                        return Request.CreateResponse(HttpStatusCode.OK, new { token = TokenManager.GenerateToken(userObj.email, userObj.isDeletable) });
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Wait for admin approval");
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, new { message = "Wait for admin approval" });
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Incorrect Email or Password");
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { message = "Incorrect Email or Password" });
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("44");
                System.Diagnostics.Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = e.Message });
            }

        }
        
        
        [HttpPost, Route("add")]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Add([FromBody] Appuser appuser)
        {
            try
            {
                Appuser userObj = db.Appusers
                    .Where(u => (u.email == appuser.email)).FirstOrDefault();
                if (userObj == null)
                {
                    appuser.status = "false";
                    appuser.isDeletable = "true";
                    db.Appusers.Add(appuser);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new { message = "Sucessufully Registerd" });

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "Email Already Exist" });
                }
            }
            catch (Exception e)
            {
                // Créez un message d'erreur et retournez-le avec un code HTTP 500
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        [HttpGet, Route("get")]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Get()
        {
            try
            {
                var token = Request.Headers.GetValues("authorization").First();
                TokenClaim tokenClaim = TokenManager.ValidateToken(token);


                if (tokenClaim.isDeletable == "false")
                {
                    var result = db.Appusers
                        .Select(u => new { u.id, u.name, u.email, u.status, u.isDeletable })
                        .Where(u => (u.isDeletable == "true"))
                        .ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, result);

                }
                else
                {
                    var result = db.Appusers
                         .Select(u => new { u.id, u.name, u.email, u.status, u.isDeletable })
                         .Where(u => (u.isDeletable == "true") && u.email != tokenClaim.Email)
                         .ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception e)
            {
                // Créez un message d'erreur et retournez-le avec un code HTTP 500
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        [HttpPost, Route("updateStatus")]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Updatestatus([FromBody] Appuser appuser)
        {
            try
            {
                Appuser userObj = db.Appusers
                    .FirstOrDefault(u => u.id == appuser.id && u.isDeletable == "true");
                if (userObj == null)
                {

                    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "User is does not found" });

                }
                userObj.status = appuser.status;
                db.Entry(userObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { message = "User update Sucessufully" });

            }
            catch (Exception e)
            {
                // Créez un message d'erreur et retournez-le avec un code HTTP 500
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        [HttpPost, Route("update")]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Update([FromBody] Appuser appuser)
        {
            try
            {
                Appuser userObj = db.Appusers
                    .FirstOrDefault(u => u.id == appuser.id && u.isDeletable == "true");
                if (userObj == null)
                {

                    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "User is does not found" });

                }
                userObj.name = appuser.name;
                userObj.email = appuser.email;
                db.Entry(userObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { message = "User update Sucessufully" });

            }
            catch (Exception e)
            {
                // Créez un message d'erreur et retournez-le avec un code HTTP 500
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

        }


    }


}

﻿using API_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_CRUD.Controllers
{
    public class CRUDController : ApiController
    {
       
        [HttpPost]
        [Route("add")]
        public string add(API_profile pro)
        {
            Application1Entities api = new Application1Entities();
            API_profile obj = new API_profile();
            obj.id = pro.id;
            obj.name = pro.name;
            obj.address =pro.address;
            api.API_profile.Add(obj);
            api.SaveChanges();
            return ("Success");
        }

        //display
        [HttpGet]
        [Route("detail")]
        public IHttpActionResult detail()
        {
            List<API_profile> api_pofiles;
            using (var context = new Application1Entities())
            {
                api_pofiles= context.API_profile.ToList();
            }
                return Ok(api_pofiles);

        }

        //search
        [HttpGet]
        [Route("search")]
        public IHttpActionResult search(int sid)
        {
            API_profile api_search;
            using (var context = new Application1Entities())
            {
                api_search = context.API_profile.Where(x =>x.id==sid).FirstOrDefault();
            }
            
            return Ok(api_search);
          
        }
       
        //update
        [HttpPost]
        [Route("update")]
        public string update(API_profile pro)
        {
            Application1Entities api = new Application1Entities();
            API_profile obj = api.API_profile.Single(x => x.id == pro.id);
            obj.name = pro.name;
            obj.address = pro.address;
            api.SaveChanges();
            return ("Success");
        }

        //Delete
        [HttpPost]
        [Route("delete")]
        public string delete(API_profile pro)
        {
            Application1Entities api = new Application1Entities();
            API_profile obj = api.API_profile.Single(x => x.id == pro.id);
            api.API_profile.Remove(obj);
            api.SaveChanges();
            return ("Success");
        }

        //example of HttpResponseMessage
        //[HttpGet]
        //[Route("try")]
        //public HttpResponseMessage exm(int a)
        //{
        //    if(a==1)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, a);
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "only 1");
        //    }
        //}
    }
}

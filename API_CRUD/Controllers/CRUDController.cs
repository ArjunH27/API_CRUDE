using API_CRUD.Models;
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
        //insert
        [HttpPost][Route("add")]
        public string add(int pid,string pname,string paddress)
        {
            Application1Entities api = new Application1Entities();
            API_profile obj = new API_profile();
            obj.id = pid;
            obj.name = pname;
            obj.address = paddress;
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
            List<API_profile> api_search;
            using (var context = new Application1Entities())
            {
                api_search = context.API_profile.Where(x =>x.id==sid).ToList();
            }
            return Ok(api_search);
          
        }
       
        //update
        [HttpPost]
        [Route("update")]
        public string update(int uid,string uname,string uaddress)
        {
            Application1Entities api = new Application1Entities();
            API_profile obj = api.API_profile.Single(x => x.id == uid);
            obj.name = uname;
            obj.address = uaddress;
            api.SaveChanges();
            return ("Success");
        }

        //Delete
        [HttpPost]
        [Route("delete")]
        public string delete(int did)
        {
            Application1Entities api = new Application1Entities();
            API_profile obj = api.API_profile.Single(x => x.id == did);
            api.API_profile.Remove(obj);
            api.SaveChanges();
            return ("Success");
        }
    }
}

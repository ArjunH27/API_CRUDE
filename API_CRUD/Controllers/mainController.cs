using API_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace API_CRUD.Controllers
{
    public class mainController : Controller
    {
        //DISPLAY
        // GET: main
        public ActionResult Index()
        {
            IEnumerable<API_profile> profiles = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57591/");
                var responseTask = client.GetAsync("detail");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<IList<API_profile>>();
                readTask.Wait();
                profiles = readTask.Result;
            }
                return View(profiles);
        }

        //CREATE
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(API_profile pro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57591/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("add",pro);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(pro);
        }

        //[HttpGet]
        //public ActionResult Edit()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            API_profile obj = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57591/");
                //HTTP GET
                var responseTask = client.GetAsync("search?sid=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<API_profile>();
                    readTask.Wait();

                    obj = readTask.Result;
                }
            }

            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(API_profile pro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57591/");

                //HTTP POST
                var putTask = client.PostAsJsonAsync("update", pro);
                putTask.Wait();


                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(pro);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            API_profile obj = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57591/");
                //HTTP GET
                var responseTask = client.GetAsync("search?sid=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<API_profile>();
                    readTask.Wait();

                    obj = readTask.Result;
                }
            }

            return View(obj);
        }


        [HttpPost]
        public ActionResult Delete(API_profile pro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57591/");

                //HTTP DELETE
                var deleteTask = client.PostAsJsonAsync("delete",pro);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }
    }
}
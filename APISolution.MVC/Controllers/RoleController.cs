using APISoluton.Application.Service.CacheServices;
using APISoluton.Database.ViewModel.RoleView;
using APISoluton.Database.ViewModel.UserView.UserViewShow;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Text;

namespace APISolution.MVC.Controllers
{
    public class RoleController : Controller
    {
         private readonly  HttpClient _httpClient;
        Uri basse = new Uri("http://localhost:5081/api");
        private readonly CacheServices _cache;
        private readonly string key = "Role";
        public RoleController(CacheServices cache )
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = basse;
            _cache = cache;
        }
        [HttpGet]
        public IActionResult Index(int pageNumber=1 , int pageSize =3)
        {
            List<RoleVM>list = new List<RoleVM>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress+ $"/Role?pageNumber={pageNumber}&pageSize={pageSize}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<RoleVM>>(data);
            }
            return View(list);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateRole(RoleVM model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data,Encoding.UTF8,"application/json");
                HttpResponseMessage response= _httpClient.PostAsync(_httpClient.BaseAddress +"/Role", content).Result;
                if (response.IsSuccessStatusCode) { 
                    return RedirectToAction("Index");  
                }
                _cache.Remove(key);
            }
            catch (Exception ex) {
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id) {
            try
            {
                RoleVM role = new RoleVM();
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Role/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    role = JsonConvert.DeserializeObject<RoleVM>(data);
                }
                _cache.Remove(key);
                return View(role);
            }
            catch (Exception ex)
            {
                throw;
            }
                
        }
        [HttpPost]
        public IActionResult Edit(RoleVM  model)
        {
            http://localhost:5081/api/Role/7
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data,Encoding.UTF8,"application/json");
            HttpResponseMessage response =_httpClient.PutAsync(_httpClient.BaseAddress + $"/Role/{model.Id}",content).Result;
            if (response.IsSuccessStatusCode) {
                return RedirectToAction("Index");
            }
            return View();  
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                RoleVM role = new RoleVM();
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Role/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    role = JsonConvert.DeserializeObject<RoleVM>(data);
                }
                return View (role);
            }
            catch (Exception e)
            {
                return View("Error");
            }
           
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                http://localhost:5081/api/Role?id=5
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + $"/Role?id={id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View();
        }

    }
}

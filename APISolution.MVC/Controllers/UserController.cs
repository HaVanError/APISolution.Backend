using APISoluton.Database.ViewModel.RoleView;
using APISoluton.Database.ViewModel.UserView.UserViewShow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Text;

namespace APISolution.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        Uri basse = new Uri("http://localhost:5081/api");
        public UserController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = basse;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            List<UserVMShowAll> list = new List<UserVMShowAll>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"/User?pageNumber={pageNumber}&pageSize={pageSize}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<UserVMShowAll>>(data);
            }
            return View(list);
        }
       
        [HttpGet]
        public IActionResult Create() {
            var roles = GetRole(); // Hàm này lấy danh sách roles từ cơ sở dữ liệu hoặc nguồn dữ liệu khác
            ViewBag.Roles = new SelectList(roles, "RoleId", "RoleName");
            return View();
        }
        //[HttpPost]
        //public IActionResult Create(UserVMShowAll model)
        //{
        //    //try
        //    //{
        //    //    //var roles = GetRole(); // Hàm này lấy danh sách roles từ cơ sở dữ liệu hoặc nguồn dữ liệu khác
        //    //    //ViewBag.Roles = new SelectList(roles, "RoleId", "RoleName");
        //    //    string data = JsonConvert.SerializeObject(model);
        //    //    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        //    //    HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/User", content).Result;
        //    //    if (response.IsSuccessStatusCode)
        //    //    {
        //    //        return RedirectToAction("Index");
        //    //    }

        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return View();
        //    //}
        //    //return View();
        //    try
        //    {
        //        string data = JsonConvert.SerializeObject(model);
        //        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = _httpClient.PostAsync("/User", content).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", "Error creating user: " + ex.Message);
        //    }

        //    var roles = GetRole();
        //    ViewBag.Roles = new SelectList(roles, "RoleId", "RoleName");
        //    return View(model);

        //}
        [HttpPost]
        public IActionResult Create(UserVMShowAll model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync("/User", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating user: " + ex.Message);
            }

            var roles = GetRole();
            if (roles == null)
            {
                roles = new List<RoleVM>(); // Ensure roles is not null
            }
            ViewBag.Roles = new SelectList(roles, "RoleId", "RoleName");
            return View(model);
        }
        private List<RoleVM> GetRole()
        {
            List<RoleVM> list = new List<RoleVM>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"/Role?pageNumber={1}&pageSize={2}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<RoleVM>>(data);
            }
            return list;
        }
    }
}

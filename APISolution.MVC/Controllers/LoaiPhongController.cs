using APISolution.MVC.Models.DichVu;
using APISoluton.Application.Service.CacheServices;
using APISoluton.Database.ViewModel.DichVuView;
using APISoluton.Database.ViewModel.LoaiPhongView;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace APISolution.MVC.Controllers
{
    public class LoaiPhongController : Controller
    {
        private readonly HttpClient _httpClient;
        Uri basse = new Uri("http://localhost:5081/api");
        private readonly CacheServices _cache;
        private readonly string key = "LoaiPhong";
        public LoaiPhongController(CacheServices cache)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = basse;
            _cache = cache;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 2)
        {
            List<LoaiPhongVM> list = new List<LoaiPhongVM>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"/LoaiPhong?pageNumber={pageNumber}&pageSize={pageSize}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<LoaiPhongVM>>(data);
            }
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(LoaiPhongVM model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/LoaiPhongVM", content).Result;
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
                _cache.Remove(key);
            }
            catch (Exception ex)
            {
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                LoaiPhongVM dichVu = new LoaiPhongVM();
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/LoaiPhong/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    dichVu = JsonConvert.DeserializeObject<LoaiPhongVM>(data);
                }

                return View(dichVu);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        [HttpPost]
        public IActionResult Edit(LoaiPhongVM model)
        {

            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + $"/DichVu/{model.Id}", content).Result;
            if (response.IsSuccessStatusCode)
            {
                _cache.Remove(key);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                DichVuVM role = new DichVuVM();
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/DichVu/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    role = JsonConvert.DeserializeObject<DichVuVM>(data);
                }
                return View(role);
            }
            catch (Exception e)
            {
                return View("Error");
            }

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteRole(int id)
        {
            try
            {

                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + $"/DichVu?id={id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                _cache.Remove(key);
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View();
        }
    }
}

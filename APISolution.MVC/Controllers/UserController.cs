using APISolution.Database.Entity;
using APISolution.MVC.Extensions;
using APISoluton.Database.ViewModel.UserView.UserViewShow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;

namespace APISolution.MVC.Controllers
{
    public class UserController : Controller
    {
         Uri bass = new Uri("http://localhost:5081/api");
        private readonly HttpClient _httpClient;
       // private readonly ApiManager _apiService;
        public UserController( )
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = bass; 

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string apiUrl = "/User";
         
            List<UserVMShowAll> list = new List<UserVMShowAll>();

           HttpResponseMessage response =  _httpClient.GetAsync(_httpClient.BaseAddress + apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<UserVMShowAll>>(data);
            }
          
            return View(list);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Database.ViewModel.UserView.UserViewShow
{
    public class UserVMShowAll
    {
        [Required]
        [DisplayName("Tên Người Dùng")]
        public string? Name { get; set; }
        [Required]
        [DisplayName("Đia chỉ")]
        public string? Address { get; set; }
        [Required]
        [DisplayName("Thành phố")]
        public string? City { get; set; }
        [Required]
        [DisplayName("Email")]
        public string? Email { get; set; }
        [Required]
        [DisplayName("Mật Khẩu")]
        public string? Password { get; set; }
        [Required]
        [DisplayName("Quyền")]
        public string? Role { get; set; }
    }
}

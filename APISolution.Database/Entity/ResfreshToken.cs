using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Entity
{
    public class ResfreshToken
    {
        public int IdToken { get; set; } 
        public string RoleName {  get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
        public string Accesstoken {  get; set; } = string.Empty;
        public string Resfreshtoken {  get; set; } = string.Empty;
        public int idUser { get; set; }
        public User User { get; set; }
        
        
      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Entity
{
    public class Role
    {
        public int IdRole {  get; set; }
        public string NameRole {  get; set; } = string.Empty;
        public string MoTa {  get; set; } = string.Empty;
        public List <User> User { get; set; } = default!;
    }
}

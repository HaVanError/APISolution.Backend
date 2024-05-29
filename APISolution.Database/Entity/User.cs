using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string ? Email {  get; set; }
        public  string ? Password {  get; set; }
        public string? Name { get; set; }
        public string? Address {  get; set; }
        public string? City { get; set; }
        public int IdRole {  get; set; }
        public  Role Role { get; set; } =default!;
        public ResfreshToken ResfreshToken { get; set; } = default!;

    }
}

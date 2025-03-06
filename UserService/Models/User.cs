using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class User
    {
        private static int _nextId = 1;
        public int Id { get; set;}
        public string Name { get; set;}
        public string Email { get; set;}
    

        public User()
        {
            Id = _nextId++;
        }
    }
}
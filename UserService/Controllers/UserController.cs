using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private static List<User> Users = new List<User>();

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = Users.FirstOrDefault(u=>u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            Users.Add(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id, user});
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User updaredUser)
        {
            var user = Users.FirstOrDefault(u=>u.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            user.Name = updaredUser.Name;
            user.Email = updaredUser.Email;
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var user = Users.FirstOrDefault(u =>u.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            Users.Remove(user);
            return NoContent();
        }
    }
}
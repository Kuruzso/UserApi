using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Models;
using static UserApi.Models.Dto;

namespace UserApi.Controllers
{
    [Route("Users/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]

        public ActionResult<List<User>> Get() {
            using (var context = new UserDbContext())
            {

                return StatusCode(201, context.NewUser.ToList());

            }

               
        }

        [HttpPost]
        public ActionResult<User> Post(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = createUserDto.Name,
                Age = createUserDto.Age,
                License = createUserDto.License,
            };
            using (var context = new UserDbContext())
            {
                context.NewUser.Add(user);
                context.SaveChanges();

                return StatusCode(201,user);

            }
        }
        [HttpPut("{azon}")]
        public ActionResult<User> Put(Guid azon,UpdateUserDto updateUserDto)
        {
          
            using (var context = new UserDbContext())
            {
                var existingUser = context.NewUser.FirstOrDefault(x => x.Id == azon);

                existingUser.Name = updateUserDto.Name;
                existingUser.Age = updateUserDto.Age;
                existingUser.License = updateUserDto.License;

                context.NewUser.Update(existingUser);
                context.SaveChanges();

                return StatusCode(200,existingUser);
            }
        }
        [HttpDelete("{azon}")]
        public ActionResult<object> Delete(Guid azon)
        {
            using (var context = new UserDbContext()) {
            
                var existingUser = context.NewUser.FirstOrDefault(x =>x.Id == azon);
                if (existingUser == null) {
                    return NotFound(new{ message = "Nincs!" });

                }

                context.NewUser.Remove(existingUser);
                context.SaveChanges();
                return StatusCode(200, new { message = "Sikeres Törlés!" });
            }
            
           


        }
        [HttpGet("{azon}")]

            public ActionResult<object> Get(Guid azon)
            {
                using (var context = new UserDbContext())
                {

                    var existingUser = context.NewUser.FirstOrDefault(x => x.Id == azon);
                    if (existingUser == null)
                    {
                        return NotFound(new { message = "Nincs!" });

                    }

                   
                    return StatusCode(200, existingUser);
                }




            }
          


        
    }
}

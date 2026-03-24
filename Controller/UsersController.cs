using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using API_Kazakov.Contexts;
using API_Kazakov.Models;

namespace API_Kazakov.Controller
{
    [Microsoft.AspNetCore.Mvc.Route("api/usersController")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="Login">Логин пользователя</param>
        /// <param name="Password">Пароль пользователя</param>
        /// <returns>Данный метод преднозначен для авторизации пользователя на сайте</returns>
        /// /// <response code="200">Пользователь успешно авторизирован</response>
        /// /// <response code="403">Ошибка запроса данные не указаны</response>
        /// /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("SignIn")]
        [HttpPost]
        [ProducesResponseType(typeof(Users), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]

        public ActionResult SignIn([FromForm] string Login, [FromForm] string Password)
        {
            if (Login == null || Password == null)
                return StatusCode(403);

            try
            {
                Users User = new UsersContext().Users.Where(x => x.Login == Login &&
                x.Password == Password).First();

                return Ok(User);
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("RegIn")]
        public ActionResult RegIn([FromForm] string Login, [FromForm] string Password)
        {
            if (Login == null || Password == null)
                return StatusCode(403);

            try
            {
                using (var db = new UsersContext())
                {
                    if (db.Users.Any(u => u.Login == Login))
                    {
                        return BadRequest("Пользователь с таким логином уже существует");
                    }

                    var newUser = new Users
                    {
                        Login = Login,
                        Password = Password,
                    };
                    db.Add(newUser);
                    db.SaveChanges();

                    return Ok(newUser);
                }
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }
    } 
}

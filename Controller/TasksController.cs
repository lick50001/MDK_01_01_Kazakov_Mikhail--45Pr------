using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using API_Kazakov.Contexts;
using API_Kazakov.Models;
using Task = API_Kazakov.Models.Task;

namespace API_Kazakov.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        [HttpGet("List")]
        [ProducesResponseType(typeof(List<Task>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List()
        {
            try
            {
                using (var db = new TaskContext())
                {
                    var tasksList = db.Tasks.ToList();
                    return Ok(tasksList);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + " | " + ex.InnerException?.Message);
            }
        }

        [HttpGet("Item")]
        [ProducesResponseType(typeof(Task), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(int Id)
        {
            try
            {
                using (var db = new TaskContext())
                {
                    var task = db.Tasks.FirstOrDefault(x => x.Id == Id);
                    if (task == null) return NotFound();
                    return Ok(task);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
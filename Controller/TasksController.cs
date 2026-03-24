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
    [ApiExplorerSettings(GroupName = "v1")]
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
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")] 
        [ProducesResponseType(typeof(Task), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult GetTaskById(int id)
        {
            try
            {
                using (var db = new TaskContext())
                {
                    var task = db.Tasks.FirstOrDefault(t => t.Id == id);

                    if (task == null)
                    {
                        return NotFound(new { message = $"Задача с Id={id} не найдена" });
                    }

                    return Ok(task);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("search")] 
        [ProducesResponseType(typeof(List<Task>), 200)]
        [ProducesResponseType(500)]
        public ActionResult SearchTasks([FromQuery] string search) 
        {
            try
            {
                using (var db = new TaskContext())
                {
                    if (string.IsNullOrEmpty(search))
                    {
                        return Ok(db.Tasks.ToList());
                    }

                    var foundTasks = db.Tasks
                        .Where(t => t.Name.ToLower().Contains(search.ToLower()))
                        .ToList();

                    return Ok(foundTasks);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
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
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Метод добавления задачи
        /// </summary>
        /// <param name="task">Данные о задачи</param>
        /// <returns>Статус выполнения запроса</returns>
        /// <remarks>Данный метод добавляет задачу в базу данных</remarks>
        [HttpPut("Add")]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        
        public ActionResult Add([FromForm]Task task)
        {
            try
            {
                TaskContext taskContext = new TaskContext();
                taskContext.Tasks.Add(task);
                taskContext.SaveChanges();
                return StatusCode(200);
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Метод изменения задачи
        /// </summary>
        /// <param name="task">Новые данные задачи</param>
        [HttpPut("Update")]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult Update([FromForm] Task task)
        {
            try
            {
                using (var db = new TaskContext())
                {
                    var existingTask = db.Tasks.FirstOrDefault(x => x.Id == task.Id);

                    if (existingTask == null)
                    {
                        return NotFound(new { message = $"Задача с Id={task.Id} не найдена для обновления" });
                    }

                    existingTask.Name = task.Name;
                    existingTask.Property = task.Property;
                    existingTask.DataExcute = task.DataExcute;
                    existingTask.Done = task.Done;

                    db.SaveChanges();

                    return Ok(new { message = "Задача успешно обновлена" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
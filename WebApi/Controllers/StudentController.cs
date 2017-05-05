using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Abstract;
using Service.DataContracts;
using Service.Extensions;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/student
        [HttpGet]
        public IActionResult Get(string keyWord, string sortColumn, bool desc, int? pageSize = 20, int? pageIndex = 0, string fields = "")
        {
            try
            {
                pageSize = pageSize > 0 ? pageSize : 20;
                pageIndex = pageIndex >= 0 ? pageIndex : 0;

                var students = _studentService.Get(pageSize.Value, pageIndex.Value, keyWord, sortColumn, desc);
                if (students != null)
                {
                    foreach (var item in students)
                    {
                        item.SetSerializableProperties(fields);
                    }
                }
                return Json(students, new JsonSerializerSettings { ContractResolver = new SerializeContractResolver() });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/student/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var student = _studentService.GetById(id);
                if (student == null)
                {
                    return NotFound();
                }
                return Json(student, new JsonSerializerSettings { ContractResolver = new SerializeContractResolver() });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/student
        [HttpPost]
        public IActionResult Post([FromBody] StudentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _studentService.Create(model);
                    return Created("","Created Success");
                }
                string[] errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray();
                return BadRequest(errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/student/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]StudentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _studentService.Edit(model);
                    return Accepted();
                }
                string[] errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray();
                return BadRequest(errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/student/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _studentService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
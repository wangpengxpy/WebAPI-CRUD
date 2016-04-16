using Core;
using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI之CRUD.Controllers
{

    public class StudentController : ApiController
    {
        public IRepository<Student> _repository;
        public EFDbContext _ctx;
        public StudentController()
        {
            _ctx = new EFDbContext("DBByConnectionString");
            _repository = new RepositoryService<Student>(_ctx, true);
        }
        public IEnumerable<Student> GetAllStudent()
        {


            var list = (from stu in _repository.GetList() select new { Name = stu.Name, Flower = stu.Flower, Id = stu.Id }).ToList();
            
            return list;

        }

        public Student GetStudentById(int id)
        {
            var student = _repository.GetById(id);
            if (student == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
                return student;
        }


        public HttpResponseMessage PostStudent(Student stu)
        {

            var insertStudent = _repository.Insert(stu);
            var response = Request.CreateResponse<Student>(HttpStatusCode.Created, stu);

            string uri = Url.Link("DefaultApi", new { id = stu.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }


        public void PutStudent(int id, Student stu)
        {
            stu.Id = id;
            if (_repository.Update(stu) <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteStudent(int id)
        {
            Student stu = _repository.GetById(id);
            if (stu == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _repository.Delete(stu);
        }
    }
}

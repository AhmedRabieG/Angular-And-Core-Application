using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcTaskManager.Models;

namespace MvcTaskManager.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private TaskManagerDbContext db;

        public ProjectsController(TaskManagerDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("api/projects")]
        public List<Plan> Get()
        {
            
            List<Plan> plans = db.plans.ToList();
            return plans;
        }

        [HttpGet]
        [Route("api/projects/search/{searchby}/{searchtext}")]
        public List<Plan> Search(string searchBy, string searchText)
        {
            
            List<Plan> projects = null;
            if (searchBy == "ProjectID")
                projects = db.plans.Where(temp => temp.ProjectID.ToString().Contains(searchText)).ToList();
            else if (searchBy == "ProjectName")
                projects = db.plans.Where(temp => temp.ProjectName.Contains(searchText)).ToList();
            else if (searchBy == "DateOfStart")
                projects = db.plans.Where(temp => temp.DateOfStart.ToString().Contains(searchText)).ToList();
            else if (searchBy == "TeamSize")
                projects = db.plans.Where(temp => temp.TeamSize.ToString().Contains(searchText)).ToList();

            return projects;
        }

        [HttpPost]
        [Route("api/projects")]
        public Plan Post([FromBody] Plan plan)
        {
            
            db.plans.Add(plan);
            db.SaveChanges();
            return plan;
        }

        [HttpPut]
        [Route("api/projects")]
        public Plan Put([FromBody] Plan project)
        {
            
            Plan existingProject = db.plans.Where(temp => temp.ProjectID == project.ProjectID).FirstOrDefault();
            if (existingProject != null)
            {
                existingProject.ProjectName = project.ProjectName;
                existingProject.DateOfStart = project.DateOfStart;
                existingProject.TeamSize = project.TeamSize;
                db.SaveChanges();
                return existingProject;
            }
            else
            {
                return null;
            }
        }

        [HttpDelete]
        [Route("api/projects")]
        public int Delete(int ProjectID)
        {
            
            Plan existingProject = db.plans.Where(temp => temp.ProjectID == ProjectID).FirstOrDefault();
            if (existingProject != null)
            {
                db.plans.Remove(existingProject);
                db.SaveChanges();
                return ProjectID;
            }
            else
            {
                return -1;
            }
        }
    }
}



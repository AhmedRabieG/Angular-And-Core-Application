using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcTaskManager.Models;
using MvcTaskManager.ViewModels;

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get()
        {
            System.Threading.Thread.Sleep(1000);
            List<Plan> projects = db.plans.Include("ClientLocation").ToList();

            List<PlanViewModel> projectsViewModel = new List<PlanViewModel>();
            foreach (var project in projects)
            {
                projectsViewModel.Add(new PlanViewModel() 
                { ProjectID = project.ProjectID, 
                  ProjectName = project.ProjectName,
                  TeamSize = project.TeamSize, 
                  DateOfStart = project.DateOfStart.ToString("dd/MM/yyyy"), 
                  Active = project.Active, ClientLocation = project.ClientLocation, 
                  ClientLocationID = project.ClientLocationID,
                  Status = project.Status });
            }
            return Ok(projectsViewModel);
        }

        [HttpGet]
        [Route("api/projects/search/{searchby}/{searchtext}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Search(string searchBy, string searchText)
        {
            List<Plan> projects = null;
            if (searchBy == "ProjectID")
                projects = db.plans.Include("ClientLocation").Where(temp => temp.ProjectID.ToString().Contains(searchText)).ToList();
            else if (searchBy == "ProjectName")
                projects = db.plans.Include("ClientLocation").Where(temp => temp.ProjectName.Contains(searchText)).ToList();
            if (searchBy == "DateOfStart")
                projects = db.plans.Include("ClientLocation").Where(temp => temp.DateOfStart.ToString().Contains(searchText)).ToList();
            if (searchBy == "TeamSize")
                projects = db.plans.Include("ClientLocation").Where(temp => temp.TeamSize.ToString().Contains(searchText)).ToList();

            List<PlanViewModel> projectsViewModel = new List<PlanViewModel>();
            foreach (var project in projects)
            {
                projectsViewModel.Add(new PlanViewModel() 
                {   ProjectID = project.ProjectID, 
                    ProjectName = project.ProjectName, 
                    TeamSize = project.TeamSize, 
                    DateOfStart = project.DateOfStart.ToString("dd/MM/yyyy"), 
                    Active = project.Active, ClientLocation = project.ClientLocation,
                    ClientLocationID = project.ClientLocationID, Status = project.Status });
            }

            return Ok(projectsViewModel);
        }

        [HttpPost]
        [Route("api/projects")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Post([FromBody] Plan project)
        {
            project.ClientLocation = null;
            db.plans.Add(project);
            db.SaveChanges();

            Plan existingProject = db.plans.Include("ClientLocation").Where(temp => temp.ProjectID == project.ProjectID).FirstOrDefault();
            PlanViewModel projectViewModel = new PlanViewModel()
            {   ProjectID = existingProject.ProjectID,
                ProjectName = existingProject.ProjectName, 
                TeamSize = existingProject.TeamSize,
                DateOfStart = existingProject.DateOfStart.ToString("dd/MM/yyyy"),
                Active = existingProject.Active,
                ClientLocation = existingProject.ClientLocation, 
                ClientLocationID = existingProject.ClientLocationID,
                Status = existingProject.Status };

            return Ok(projectViewModel);
        }

        [HttpPut]
        [Route("api/projects")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Put([FromBody] Plan project)
        {
            Plan existingProject = db.plans.Where(temp => temp.ProjectID == project.ProjectID).FirstOrDefault();
            if (existingProject != null)
            {
                existingProject.ProjectName = project.ProjectName;
                existingProject.DateOfStart = project.DateOfStart;
                existingProject.TeamSize = project.TeamSize;
                existingProject.Active = project.Active;
                existingProject.ClientLocationID = project.ClientLocationID;
                existingProject.Status = project.Status;
                existingProject.ClientLocation = null;
                db.SaveChanges();

                Plan existingProject2 = db.plans.Include("ClientLocation").Where(temp => temp.ProjectID == project.ProjectID).FirstOrDefault();
                PlanViewModel projectViewModel = new PlanViewModel()
                {   ProjectID = existingProject2.ProjectID,
                    ProjectName = existingProject2.ProjectName,
                    TeamSize = existingProject2.TeamSize,
                    ClientLocationID = existingProject2.ClientLocationID,
                    DateOfStart = existingProject2.DateOfStart.ToString("dd/MM/yyyy"),
                    Active = existingProject2.Active, Status = existingProject2.Status,
                    ClientLocation = existingProject2.ClientLocation };

                    return Ok(projectViewModel);
            }
            else
            {
                return null;
            }
        }

        [HttpDelete]
        [Route("api/projects")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

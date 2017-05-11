using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using data;
using data.Projects;
using webMvcWithAngular.Models;

namespace webMvcWithAngular.Controllers
{
    public class ProjectController : Controller
    {
        DataContext dc = new DataContext();
       

        static List<ProjectModel> _projects = null;

        public ProjectController()
        {
           
        }

        public ActionResult Index()
        {
            return View();
        }


        // GET: All Customer
        [HttpGet]
        public JsonResult GetAllData(string pram1)
        {


            if (string.IsNullOrEmpty(pram1))
            {
                using (var context = new DataContext())
                {
                    var projets = context.Projects.Select(p => new ProjectModel { ProjectId = p.ProjectId, Description = p.Description, Title = p.Title });


                    return Json(projets.ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                using (var context = new DataContext())
                {
                    var projets = context
                        .Projects
                        .Where
                        (p => p.Title.ToLower().StartsWith(pram1.ToLower()))
                        .Select(p => new ProjectModel { ProjectId = p.ProjectId, Description = p.Description, Title = p.Title });


                    return Json(projets.ToList(), JsonRequestBehavior.AllowGet);
                }

            }

        }

        public ActionResult ProjectDetail(int? id)
        {
            return View("ProjectDetail");
        }
        

        // GET: Get Single Customer
        [HttpGet]
        public JsonResult GetProjectbyId(int id)
        {
            using (var context = new DataContext())
            {
                var project =
                    context
                        .Projects
                        .SingleOrDefault
                        (p =>
                            p.ProjectId ==
                            id);
                return Json(project, 
                    JsonRequestBehavior.AllowGet);
            }
            
        }
        
       
        [HttpPost]
        public ActionResult Save(ProjectModel project)
        {

        	if (project == null)
        	{
        		return 
                    new HttpStatusCodeResult
                        (500, 
                        "Impossible de créer un projet vide");
        	}
            
            //Ajout d'un nouveau projet
        	if (project.ProjectId <=0)
        	{
        	    using (var context = new DataContext())
        	    {
                    if (
                     context
                     .Projects
                     .SingleOrDefault
                     (p =>
                         p.Title.ToLower() ==
                         project.Title.ToLower()) != null)
                    {
                        return
                        new HttpStatusCodeResult(500,
                            "Projet existant!");
                    }

        	        context
                        .Projects
                        .Add(new Project() {Title = project.Title, 
                            Description = project.Description});
        	        context.SaveChanges();
        	    }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
        		
        	}

            //Mise à jour d'un projet 
            using (var context = new DataContext())
            {
                var projectToUpdate =
                    context
                        .Projects
                        .SingleOrDefault
                        (p =>
                            p.ProjectId ==
                            project.ProjectId);

                if (projectToUpdate == null)
                    return
                    new HttpStatusCodeResult(500,
                        "Projet non existant!");

                projectToUpdate.Title = project.Title;
                projectToUpdate.Description = project.Description;
                context.SaveChanges();

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }



        }
        //delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var context = new DataContext())
            {
                var projectToDelete =
                    context
                        .Projects
                        .SingleOrDefault
                        (p =>
                            p.ProjectId ==
                            id);

                if (projectToDelete == null)
                    return
                    new HttpStatusCodeResult(500,
                        "Projet non existant!");


                context.Projects.Remove(projectToDelete);
                context.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
                
            }
        }

    }
}

using data;
using data.Contenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web03.Models;

namespace web03.Controllers
{
    public class ContenuController : Controller
    {
        DataContext dc = new DataContext();

        static List<ContenuModel> _contenus = null;

        public ContenuController()
        {

        }


        //
        // GET: /Contenu/

        public ActionResult Index()
        {
            return View();
        }
       

        // GET: All Customer
        [HttpGet]
        public JsonResult GetAllData(string title,int? projectId)
        {


            if (string.IsNullOrEmpty(title) && projectId == null)
            {
                using (var context = new DataContext())
                {
                    var contenus = context.Contenus.Select(c => new ContenuModel
                    {
                        ContenuId = c.ContenuId,
                        Description = c.Description,
                        ProjectId = c.ProjectId,
                        ProjectTitle = c.Project.Title,
                        Title = c.Title
                    });
                    return Json(contenus.ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            
            else
            {
                using (var context = new DataContext())
                {
                    var contenus = context
                        .Contenus
                        .Where
                        (p => 

                        (string.IsNullOrEmpty(title) ||
                        p.Title.ToLower().StartsWith(title.ToLower())
                        )
                        &&
                        (
                        p.ProjectId == projectId || projectId == null
                        ))
                        .Select(c => new ContenuModel
                        {
                            ContenuId = c.ContenuId,
                            Description = c.Description,
                            ProjectId = c.ProjectId,
                            ProjectTitle = c.Project.Title,
                            Title = c.Title
                        });
                    return Json(contenus.ToList(), JsonRequestBehavior.AllowGet);
                }

            }
            
        }

        /*private new JsonResult Json(object p, JsonRequestBehavior allowGet)
        {
            throw new NotImplementedException();
        }*/

        public ActionResult ContenuDetail(int? id)
        {
            return View("ContenuDetail");
        }


        // GET: Get Single Customer
        [HttpGet]
        public JsonResult GetContenubyId(int id)
        {
            using (var context = new DataContext())
            {
                var contenu =
                    context
                        .Contenus.Select(c => new ContenuModel
                        {
                            ContenuId = c.ContenuId,
                            Description = c.Description,
                            ProjectId = c.ProjectId,
                            ProjectTitle = c.Project.Title,
                            Title = c.Title
                        })
                        .SingleOrDefault
                        (p =>
                            p.ContenuId ==
                            id);
                return Json(contenu,
                    JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        public ActionResult Save(ContenuModel contenu)
        {

            if (contenu == null)
            {
                return
                    new HttpStatusCodeResult
                        (500,
                        "Impossible de créer un contenu vide");
            }

            //Ajout d'un nouveau projet
            if (contenu.ContenuId <= 0)
            {
                using (var context = new DataContext())
                {
                    if (
                     context
                     .Contenus.Select(c => new ContenuModel
                     {
                         ContenuId = c.ContenuId,
                         Description = c.Description,
                         ProjectId = c.ProjectId,
                         ProjectTitle = c.Project.Title,
                         Title = c.Title
                     })
                     .SingleOrDefault
                     (p =>
                         p.Title.ToLower() ==
                         contenu.Title.ToLower()) != null)
                    {
                        return
                        new HttpStatusCodeResult(500,
                            "Contenu existant!");
                    }

                    context
                        .Contenus
                        .Add(new Contenu()
                        {
                            Title = contenu.Title,
                            Description = contenu.Description
                        });
                    context.SaveChanges();
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);

            }
            //Mise à jour d'un projet 
            using (var context = new DataContext())
            {
                var contenuToUpdate =
                    context
                        .Contenus.Select(c => new ContenuModel
                        {
                            ContenuId = c.ContenuId,
                            Description = c.Description,
                            ProjectId = c.ProjectId,
                            ProjectTitle = c.Project.Title,
                            Title = c.Title
                        })
                        .SingleOrDefault
                        (p =>
                            p.ContenuId ==
                            contenu.ContenuId);

                if (contenuToUpdate == null)
                    return
                    new HttpStatusCodeResult(500,
                        "Projet non existant!");

                contenuToUpdate.Title = contenu.Title;
                contenuToUpdate.Description = contenu.Description;
                context.SaveChanges();

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }



        }
        //delete
        [HttpPost]
        public ActionResult Delete(ContenuModel item)
        {
            using (var context = new DataContext())
            {
                var contenuToDelete =
                    context
                        .Contenus
                        .SingleOrDefault
                        (p =>
                            p.ContenuId ==
                            item.ContenuId);

                if (contenuToDelete == null)
                    return
                    new HttpStatusCodeResult(500,
                        "Projet non existant!");


                context.Contenus.Remove(contenuToDelete);
                context.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);

            }
        }

    }


}


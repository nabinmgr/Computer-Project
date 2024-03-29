﻿using ClassifiedAdvertising.Models.Data;
using ClassifiedAdvertising.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassifiedAdvertising.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            //Decalre list of PageVM
            List<PageVM> pagesList;

           
            using (Db db=new Db())
            {
                //Init the list
                pagesList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
            }
            return View(pagesList);
        }

        public ActionResult AddPage()
        {
            return View();
        }
    }
}
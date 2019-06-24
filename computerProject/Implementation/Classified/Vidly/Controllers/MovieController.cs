using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;
using System.Data.Entity;

namespace Vidly.Controllers
{
    [Authorize(Roles = RoleName.CanManageMovies)]
    public class MovieController : Controller
    {
        // GET: Movie
        private ApplicationDbContext _context;
        public MovieController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Random()
        {
            Movie movie = new Movie() { Name = "Family Guy" };
            List<Customer> customers = new List<Customer>
            {
                new Customer() {Name="Customer 1" },
                new Customer() {Name="Customer 2" }
            };
            MovieCustomerViewModel viewModel = new MovieCustomerViewModel()
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }
        //public ActionResult Edit(int movieID)
        //{
        //    return Content($"Id={movieID}");
        //}
        //public ActionResult Index(int? pageIndex,string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 1;
        //    }
        //    if (string.IsNullOrWhiteSpace(sortBy))
        //    {
        //        sortBy = "Random";
        //    }
        //    return Content(string.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
            return View("ReadOnlyList");
        }
        //private IEnumerable<Movie> getMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie() {Name="Family Guy" },
        //        new Movie() {Name="Sausage Party" }
        //    };
        //}
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year,int month)
        {
            return Content($"{ year}/{month}");
        }
        public ActionResult Details(int id)
        {
            var movie = _context.Movie.Include(m => m.Genre).SingleOrDefault(m => m.ID == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

      
        public ActionResult New()
        {
            var genre = _context.Genre.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genre = genre
            };
            return View("MovieForm",viewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genre = _context.Genre.ToList()
                };
            }
            if (movie.ID == 0)
                _context.Movie.Add(movie);
            else
            {
                var customerInDb = _context.Movie.Single(m => m.ID == movie.ID);

                customerInDb.Name = movie.Name;
                customerInDb.ReleasedDate = movie.ReleasedDate;
                customerInDb.GenreId = movie.GenreId;
                customerInDb.Stock = movie.Stock;

            }

            _context.SaveChanges();
            return RedirectToAction("Index","Movie");
        }
        public ActionResult Edit(int id)
        {
            var movie = _context.Movie.SingleOrDefault(m => m.ID == id);
            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel(movie)
            {
                Genre = _context.Genre.ToList()
            };
            return View("MovieForm",viewModel);
        }

    }
}
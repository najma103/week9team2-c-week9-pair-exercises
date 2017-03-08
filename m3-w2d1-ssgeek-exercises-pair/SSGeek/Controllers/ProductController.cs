using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSGeek.DAL;
using SSGeek.Models;

namespace SSGeek.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult GetProducts()
        {
            IProductDAL DAL = new ProductSqlDAL();
            List<Product> productList = DAL.GetProducts();
            
            return View("GetProducts", productList);
        }
    }
}
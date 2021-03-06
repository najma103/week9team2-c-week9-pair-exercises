﻿using System;
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

        public ActionResult ProductDetail(int id )
        {
            IProductDAL DAL = new ProductSqlDAL();
            List<Product> productList = DAL.GetProducts();
            Product model = null;

            foreach (Product product in productList)
            {
                if (id == product.ProductId)
                {
                    model = product;
                }
            }
            return View("ProductDetail", model);
        }

        //post shopping cart
        public ActionResult ViewCart()
        {
            IProductDAL DAL = new ProductSqlDAL();
            List<Product> productList = DAL.GetProducts();

            Cart();
            List<Product> currentCart = (List<Product>)Session["cart"];
            int quantity = Convert.ToInt32(Request.Params["quantity"]);
            int productid = Convert.ToInt32(Request.Params["productid"]);

            for (int i = 0; i < quantity; i++)
            {
                currentCart.Add(DAL.GetProduct(productid));
            }

            Session["cart"] = currentCart;


            return View("ViewCart", currentCart);
        }

        private void Cart()
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Product>();
            }
        }
    }
}
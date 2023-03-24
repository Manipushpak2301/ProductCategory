using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Products.Repository;
using Products.Models;
using PagedList;

namespace Products.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        public ActionResult IndexProd(int? page)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<ProductModel> pd = null;
            ProductModel pdm = new ProductModel();
            List<ProductModel> pdmList = new List<ProductModel>();
            
            ProductRepository prod = new ProductRepository();
            pdmList = prod.GetAllProducts();
            
            pd = pdmList.ToPagedList(pageIndex, pageSize);
            ModelState.Clear();
            return View(pd);
        }

        public ActionResult Create()
        {
            return View();
        }    
  

        [HttpPost]
        public ActionResult Create(ProductModel pd)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProductRepository prod = new ProductRepository();

                    if (prod.AddProd(pd))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET:  
        public ActionResult Edit(int id)
        {
            ProductRepository prod = new ProductRepository();
            return View(prod.GetAllProducts().Find(pd => pd.ProductId == id));
        }

        // POST:  
        [HttpPost]
        public ActionResult Edit(int id, ProductModel obj)
        {
            try
            {
                ProductRepository prod = new ProductRepository();
                prod.UpdateProduct(obj);
                return RedirectToAction("IndexProd");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                ProductRepository prod = new ProductRepository();
                if (prod.DeleteProduct(id))
                {
                    ViewBag.AlertMsg = "Employee details deleted successfully";

                }
                return RedirectToAction("IndexProd");

            }
            catch
            {
                return View();
            }
        }
  
	}
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.Models
{
    public class ProductModel
    {
       
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ProductModel()
        {

        }
        
        public List<ProductModel> PdList { get; set; }
    }
}
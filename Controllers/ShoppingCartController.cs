﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Orchard.ContentManagement;
using Orchard.Mvc;
using Orchard.Themes;
using Orchard.Webshop.Models;
using Orchard.Webshop.Services;

namespace Orchard.Webshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCart _shoppingCart;
        private readonly IOrchardServices _services;

        public ShoppingCartController(IShoppingCart shoppingCart, IOrchardServices services)
        {
            _shoppingCart = shoppingCart;
            _services = services;
        }

        [HttpPost]
        public ActionResult Add(int id)
        {
            // Add the specified content id to the shopping cart with a quantity of 1
            _shoppingCart.Add(id, 1);

            // Redirect the user to the Index action
            return RedirectToAction("Index");
        }

        [Themed]
        public ActionResult Index()
        {
            // Create a new shape using the "New" property of IOrchardServices
            var shape = _services.New.ShoppingCart(
                Products: _shoppingCart.GetProducts().Select(p => _services.New.ShoppingCartItem(
                    ProductPart: p.ProductPart,
                    Quantity: p.Quantity,
                    Title: _services.ContentManager.GetItemMetadata(p.ProductPart).DisplayText)
                ).ToList(),
                Total: _shoppingCart.Total(),
                Subtotal: _shoppingCart.Subtotal(),
                Vat: _shoppingCart.Vat()
            );

            // Return a ShapeResult. This is more opitmal because it can be override
            // with themes as shape alternatives
            return new ShapeResult(this, shape);
        }
    }
}

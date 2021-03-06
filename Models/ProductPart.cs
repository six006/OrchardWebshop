﻿using Orchard.ContentManagement;

namespace Orchard.Webshop.Models
{
    public class ProductPart : ContentPart<ProductPartRecord>
    {
        public decimal Price
        {
            get { return Retrieve(r => r.Price); }
            set { Store(r => r.Price, value); }
        }

        public string Sku
        {
            get { return Retrieve(r => r.Sku); }
            set { Store(r => r.Sku, value); }
        }
    }
}

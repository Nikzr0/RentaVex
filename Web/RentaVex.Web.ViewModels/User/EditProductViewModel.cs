namespace RentaVex.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using RentaVex.Data.Models;

    public class EditProductViewModel
    {
        public Product Product { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
    }
}
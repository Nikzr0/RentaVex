namespace RentaVex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RentaVex.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extention { get; set; } // What type is the image -->> (.png or etc.)

        public int AddedByUserId { get; set; } // Niki said that it should be string but it didn't work so I changed it :)

        public virtual Product Product { get; set; }

        public ApplicationUser AddedByUser { get; set; }


        // The content must be stored in the file system.
    }
}

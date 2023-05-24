﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace tzz.Model
{
    public class Products
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public string Description { get; set; }


        [NotMapped] public ImageSource QRCode { get; set; }

    }
}

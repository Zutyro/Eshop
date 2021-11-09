using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Turecky.Eshop.Web.Models.Entity
{
    [Table(nameof(CarouselItem))]
    public class CarouselItem
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [StringLength(255)]
        [Required]
        public string ImageSrc { get; set; }

        [StringLength(50)]
        public string ImageAlt { get; set; }

    }
}

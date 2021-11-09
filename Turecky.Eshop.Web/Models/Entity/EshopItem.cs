using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Turecky.Eshop.Web.Models.Entity
{
    [Table(nameof(EshopItem))]
    public class EshopItem
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
    }
}

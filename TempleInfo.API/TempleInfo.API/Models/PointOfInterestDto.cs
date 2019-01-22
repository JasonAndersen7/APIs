using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TempleInfo.API.Models
{
    public class PointOfInterestDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You need to provide a name value")]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}

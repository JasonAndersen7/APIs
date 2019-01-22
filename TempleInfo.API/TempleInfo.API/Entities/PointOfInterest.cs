using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TempleInfo.API.Entities
{


    public class PointOfInterest
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(200)]
        public string Description { get; set; }
        
        //parent type of point of interest
        [ForeignKey("templeId")]
        public Temple temple { get; set; }

        public int templeId { get; set; }


    }
}

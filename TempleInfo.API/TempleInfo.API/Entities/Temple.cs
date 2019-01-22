using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TempleInfo.API.Entities
{
    public class Temple
    {
        [Key] //declares the primary key or identifier of this class explicitly
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // new automatic key will be used
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

    
        public ICollection<PointOfInterest> PointsOfInterest { get; set; }
        = new List<PointOfInterest>();

    }
}

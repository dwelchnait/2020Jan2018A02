using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace ChinookSystem.Data.Entities
{
    //identify the sql entity (table) this class maps
    [Table("Artists")]
    public class Artist
    {
        private string _Name;

        //check your sql entity for type of pkey (identity or other)
        [Key]
        public int ArtistId { get; set; }

        //fully implement nullable strings (course standard)
        //check if the sql entity attribute has any constraints
        [StringLength(120, ErrorMessage ="Artist Name is limited to 120 characters")]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                //if (string.IsNullOrEmpty(value))
                //{
                //    _Name = null;
                //}
                //else
                //{
                //    _Name = value;
                //}

                _Name = string.IsNullOrEmpty(value) ? null : value;
            }
        }

        //[NotMapped] properties

        //navigational properties
        public virtual ICollection<Album> Albums { get; set; }
    }
}

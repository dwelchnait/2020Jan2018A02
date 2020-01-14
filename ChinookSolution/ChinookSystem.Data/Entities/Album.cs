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
    [Table("Albums")]
    public class Album
    {
        private string _ReleaseLabel;

        [Key]
        public int AlbumId { get; set; }
        [Required(ErrorMessage ="Album title is required.")]
        [StringLength(160, ErrorMessage ="Album title is limited to 160 characters")]
        public string Title { get; set; }
       // [Required(ErrorMessage ="Artist Id is required")]
        [Range(1,int.MaxValue,ErrorMessage =("Artist Id out of range"))]
        public int ArtistId { get; set; }

        public int ReleaseYear { get; set; }
        [StringLength(50, ErrorMessage ="Album release label is limited to 50 characters")]
        public string ReleaseLabel
        {
            get
            {
                return _ReleaseLabel;
            }
            set
            {
                _ReleaseLabel = string.IsNullOrEmpty(value) ? null : value;
            }
        }

        //notmapped properties
        [NotMapped]
        public string ReleaseInfo
        {
            get
            {
                return string.IsNullOrEmpty(ReleaseLabel) ? ReleaseYear + " (unknown)" :
                    ReleaseYear + " (" + ReleaseLabel + ")";
            }
        }

        //navigational properties
        public virtual Artist Artist { get; set; }
        //public virtual ICollection<Track> Tracks { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
#endregion

namespace ChinookSystem.BLL
{
    public class ArtistController
    {
        //basic query: complete list of DbSet
        public List<Artist> Artist_List()
        {
            //set up the code block to ensure the release of the sql connection
            using(var context = new ChinookContext())
            {
                //.ToList<T> is used to convert the DbSet<T> into a List<T> collection
                return context.Artists.ToList();
            }

        }

        //basic query: return a recorded based on pkey
        public Artist Artist_FindByID (int artistid)
        {
            using(var context = new ChinookContext())
            {
                return context.Artists.Find(artistid);
            }
        }
    }
}

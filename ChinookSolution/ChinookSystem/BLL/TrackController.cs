using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
using DMIT2018Common.UserControls;
using ChinookSystem.Data.POCOs;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TrackList> List_TracksForPlaylistSelection(string tracksby, string arg)
        {
            using (var context = new ChinookContext())
            {
                List<TrackList> results = null;
                int id = 0;
                if (int.TryParse(arg, out id))
                {
                    //lookup for MediaType or Genre
                    results = (from x in context.Tracks
                               where (tracksby.Equals("MediaType") && x.MediaTypeId == id)
                                 || (tracksby.Equals("Genre") && x.GenreId == id)
                               orderby x.Name
                               select new TrackList
                               {
                                   TrackID = x.TrackId,
                                   Name = x.Name,
                                   Title = x.Album.Title,
                                   ArtistName = x.Album.Artist.Name,
                                   MediaName = x.MediaType.Name,
                                   GenreName = x.Genre.Name,
                                   Composer = x.Composer,
                                   Milliseconds = x.Milliseconds,
                                   Bytes = x.Bytes,
                                   UnitPrice = x.UnitPrice
                               }).ToList();
                }
                else
                {
                    //lookup for artist or album
                    results = (from x in context.Tracks
                               where (tracksby.Equals("Artist") && x.Album.Artist.Name.Contains(arg))
                                 || (tracksby.Equals("Album") && x.Album.Title.Contains(arg))
                               orderby x.Name
                               select new TrackList
                               {
                                   TrackID = x.TrackId,
                                   Name = x.Name,
                                   Title = x.Album.Title,
                                   ArtistName = x.Album.Artist.Name,
                                   MediaName = x.MediaType.Name,
                                   GenreName = x.Genre.Name,
                                   Composer = x.Composer,
                                   Milliseconds = x.Milliseconds,
                                   Bytes = x.Bytes,
                                   UnitPrice = x.UnitPrice
                               }).ToList();
                }
              
                        

                return results;
            }
        }//eom

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Track_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track Track_Find(int trackid)
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.Find(trackid);
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Track_GetByAlbumId(int albumid)
        {
            using (var context = new ChinookContext())
            {
                var results = from aRowOn in context.Tracks
                              where aRowOn.AlbumId.HasValue
                              && aRowOn.AlbumId == albumid
                              select aRowOn;
                return results.ToList();
            }
        }
    }
}

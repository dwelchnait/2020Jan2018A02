using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel; //ODS
using DMIT2018Common.UserControls;  //used by error handle user control
using ChinookSystem.Data.POCOs;
using ChinookSystem.Data.DTOs;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class PlayListController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<ClientPlayList> Playlist_GetBySize(int playlistsize)
        {
            using (var context = new ChinookContext())
            {
                var plresults = from pl in context.Playlists
                                where pl.PlaylistTracks.Count() == playlistsize
                                select new ClientPlayList
                                {
                                    Name = pl.Name,
                                    TrackCount = pl.PlaylistTracks.Count(),
                                    PlayTime = pl.PlaylistTracks.Sum(pltrk => pltrk.Track.Milliseconds),
                                    PlaylistSongs = from strk in pl.PlaylistTracks
                                                    orderby strk.Track.Genre.Name
                                                    select new PlayListSong
                                                    {
                                                        SongName = strk.Track.Name,
                                                        Genre = strk.Track.Genre.Name
                                                    }
                                };
                return plresults.ToList();
            }
        }
    }
}

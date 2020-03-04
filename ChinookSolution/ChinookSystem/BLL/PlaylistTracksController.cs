using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.DTOs;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {
                List<UserPlaylistTrack> results = (from x in context.PlaylistTracks
                                                  where x.Playlist.Name.Equals(playlistname)
                                                    && x.Playlist.UserName.Equals(username)
                                                  orderby x.TrackNumber
                                                  select new UserPlaylistTrack
                                                  {
                                                      TrackID = x.TrackId,
                                                      TrackNumber = x.TrackNumber,
                                                      TrackName = x.Track.Name,
                                                      Milliseconds = x.Track.Milliseconds,
                                                      UnitPrice = x.Track.UnitPrice
                                                  }).ToList();

                return results;
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookContext())
            {
                //trx
                //query the PlayList table to see if the playlistname exists
                //if not
                //    create an instance of PlayList
                //    load
                //    add
                //    set tracknumber to 1
                //if yes
                //    query PlaylistTrack for track id
                //    if found
                //       yes:throw an error
                //       no:query Playlist to max tracknumber, increment++
                //create an instance of PlaylistTrack
                //load
                //add
                //save changes
                int tracknumber = 0;
                PlaylistTrack newtrack = null;
                Playlist exists = (from x in context.Playlists
                                  where x.Name.Equals(playlistname)
                                      && x.UserName.Equals(username)
                                  select x).FirstOrDefault();
                if (exists == null)
                {
                    //new playlist
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    context.Playlists.Add(exists);
                    tracknumber = 1;
                }
                else
                {
                    //playlist that exists
                    newtrack = (from x in context.PlaylistTracks
                                where x.Playlist.Name.Equals(playlistname)
                                   && x.Playlist.UserName.Equals(username)
                                   && x.TrackId == trackid
                                select x).FirstOrDefault();


                }
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
               //code to go here


            }
        }//eom
    }
}

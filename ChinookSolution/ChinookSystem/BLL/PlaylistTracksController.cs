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
using DMIT2018Common.UserControls;
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
                //       no:query PlaylistTracks to max tracknumber, increment++
                //create an instance of PlaylistTrack
                //load
                //add
                //save changes
                List<string> errors = new List<string>();
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
                    context.Playlists.Add(exists);  //stages ONLY
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
                    if (newtrack == null)
                    {
                        //can add to playlist
                        tracknumber = (from x in context.PlaylistTracks
                                       where x.Playlist.Name.Equals(playlistname)
                                          && x.Playlist.UserName.Equals(username)
                                       select x.TrackNumber).Max();
                        tracknumber++;
                    }
                    else
                    {
                        //track already on playlist
                        //business rule states duplicate tracks not allowed
                        //violates the business rule

                        //throw an exception
                        //throw new Exception("Track already on the playlist. Duplicates not allowed.");

                        //throw a Business Rule Exception
                        //collect the errors into a List<string>
                        //After all validation is done, test the collection (List<T>) for
                        //    having any messages, if so, throw new BusinessRuleException()
                        errors.Add("Track already on the playlist. Duplicates not allowed.");
                    }

                }

                //all validation of Playlist and Playlisttrack is complete
                if (errors.Count > 0)
                {
                    throw new BusinessRuleException("Adding a Track", errors);
                }
                else
                {
                    //create/load/add a PlaylistTrack
                    newtrack = new PlaylistTrack();
                    
                    newtrack.TrackId = trackid;
                    newtrack.TrackNumber = tracknumber;
                    exists.PlaylistTracks.Add(newtrack);  //stage ONLY,USE THE PARENT

                    context.SaveChanges();  //physical addition
                }
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 
                //business rules need to be executed within your BLL
                var exists = (from x in context.Playlists
                              where x.UserName.Equals(username)
                                 && x.Name.Equals(playlistname)
                              select x).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("Play list has been remove from the system.");

                }
                else
                {
                    var moveTrack = (from x in exists.PlaylistTracks
                                     where x.TrackId == trackid
                                     select x).FirstOrDefault();
                    if (moveTrack == null)
                    {
                        throw new Exception("Song on play list has been remove from the system.");
                    }
                    else
                    {
                        //try moving
                        //check movement is still possible
                        //determine direction
                        PlaylistTrack otherTrack = null;
                        if (direction.Equals("up"))
                        {
                            //up
                            if (moveTrack.TrackNumber == 1)
                            {
                                throw new Exception("Song on play list already at the top.");
                            }
                            else
                            {
                                //prep for move
                                otherTrack = (from x in exists.PlaylistTracks
                                                  where x.TrackNumber == moveTrack.TrackNumber - 1
                                                  select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("Missing required other song track record.");
                                }
                                else
                                {
                                    moveTrack.TrackNumber -= 1;
                                    otherTrack.TrackNumber += 1;
                                }
                            }
                        }
                        else
                        {
                            //down
                            if (moveTrack.TrackNumber == exists.PlaylistTracks.Count)
                            {
                                throw new Exception("Song on play list already at the bottom.");
                            }
                            else
                            {
                                //prep for move
                                otherTrack = (from x in exists.PlaylistTracks
                                                  where x.TrackNumber == moveTrack.TrackNumber + 1
                                                  select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("Missing required other song track record.");
                                }
                                else
                                {
                                    moveTrack.TrackNumber += 1;
                                    otherTrack.TrackNumber -= 1;
                                }
                            }
                        }
                        //alter the database
                        //this is not a CRUD update where multiple fields on a record
                        //   could be altered and you do not know which fields to alter
                        //you have one field on the instance of the entity record
                        //transaction
                        context.Entry(moveTrack).Property("TrackNumber").IsModified = true;
                        context.Entry(otherTrack).Property("TrackNumber").IsModified = true;
                        context.SaveChanges();
                    }
                }
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

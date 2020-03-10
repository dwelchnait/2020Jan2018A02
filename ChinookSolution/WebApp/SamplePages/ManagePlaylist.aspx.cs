using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.POCOs;
#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                //message to the user
                MessageUserControl.ShowInfo("Entry Error", "Select an artist name or part of.");
            }
            else
            {
                TracksBy.Text = "Artist";
                SearchArg.Text = ArtistName.Text;
                TracksSelectionList.DataBind();
            }
        }

        protected void MediaTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MediaTypeDDL.SelectedIndex == 0)
            {
                //message to the user
                MessageUserControl.ShowInfo("Selection Error", "Enter a media type");
            }
            else
            {
                TracksBy.Text = "MediaType";
                SearchArg.Text = MediaTypeDDL.SelectedValue;
                TracksSelectionList.DataBind();
            }
        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Genre";
            SearchArg.Text = GenreDDL.SelectedValue;
            TracksSelectionList.DataBind();
            
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                //message to the user
                MessageUserControl.ShowInfo("Entry Error", "Enter an album title or part of.");
            }
            else
            {
                TracksBy.Text = "Album";
                SearchArg.Text = AlbumTitle.Text;
                TracksSelectionList.DataBind();
            }
        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //security is yet to be implemented
            //this page needs to known the username of the currently logged user
            //temporarily we will hard core the username
            string username = "HansenB";
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data","Enter the playlist name");
            }
            else
            {
                //Your code does NOT need to have a try/catch
                //The try/catch is embedded within MessageUserControl
                //The syntax for executing with MessageUserControl
                //   MessageUserControl.TryRun(() => { coding block},"Success Title","Success message");
                MessageUserControl.TryRun(( ) =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                    PlayList.DataSource = info;
                    PlayList.DataBind();
                },"Playlist","View current songs on playlist");
            }
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            if(PlayList.Rows.Count == 0)
            {
                MessageUserControl.ShowInfo("Track Movement", 
                    "You must have a play list showing. Fetch your play list");
            }
            else
            {
                if (string.IsNullOrEmpty(PlaylistName.Text))
                {
                    MessageUserControl.ShowInfo("Track Movement",
                    "You must have a play list name.");
                }
                else
                {
                    //determine is a single song on the playlist has been selected
                    //collect the trackid, tracknumber
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowsSelected = 0;
                    CheckBox songSelected = null; //reference pointer to a control

                    //traverse the song list
                    for(int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        //point to a checkbox on the gridview row
                        songSelected = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        //selected?
                        if (songSelected.Checked)
                        {
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                            rowsSelected++;
                        }
                    }
                    if (rowsSelected != 1)
                    {
                        MessageUserControl.ShowInfo("Track Movement",
                                "You must select a single song to move.");
                    }
                    else
                    {
                        if(tracknumber == PlayList.Rows.Count)
                        {
                            MessageUserControl.ShowInfo("Track Movement",
                                "Song is at the bottom of the list already. No move necessary.");
                        }
                        else
                        {
                            //move the track
                            MoveTrack(trackid, tracknumber, "down");
                        }
                    }
                }
            }
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            if (PlayList.Rows.Count == 0)
            {
                MessageUserControl.ShowInfo("Track Movement",
                    "You must have a play list showing. Fetch your play list");
            }
            else
            {
                if (string.IsNullOrEmpty(PlaylistName.Text))
                {
                    MessageUserControl.ShowInfo("Track Movement",
                    "You must have a play list name.");
                }
                else
                {
                    //determine is a single song on the playlist has been selected
                    //collect the trackid, tracknumber
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowsSelected = 0;
                    CheckBox songSelected = null; //reference pointer to a control

                    //traverse the song list
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        //point to a checkbox on the gridview row
                        songSelected = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        //selected?
                        if (songSelected.Checked)
                        {
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                            rowsSelected++;
                        }
                    }
                    if (rowsSelected != 1)
                    {
                        MessageUserControl.ShowInfo("Track Movement",
                                "You must select a single song to move.");
                    }
                    else
                    {
                        if (tracknumber == 1)
                        {
                            MessageUserControl.ShowInfo("Track Movement",
                                "Song is at the up of the list already. No move necessary.");
                        }
                        else
                        {
                            //move the track
                            MoveTrack(trackid, tracknumber, "up");
                        }
                    }
                }
            }
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            string username = "HansenB";
            //call BLL to move track
            MessageUserControl.TryRun(() =>
            {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack(username, PlaylistName.Text, trackid, direction);
                List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                PlayList.DataSource = info;
                PlayList.DataBind();
                //optionally flag the song that was moved,
                //traverse the GridView again looking for the trackid
            }, "Move Track", "Track has been moved");
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            string username = "HansenB";
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter the playlist name");
            }
            else
            {
               
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username,
                        int.Parse(e.CommandArgument.ToString()));
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                    PlayList.DataSource = info;
                    PlayList.DataBind();
                }, "Add track to Playlist", "Track has been added to the playlist");
            }
        }


    }
}
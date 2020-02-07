<Query Kind="Program">
  <Connection>
    <ID>8fae8096-aa9b-4a23-a6bc-00e0c98a4d7e</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	//Nested queries
	
	//a query within a query
	
	//the query is returned as an IEnumberable<T> or IQueryable<T>
	//if you need to return your query as a List<T> then you must
	//   encapsulate your query and add a .ToList() on the query
	//   (from ....).ToList()
	
	//.ToList() is also useful if you require your data in memory
	//   for some execution
	
	//Create a list of albums showing their title and artist.
	//Show only albums with 25 or more tracks.
	//Show the songs on the album (name and length)
	//Use strongly datatype elements of all data
	
	//   Artist          Album            Track
	//     .Name           .Title           List<T>  t:(name,length)
	
	var results = from x in Albums
					where x.Tracks.Count() >= 25
					select new MyPlayList
					{
						AlbumTitle = x.Title,
						ArtistName = x.Artist.Name,
						Songs = (from trk in x.Tracks
								select new Song
								{
									SongName = trk.Name,
									SongLength = trk.Milliseconds
								}).ToList()
					};
	//results.Dump();
	
	
	//Create a list of Playlist with x number of tracks.
	//Show the playlist name, count of tracks, total play time for 
	//   the playlist and the list of tracks on the playlist.
	//For each track show the song name and Genre.
	//Order the track list by Genre.
	//Use strong datatypes for all data.
	
	var playlistsize = 10;
	
	var plresults = from pl in Playlists
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
	plresults.Dump();
}

// Define other methods and classes here

//POCO: flat data collection, NO structures (List, arrays, structs)
public class Song
{
	public string SongName{get;set;}
	public int SongLength{get;set;}
}

public class PlayListSong
{
	public string SongName {get;set;}
	public string Genre {get;set;}
}

//DTO: everything of a POCO PLUS a structure

public class MyPlayList
{
	public string AlbumTitle{get;set;}
	public string ArtistName{get;set;}
	public List<Song> Songs {get;set;}
}

public class ClientPlayList
{
	public string Name {get;set;}
	public int TrackCount {get;set;}
	public int PlayTime {get;set;}
	public IEnumerable <PlayListSong> PlaylistSongs {get;set;}
}
<Query Kind="Expression">
  <Connection>
    <ID>2679fc39-7e05-4ba2-bc87-a4b797ce2943</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Aggregates

//aggregates work against collections (0, 1 or more records in a dataset)
//.Count(), .Sum(), .Min(), .Max(), .Average()
//aggregates can be used in both query syntax and method syntax

//list all albums showing the album title, album artist name,
//and the number of tracks on the album
//  Artists  ->  Albums -> Tracks
//      ICollection   ICollection     (parent -> child navigational property)
//         Artist       Album         (child -> parent navigational property)

//method syntax used for the aggregate
//query syntax for the overall query
from x in Albums
select new
{
	title = x.Title,
	artist = x.Artist.Name,
	trackcount = x.Tracks.Count()
}

//complete method syntax
Albums
	.Select(x => new {
						title = x.Title,
						artist = x.Artist.Name,
						trackcount = x.Tracks.Count()
						})

//query syntax
from x in Albums
select new
{
	title = x.Title,
	artist = x.Artist.Name,
	trackcount = (from y in x.Tracks
					select y).Count()
}

from x in Albums
select new
{
	title = x.Title,
	artist = x.Artist.Name,
	trackcount = (from y in Tracks
					where y.AlbumId == x.AlbumId
					select y).Count()
}

//list the artists and their number of albums
//order the list from most albums to least albums
//if the count is tied, order by artist name

//  Artists  ->  Albums 
//      ICollection        (parent -> child navigational property)
//         Artist          (child -> parent navigational property)

from x in Artists
orderby x.Albums.Count() descending, x.Name ascending
select new
{
	name = x.Name,
	albumcount = x.Albums.Count()

}

//find the maximum number of albums for all artists

//get a list of counts, find the max in that list
(from x in Artists
select 	x.Albums.Count()
).Max()

(Artists.Select(x => x.Albums.Count())).Max()

//produce a list of albums which have tracks showing their
//title, artist name, number of tracks on album and
//total price of all tracks for that album

// number of x : Count()
//total of x : Sum()

from x in Albums
select new
{
	title = x.Title,
	artist = x.Artist.Name,
	trackcount = x.Tracks.Count(),
	trackcost = x.Tracks.Sum(tr => tr.UnitPrice)
}

from x in Albums
select new
{
	title = x.Title,
	artist = x.Artist.Name,
	trackcount = (from y in x.Tracks
					select y).Count(),
	trackcost = (from y in Tracks
					where y.AlbumId == x.AlbumId
					select y.UnitPrice).Sum()
}

//list all the playlist which have a track showing
//the playlist name, number of tracks for the playlist
//the cost of the playlist, 
//and total storage size for the playlist in megabytes.

//Playlists         PlaylistTracks 		        Tracks
//   .Name       based on playlist.Count()	.UnitPrice, .Bytes
//		  ICollection					Track
//			Playlists				ICollection

from x in Playlists
where x.PlaylistTracks.Count() > 0
select new
{
	name = x.Name,
	numberoftracks = x.PlaylistTracks.Count(),
	cost = x.PlaylistTracks.Sum(plt => plt.Track.UnitPrice),
	storage = x.PlaylistTracks.Sum(plt => plt.Track.Bytes/1000000.0)
}












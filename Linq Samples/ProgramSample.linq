<Query Kind="Program">
  <Connection>
    <ID>0d2fa424-3494-49d8-b0e1-e4cc5b81b56d</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	var results = from x in Albums
				select new AlbumArtists
				{
					AlbumTitle = x.Title,
					Year = x.ReleaseYear,
					ArtistName = x.Artist.Name
				};
				
	//.Dump() is a LinqPad method ONLY
	results.Dump();
}

// Define other methods and classes here
public class AlbumArtists
{
	public string AlbumTitle{get;set;}
	public int Year {get;set;}
	public string ArtistName {get; set;}
}
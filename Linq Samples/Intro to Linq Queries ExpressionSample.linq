<Query Kind="Expression">
  <Connection>
    <ID>0d2fa424-3494-49d8-b0e1-e4cc5b81b56d</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

from x in Albums
select x

Albums
   .Select (x => x)
   
Albums

from x in Albums
where x.AlbumId == 5
select x

from x in Albums
where x.Title.Contains("de")
select x

from x in Employees
where !x.ReportsTo.HasValue
select x

from x in Albums
where x.Title.Contains("de")
orderby x.ReleaseYear descending, x.Title
select x

from x in Albums
select new
{
	AlbumTitle = x.Title,
	Year = x.ReleaseYear,
	ArtistName = x.Artist.Name
}










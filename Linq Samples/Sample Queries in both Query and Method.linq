<Query Kind="Expression">
  <Connection>
    <ID>2679fc39-7e05-4ba2-bc87-a4b797ce2943</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//query syntax
//from placeholder in datasource
//[where ...]
//[orderby ...]
//[group ...]
//select ...

//select all albums
from x in Albums
select x

//method syntax is written as a composite object expression
//datasource.[Where(placeholder => expression)]
//			.[OrderBy(placeholder => expression)]
//			.[OrderByDescending(placeholder => expression)]
//			.[ThenBy(placeholder => expression)]
//			.[Group(...)]
//          .Select(placeholder => expression)]

//select all albums
Albums.Select(x => x)


//navigational properties can be use in both query and method
//list all records from Albums ordered by descending Year
//   ascending Title release between 2007 and 2010 inclusive.

//the ordeby and where clauses can be entered in any order
from x in Albums
orderby x.ReleaseYear descending, x.Title 
where x.ReleaseYear >= 2007 && x.ReleaseYear <= 2010 
select x

Albums
	  .OrderByDescending(x => x.ReleaseYear)
	  .ThenBy(x => x.Title)
	  .Where(x => x.ReleaseYear >= 2007 && x.ReleaseYear <= 2010)
	  .Select(x => x)

//list all USA customers with an yahoo email address
//Order by lastname then firstname
//Show the customer full name, email address and phone

from c in Customers
where c.Country.Equals("USA") && c.Email.Contains("yahoo")
orderby c.LastName, c.FirstName
select new
{
	Name = c.LastName + ", " + c.FirstName,
	Email = c.Email,
	Phone = c.Phone
}

Customers.Where(c => c.Country.Equals("USA") && c.Email.Contains("yahoo"))
		.OrderBy(c => c.LastName)
		.ThenBy(c => c.FirstName)
		.Select(c => new
					{
						Name = c.LastName + ", " + c.FirstName,
						Email = c.Email,
						Phone = c.Phone
					})

//create an alphabetic list of albums by decades
// early (pre-1970), 70s, 80s, 90s, modern (>2000)
//list is the Title, Artist Name, Year and decade

from x in Albums
orderby x.Title
select new
		{
		 	Title = x.Title,
			Name = x.Artist.Name,
			Year = x.ReleaseYear,
			Decade = x.ReleaseYear < 1970 ? "early" :
					 x.ReleaseYear < 1980 ? "70s" :
					 x.ReleaseYear < 1990 ? "80s" :
					 x.ReleaseYear < 2000 ? "90s" : "modern"
		}













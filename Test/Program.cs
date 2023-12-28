
using Web.ORM.DbContext;
using Web.ORM.Entities;
using Web.ORM.Repository;

var dbc = new DbContext();

var rep = new BossesRepository();

var r = await rep.Select();

foreach (var s in r)
{
    Console.WriteLine(s.Name);
}
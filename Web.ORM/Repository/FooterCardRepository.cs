// using Web.Models;
// using Web.ORM.Interfaces;
//
// namespace Web.ORM.Repository;
//
// public class FooterCardRepository : IFooterCardRepository
// {
//     private readonly DbContext.DbContext _context;
//     public bool Add(FooterCard entity)
//         => _context.Add(entity);
//
//     public bool Update(FooterCard entity)
//         => _context.Update(entity);
//
//     public bool Delete(int id)   
//         => _context.Delete<FooterCard>(id);
//
//     public IEnumerable<FooterCard> Select(FooterCard entity)
//         => _context.Select(entity);
//
//     public FooterCard SelectById(int id)
//         => _context.SelectById<FooterCard>(id);
// }
using Microsoft.EntityFrameworkCore;
using SuperjackProducts.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperjackProducts.Api.Services
{

  public interface ITagService
  {

    IEnumerable<Tag> GetAll();
    Tag GetById(long id);
    Tag Create(Tag item);
    void Update(Tag newitem);
    void Delete(long id);

  }
  public class TagService : ITagService
  {
    private AppDbContext _context;

    public TagService(AppDbContext context)
    {
      _context = context;
    }

    public IEnumerable<Tag> GetAll()
    {
      return _context.Tags;

    }


    public Tag GetById(long id)
    {
      return _context.Tags.Find(id);
    }

    public Tag Create(Tag item)
    {

      _context.Tags.Add(item);
      _context.SaveChanges();

      return item;
    }

    public void Update(Tag newitem)
    {
      var item = _context.Tags.Find(newitem.Id);


      item.Title = newitem.Title;


      // update Tag properties

      _context.Tags.Update(item);
      _context.SaveChanges();
    }

    public void Delete(long id)
    {
      var item = _context.Tags.Find(id);
      if (item != null)
      {
        _context.Tags.Remove(item);
        _context.SaveChanges();
      }
    }

  }
}

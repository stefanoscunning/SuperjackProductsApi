using Microsoft.EntityFrameworkCore;
using SuperjackProducts.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperjackProducts.Api.Services
{

  public interface ILanguageService
  {

    IEnumerable<Language> GetAll();
    Language GetById(long id);
    Language Create(Language item);
    void Update(Language newitem);
    void Delete(long id);

  }
  public class LanguageService : ILanguageService
  {
    private AppDbContext _context;

    public LanguageService(AppDbContext context)
    {
      _context = context;
    }

    public IEnumerable<Language> GetAll()
    {
      return _context.Languages;

    }


    public Language GetById(long id)
    {
      return _context.Languages.Find(id);
    }

    public Language Create(Language item)
    {

      _context.Languages.Add(item);
      _context.SaveChanges();

      return item;
    }

    public void Update(Language newitem)
    {
      var item = _context.Languages.Find(newitem.Id);


      item.Title = newitem.Title;
      item.Culture = newitem.Culture;


      // update Language properties

      _context.Languages.Update(item);
      _context.SaveChanges();
    }

    public void Delete(long id)
    {
      var item = _context.Languages.Find(id);
      if (item != null)
      {
        _context.Languages.Remove(item);
        _context.SaveChanges();
      }
    }

  }
}

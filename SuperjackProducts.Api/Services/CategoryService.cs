using Microsoft.EntityFrameworkCore;
using SuperjackProducts.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperjackProducts.Api.Services
{

  public interface ICategoryService
  {

    IEnumerable<Category> GetAll();
    Category GetById(long id);
    Category Create(Category item);
    void Update(Category newitem);
    void Delete(long id);

  }
  public class CategoryService : ICategoryService
  {
    private AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
      _context = context;
    }

    public IEnumerable<Category> GetAll()
    {
      return _context.Categories;

    }


    public Category GetById(long id)
    {
      return _context.Categories.Find(id);
    }

    public Category Create(Category item)
    {

      _context.Categories.Add(item);
      _context.SaveChanges();

      return item;
    }

    public void Update(Category newitem)
    {
      var item = _context.Categories.Find(newitem.Id);


      item.Title = newitem.Title;
      

      // update Category properties

      _context.Categories.Update(item);
      _context.SaveChanges();
    }

    public void Delete(long id)
    {
      var item = _context.Categories.Find(id);
      if (item != null)
      {
        _context.Categories.Remove(item);
        _context.SaveChanges();
      }
    }

  }
}

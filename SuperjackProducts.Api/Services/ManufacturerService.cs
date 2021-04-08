using Microsoft.EntityFrameworkCore;
using SuperjackProducts.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperjackProducts.Api.Services
{

  public interface IManufacturerService
  {

    IEnumerable<Manufacturer> GetAll();
    Manufacturer GetById(long id);
    Manufacturer Create(Manufacturer item);
    void Update(Manufacturer newitem);
    void Delete(long id);

  }
  public class ManufacturerService : IManufacturerService
  {
    private AppDbContext _context;

    public ManufacturerService(AppDbContext context)
    {
      _context = context;
    }

    public IEnumerable<Manufacturer> GetAll()
    {
      return _context.Manufacturers;

    }


    public Manufacturer GetById(long id)
    {
      return _context.Manufacturers.Find(id);
    }

    public Manufacturer Create(Manufacturer item)
    {

      _context.Manufacturers.Add(item);
      _context.SaveChanges();

      return item;
    }

    public void Update(Manufacturer newitem)
    {
      var item = _context.Manufacturers.Find(newitem.Id);


      item.Title = newitem.Title;


      // update Manufacturer properties

      _context.Manufacturers.Update(item);
      _context.SaveChanges();
    }

    public void Delete(long id)
    {
      var item = _context.Manufacturers.Find(id);
      if (item != null)
      {
        _context.Manufacturers.Remove(item);
        _context.SaveChanges();
      }
    }

  }
}

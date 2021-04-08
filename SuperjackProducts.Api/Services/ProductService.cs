using Microsoft.EntityFrameworkCore;
using SuperjackProducts.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperjackProducts.Api.Services
{

  public interface IProductService
  {

    IEnumerable<Product> GetAll();
    Product GetById(long id);
    Product Create(Product item);
    void Update(Product newitem);
    void Delete(long id);

  }
  public class ProductService : IProductService
  {
    private AppDbContext _context;

    public ProductService(AppDbContext context)
    {
      _context = context;
    }

    public IEnumerable<Product> GetAll()
    {
      return _context.Products
        .Include(product => product.Manufacturer)
        .Include(product => product.ProductCategories).ThenInclude(productcategory => productcategory.Category)
        .Include(product => product.ProductTags).ThenInclude(producttag => producttag.Tag);

    }


    public Product GetById(long id)
    {
      return _context.Products
        .Where(product=>product.Id==id)
        .Include(product => product.Manufacturer)
        .Include(product => product.ProductCategories).ThenInclude(productcategory => productcategory.Category)
        .Include(product => product.ProductTags).ThenInclude(producttag => producttag.Tag)
        .FirstOrDefault();
    }

    public Product Create(Product item)
    {
     
      _context.Entry(item).State = EntityState.Detached;

      _context.Add(item);
      _context.SaveChanges();


      return GetById(item.Id);
    }

    public void Update(Product newitem)
    {
      var categories = newitem.ProductCategories.Select(x=>x.CategoryId);
      var tags = newitem.ProductTags.Select(x=>x.TagId);
      var categoryList = new List<ProductCategory>();
      var tagList = new List<ProductTag>();
      var catList = _context.ProductCategories.Where(td => td.ProductId == newitem.Id).ToArray();
      _context.RemoveRange(catList);

      var tList = _context.ProductTags.Where(td => td.ProductId == newitem.Id).ToArray();
      _context.RemoveRange(tList);

      var item = _context.Products.Find(newitem.Id);

      foreach(var c in categories)
      {
        categoryList.Add(new ProductCategory() { CategoryId = c, ProductId = newitem.Id });
      }

      foreach (var t in tags)
      {
        tagList.Add(new ProductTag() { TagId = t, ProductId = newitem.Id });
      }


      item.ManufacturerId = newitem.ManufacturerId;
      item.Name = newitem.Name;


      // update Product properties
      _context.AddRange(categoryList);
      _context.AddRange(tagList);
      _context.Products.Update(item);
      _context.SaveChanges();
    }

    public void Delete(long id)
    {
      var item = _context.Products.Find(id);
      if (item != null)
      {
        _context.Products.Remove(item);
        _context.SaveChanges();
      }
    }

  }
}

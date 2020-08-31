using AutoShopping.Models;
using AutoShopping.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopping.Repository
{
    public class AutoRepository : IAutoRepository
    {
        private readonly IHostingEnvironment _env;

        private readonly AutoShoppingDbContext _context;
        public AutoRepository(AutoShoppingDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<List<BrandViewModel>> GetAllBrands()
        {
            var result = await _context.Brands.Select(p => new BrandViewModel()
            {
                ID = p.ID,
                BrandName = p.BrandName,
                LogoFileName = p.LogoNameImg
            }).ToListAsync();

            return result;
        }
        public async Task<BrandVM> GetBrand(int? id)
        {
            var obj = new BrandVM();

            try
            {
                BrandViewModel data = await _context.Brands.Where(p => p.ID == id).Select(s => new BrandViewModel()
                {
                    ID = s.ID,
                    BrandName = s.BrandName,
                    LogoFileName = s.LogoNameImg
                }).FirstOrDefaultAsync();

                obj.Brand = data;
                obj.isAccept = true;
            }
            catch (Exception)
            {
                obj.isAccept = false;
            }

            return obj;
        }
        public async Task<bool> AddNewBrand(BrandViewModel viewModel)
        {
            try
            {
                List<string> img = null;

                if (viewModel.LogoImg != null)
                {
                    img = await AddImage("Brand", viewModel.LogoImg);
                }

                var brand = new Brand()
                {
                    BrandName = viewModel.BrandName,
                    LogoNameImg = img[0]
                };

                await _context.Brands.AddAsync(brand);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                var a = e.Message;
                return false;
            }
        }
        public async Task<bool> UpdateBrand(BrandViewModel viewModel)
        {
            try
            {
                var update = await _context.Brands.FirstOrDefaultAsync(p => p.ID == viewModel.ID);
                List<string> img = null;

                if (viewModel.LogoImg != null)
                {
                    img = await AddImage("Brand", viewModel.LogoImg);
                    update.LogoNameImg = img[0];
                }
                else
                {
                    string path = Path.Combine(_env.WebRootPath, "GalleryFile", "Brand", update.LogoNameImg);
                    File.Delete(path);
                    update.LogoNameImg = null;
                }

                update.BrandName = viewModel.BrandName;

                _context.Brands.Update(update);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteBrand(BrandViewModel viewModel)
        {
            try
            {
                var data = await _context.Brands.FirstOrDefaultAsync(p => p.ID == viewModel.ID);
                _context.Brands.Remove(data);
                await _context.SaveChangesAsync();

                string path = Path.Combine(_env.WebRootPath, "GalleryFile", "Brand", data.LogoNameImg);

                File.Delete(path);

                return true;
            }
            catch (Exception e)
            {
                var a = e;
                return false;
            }

        }
        public async Task<List<ModelViewModel>> GetAllModels()
        {
            var result = await _context.Models.Include(p => p.Brand).Select(s => new ModelViewModel()
            {
                ID = s.ID,
                ModelName = s.ModelName,
                BrandID = s.Brand.ID,
                BrandName = s.Brand.BrandName,
                BrandLogo = s.Brand.LogoNameImg
            }).ToListAsync();

            return result;
        }
        public async Task<bool> AddNewModel(ModelViewModel viewModel)
        {
            try
            {
                var model = new Model()
                {
                    ModelName = viewModel.ModelName,
                    BrandID = viewModel.BrandID
                };

                await _context.Models.AddAsync(model);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<ModelVM> GetModel(int? id)
        {
            var obj = new ModelVM();

            try
            {
                ModelViewModel data = await _context.Models.Include(p => p.Brand).Where(p => p.ID == id).Select(s => new ModelViewModel()
                {
                    ID = s.ID,
                    ModelName = s.ModelName,
                    BrandID = s.Brand.ID,
                    BrandName = s.Brand.BrandName

                }).FirstOrDefaultAsync();

                obj.Model = data;
                obj.isAccept = true;
            }
            catch (Exception)
            {
                obj.isAccept = false;
            }

            return obj;
        }
        public async Task<bool> UpdateModel(ModelViewModel viewModel)
        {
            try
            {
                var data = await _context.Models.FirstOrDefaultAsync(p => p.ID == viewModel.ID);
                data.ModelName = viewModel.ModelName;
                data.BrandID = viewModel.BrandID;

                _context.Models.Update(data);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteModel(ModelViewModel viewModel)
        {
            try
            {
                var data = await _context.Models.FirstOrDefaultAsync(s => s.ID == viewModel.ID);

                _context.Models.Remove(data);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<ColorViewModel>> GetAllColor()
        {
            var data = await _context.Colors.Select(s => new ColorViewModel()
            {
                ID = s.ID,
                ColorCode = s.ColorCode,
                ColorName = s.ColorName
            }).ToListAsync();

            return data;
        }
        public async Task<bool> AddNewColor(ColorViewModel viewModel)
        {
            try
            {
                var data = new Color()
                {
                    ColorName = viewModel.ColorName,
                    ColorCode = viewModel.ColorCode
                };

                await _context.Colors.AddAsync(data);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateColor(ColorViewModel viewModel)
        {
            try
            {
                var data = await _context.Colors.FirstOrDefaultAsync(p => p.ID == viewModel.ID);
                data.ColorName = viewModel.ColorName;
                data.ColorCode = viewModel.ColorCode;

                _context.Colors.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<ColorVM> GetColor(int? id)
        {
            var obj = new ColorVM();

            try
            {
                ColorViewModel color = await _context.Colors.Where(p => p.ID == id).Select(s => new ColorViewModel()
                {
                    ID = s.ID,
                    ColorName = s.ColorName,
                    ColorCode = s.ColorCode
                }).FirstOrDefaultAsync();

                obj.Color = color;
                obj.isAccept = true;
            }
            catch (Exception)
            {
                obj.isAccept = false;
            }

            return obj;
        }
        public async Task<bool> DeleteColor(ColorViewModel viewModel)
        {
            try
            {
                var data = await _context.Colors.FirstOrDefaultAsync(p => p.ID == viewModel.ID);

                _context.Colors.Remove(data);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<CarViewModel>> GetAllCar()
        {
            var data = await _context.Cars
                .Include(p => p.Model)
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Model.Brand)
                .Select(p => new CarViewModel()
                {
                    ID = p.ID,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    ColorId = p.ColorId,
                    ColorName = p.Color.ColorName,
                    ColorCode = p.Color.ColorCode,
                    CreateDate = p.CreateDate,
                    ModelId = p.ModelId,
                    ModelName = p.Model.ModelName,
                    BrandId = p.Model.BrandID,
                    BrandName = p.Model.Brand.BrandName,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToListAsync();

            return data;
        }
        public async Task<bool> AddCar(CarViewModel viewModel)
        {
            try
            {
                var a = await AddImage("Car", viewModel.CarImages);


                var data = new Car()
                {
                    ColorId = viewModel.ColorId,
                    CategoryId = viewModel.CategoryId,
                    ModelId = viewModel.ModelId,
                    Price = viewModel.Price,
                    Quantity = viewModel.Quantity,
                    CreateDate = viewModel.CreateDate,
                };

                await _context.Cars.AddAsync(data);
                await _context.SaveChangesAsync();

                List<Images> images = new List<Images>();


                foreach (var img in a)
                {

                    images.Add(new Images()
                    {
                        Name = img,
                        CarID = data.ID
                    });
                }
                await _context.Images.AddRangeAsync(images);
                await _context.SaveChangesAsync();
                
                return true;
            }
            catch (Exception e)
            {
                var a = e.Message;

                return false;
            }
        }
        public async Task<CarVM> GetCar(int? id)
        {
            var obj = new CarVM();

            try
            {
                CarViewModel data = await _context.Cars.Where(p => p.ID == id).Select(p => new CarViewModel()
                {
                    ID = p.ID,
                    BrandId = p.Model.BrandID,
                    BrandName = p.Model.Brand.BrandName,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    ColorId = p.ColorId,
                    ColorCode = p.Color.ColorCode,
                    ColorName = p.Color.ColorName,
                    CreateDate = p.CreateDate,
                    ModelId = p.ModelId,
                    ModelName = p.Model.ModelName,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).FirstOrDefaultAsync();

                obj.CarViewModel = data;
                obj.isAccept = true;
            }
            catch (Exception)
            {
                obj.isAccept = false;
            }

            return obj;
        }
        public async Task<bool> UpdateCar(CarViewModel viewModel)
        {
            try
            {
                var data = await _context.Cars.Include(p => p.Model).FirstOrDefaultAsync(p => p.ID == viewModel.ID);

                data.CategoryId = viewModel.CategoryId;
                data.Model.BrandID = viewModel.BrandId;
                data.ModelId = viewModel.ModelId;
                data.ColorId = viewModel.ColorId;
                data.CreateDate = viewModel.CreateDate;
                data.Price = viewModel.Price;
                data.Quantity = viewModel.Quantity;

                _context.Cars.Update(data);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                var a = e.Message;
                return false;
            }
        }

        public async Task<bool> DeleteCar(CarViewModel viewModel)
        {
            try
            {
                var data = await _context.Cars.FirstOrDefaultAsync(p => p.ID == viewModel.ID);
                
                _context.Cars.Remove(data);

                var img = _context.Images.Where(p => p.CarID == viewModel.ID);

                foreach (var item in img)
                {
                    string path = Path.Combine(_env.WebRootPath, "GalleryFile", "Car", item.Name);

                    File.Delete(path);
                }

                _context.Images.RemoveRange(img);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<List<string>> AddImage(string FolderName, IFormFileCollection imgs)
        {
            var UploadRootPath = Path.Combine(_env.WebRootPath, "GalleryFile", FolderName);
            List<String> fileNames = new List<string>();
            foreach (var img in imgs)
            {
                string FileExtension = Path.GetExtension(img.FileName);
                var FileName = string.Concat(Guid.NewGuid().ToString());
                var path = Path.Combine(UploadRootPath,String.Concat(FileName, FileExtension));


                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await img.CopyToAsync(fileStream);
                }

                string ret = String.Concat(FileName, FileExtension);
                fileNames.Add(ret);
            }
            return fileNames;
        }
    }
}
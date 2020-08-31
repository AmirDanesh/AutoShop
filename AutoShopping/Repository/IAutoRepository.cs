using AutoShopping.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopping.Repository
{
    public interface IAutoRepository
    {
        #region Brand ViewModels
        Task<List<BrandViewModel>> GetAllBrands();
        Task<bool> AddNewBrand(BrandViewModel viewModel);
        Task<BrandVM> GetBrand(int? id);
        Task<bool> UpdateBrand(BrandViewModel viewModel);
        Task<bool> DeleteBrand(BrandViewModel viewModel);
        #endregion
        #region Model ViewModels
        Task<List<ModelViewModel>> GetAllModels();
        Task<bool> AddNewModel(ModelViewModel viewModel);
        Task<ModelVM> GetModel(int? id);
        Task<bool> UpdateModel(ModelViewModel viewModel);
        Task<bool> DeleteModel(ModelViewModel viewModel);
        #endregion
        #region Color ViewModels
        Task<List<ColorViewModel>> GetAllColor();
        Task<bool> AddNewColor(ColorViewModel viewModel);
        Task<bool> UpdateColor(ColorViewModel viewModel);
        Task<ColorVM> GetColor(int? id);
        Task<bool> DeleteColor(ColorViewModel viewModel);
        #endregion

        Task<bool> AddCar(CarViewModel viewModel);
        Task<List<CarViewModel>> GetAllCar();
        Task<bool> UpdateCar(CarViewModel viewModel);
        Task<CarVM> GetCar(int? id);
        Task<bool> DeleteCar(CarViewModel viewModel);
    }
}
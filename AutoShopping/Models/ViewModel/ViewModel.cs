using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoShopping.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoShopping.Models.ViewModel
{
    public class BrandViewModel
    {
        public int ID { get; set; }
        [Display(Name = "نام برند")]
        [Required(ErrorMessage = AnnotationErrors.message)]
        public string BrandName { get; set; }

        [Display(Name = "لوگو برند")]
        public IFormFileCollection LogoImg { get; set; }
        public string LogoFileName { get; set; }
    }

    public class BrandVM
    {
        public BrandViewModel Brand { get; set; }
        public bool isAccept { get; set; }
    }

    public class ModelViewModel
    {
        public int ID { get; set; }
        [Display(Name = "نام مدل")]
        [Required(ErrorMessage = AnnotationErrors.message)]
        public string ModelName { get; set; }
        [Display(Name = "نام برند")]
        [Required(ErrorMessage = AnnotationErrors.message)]
        public int BrandID { get; set; }
        [Display(Name = "نام برند")]
        public string BrandName { get; set; }
        public string BrandLogo { get; set; }
        public SelectList BrandList { get; set; }
    }

    public class ModelVM
    {
        public ModelViewModel Model { get; set; }
        public bool isAccept { get; set; }
    }

    public class ColorViewModel
    {
        public int ID { get; set; }
        [Display(Name = "نام رنگ")]
        [Required(ErrorMessage = AnnotationErrors.message)]
        public string ColorName { get; set; }
        [Display(Name = "کد رنگ")]
        [Required(ErrorMessage = AnnotationErrors.message)]
        public string ColorCode { get; set; }
    }

    public class ColorVM
    {
        public ColorViewModel Color { get; set; }
        public bool isAccept { get; set; }
    }
    public class CarViewModel
    {
        public int ID { get; set; }

        [Display(Name = "تاریخ تولید")]
        [Required(ErrorMessage = AnnotationErrors.message)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = AnnotationErrors.message)]
        public int Price { get; set; }

        [Display(Name = "تعداد")]
        [Required(ErrorMessage = AnnotationErrors.message)]
        [DataType("Number")]
        public int Quantity { get; set; }

        [Display(Name = "رنگ")]
        [Required(ErrorMessage = AnnotationErrors.select)]
        public int ColorId { get; set; }

        public string ColorName { get; set; }
        public string ColorCode { get; set; }


        [Display(Name = "مدل")]
        [Required(ErrorMessage = AnnotationErrors.select)]
        public int ModelId { get; set; }

        public string ModelName { get; set; }

        [Display(Name = "برند")]
        [Required(ErrorMessage = AnnotationErrors.select)]
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = AnnotationErrors.select)]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public bool isAccept { get; set; }
        public SelectList ColorList { get; set; }
        public SelectList BrandList { get; set; }
        public SelectList ModelList { get; set; }
        public string FileNameImg { get; set; }
        public IFormFileCollection CarImages { get; set; }
        public List<string> CarImgName { get; set; }
    }

    public class CarVM
    {
        public CarViewModel CarViewModel { get; set; }
        public bool isAccept { get; set; }
    }
}

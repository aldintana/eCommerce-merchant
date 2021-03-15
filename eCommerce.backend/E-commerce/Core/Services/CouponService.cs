using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class CouponService : ICouponService
    {
        private E_commerceDB _context;
        public CouponService(E_commerceDB context)
        {
            _context = context;
        }
        public void Create(CouponAddVM couponVM)
        {
            int quantity = couponVM.Quantity;
            for (int i = 0; i < quantity; i++)
            {
                Coupon coupon = new Coupon
                {
                    Code = NewCode(),
                    ExpiredDate = DateTime.Now.AddDays(couponVM.Days),
                    IsValid = true,
                    Value = couponVM.Value
                };
                _context.Coupon.Add(coupon);
                _context.SaveChanges();
            }

        }

        public List<CouponGetVM> GetAll()
        {
            return _context.Coupon.Where(x=>x.IsValid==true)
                .Select(
                x => new CouponGetVM
                {
                    Code = x.Code,
                    ExpiredDate = x.ExpiredDate.ToString("dd.MM.yyyy."),
                    Value=$"{x.Value * 100}%" 
                }
                ).ToList();
        }



        private string NewCode()
        {
            string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";
            return Random(numbers, 3) + Random(uppercase, 1) + "-" + Random(numbers, 3) + Random(uppercase, 1);
        }
        private string Random(string text, int length)
        {
            Random random = new Random();
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += text[random.Next(text.Length)];
            }
            return result;
        }
    }
}

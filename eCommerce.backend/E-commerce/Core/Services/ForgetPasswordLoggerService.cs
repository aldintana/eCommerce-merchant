using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class ForgetPasswordLoggerService : IForgetPasswordLoggerService
    {
        private E_commerceDB _context;
        public ForgetPasswordLoggerService(E_commerceDB context)
        {
            _context = context;
        }
        public bool Add(Account user)
        {
            var last = _context.ForgetPasswordLogger.OrderByDescending(x => x.DateTime)
                .Where(x => x.AccountId == user.Id).FirstOrDefault();
            if(last!=null)
            {
                var difference = (DateTime.Now - last.DateTime).TotalDays;
                if (difference < 7)
                    return false;
                else
                {
                    var logger = new ForgetPasswordLogger
                    {
                        AccountId = user.Id,
                        DateTime = DateTime.Now
                    };
                    _context.ForgetPasswordLogger.Add(logger);
                    _context.SaveChanges();
                    return true;
                }
            }
            else
            {
                var logger2 = new ForgetPasswordLogger
                {
                    AccountId = user.Id,
                    DateTime = DateTime.Now
                };
                _context.ForgetPasswordLogger.Add(logger2);
                _context.SaveChanges();
                return true;
            }
            

        }


    }
}

﻿using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IForgetPasswordLoggerService
    {
        bool Add(Account user);
    }
}

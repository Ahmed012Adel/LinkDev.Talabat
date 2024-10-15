﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Apis.Controller.Exceptions
{
    public class NotFoundException :ApplicationException
    {
        public NotFoundException(string name , object key)
            :base($"{name} with {key} Not Found")
        {
            
        }
    }
}

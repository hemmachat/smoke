﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRanking.Interfaces
{
    // https://github.com/johnthiriet/AsyncVoid
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}

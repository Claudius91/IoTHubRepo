﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServer
{
    public interface IHub
    {
        Task SendNotification(string message);
    }
}

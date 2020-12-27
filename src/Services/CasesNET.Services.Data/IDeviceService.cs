using System;
using System.Collections.Generic;
using System.Text;

namespace CasesNET.Services.Data
{
    public interface IDeviceService
    {
        IEnumerable<T> GetAll<T>();
    }
}

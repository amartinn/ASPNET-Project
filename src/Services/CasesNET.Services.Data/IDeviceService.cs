namespace CasesNET.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IDeviceService
    {
        IEnumerable<T> GetAll<T>();
    }
}

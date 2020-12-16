namespace CasesNET.Services.Data
{
    using System.Collections.Generic;

    public interface IManufacturerService
    {
        IEnumerable<T> GetAll<T>();

        string GetNameById(string id);
    }
}

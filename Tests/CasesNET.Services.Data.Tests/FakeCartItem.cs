namespace CasesNET.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using Moq;
    using Xunit;

    public class FakeCartItem :IMapFrom<CartItem>
    {

    }
}

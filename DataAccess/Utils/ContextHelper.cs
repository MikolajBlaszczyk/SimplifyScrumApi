using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Utils;

public abstract class ContextHelper
{
    public static SimplifyAppDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<SimplifyAppDbContext>()
            .UseInMemoryDatabase("TestingDatabase")
            .Options;

        return new SimplifyAppDbContext(options);
    }
}
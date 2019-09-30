using PDKSWebServer.Dtos;
using System.Collections.Generic;

namespace PDKSWebServer.Managers
{
    interface ICategoryManager
    {
        IEnumerable<CategoryDto> GetCategories();
    }
}

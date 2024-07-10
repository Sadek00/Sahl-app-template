
namespace AppTemplate.Entities.DBManager
{
    public static class HelperMethods
    {
        public static object PreparePaginatedResponse<T>(List<T> data, int currentPage, int totalElements, int totalPages) where T : class
        {
            return new
            { 
                Collection = data,
                TotalElements = totalElements,
                CurrentPage = currentPage,
                TotalPages = totalPages
            };
        }
    }
}

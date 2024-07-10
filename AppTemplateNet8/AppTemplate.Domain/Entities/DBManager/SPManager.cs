using AppTemplate.Domain.Utilities;



namespace AppTemplate.Entities.DBManager
{
    public static class SPManager
    {
        public static ResponseModelList FinalPasignatedResult<T>(PaginatedData<T> paginatedData, int page, int size) where T : class
        {
            int totalElement = paginatedData.TotalRows ?? 0;
            return new ResponseModelList(paginatedData.Data, totalElement, page, size);
        }

        public static ResponseModelList FinalPasignatedResultByNewKey<T>(PaginatedData<T> paginatedData, int page, int size) where T : new()
        {
            int totalElement = paginatedData.TotalRows ?? 0;
            return new ResponseModelList(paginatedData.Data, totalElement, page, size);

        }

        public static ResponseModelList PreparePaginatedResponse<T>(List<T> data, int totalElements, int totalPages, int page, int size) where T : class
        {
            return new ResponseModelList(data, totalElements, page, size);
        }

    }
}

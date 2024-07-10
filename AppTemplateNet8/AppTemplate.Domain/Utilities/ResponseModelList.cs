using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.Utilities
{
    public class ResponseModelList
    {
        public ResponseModelList(object? collection, int totalElements, int? page, int? size)
        {
            this.Collection = collection;
            this.TotalElements = totalElements;
            this.Page = page;
            this.Size = size;

            this.TotalPages = Convert.ToInt32(Math.Ceiling((decimal)((decimal)(totalElements) / size)));

            if (page < this.TotalPages) this.HasNext = true;
            else this.HasNext = false;

            if (page > 1) this.HasPrevious = true;
            else this.HasPrevious = false;
        }
        public object? Collection { get; set; }
        public int TotalElements { get; set; } = 0;
        public int TotalPages { get; set; } = 1;
        public int? Page { get; set; } = 1;
        public int? Size { get; set; } = 1;
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }

    public static class SpResponse
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

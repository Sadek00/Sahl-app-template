namespace AppTemplate.Infrastructure.Interface.Common;

public interface ICommonRepository
{
    Task<object> GetDepartments();
    Task<object> GetDesignations();
    Task<object> GetUserTypes();
}

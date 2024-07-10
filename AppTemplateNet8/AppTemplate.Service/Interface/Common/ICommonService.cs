namespace AppTemplate.Service.Interface.Common
{
    public interface ICommonService
    {
        Task<object> GetDepartments();
        Task<object> GetDesignations();
        Task<object> GetUserTypes();
    }
}

using AppTemplate.Domain.Entities.Settings;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Settings;
using AppTemplate.Infrastructure.Interface.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Infrastructure.Interface.Settings
{
    public interface IDesignationRepository : IBaseRepository<Designation>
    {
        Task<object> GetDesignations(int page, int size, int statusId);
        Task<object> GetDesignationById(int id);
        Task<ResponseModel> AddDesignation(DesignationCreateVm model, int loginId);
        Task<ResponseModel> UpdateDesignation(DesignationUpdateVm model, int loginId);
        Task<ResponseModel> ModifyStatus(int id, int statusId, int loginId);
    }
}

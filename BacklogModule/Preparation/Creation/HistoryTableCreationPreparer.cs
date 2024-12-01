using BacklogModule.Abstraction;
using DataAccess.Abstraction.Tables;
using Microsoft.AspNetCore.Http;
using SimplifyScrum.Utils;

namespace BacklogModule.Preparation.Creation;

public class HistoryTableCreationPreparer(IHttpContextAccessor contextAccessor)  : IPrepareCreation<HistoryTable>
{
    
    public void Prepare(HistoryTable entity)
    {
        string userGuid = contextAccessor.HttpContext?.User.GetUserGuid();
        DateTime now = DateTime.Now;
        
        if (string.IsNullOrEmpty(entity.CreatedBy))
            entity.CreatedBy = userGuid;
        
        if (entity.CreatedOn == DateTime.MinValue)
            entity.CreatedOn = now;
        
        if (string.IsNullOrEmpty(entity.LastUpdatedBy))
            entity.LastUpdatedBy = userGuid;
        
        if (entity.LastUpdateOn == DateTime.MinValue)
            entity.LastUpdateOn = now;
    }
    
}
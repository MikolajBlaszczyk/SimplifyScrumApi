using BacklogModule.Abstraction;
using DataAccess.Abstraction.Tables;
using Microsoft.AspNetCore.Http;
using SimplifyScrum.Utils;

namespace BacklogModule.Preparation.Creation;

public class HistoryTableCreationPreparer(IHttpContextAccessor contextAccessor)  : IPrepareCreation<HistoryTable>
{
    public void Prepare(HistoryTable entity)
    {
        string userGuid = contextAccessor.HttpContext.User.GetUserGuid();
        DateTime now = DateTime.Now;
        
        if (string.IsNullOrEmpty(entity.CreatedBy))
        {
            entity.CreatedBy = userGuid;
        }
          

        if (AreCreationPropsPresent(entity))
        {
            entity.CreatedBy = userGuid;
            entity.CreatedOn = now;
            entity.LastUpdatedBy = userGuid;
            entity.LastUpdateOn = now;
        }

        if (AreLastUpdatePropsPresent(entity))
        {
            entity.LastUpdatedBy = userGuid;
            entity.LastUpdateOn = now;
        }
    }
    
    private bool AreCreationPropsPresent(HistoryTable entity)
    {
        if (string.IsNullOrEmpty(entity.CreatedBy) || entity.CreatedOn == DateTime.MinValue)
        {
            return false;
        }

        return true;
    }
    
    private bool AreLastUpdatePropsPresent(HistoryTable entity)
    {
        if (string.IsNullOrEmpty(entity.CreatedBy) || entity.CreatedOn == DateTime.MinValue)
        {
            return false;
        }

        return true;
    }
}
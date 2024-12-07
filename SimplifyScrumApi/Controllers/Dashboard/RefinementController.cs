using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Models;
using DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimplifyScrum.Utils;
using SimplifyScrum.Utils.Messages;
using SimplifyScrum.Utils.Requests;

namespace SimplifyScrum.Controllers.Dashboard;

[ApiController]
[Authorize]
[Route("api/v1/scrum/refinement/")]
public class RefinementController(IManageFeature featureManager, ResultUnWrapper unWrapper) : ControllerBase
{
    private static ResponseProducer _producer = ResponseProducer.Shared;
    
    [HttpPost]
    [Route("ready")]
    public async Task<IActionResult> ReadyForRefinement([FromBody] string featureGuid)
    {
        if (featureGuid.IsNullOrEmpty())
            return _producer.BadRequest(Messages.ReadyForRefinementParams);

        var featureGetResult = await featureManager.GetFeatureByGuid(featureGuid);
        if (featureGetResult.IsFailure)
            return _producer.InternalServerError();

        unWrapper.Unwrap(featureGetResult, out FeatureRecord feature);
        feature = feature with { RefinementState = RefinementState.Ready};
        
        var result  = await featureManager.UpdateFeature(feature);
        
        if (result.IsFailure)
            return _producer.InternalServerError();

        return Ok(result.Data);
    }
    
    public record RefinedFeatureRequest(string FeatureGuid, int Points);
    
    [HttpPost]
    [Route("refined")]
    public async Task<IActionResult> RefinedFeature([FromBody] RefinedFeatureRequest body)
    {
        if (body.FeatureGuid.IsNullOrEmpty() || body.Points < 1 || body.Points > 8)
            return _producer.BadRequest(Messages.RefinedFeatureParams);

        var featureGetResult = await featureManager.GetFeatureByGuid(body.FeatureGuid);
        if (featureGetResult.IsFailure)
            return _producer.InternalServerError();

        unWrapper.Unwrap(featureGetResult, out FeatureRecord feature);
        feature = feature with { Points = body.Points, RefinementState = RefinementState.Refined};
        
        var result  = await featureManager.UpdateFeature(feature);
        
        if (result.IsFailure)
            return _producer.InternalServerError();

        return Ok(result.Data);
    }
    
    [HttpPost]
    [Route("split")]
    public async Task<IActionResult> SplitFeature([FromBody] string featureGuid)
    {
        if (featureGuid.IsNullOrEmpty())
            return _producer.BadRequest(Messages.SplitFeatureParams);

        var featureGetResult = await featureManager.GetFeatureByGuid(featureGuid);
        if (featureGetResult.IsFailure)
            return _producer.InternalServerError();

        unWrapper.Unwrap(featureGetResult, out FeatureRecord feature);
        feature = feature with { RefinementState = RefinementState.ShouldBeSplitted};
        
        var result  = await featureManager.UpdateFeature(feature);
        
        if (result.IsFailure)
            return _producer.InternalServerError();

        return Ok(result.Data);
    }
    
    [HttpPost]
    [Route("require-info")]
    public async Task<IActionResult> RequireMoreInfo([FromBody] string featureGuid)
    {
        if (featureGuid.IsNullOrEmpty())
            return _producer.BadRequest(Messages.SplitFeatureParams);

        var featureGetResult = await featureManager.GetFeatureByGuid(featureGuid);
        if (featureGetResult.IsFailure)
            return _producer.InternalServerError();

        unWrapper.Unwrap(featureGetResult, out FeatureRecord feature);
        feature = feature with { RefinementState = RefinementState.MoreInfoNeeded};
        
        var result  = await featureManager.UpdateFeature(feature);
        
        if (result.IsFailure)
            return _producer.InternalServerError();

        return Ok(result.Data);
    }
}
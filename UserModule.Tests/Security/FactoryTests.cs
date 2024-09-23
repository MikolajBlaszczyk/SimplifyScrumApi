using UserModule.Security.Models;

namespace UserModuleTests.Security;

[TestFixture]
public class FactoryTests
{
    [Test]
    public void SecurityResultTest_ShouldCreateSuccessResult()
    {
        var actual = SecurityResultsFactory.Success();
        
        Assert.IsTrue(actual.IsSuccess, "IsSuccess property for success result should be set to true.");
        Assert.IsFalse(actual.IsFailure, "IsFailure property for success result should be set to false.");
        Assert.IsNull(actual.Exception, "Exception property for success result should be set to null.");
    }
    
    [Test]
    public void SecurityResultTest_ShouldCreateFailureResult()
    {
        var ex = new Exception();
        
        var actual = SecurityResultsFactory.Failure(ex);
        
        Assert.IsTrue(actual.IsFailure, "IsFailure property for success result should be set to true.");
        Assert.IsFalse(actual.IsSuccess, "IsSuccess property for success result should be set to false.");
        Assert.IsNotNull(actual.Exception, "Exception property for success result should not be set to null.");
    }
}
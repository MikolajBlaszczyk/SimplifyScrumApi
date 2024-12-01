using DataAccess.Abstraction.Tables;
using DataAccess.Models.Factories;
using DataAccess.Models.Projects;
using DataAccess.Tests.TestUtils;
using Microsoft.VisualBasic.CompilerServices;

namespace DataAccess.Tests;

[TestFixture]
public class HistoryTableHelperTests
{
    public static IEnumerable<TestCaseData> HistoryTableHelperData
    {
        get
        {
            foreach (var project in DataProvider.Projects)
            {
                yield return new TestCaseData(project,
                    new HistoryTable()
                    {
                        LastUpdateOn = DateTime.MaxValue, LastUpdatedBy = $"someone {Random.Shared.Next()}", CreatedBy = $"someone {Random.Shared.Next()}",
                        CreatedOn = DateTime.MaxValue
                    });
            }
        }
    }
    
    [Test]
    [TestCaseSource(nameof(HistoryTableHelperData))]
    public void PopulateMissingHistoryValues_CorrectlyPopulateHistoryTableValues(HistoryTable targetTable, HistoryTable values)
    {
        HistoryTableHelper.PopulateMissingValues(targetTable, values.CreatedBy, values.CreatedOn, values.LastUpdatedBy, values.LastUpdateOn);
        
        Assert.That(targetTable.CreatedOn, Is.EqualTo(values.CreatedOn));
        Assert.That(targetTable.CreatedBy, Is.EqualTo(values.CreatedBy));
        Assert.That(targetTable.LastUpdatedBy, Is.EqualTo(values.LastUpdatedBy));
        Assert.That(targetTable.LastUpdateOn, Is.EqualTo(values.LastUpdateOn));
    }
}
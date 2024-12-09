using DataAccess.Abstraction.Storage;
using DataAccess.Models.Projects;
using DataAccess.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Utils;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Tests.Storage
{
    [TestFixture]
    public class FeatureStorageTests
    {
        private Mock<ICreateAccessors> _mockFactory;
        private Mock<IAccessor<Feature>> _mockAccessor;
        private Mock<ILogger<FeatureStorage>> _mockLogger;
        private IFeatureStorage _featureStorage;

        [SetUp]
        public void SetUp()
        {
            _mockFactory = new Mock<ICreateAccessors>();
            _mockAccessor = new Mock<IAccessor<Feature>>();
            _mockLogger = new Mock<ILogger<FeatureStorage>>();

            _mockFactory.Setup(f => f.Create<Feature>()).Returns(_mockAccessor.Object);

            _featureStorage = new FeatureStorage(_mockFactory.Object, _mockLogger.Object);
        }

        [Test]
        public async Task AddFeature_ShouldAddFeature()
        {
            var feature = new Feature { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.Add(It.IsAny<Feature>())).ReturnsAsync(feature);

            var result = await _featureStorage.AddFeature(feature);

            Assert.AreEqual(feature, result);
            _mockAccessor.Verify(a => a.Add(It.IsAny<Feature>()), Times.Once);
        }

        [Test]
        public void AddFeature_ShouldThrowException_WhenAddFails()
        {
            var feature = new Feature { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.Add(It.IsAny<Feature>())).ReturnsAsync((Feature)null);

            Assert.ThrowsAsync<AccessorException>(() => _featureStorage.AddFeature(feature));
            _mockAccessor.Verify(a => a.Add(It.IsAny<Feature>()), Times.Once);
        }

        [Test]
        public async Task UpdateFeature_ShouldUpdateFeature()
        {
            var feature = new Feature { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.Update(It.IsAny<Feature>())).ReturnsAsync(feature);

            var result = await _featureStorage.UpdateFeature(feature);

            Assert.AreEqual(feature, result);
            _mockAccessor.Verify(a => a.Update(It.IsAny<Feature>()), Times.Once);
        }

        [Test]
        public void UpdateFeature_ShouldThrowException_WhenUpdateFails()
        {
            var feature = new Feature { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.Update(It.IsAny<Feature>())).ReturnsAsync((Feature)null);

            Assert.ThrowsAsync<AccessorException>(() => _featureStorage.UpdateFeature(feature));
            _mockAccessor.Verify(a => a.Update(It.IsAny<Feature>()), Times.Once);
        }

        [Test]
        public async Task DeleteFeature_ShouldDeleteFeature()
        {
            var feature = new Feature { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.Delete(It.IsAny<Feature>())).ReturnsAsync(feature);

            var result = await _featureStorage.DeleteFeature(feature);

            Assert.AreEqual(feature, result);
            _mockAccessor.Verify(a => a.Delete(It.IsAny<Feature>()), Times.Once);
        }

        [Test]
        public void DeleteFeature_ShouldThrowException_WhenDeleteFails()
        {
            var feature = new Feature { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.Delete(It.IsAny<Feature>())).ReturnsAsync((Feature)null);

            Assert.ThrowsAsync<AccessorException>(() => _featureStorage.DeleteFeature(feature));
            _mockAccessor.Verify(a => a.Delete(It.IsAny<Feature>()), Times.Once);
        }

        [Test]
        public async Task GetFeatureByGUID_ShouldReturnFeature()
        {
            var feature = new Feature { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.GetByPK(It.IsAny<string>())).ReturnsAsync(feature);

            var result = await _featureStorage.GetFeatureByGUID(feature.GUID);

            Assert.AreEqual(feature, result);
            _mockAccessor.Verify(a => a.GetByPK(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetFeatureByGUID_ShouldThrowException_WhenFeatureNotFound()
        {
            _mockAccessor.Setup(a => a.GetByPK(It.IsAny<string>())).ReturnsAsync((Feature)null);

            Assert.ThrowsAsync<AccessorException>(() => _featureStorage.GetFeatureByGUID(Guid.NewGuid().ToString()));
            _mockAccessor.Verify(a => a.GetByPK(It.IsAny<string>()), Times.Once);
        }

       
    }
}
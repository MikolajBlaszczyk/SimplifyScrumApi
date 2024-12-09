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
    public class ProjectStorageTests
    {
        private Mock<ICreateAccessors> _mockFactory;
        private Mock<IAccessor<Project>> _mockAccessor;
        private Mock<ILogger<ProjectStorage>> _mockLogger;
        private IProjectStorage _projectStorage;

        [SetUp]
        public void SetUp()
        {
            _mockFactory = new Mock<ICreateAccessors>();
            _mockAccessor = new Mock<IAccessor<Project>>();
            _mockLogger = new Mock<ILogger<ProjectStorage>>();

            _mockFactory.Setup(f => f.Create<Project>()).Returns(_mockAccessor.Object);

            _projectStorage = new ProjectStorage(_mockFactory.Object, _mockLogger.Object);
        }

        [Test]
        public async Task AddProject_ShouldAddProject()
        {
            var project = new Project { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.Add(It.IsAny<Project>())).ReturnsAsync(project);

            var result = await _projectStorage.AddProject(project);

            Assert.AreEqual(project, result);
            _mockAccessor.Verify(a => a.Add(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public void AddProject_ShouldThrowException_WhenAddFails()
        {
            var project = new Project { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.Add(It.IsAny<Project>())).ReturnsAsync((Project)null);

            Assert.ThrowsAsync<AccessorException>(() => _projectStorage.AddProject(project));
            _mockAccessor.Verify(a => a.Add(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public async Task UpdateProject_ShouldUpdateProject()
        {
            var project = new Project { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.Update(It.IsAny<Project>())).ReturnsAsync(project);

            var result = await _projectStorage.UpdateProject(project);

            Assert.AreEqual(project, result);
            _mockAccessor.Verify(a => a.Update(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public void UpdateProject_ShouldThrowException_WhenUpdateFails()
        {
            var project = new Project { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.Update(It.IsAny<Project>())).ReturnsAsync((Project)null);

            Assert.ThrowsAsync<AccessorException>(() => _projectStorage.UpdateProject(project));
            _mockAccessor.Verify(a => a.Update(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public async Task DeleteProject_ShouldDeleteProject()
        {
            var project = new Project { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.Delete(It.IsAny<Project>())).ReturnsAsync(project);

            var result = await _projectStorage.DeleteProject(project);

            Assert.AreEqual(project, result);
            _mockAccessor.Verify(a => a.Delete(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public void DeleteProject_ShouldThrowException_WhenDeleteFails()
        {
            var project = new Project { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.Delete(It.IsAny<Project>())).ReturnsAsync((Project)null);

            Assert.ThrowsAsync<AccessorException>(() => _projectStorage.DeleteProject(project));
            _mockAccessor.Verify(a => a.Delete(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public async Task GetProjectByGUID_ShouldReturnProject()
        {
            var project = new Project { GUID = Guid.NewGuid().ToString() };
            _mockAccessor.Setup(a => a.GetByPK(It.IsAny<string>())).ReturnsAsync(project);

            var result = await _projectStorage.GetProjectByGUID(project.GUID);

            Assert.AreEqual(project, result);
            _mockAccessor.Verify(a => a.GetByPK(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetProjectByGUID_ShouldThrowException_WhenProjectNotFound()
        {
            _mockAccessor.Setup(a => a.GetByPK(It.IsAny<string>())).ReturnsAsync((Project)null);

            Assert.ThrowsAsync<AccessorException>(() => _projectStorage.GetProjectByGUID(Guid.NewGuid().ToString()));
            _mockAccessor.Verify(a => a.GetByPK(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetAllProjects_ShouldReturnProjects()
        {
            var projects = new List<Project> { new Project { GUID = Guid.NewGuid().ToString() } };
            _mockAccessor.Setup(a => a.GetAll()).ReturnsAsync(projects);

            var result = await _projectStorage.GetAllProjects();

            Assert.AreEqual(projects, result);
            _mockAccessor.Verify(a => a.GetAll(), Times.Once);
        }

        [Test]
        public void GetAllProjects_ShouldThrowException_WhenProjectsNotFound()
        {
            _mockAccessor.Setup(a => a.GetAll()).ReturnsAsync((List<Project>)null);

            Assert.ThrowsAsync<AccessorException>(() => _projectStorage.GetAllProjects());
            _mockAccessor.Verify(a => a.GetAll(), Times.Once);
        }
    }
}
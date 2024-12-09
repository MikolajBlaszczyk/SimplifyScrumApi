using DataAccess.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using DataAccess.Abstraction;
using DataAccess.Abstraction.Accessors.Factories;
using DataAccess.Context;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using Task = DataAccess.Models.Projects.Task;

namespace DataAccess.Tests.Storage
{
    [TestFixture]
    public class TaskStorageTests
    {
        private Mock<ICreateAccessors> _mockFactory;
        private Mock<IAccessor<Task>> _mockAccessor;
        private Mock<ILogger<TaskStorage>> _mockLogger;
        private ITaskStorage _taskStorage;

        [SetUp]
        public void SetUp()
        {
            _mockFactory = new Mock<ICreateAccessors>();
            _mockAccessor = new Mock<IAccessor<Task>>();
            _mockLogger = new Mock<ILogger<TaskStorage>>();
            
            _mockFactory.Setup(f => f.Create<Task>()).Returns(_mockAccessor.Object);

            _taskStorage = new TaskStorage(_mockFactory.Object, _mockLogger.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task AddTask_ShouldAddTask()
        {
            var task = new Task { ID = 1 };
            _mockAccessor.Setup(a => a.Add(It.IsAny<Task>())).ReturnsAsync(task);

            var result = await _taskStorage.AddTask(task);

            Assert.AreEqual(task, result);
            _mockAccessor.Verify(a => a.Add(It.IsAny<Task>()), Times.Once);
        }

        [Test]
        public void AddTask_ShouldThrowException_WhenAddFails()
        {
            var task = new Task { ID = 1 };
            _mockAccessor.Setup(a => a.Add(It.IsAny<Task>())).ReturnsAsync((Task)null);

            Assert.ThrowsAsync<AccessorException>(() => _taskStorage.AddTask(task));
            _mockAccessor.Verify(a => a.Add(It.IsAny<Task>()), Times.Once);
        }

        [Test]
        public async System.Threading.Tasks.Task UpdateTask_ShouldUpdateTask()
        {
            var task = new Task { ID = 1 };
            _mockAccessor.Setup(a => a.Update(It.IsAny<Task>())).ReturnsAsync(task);

            var result = await _taskStorage.UpdateTask(task);

            Assert.AreEqual(task, result);
            _mockAccessor.Verify(a => a.Update(It.IsAny<Task>()), Times.Once);
        }

        [Test]
        public void UpdateTask_ShouldThrowException_WhenUpdateFails()
        {
            var task = new Task { ID = 1 };
            _mockAccessor.Setup(a => a.Update(It.IsAny<Task>())).ReturnsAsync((Task)null);

            Assert.ThrowsAsync<AccessorException>(() => _taskStorage.UpdateTask(task));
            _mockAccessor.Verify(a => a.Update(It.IsAny<Task>()), Times.Once);
        }

        [Test]
        public async System.Threading.Tasks.Task DeleteTask_ShouldDeleteTask()
        {
            var task = new Task { ID = 1 };
            _mockAccessor.Setup(a => a.Delete(It.IsAny<Task>())).ReturnsAsync(task);

            var result = await _taskStorage.DeleteTask(task);

            Assert.AreEqual(task, result);
            _mockAccessor.Verify(a => a.Delete(It.IsAny<Task>()), Times.Once);
        }

        [Test]
        public void DeleteTask_ShouldThrowException_WhenDeleteFails()
        {
            var task = new Task { ID = 1 };
            _mockAccessor.Setup(a => a.Delete(It.IsAny<Task>())).ReturnsAsync((Task)null);

            Assert.ThrowsAsync<AccessorException>(() => _taskStorage.DeleteTask(task));
            _mockAccessor.Verify(a => a.Delete(It.IsAny<Task>()), Times.Once);
        }

        [Test]
        public async System.Threading.Tasks.Task GetTaskByID_ShouldReturnTask()
        {
            var task = new Task { ID = 1 };
            _mockAccessor.Setup(a => a.GetByPK(It.IsAny<int>())).ReturnsAsync(task);

            var result = await _taskStorage.GetTaskByID(task.ID);

            Assert.AreEqual(task, result);
            _mockAccessor.Verify(a => a.GetByPK(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetTaskByID_ShouldThrowException_WhenTaskNotFound()
        {
            _mockAccessor.Setup(a => a.GetByPK(It.IsAny<int>())).ReturnsAsync((Task)null);

            Assert.ThrowsAsync<AccessorException>(() => _taskStorage.GetTaskByID(1));
            _mockAccessor.Verify(a => a.GetByPK(It.IsAny<int>()), Times.Once);
        }
    }
}
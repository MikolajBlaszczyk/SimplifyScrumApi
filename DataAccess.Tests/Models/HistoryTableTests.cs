using System;
using DataAccess.Abstraction.Tables;
using NUnit.Framework;

namespace DataAccess.Tests.Abstraction.Tables
{
    [TestFixture]
    public class HistoryTableTests
    {
        [Test]
        public void EqualityOperator_ShouldReturnTrue_WhenAllPropertiesAreEqual()
        {
            var first = new HistoryTable
            {
                CreatedBy = "User1",
                LastUpdatedBy = "User1",
                CreatedOn = new DateTime(2023, 1, 1),
                LastUpdateOn = new DateTime(2023, 1, 1)
            };

            var second = new HistoryTable
            {
                CreatedBy = "User1",
                LastUpdatedBy = "User1",
                CreatedOn = new DateTime(2023, 1, 1),
                LastUpdateOn = new DateTime(2023, 1, 1)
            };

            Assert.IsTrue(first == second);
        }

        [Test]
        public void EqualityOperator_ShouldReturnFalse_WhenAnyPropertyIsDifferent()
        {
            var first = new HistoryTable
            {
                CreatedBy = "User1",
                LastUpdatedBy = "User1",
                CreatedOn = new DateTime(2023, 1, 1),
                LastUpdateOn = new DateTime(2023, 1, 1)
            };

            var second = new HistoryTable
            {
                CreatedBy = "User2",
                LastUpdatedBy = "User1",
                CreatedOn = new DateTime(2023, 1, 1),
                LastUpdateOn = new DateTime(2023, 1, 1)
            };

            Assert.IsFalse(first == second);
        }

        [Test]
        public void InequalityOperator_ShouldReturnTrue_WhenAnyPropertyIsDifferent()
        {
            var first = new HistoryTable
            {
                CreatedBy = "User1",
                LastUpdatedBy = "User1",
                CreatedOn = new DateTime(2023, 1, 1),
                LastUpdateOn = new DateTime(2023, 1, 1)
            };

            var second = new HistoryTable
            {
                CreatedBy = "User2",
                LastUpdatedBy = "User1",
                CreatedOn = new DateTime(2023, 1, 1),
                LastUpdateOn = new DateTime(2023, 1, 1)
            };

            Assert.IsTrue(first != second);
        }

        [Test]
        public void InequalityOperator_ShouldReturnFalse_WhenAllPropertiesAreEqual()
        {
            var first = new HistoryTable
            {
                CreatedBy = "User1",
                LastUpdatedBy = "User1",
                CreatedOn = new DateTime(2023, 1, 1),
                LastUpdateOn = new DateTime(2023, 1, 1)
            };

            var second = new HistoryTable
            {
                CreatedBy = "User1",
                LastUpdatedBy = "User1",
                CreatedOn = new DateTime(2023, 1, 1),
                LastUpdateOn = new DateTime(2023, 1, 1)
            };

            Assert.IsFalse(first != second);
        }
    }
}
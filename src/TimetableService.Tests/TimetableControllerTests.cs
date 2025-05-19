using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UniversityHelper.TimetableService.Business.Interfaces;
using UniversityHelper.TimetableService.Models.Dto;
using UniversityHelper.TimetableService.Controllers;
using Xunit;

namespace UniversityHelper.TimetableService.Tests
{
    public class TimetableControllerTests
    {
        private readonly Mock<ITimetableService> _mockTimetableService;
        private readonly TimetableController _controller;

        public TimetableControllerTests()
        {
            _mockTimetableService = new Mock<ITimetableService>();
            _controller = new TimetableController(_mockTimetableService.Object);
        }

        [Fact]
        public async Task GetAllGroups_ReturnsOkResult_WithGroups()
        {
            // Arrange
            var groups = new List<GroupDto> { new GroupDto { Id = System.Guid.NewGuid(), Group = "Group 1" } };
            _mockTimetableService.Setup(x => x.GetAllGroupsAsync()).ReturnsAsync(groups);

            // Act
            var result = await _controller.GetAllGroups();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<GroupDto>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("Group 1", returnValue[0].Group);
        }
    }
} 
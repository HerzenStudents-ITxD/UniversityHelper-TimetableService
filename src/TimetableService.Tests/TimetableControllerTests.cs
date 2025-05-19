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

        [Fact]
        public async Task GetGroupById_ReturnsOkResult_WithGroup()
        {
            // Arrange
            var groupId = System.Guid.NewGuid();
            var group = new GroupDto { Id = groupId, Group = "Group 1" };
            _mockTimetableService.Setup(x => x.GetGroupByIdAsync(groupId)).ReturnsAsync(group);

            // Act
            var result = await _controller.GetGroupById(groupId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<GroupDto>(okResult.Value);
            Assert.Equal(groupId, returnValue.Id);
            Assert.Equal("Group 1", returnValue.Group);
        }

        [Fact]
        public async Task CreateGroup_ReturnsCreatedAtAction_WithGroup()
        {
            // Arrange
            var createGroupDto = new CreateGroupDto { Group = "New Group" };
            var createdGroup = new GroupDto { Id = System.Guid.NewGuid(), Group = "New Group" };
            _mockTimetableService.Setup(x => x.CreateGroupAsync(createGroupDto)).ReturnsAsync(createdGroup);

            // Act
            var result = await _controller.CreateGroup(createGroupDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<GroupDto>(createdAtActionResult.Value);
            Assert.Equal(createdGroup.Id, returnValue.Id);
            Assert.Equal("New Group", returnValue.Group);
        }

        [Fact]
        public async Task UpdateGroup_ReturnsOkResult_WithUpdatedGroup()
        {
            // Arrange
            var groupId = System.Guid.NewGuid();
            var updateGroupDto = new UpdateGroupDto { Group = "Updated Group" };
            var updatedGroup = new GroupDto { Id = groupId, Group = "Updated Group" };
            _mockTimetableService.Setup(x => x.UpdateGroupAsync(groupId, updateGroupDto)).ReturnsAsync(updatedGroup);

            // Act
            var result = await _controller.UpdateGroup(groupId, updateGroupDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<GroupDto>(okResult.Value);
            Assert.Equal(groupId, returnValue.Id);
            Assert.Equal("Updated Group", returnValue.Group);
        }

        [Fact]
        public async Task DeleteGroup_ReturnsNoContent()
        {
            // Arrange
            var groupId = System.Guid.NewGuid();
            _mockTimetableService.Setup(x => x.DeleteGroupAsync(groupId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteGroup(groupId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
} 
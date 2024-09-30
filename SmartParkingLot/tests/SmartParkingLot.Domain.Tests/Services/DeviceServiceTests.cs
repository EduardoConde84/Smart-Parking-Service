using Moq;
using SmartParkingLot.Domain.Dtos;
using SmartParkingLot.Domain.Helpers;
using SmartParkingLot.Domain.Interfaces;
using SmartParkingLot.Domain.Services;
using SmartParkingLot.Domain.Tests.TestData;
using SmartParkingLot.Infrastructure.Context.Entities;
using SmartParkingLot.Infrastructure.Interfaces;

namespace SmartParkingLot.Domain.Tests.Services
{
    public class DeviceServiceTests
    {
        private readonly Mock<IDevicesRepository> _deviceRepositoryMock;
        private readonly IDevicesService _deviceService;

        public DeviceServiceTests()
        {
            _deviceRepositoryMock = new Mock<IDevicesRepository>();
            _deviceService = new DevicesService(_deviceRepositoryMock.Object);
        }

        [Theory]
        [ClassData(typeof(DeviceServiceTestData))]
        public async void Device_Must_Exist(Devices devices, Result result)
        {
            // Arrange
            var testDeviceDto = new DevicesDto() { Id = 1, DeviceAsignedNumber = new Guid() };
            _deviceRepositoryMock.Setup(d => d.GetByGuidAsync(testDeviceDto.DeviceAsignedNumber)).ReturnsAsync(devices);

            // Act
            var testResult = await _deviceService.GetDeviceRegistered(testDeviceDto);

            // Assert
            Assert.Equal(result.IsSuccess, testResult.IsSuccess);
        }
    }
}
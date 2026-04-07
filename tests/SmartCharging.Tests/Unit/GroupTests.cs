using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Exceptions;

namespace SmartCharging.Tests.Unit;

public class GroupTests
{
    [Fact]
    public void CreateGroup_WithValidData_ShouldSucceed()
    {
        Group group = new Group(Guid.NewGuid(), "Test Group", 100);

        Assert.Equal("Test Group", group.Name);
        Assert.Equal(100, group.Capacity);
        Assert.Empty(group.ChargeStations);
    }

    [Fact]
    public void CreateGroup_WithZeroCapacity_ShouldThrow()
    {
        Assert.Throws<DomainException>(() => new Group(Guid.NewGuid(), "Test", 0));
    }

    [Fact]
    public void CreateGroup_WithEmptyName_ShouldThrow()
    {
        Assert.Throws<DomainException>(() => new Group(Guid.NewGuid(), "", 100));
    }

    [Fact]
    public void AddChargeStation_WhenCapacityIsSufficient_ShouldSucceed()
    {
        Group group = new Group(Guid.NewGuid(), "Test Group", 100);
        ChargeStation station = CreateStation(10);

        group.AddChargeStation(station);

        Assert.Single(group.ChargeStations);
    }

    [Fact]
    public void AddChargeStation_WhenExceedsCapacity_ShouldThrow()
    {
        Group group = new Group(Guid.NewGuid(), "Test Group", 5);
        ChargeStation station = CreateStation(10);

        Assert.Throws<DomainException>(() => group.AddChargeStation(station));
    }

    [Fact]
    public void UpdateCapacity_BelowCurrentLoad_ShouldThrow()
    {
        Group group = new Group(Guid.NewGuid(), "Test Group", 100);
        group.AddChargeStation(CreateStation(50));

        Assert.Throws<DomainException>(() => group.UpdateCapacity(30));
    }

    [Fact]
    public void UpdateCapacity_AboveCurrentLoad_ShouldSucceed()
    {
        Group group = new Group(Guid.NewGuid(), "Test Group", 100);
        group.AddChargeStation(CreateStation(50));

        group.UpdateCapacity(60);

        Assert.Equal(60, group.Capacity);
    }

    [Fact]
    public void RemoveChargeStation_WhenExists_ShouldSucceed()
    {
        Group group = new Group(Guid.NewGuid(), "Test Group", 100);
        ChargeStation station = CreateStation(10);
        group.AddChargeStation(station);

        group.RemoveChargeStation(station.Id);

        Assert.Empty(group.ChargeStations);
    }

    [Fact]
    public void RemoveChargeStation_WhenNotExists_ShouldThrow()
    {
        Group group = new Group(Guid.NewGuid(), "Test Group", 100);

        Assert.Throws<DomainException>(() => group.RemoveChargeStation(Guid.NewGuid()));
    }

    private ChargeStation CreateStation(int maxCurrent)
    {
        List<Connector> connectors = new List<Connector> { new Connector(1, maxCurrent) };
        return new ChargeStation(Guid.NewGuid(), "Station", connectors);
    }
}
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Exceptions;

namespace SmartCharging.Tests.Unit;

public class ConnectorTests
{
    [Fact]
    public void CreateConnector_WithValidData_ShouldSucceed()
    {
        Connector connector = new Connector(1, 32);

        Assert.Equal(1, connector.Id);
        Assert.Equal(32, connector.MaxCurrent);
    }

    [Fact]
    public void CreateConnector_WithIdZero_ShouldThrow()
    {
        Assert.Throws<DomainException>(() => new Connector(0, 32));
    }

    [Fact]
    public void CreateConnector_WithIdSix_ShouldThrow()
    {
        Assert.Throws<DomainException>(() => new Connector(6, 32));
    }

    [Fact]
    public void CreateConnector_WithZeroMaxCurrent_ShouldThrow()
    {
        Assert.Throws<DomainException>(() => new Connector(1, 0));
    }

    [Fact]
    public void UpdateMaxCurrent_WithValidValue_ShouldSucceed()
    {
        Connector connector = new Connector(1, 32);

        connector.UpdateMaxCurrent(50);

        Assert.Equal(50, connector.MaxCurrent);
    }

    [Fact]
    public void UpdateMaxCurrent_WithZero_ShouldThrow()
    {
        Connector connector = new Connector(1, 32);

        Assert.Throws<DomainException>(() => connector.UpdateMaxCurrent(0));
    }
}
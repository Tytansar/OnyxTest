using FluentAssertions;
using System.Globalization;
using ToolsBazaar.Persistence;

namespace ToolsBazaar.Tests;

public class Tests
{
    [Fact]
    public void GetTop_ShouldReturn5Customers()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

        var repo = new CustomerRepository();

        var result = repo.GetTop(1, 5, new DateTime(2015, 1, 1), new DateTime(2022, 12, 31));

        result.Should().HaveCount(5);
    }

    [Fact]
    public void GetTop_ShouldReturnPage2Size2Customers()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

        var repo = new CustomerRepository();

        var result = repo.GetTop(2, 2, new DateTime(2015, 1, 1), new DateTime(2022, 12, 31));

        result.Should().HaveCount(2);
        result.First().Id.Should().Equals(31);
        result.Last().Id.Should().Equals(83);
    }

    [Fact]
    public void GetTop_ShouldReturnTopCustomerForMay2022()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

        var repo = new CustomerRepository();

        var result = repo.GetTop(1, 1, new DateTime(2022, 5, 1), new DateTime(2022, 5, 31));

        result.Should().HaveCount(1);
        result.First().Id.Should().Equals(19);
    }
}
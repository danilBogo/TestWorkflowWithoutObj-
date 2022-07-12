using System.Net.Http;
using System.Threading.Tasks;
using Homework7.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Hw7Tests;

public class TestFormTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly string _url = "/Test/TestModel";

    public TestFormTests(WebApplicationFactory<Program> fixture)
    {
        _client = fixture.CreateClient();
    }
    
    [Theory]
    [InlineData("FirstName", "First Name")]
    [InlineData("Age", "Age")]
    public async Task GetTestForm_PropsWithoutDisplayAttr_CamelCaseSplit(string propertyName, string expected)
    {
        //arrange
        var response = await TestHelper.GetFormHtml(_client, _url);

        //act
        var actual = TestHelper.GetPropertyNameFromLabel(response, propertyName);

        //assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("FirstName", TestHelper.RequiredMessage)]
    [InlineData("LastName", "")]
    [InlineData("MiddleName", TestHelper.RequiredMessage)]
    [InlineData("Age", TestHelper.AgeRangeErrorMessage)]
    public async Task PostEmptyTestForm_CheckForRequiredProperty_EveryPropertyIsRequiredExceptLastName(string propertyName,
        string expected)
    {
        //arrange
        var model = new BaseModel();
        var response = await TestHelper.SendForm(_client, _url, model);

        //act
        var actual = TestHelper.GetValidationMessageFromSpan(response, propertyName);

        //assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("FirstName", $"First Name {TestHelper.MaxLengthMessage}")]
    [InlineData("LastName", $"Last Name {TestHelper.MaxLengthMessage}")]
    [InlineData("MiddleName", "")]
    public async Task PostInvalidTestForm_CheckForMaxLengthValidation_EveryStringPropertyIsValidatedExceptMiddleName(string propertyName,
        string expected)
    {
        //arrange
        var model = new BaseModel{FirstName = TestHelper.LongString, LastName = TestHelper.LongString, MiddleName = TestHelper.LongString};
        var response = await TestHelper.SendForm(_client, _url, model);

        //act
        var actual = TestHelper.GetValidationMessageFromSpan(response, propertyName);

        //assert";
        Assert.Equal(expected, actual);
    }
}
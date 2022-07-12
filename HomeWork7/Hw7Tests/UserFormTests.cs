using System.Net.Http;
using System.Threading.Tasks;
using Homework7.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;


namespace Hw7Tests;

public class UserFormTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly string _url = "/Home/UserProfile";

    public UserFormTests(WebApplicationFactory<Program> fixture)
    {
        _client = fixture.CreateClient();
    }

    [Theory]
    [InlineData("FirstName", "text")]
    [InlineData("LastName", "text")]
    [InlineData("MiddleName", "text")]
    [InlineData("Age", "number")]
    public async Task EditorForModel_CheckInputTypes_CorrectTypes(string propertyName, string expectedType)
    {
        //arrange
        var response = await TestHelper.GetFormHtml(_client, _url);

        //act
        var actual = TestHelper.GetInputTypeForProperty(response, propertyName);

        //assert
        Assert.Equal(expectedType, actual);
    }

    [Fact]
    public async Task EditorForModel_CheckEnumTypeForSex_CorrectType()
    {
        //arrange
        var response = await TestHelper.GetFormHtml(_client, _url);

        //act
        var actual = TestHelper.TryGetSelect(response, "Sex");

        //assert
        Assert.True(actual);
    }
    
    [Theory]
    [InlineData("FirstName", "Имя")]
    [InlineData("LastName", "Фамилия")]
    [InlineData("MiddleName", "Отчество")]
    [InlineData("Age", "Возраст")]
    [InlineData("Sex", "Пол")]
    public async Task GetUserForm_ModelWithDisplayAttr_CorrectLabelsForProperties(string propertyName, string expected)
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
    [InlineData("LastName", TestHelper.RequiredMessage)]
    [InlineData("MiddleName", TestHelper.RequiredMessage)]
    [InlineData("Age", TestHelper.AgeRangeErrorMessage)]
    public async Task PostEmptyUserForm_ModelWithRequiredProps_EveryPropertyIsRequired(string propertyName,
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
    [InlineData("MiddleName", $"Middle Name {TestHelper.MaxLengthMessage}")]
    [InlineData("Age", "")]
    public async Task PostUserForm_ModelWithRequiredProps_EveryPropertyIsValidated(string propertyName,
        string expected)
    {
        //arrange
        var model = new BaseModel{FirstName = TestHelper.LongString, LastName = TestHelper.LongString, MiddleName = TestHelper.LongString, Age = 15, Sex = Sex.Male};
        var response = await TestHelper.SendForm(_client, _url, model);
    
        //act
        var actual = TestHelper.GetValidationMessageFromSpan(response, propertyName);
    
        //assert
        Assert.Equal(expected, actual);
    }
}
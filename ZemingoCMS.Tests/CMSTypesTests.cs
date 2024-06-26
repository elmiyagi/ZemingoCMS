using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using ZemingoCMS.Application.CMS.Types.DTOs;
using ZemingoCMS.Application.CMS.Types.Validation;
using ZemingoCMS.Application.Models;
using ZemingoCMS.Domain.CMS.Entities;
using ZemingoCMS.Infastructure.Data.EF;

namespace ZemingoCMS.Tests
{
    public class CMSTypesTests : IClassFixture<IntegrationTestsWebApplicationFactory<Program>>
    {
        private readonly IntegrationTestsWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public CMSTypesTests(IntegrationTestsWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            using var scope = _factory.Server.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var dbContext = scopedServices.GetRequiredService<CMSDbContext>();

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            SeedData(dbContext);

            _client = _factory.CreateClient();
            _client.BaseAddress = new Uri($"{_client.BaseAddress}api/CMSTypes/");
        }

        [Fact]
        public async Task PostCMSType_Returns_OK_With_Object()
        {
            var request = JsonContent.Create(new AddCMSTypeDTO("Name"));
            var response = await _client.PostAsync("", request);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var cmsTypeDTO = JsonConvert.DeserializeObject<CMSTypeDTO>(jsonResponse);

            Xunit.Assert.Equal("Name", cmsTypeDTO.Name);
            Xunit.Assert.False(cmsTypeDTO.Id == default);
        }

        [Fact]
        public async Task PostCMSType_Returns_Error_400_Name_Empty()
        {
            var request = JsonContent.Create(new AddCMSTypeDTO(string.Empty));
            var response = await _client.PostAsync("", request);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var commandResult = JsonConvert.DeserializeObject<CommandResult>(jsonResponse);

            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Xunit.Assert.False(commandResult.IsSuccess);
            Xunit.Assert.True(commandResult.Errors.Count == 1);
            Xunit.Assert.Equal(commandResult.Errors.First(), CMSTypeValidationConsts.NameLength());
        }

        [Fact]
        public async Task PostCMSType_Returns_Error_409_Name_Already_Exists()
        {
            var request = JsonContent.Create(new AddCMSTypeDTO("Name1"));
            var response = await _client.PostAsync("", request);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var commandResult = JsonConvert.DeserializeObject<CommandResult>(jsonResponse);

            Xunit.Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
            Xunit.Assert.False(commandResult.IsSuccess);
            Xunit.Assert.True(commandResult.Errors.Count == 1);
            Xunit.Assert.Equal(commandResult.Errors.First(), CMSTypeValidationConsts.NameAlreadyExists("Name1"));
        }

        [Fact]
        public async Task GetCMSTypes_Returns_List_Of_CMSTypeDTOs()
        {
            var jsonResponse = await _client.GetStringAsync("?Page=0&PageSize=100");
            var result = JsonConvert.DeserializeObject<List<CMSTypeDTO>>(jsonResponse);

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task GetCMSType_Returns_List_CMSTypeDTO()
        {
            var jsonResponse = await _client.GetStringAsync("9e908868-97ea-40e3-8381-577811ea9edd");
            var result = JsonConvert.DeserializeObject<CMSTypeDTO?>(jsonResponse);

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(Guid.Parse("9e908868-97ea-40e3-8381-577811ea9edd"), result.Id);
            Xunit.Assert.Equal("Name1", result.Name);
        }

        [Fact]
        public async Task GetCMSType_Returns_Error_404_Id_Not_Found()
        {
            var response = await _client.GetAsync("9e213768-97ea-40e3-8381-577811ea9edd");
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PutCMSType_Returns_OK_With_Object()
        {
            var request = JsonContent.Create(new UpdateCMSTypeDTO("New Name"));
            var response = await _client.PutAsync("9e908868-97ea-40e3-8381-577811ea9edd", request);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var cmsTypeDTO = JsonConvert.DeserializeObject<CMSTypeDTO>(jsonResponse);

            Xunit.Assert.Equal("New Name", cmsTypeDTO.Name);
            Xunit.Assert.Equal(Guid.Parse("9e908868-97ea-40e3-8381-577811ea9edd"), cmsTypeDTO.Id);
        }

        [Fact]
        public async Task PutCMSType_Returns_404_Id_Not_Found()
        {
            var request = JsonContent.Create(new UpdateCMSTypeDTO("New Name"));
            var response = await _client.PutAsync("9e213768-97ea-40e3-8381-577811ea9edd", request);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var commandResult = JsonConvert.DeserializeObject<CommandResult>(jsonResponse);

            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Xunit.Assert.False(commandResult.IsSuccess);
            Xunit.Assert.True(commandResult.Errors.Count == 1);
            Xunit.Assert.Equal(commandResult.Errors.First(), 
                CMSTypeValidationConsts.NotFoundById(Guid.Parse("9e213768-97ea-40e3-8381-577811ea9edd")));
        }

        [Fact]
        public async Task PutCMSType_Returns_409_Id_Name_Already_Exists()
        {
            var request = JsonContent.Create(new UpdateCMSTypeDTO("Name5"));
            var response = await _client.PutAsync("9e908868-97ea-40e3-8381-577811ea9edd", request);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var commandResult = JsonConvert.DeserializeObject<CommandResult>(jsonResponse);

            Xunit.Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
            Xunit.Assert.False(commandResult.IsSuccess);
            Xunit.Assert.True(commandResult.Errors.Count == 1);
            Xunit.Assert.Equal(commandResult.Errors.First(),
                CMSTypeValidationConsts.NameAlreadyExists("Name5"));
        }

        [Fact]
        public async Task PutCMSType_Returns_400_Name_Empty()
        {
            var request = JsonContent.Create(new UpdateCMSTypeDTO(string.Empty));
            var response = await _client.PutAsync("9e908868-97ea-40e3-8381-577811ea9edd", request);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var commandResult = JsonConvert.DeserializeObject<CommandResult>(jsonResponse);

            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Xunit.Assert.False(commandResult.IsSuccess);
            Xunit.Assert.True(commandResult.Errors.Count == 1);
            Xunit.Assert.Equal(commandResult.Errors.First(),
                CMSTypeValidationConsts.NameLength());
        }

        [Fact]
        public async Task Delete_CMSType_Returns_Ok()
        {
            var response = await _client.DeleteAsync("9e908868-97ea-40e3-8381-577811ea9edd");

            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_CMSType_Returns_Error_Not_Found()
        {
            var response = await _client.DeleteAsync("9e213768-97ea-40e3-8381-577811ea9edd");

            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        private static void SeedData(CMSDbContext dbContext)
        {
            dbContext.Set<CMSType>().Add(CMSType.Create(Guid.Parse("9e908868-97ea-40e3-8381-577811ea9edd"), "Name1"));
            dbContext.Set<CMSType>().Add(CMSType.Create(Guid.Parse("19796e65-f0e9-49be-aeeb-f3335d9f27aa"), "Name2"));
            dbContext.Set<CMSType>().Add(CMSType.Create(Guid.Parse("4d5137b0-7faa-4e69-bc59-b0edeaf4b7a5"), "Name3"));
            dbContext.Set<CMSType>().Add(CMSType.Create(Guid.Parse("dbade223-89b2-4a66-80cb-92fb5aa8a610"), "Name4"));
            dbContext.Set<CMSType>().Add(CMSType.Create(Guid.Parse("b64154d1-042b-445b-b43d-3ac2537d3e2c"), "Name5"));
            dbContext.SaveChanges();
        }
    }
}

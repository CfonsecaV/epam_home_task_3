using EpamHomeTask.Business.APIModels;
using EpamHomeTask.Core;
using log4net;
using log4net.Config;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace EpamHomeTask.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class APITests
    {
        private RestClientManager _restClientManager;
        protected ILog Log
        {
            get { return LogManager.GetLogger(this.GetType()); }
        }

        [SetUp]
        public void SetUp()
        {
            XmlConfigurator.Configure(new FileInfo("Log.config"));
            _restClientManager = new("https://jsonplaceholder.typicode.com");
        }
        [Test]
        public async Task ListOfUsersIsReceivedSuccesfully_ItIsRecived_Pass()
        {
            var request = new RestRequest("/users", Method.Get);
            var response = await _restClientManager.ExecuteAsync<List<User>>(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var users = response.Data;
            Assert.That(users, Is.Not.Null);
            Assert.That(users.Count, Is.GreaterThan(0));
            
            var properties = new List<string> { "Id", "Name", "UserName", "Email"
                , "Address", "Phone", "Website", "Company" };
            var missingProperties = properties.Where(property => users
                .All(user => user.GetType().GetProperty(property)==null)).ToList();
            Assert.That(missingProperties, Is.Empty, $"Missing properties: {string.Join(",", missingProperties)}");           
        }

        [Test]
        public async Task ListOfUsersContentType_ContentTypeIsCorrect_Pass()
        {
            var request = new RestRequest("/users", Method.Get);
            var response = await _restClientManager.ExecuteAsync<List<User>>(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var contentTypeHeader = response?.ContentHeaders?
                .FirstOrDefault(h => h?.Name?.Equals("Content-Type", StringComparison.Ordinal) ?? false);
            Assert.That(contentTypeHeader, Is.Not.Null, "Content-Type header is missing");

            string expectedValue = "application/json; charset=utf-8";
            var contentTypeValue = contentTypeHeader?.Value?.ToString();
            Assert.That(contentTypeValue, Is.EqualTo(expectedValue), $"Expected value: {expectedValue}," +
                $"Actual value: {contentTypeValue}");
        }

        [Test]
        public async Task ListOfUsersIsCorrect_ContentIsCorrect_Pass()
        {
            var request = new RestRequest("/users", Method.Get);
            var response = await _restClientManager.ExecuteAsync<List<User>>(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var users = response.Data;
            Assert.That(users, Has.Count.EqualTo(10));

            var distinctIds = (from user in users
                                select user.Id).Distinct();
            Assert.That(distinctIds.Count(), Is.EqualTo(10), "An Id is being repeated");

            var emptyUsers = from user in users
                             where string.IsNullOrEmpty(user.Name)
                             || string.IsNullOrEmpty(user.UserName)
                             select user;
            string emptyUserIds = string.Join(", ", emptyUsers.Select(u => u.Id));
            string emptyUserMessage = $"Users with empty name and/or UserName: {emptyUserIds}";
            Assert.That(emptyUsers.Count(), Is.EqualTo(0), emptyUserMessage);

            var emptyCompanies = from user in users
                                 where user.Company == null
                                 || string.IsNullOrEmpty (user.Company.Name)
                                 select user;
            string emptyCompanyIds = string.Join(", ", emptyCompanies.Select(u => u.Id));
            string emptyCompanyMessage = $"Users with empty company name: {emptyCompanyIds}";
            Assert.That(emptyCompanies.Count(), Is.EqualTo(0), emptyCompanyMessage);
        }
        [Test]
        public async Task UserCanBeCreated_UserIsCreated_Pass() 
        {
            User newUser = new User() 
            { 
                Name = "Andres",
                UserName = "Rodrigo"
            };
            var request = new RestRequest("/users", Method.Post);
            request.AddJsonBody(newUser);
            var response = await _restClientManager.ExecuteAsync<User>(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var createdUser = response.Data;
            Assert.That(createdUser, Is.Not.Null);
            Assert.That(createdUser.Id, Is.GreaterThan(0));
        }

        [Test]
        public async Task UserIsNotifedIfResourceDoesNotExist_UserIsNotified_Pass()
        {
            var request = new RestRequest("/invalidendpoint", Method.Get);
            var response = await _restClientManager.ExecuteAsync<List<User>>(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

    }
}

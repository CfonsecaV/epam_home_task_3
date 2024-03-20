using EpamHomeTask.Business.APIModels;
using EpamHomeTask.Core;
using log4net;
using log4net.Config;
using NUnit.Framework;
using RestSharp;
using System.Data;
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

        private void AssertAndLog(Action assertion, string correctMessage,string errorMessage)
        {
            try
            {
                assertion();
                Log.Info(correctMessage);
            }catch (AssertionException ex) 
            {
                Log.Fatal(errorMessage,ex);
            }
        }
        private void AssertStatus<T>(T response, T expectedStatus)
        {
            string statusCorrectMessage = $"status code is '{response}' as expected";
            string statusErrorMessage = $"Expected status: '{expectedStatus}', Actual status: '{response}'";
            AssertAndLog(() => Assert.That(response, Is.EqualTo(expectedStatus))
                , statusCorrectMessage, statusErrorMessage);
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
            Log.Info("Sending request...");
            var request = new RestRequest("/users", Method.Get);
            var response = await _restClientManager.ExecuteAsync<List<User>>(request);

            Log.Info("Checking status code...");
            AssertStatus(response.StatusCode, HttpStatusCode.OK);

            Log.Info("Checking if list of users is null or empty...");
            var users = response.Data;
            AssertAndLog(() => Assert.That(users, Is.Not.Null)
                , "List of users is not null", "List of users is null");
            AssertAndLog(() => Assert.That(users?.Count, Is.GreaterThan(0))
                , "List of users is not empty", "List of users is empty");

            Log.Info("Cheking if any user is missing a property...");
            var properties = new List<string> { "Id", "Name", "UserName", "Email"
                , "Address", "Phone", "Website", "Company" };
            var missingProperties = properties.Where(property => users
                .All(user => user.GetType().GetProperty(property)==null)).ToList();
            string correctProperties = "No properties are missing";
            string missingPropertiesMessage = $"Missing properties: '{string.Join(",", missingProperties)}'";
            AssertAndLog(() => Assert.That(missingProperties, Is.Empty, missingPropertiesMessage)
                ,correctProperties, missingPropertiesMessage);            
        }

        [Test]
        public async Task ListOfUsersContentType_ContentTypeIsCorrect_Pass()
        {
            Log.Info("Sending request...");
            var request = new RestRequest("/users", Method.Get);
            var response = await _restClientManager.ExecuteAsync<List<User>>(request);

            Log.Info("Checking status code...");
            AssertStatus(response.StatusCode, HttpStatusCode.OK);

            Log.Info("Checking if Content-Type heather is present...");
            var contentTypeHeader = response?.ContentHeaders?
                .FirstOrDefault(h => h?.Name?.Equals("Content-Type", StringComparison.Ordinal) ?? false);
            string headerCorrect = "Content-Type header is present";
            string headerError = "Content-Type header is missing";
            AssertAndLog(() => Assert.That(contentTypeHeader, Is.Not.Null, headerError), headerCorrect, headerError);

            Log.Info("Checking header value...");
            string expectedValue = "application/json; charset=utf-8";
            var contentTypeValue = contentTypeHeader?.Value?.ToString();
            string valueCorrect = $"Value is '{contentTypeValue}' as expected";
            string valueError = $"Expected value: '{expectedValue}'," +
                $"Actual value: '{contentTypeValue}'";
            AssertAndLog(() => Assert.That(contentTypeValue, Is.EqualTo(expectedValue), valueError)
                , valueCorrect, valueError);
        }

        [Test]
        public async Task ListOfUsersIsCorrect_ContentIsCorrect_Pass()
        {
            Log.Info("Sending request...");
            var request = new RestRequest("/users", Method.Get);
            var response = await _restClientManager.ExecuteAsync<List<User>>(request);

            Log.Info("Checking status code...");
            AssertStatus(response.StatusCode, HttpStatusCode.OK);

            Log.Info("Checking if users list is complete...");
            var users = response.Data;
            string completeCorrect = $"The amount of users is '{users?.Count}' as expected";
            string completeError = $"Expected amount of users: '10', Actual amount of users: '{users?.Count}'";
            AssertAndLog(() => Assert.That(users, Has.Count.EqualTo(10)), completeCorrect, completeError);

            Log.Info("Checking if all Ids are unique...");
            var distinctIds = (from user in users
                                select user.Id).Distinct();
            string uniqueCorrect = $"The amount of unique Ids is '{distinctIds.Count()}', as expected";
            string uniqueError = $"Expected amount of unique Ids: '10', Actual amount: '{distinctIds.Count()}'";
            AssertAndLog(() => Assert.That(distinctIds.Count(), Is.EqualTo(10), uniqueError), uniqueCorrect, uniqueError);

            Log.Info("Checking if any user has empty name or username...");
            var emptyUsers = from user in users
                             where string.IsNullOrEmpty(user.Name)
                             || string.IsNullOrEmpty(user.UserName)
                             select user;
            string emptyUserIds = string.Join(", ", emptyUsers.Select(u => u.Id));
            string emptyUserCorrect = "There are no users with missing Name and/or Username, as expected";
            string emptyUserError = $"Expected users missing Name and/or User Name: '0', Actual users: {emptyUserIds}";
            AssertAndLog(() => Assert.That(emptyUsers.Count(), Is.EqualTo(0), emptyUserError)
                , emptyUserCorrect, emptyUserError);

            Log.Info("Checking if any user has empty company name...");
            var emptyCompanies = from user in users
                                 where user.Company == null
                                 || string.IsNullOrEmpty (user.Company.Name)
                                 select user;
            string emptyCompanyIds = string.Join(", ", emptyCompanies.Select(u => u.Id));
            string emptyCompanyCorrect = "There are no users with missing Company name, as expected";
            string emptyCompanyError = $"Expected users with empty company name: '0', Actual users: {emptyCompanyIds}";
            AssertAndLog(() => Assert.That(emptyCompanies.Count(), Is.EqualTo(0), emptyCompanyError)
                , emptyCompanyCorrect, emptyCompanyError);            
        }
        [Test]
        public async Task UserCanBeCreated_UserIsCreated_Pass() 
        {
            User newUser = new User() 
            { 
                Name = "Andres",
                UserName = "Rodrigo"
            };

            Log.Info("Sending request...");
            var request = new RestRequest("/users", Method.Post);
            request.AddJsonBody(newUser);
            var response = await _restClientManager.ExecuteAsync<User>(request);

            Log.Info("Checking status code...");
            AssertStatus(response.StatusCode, HttpStatusCode.Created);

            Log.Info("Checking that created user data exists...");
            var createdUser = response.Data;
            AssertAndLog(() => Assert.That(createdUser, Is.Not.Null)
                ,"User data is not null, as expected", "User data is null" );
            AssertAndLog(() => Assert.That(createdUser?.Id, Is.GreaterThan(0)),
                $"User Id is {createdUser?.Id}, as expected", "User Id is not properly created");            
        }

        [Test]
        public async Task UserIsNotifedIfResourceDoesNotExist_UserIsNotified_Pass()
        {
            Log.Info("Sending request...");
            var request = new RestRequest("/invalidendpoint", Method.Get);
            var response = await _restClientManager.ExecuteAsync<List<User>>(request);

            Log.Info("Checking status code...");
            AssertStatus(response.StatusCode, HttpStatusCode.NotFound);
        }

    }
}

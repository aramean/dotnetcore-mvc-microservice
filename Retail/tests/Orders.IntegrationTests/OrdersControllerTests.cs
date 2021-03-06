﻿using System;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.IO;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using FluentAssertions.Extensions;
using System.Collections.Generic;
using System.Text;

namespace Orders.IntegrationTests
{
    public class OrdersControllerTest : IntegrationTest
    {

        [Fact]
        public async Task GetAll_ShouldBeOKAndCorrectContentType()
        {

            // Arange
            //await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("api/orders/");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
        }

        [Fact]
        public async Task GetAllWithPaginator_ShouldBeOKAndCorrectContentType()
        {

            // Arange
            //await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("api/orders/0/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
        }

        [Theory]
        [InlineData("api/orders/1")]
        [InlineData("api/orders/2")]
        [InlineData("api/orders/3")]
        public async Task Get_ShouldBeOKAndCorrectContentType(string url)
        {

            // Arange
            //await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
        }

        [Theory]
        [InlineData("api/orders/1", 1)]
        [InlineData("api/orders/2", 2)]
        [InlineData("api/orders/3", 3)]
        public async Task Put_ShouldBeOK(string url, long key1)
        {

            // Arange
            //await AuthenticateAsync();
            var keyDate = DateTime.Now.ToString("MMddmmssff");
            var payload = "{\"OrderNumber\": " + key1 + ", \"OrderRegistrationNumber\": " + key1 + ", \"OrderDate\": " + keyDate + "}";
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(url, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("api/orders/1", 111)]
        [InlineData("api/orders/2", 222)]
        [InlineData("api/orders/3", 333)]
        public async Task Put_ShouldBeBadRequest(string url, long key1)
        {

            // Arange
            //await AuthenticateAsync();
            var keyDate = DateTime.Now.ToString("MMddmmssff");
            var payload = "{\"OrderNumber\": " + key1 + ", \"OrderRegistrationNumber\": " + key1 + ", \"OrderDate\": " + keyDate + "}";
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(url, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("api/orders/1", 1)]
        [InlineData("api/orders/2", 2)]
        [InlineData("api/orders/3", 3)]
        public async Task Patch_ShouldBeOKWithRespondArrivedAndCorrectContentType(string url, long key1)
        {
            // Arange
            //await AuthenticateAsync();
            var payload = "{\"OrderRegistrationNumber\": " + key1 + "}";
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PatchAsync(url, content);
            var contents = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            contents.Should().Contain("\"orderStatus\":\"Arrived\"");
        }

        [Theory]
        [InlineData("api/orders/")]
        public async Task Post_ShouldBeOKWithRespondNewAndCorrectContentType(string url)
        {

            // Arange
            //await AuthenticateAsync();
            var uniqueId = DateTime.Now.ToString("MMddmmssff");
            var payload = "{\"OrderNumber\": " + uniqueId + ", \"OrderRegistrationNumber\": " + uniqueId + "}";
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(url, content);
            var contents = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            contents.Should().Contain("\"orderStatus\":\"New\"");
        }


        [Theory]
        [InlineData("api/orders/", 1, 1)]
        [InlineData("api/orders/", 2, 2)]
        [InlineData("api/orders/", 3, 3)]
        public async Task Post_ShouldBeConflictAndCorrectContentType(string url, long key1, long key2)
        {

            // Arange
            //await AuthenticateAsync();
            var payload = "{\"OrderNumber\": " + key1 + ", \"OrderRegistrationNumber\": " + key2 + "}";
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(url, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

    }

}
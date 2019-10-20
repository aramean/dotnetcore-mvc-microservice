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

        [Theory]
        [InlineData("api/orders/")]
        public async Task GetAll_ShouldBeOKAndCorrectContentType(string url)
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
        [InlineData("api/orders/0")]
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
        [InlineData("api/orders/0")]
        public async Task Put_ShouldBeOKAndCorrectContentType(string url)
        {

            // Arange
            //await AuthenticateAsync();
            var payload = "{\"OrderId\": 0, \"OrderNumber\": 0, \"OrderRegistrationNumber\": 0}";
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(url, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
        }

        [Theory]
        [InlineData("api/orders/")]
        public async Task Post_ShouldBeOKWithRespondNewAndCorrectContentType(string url)
        {

            // Arange
            //await AuthenticateAsync();
            var payload = "{\"OrderNumber\": 1, \"OrderRegistrationNumber\": 1}";
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
        [InlineData("api/orders/")]
        public async Task Post_ShouldBeConflictAndCorrectContentType(string url)
        {

            // Arange
            //await AuthenticateAsync();
            var payload = "{\"OrderNumber\": 0, \"OrderRegistrationNumber\": 0}";
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(url, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

    }

}
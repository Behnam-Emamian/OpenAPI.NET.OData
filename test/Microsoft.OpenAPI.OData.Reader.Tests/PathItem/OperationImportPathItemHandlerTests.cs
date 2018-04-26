﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.OData.Edm;
using Microsoft.OpenApi.OData.Tests;
using Xunit;

namespace Microsoft.OpenApi.OData.PathItem.Tests
{
    public class OperationImportPathItemGeneratorTest
    {
        private OperationImportPathItemHandler _pathItemHandler = new OperationImportPathItemHandler();

        [Theory]
        [InlineData("GetNearestAirport", OperationType.Get)]
        [InlineData("ResetDataSource", OperationType.Post)]
        public void CreatePathItemForOperationImportReturnsCorrectPathItem(string operationImport,
            OperationType operationType)
        {
            // Arrange
            IEdmModel model = EdmModelHelper.TripServiceModel;
            ODataContext context = new ODataContext(model);
            IEdmOperationImport edmOperationImport = model.EntityContainer
                .OperationImports().FirstOrDefault(o => o.Name == operationImport);
            Assert.NotNull(edmOperationImport); // guard
            string expectSummary = "Invoke " +
                (edmOperationImport.IsActionImport() ? "action " : "function ") + operationImport;
            ODataPath path = new ODataPath(new ODataOperationImportSegment(edmOperationImport));

            // Act
            OpenApiPathItem pathItem = _pathItemHandler.CreatePathItem(context, path);

            // Assert
            Assert.NotNull(pathItem);
            Assert.NotNull(pathItem.Operations);
            var operationKeyValue = Assert.Single(pathItem.Operations);
            Assert.Equal(operationType, operationKeyValue.Key);
            Assert.NotNull(operationKeyValue.Value);

            Assert.Equal(expectSummary, operationKeyValue.Value.Summary);
        }
    }
}

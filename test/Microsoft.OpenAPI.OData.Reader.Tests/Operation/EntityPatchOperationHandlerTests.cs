﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.OData.Edm;
using Microsoft.OpenApi.OData.Tests;
using Xunit;

namespace Microsoft.OpenApi.OData.Operation.Tests
{
    public class EntityPatchOperationHandlerTests
    {
        private EntityPatchOperationHandler _operationHandler = new EntityPatchOperationHandler();

        [Fact]
        public void CreateEntityPatchOperationReturnsCorrectOperation()
        {
            // Arrange
            IEdmModel model = EdmModelHelper.BasicEdmModel;
            IEdmEntitySet entitySet = model.EntityContainer.FindEntitySet("People");
            ODataContext context = new ODataContext(model);
            ODataPath path = new ODataPath(new ODataNavigationSourceSegment(entitySet), new ODataKeySegment(entitySet.EntityType()));

            // Act
            var patch = _operationHandler.CreateOperation(context, path);

            // Assert
            Assert.NotNull(patch);
            Assert.Equal("Update entity in People", patch.Summary);
            Assert.NotNull(patch.Tags);
            var tag = Assert.Single(patch.Tags);
            Assert.Equal("People.Person", tag.Name);

            Assert.NotNull(patch.Parameters);
            Assert.Equal(1, patch.Parameters.Count);

            Assert.NotNull(patch.RequestBody);

            Assert.NotNull(patch.Responses);
            Assert.Equal(2, patch.Responses.Count);
            Assert.Equal(new[] { "204", "default" }, patch.Responses.Select(r => r.Key));
        }
    }
}

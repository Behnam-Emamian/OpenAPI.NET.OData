﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.OData.Edm;
using System.Collections.Generic;

namespace Microsoft.OpenApi.OData.Operation
{
    /// <summary>
    /// A class to provide the <see cref="IOperationHandler"/>.
    /// </summary>
    internal class OperationHandlerProvider : IOperationHandlerProvider
    {
        private IDictionary<ODataPathType, IDictionary<OperationType, IOperationHandler>> _handlers;

        /// <summary>
        /// Initializes a new instance of <see cref="OperationHandlerProvider"/> class.
        /// </summary>
        public OperationHandlerProvider()
        {
            _handlers = new Dictionary<ODataPathType, IDictionary<OperationType, IOperationHandler>>();

            // entity set (Get/Post)
            _handlers[ODataPathType.EntitySet] = new Dictionary<OperationType, IOperationHandler>
            {
                {OperationType.Get, new EntitySetGetOperationHandler() },
                {OperationType.Post, new EntitySetPostOperationHandler() }
            };

            // entity (Get/Patch/Delete)
            _handlers[ODataPathType.Entity] = new Dictionary<OperationType, IOperationHandler>
            {
                {OperationType.Get, new EntityGetOperationHandler() },
                {OperationType.Patch, new EntityPatchOperationHandler() },
                {OperationType.Delete, new EntityDeleteOperationHandler() }
            };

            // singleton (Get/Patch)
            _handlers[ODataPathType.Singleton] = new Dictionary<OperationType, IOperationHandler>
            {
                {OperationType.Get, new SingletonGetOperationHandler() },
                {OperationType.Patch, new SingletonPatchOperationHandler() }
            };

            // edm operation (Get|Post)
            _handlers[ODataPathType.Operation] = new Dictionary<OperationType, IOperationHandler>
            {
                {OperationType.Get, new EdmFunctionOperationHandler() },
                {OperationType.Post, new EdmActionOperationHandler() }
            };

            // edm operation import (Get|Post)
            _handlers[ODataPathType.OperationImport] = new Dictionary<OperationType, IOperationHandler>
            {
                {OperationType.Get, new EdmFunctionImportOperationHandler() },
                {OperationType.Post, new EdmActionImportOperationHandler() }
            };

            // navigation property (Get/Patch/Post)
            _handlers[ODataPathType.NavigationProperty] = new Dictionary<OperationType, IOperationHandler>
            {
                {OperationType.Get, new NavigationPropertyGetOperationHandler() },
                {OperationType.Patch, new NavigationPropertyPatchOperationHandler() },
                {OperationType.Post, new NavigationPropertyPostOperationHandler() }
            };
        }

        /// <inheritdoc/>
        public IOperationHandler GetHandler(ODataPathType pathType, OperationType operationType)
        {
            return _handlers[pathType][operationType];
        }
    }
}

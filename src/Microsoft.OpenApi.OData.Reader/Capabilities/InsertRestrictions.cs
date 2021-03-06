﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OpenApi.OData.Capabilities
{
    /// <summary>
    /// Org.OData.Capabilities.V1.InsertRestrictions
    /// </summary>
    internal class InsertRestrictions : CapabilitiesRestrictions
    {
        /// <summary>
        /// The Term type kind.
        /// </summary>
        public override CapabilitesTermKind Kind => CapabilitesTermKind.InsertRestrictions;

        /// <summary>
        /// Gets the Insertable value.
        /// </summary>
        public bool? Insertable { get; private set; }

        /// <summary>
        /// Gets the navigation properties which do not allow deep inserts.
        /// </summary>
        public IList<string> NonInsertableNavigationProperties { get; private set; }

        /// <summary>
        /// Test the target supports insert.
        /// </summary>
        /// <returns>True/false.</returns>
        public bool IsInsertable => Insertable == null || Insertable.Value == true;

        /// <summary>
        /// Test the input navigation property do not allow deep insert.
        /// </summary>
        /// <param name="navigationPropertyPath">The input navigation property path.</param>
        /// <returns>True/False.</returns>
        public bool IsNonInsertableNavigationProperty(string navigationPropertyPath)
        {
            return NonInsertableNavigationProperties != null ?
                NonInsertableNavigationProperties.Any(a => a == navigationPropertyPath) :
                false;
        }

        protected override bool Initialize(IEdmVocabularyAnnotation annotation)
        {
            if (annotation == null ||
                annotation.Value == null ||
                annotation.Value.ExpressionKind != EdmExpressionKind.Record)
            {
                return false;
            }

            IEdmRecordExpression record = (IEdmRecordExpression)annotation.Value;

            // Insertable
            Insertable = record.GetBoolean("Insertable");

            // NonInsertableNavigationProperties
            NonInsertableNavigationProperties = record.GetCollectionPropertyPath("NonInsertableNavigationProperties");

            return true;
        }
    }
}

// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using Microsoft.OData.Edm;
using Microsoft.OpenApi.OData.Common;

namespace Microsoft.OpenApi.OData.Edm
{
    /// <summary>
    /// Navigation property segment.
    /// </summary>
    public class ODataNavigationPropertySegment : ODataSegment
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ODataNavigationPropertySegment"/> class.
        /// </summary>
        /// <param name="navigationProperty"></param>
        public ODataNavigationPropertySegment(IEdmNavigationProperty navigationProperty)
        {
            NavigationProperty = navigationProperty ?? throw Error.ArgumentNull(nameof(navigationProperty));
        }

        /// <summary>
        /// Gets the navigation property.
        /// </summary>
        public IEdmNavigationProperty NavigationProperty { get; }

        /// <inheritdoc />
        public override IEdmEntityType EntityType => NavigationProperty.ToEntityType();

        /// <inheritdoc />
        public override string ToString()
        {
            return NavigationProperty.Name;
        }
    }
}
﻿//---------------------------------------------------------------------
// <copyright file="Program.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;

namespace OoasUtil
{
    class Program
    {
        static int Main(string[] args)
        {
            // args = new[] { "--json", "--input", @"E:\work\OneApiDesign\test\TripService.OData.xml", "-o", @"E:\work\OneApiDesign\test1\Trip.json" };
            // args = new[] { "--yaml", "-i", @"E:\work\OneApiDesign\test\TripService.OData.xml", "-o", @"E:\work\OneApiDesign\test1\Trip.yaml" };
            // args = new[] { "--yaml", "--input", @"http://services.odata.org/TrippinRESTierService", "-o", @"E:\work\OneApiDesign\test1\TripUrl.yaml" };
            // args = new[] { "--json", "-i", @"http://services.odata.org/TrippinRESTierService", "-o", @"E:\work\OneApiDesign\test1\TripUrl.json" };

            args = new[] { "--json", "--input", @"E:\work\OneApiDesign\test1\beta.xml", "-o", @"E:\work\OneApiDesign\test1\beta.json", "-a", @"E:\work\OneApiDesign\Annotations\" };

            ComLineProcesser processer = new ComLineProcesser(args);
            if (!processer.Process())
            {
                return 0; 
            }

            if (!processer.CanContinue)
            {
                return 1;
            }

            OpenApiGenerator generator;
            if (processer.IsLocalFile)
            {
                generator = new FileOpenApiGenerator(processer);
            }
            else
            {
                generator = new UrlOpenApiGenerator(processer);
            }

            if (generator.Generate())
            {
                Console.WriteLine("Successed!");
                return 1;
            }
            else
            {
                Console.WriteLine("Failed!");
                return 0;
            }
        }
    }
}

﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Unity;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.PolicyInjection.Pipeline;
using Unity.Registration;

namespace Microsoft.Practices.Unity.InterceptionExtension.Configuration
{
    /// <summary>
    /// Configuration element representing a call handler.
    /// </summary>
    public class CallHandlerElement : PolicyChildElement
    {
        internal void Configure(IUnityContainer container, PolicyDefinition policyDefinition)
        {
            if (string.IsNullOrEmpty(this.TypeName))
            {
                policyDefinition.AddCallHandler(this.Name);
            }
            else
            {
                Type handlerType = TypeResolver.ResolveType(TypeName);
                IEnumerable<InjectionMember> injectionMembers =
                    Injection.SelectMany(
                        element => element.GetInjectionMembers(container, typeof(ICallHandler), handlerType, Name));
                policyDefinition.AddCallHandler(handlerType, Lifetime.CreateLifetimeManager(),
                    injectionMembers.ToArray());
            }
        }
    }
}

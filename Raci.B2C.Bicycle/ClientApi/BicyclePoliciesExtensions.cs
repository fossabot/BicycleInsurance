// Code generated by Microsoft (R) AutoRest Code Generator 0.12.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Raci.B2C.Bicycle.ClientApi
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    public static partial class BicyclePoliciesExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='details'>
            /// </param>
            public static PolicyDTO Create(this IBicyclePolicies operations, BicyclePolicyDetailDTO details)
            {
                return Task.Factory.StartNew(s => ((IBicyclePolicies)s).CreateAsync(details), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='details'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<PolicyDTO> CreateAsync( this IBicyclePolicies operations, BicyclePolicyDetailDTO details, CancellationToken cancellationToken = default(CancellationToken))
            {
                HttpOperationResponse<PolicyDTO> result = await operations.CreateWithHttpMessagesAsync(details, null, cancellationToken).ConfigureAwait(false);
                return result.Body;
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='details'>
            /// </param>
            public static PolicyDTO SetDetails(this IBicyclePolicies operations, long? id, BicyclePolicyDetailDTO details)
            {
                return Task.Factory.StartNew(s => ((IBicyclePolicies)s).SetDetailsAsync(id, details), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='details'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<PolicyDTO> SetDetailsAsync( this IBicyclePolicies operations, long? id, BicyclePolicyDetailDTO details, CancellationToken cancellationToken = default(CancellationToken))
            {
                HttpOperationResponse<PolicyDTO> result = await operations.SetDetailsWithHttpMessagesAsync(id, details, null, cancellationToken).ConfigureAwait(false);
                return result.Body;
            }

    }
}

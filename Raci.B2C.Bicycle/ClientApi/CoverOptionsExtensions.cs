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

    public static partial class CoverOptionsExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<CoverOptionDTO> GetCoverOptions(this ICoverOptions operations)
            {
                return Task.Factory.StartNew(s => ((ICoverOptions)s).GetCoverOptionsAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<CoverOptionDTO>> GetCoverOptionsAsync( this ICoverOptions operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                HttpOperationResponse<IList<CoverOptionDTO>> result = await operations.GetCoverOptionsWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false);
                return result.Body;
            }

    }
}

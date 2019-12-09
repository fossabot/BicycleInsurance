using System;
using System.Threading.Tasks;
using Microsoft.Rest;
using Raci.B2C.Bicycle.FormHandlers;

namespace Raci.B2C.Bicycle.Utils
{
    public static class TaskExtensions
    {
        private const int DefaultCallTimeout = 1000 * 30; // 30 seconds

        public static async Task<T> Data<T>(this Task<HttpOperationResponse<T>> serviceCall)
        {
            HttpOperationResponse<T> result = await serviceCall;

            if (!result.Response.IsSuccessStatusCode)
            {
                throw new ServiceInvocationException(result.Response);
            }

            return result.Body;
        }

        public static T WaitForServiceCall<T>(this Task<HttpOperationResponse<T>> serviceCall, int timeout = DefaultCallTimeout)
        {
            HttpOperationResponse<T> result = Task.Run(async () => await serviceCall.TimeoutAfter(timeout)).Result;

            if (!result.Response.IsSuccessStatusCode)
            {
                throw new ServiceInvocationException(result.Response);
            }

            return result.Body;
        }

        public static async Task<T> TimeoutAfter<T>(this Task<T> task, int millisecondsTimeout)
        {
            if (task == await Task.WhenAny(task, Task.Delay(millisecondsTimeout)))
            {
                return await task;
            }

            throw new TimeoutException();
        }
    }
}
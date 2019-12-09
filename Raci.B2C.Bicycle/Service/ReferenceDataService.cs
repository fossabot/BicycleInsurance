using System.Collections.Generic;
using Raci.B2C.Bicycle.ClientApi;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.Utils;
using Raci.B2C.Common.Unity.Extension;

namespace Raci.B2C.Bicycle.Service
{
    public class ReferenceDataService : IReferenceDataService
    {
        private readonly IReferenceDatas _referenceDatas;

        public ReferenceDataService(IReferenceDatas referenceDatas)
        {
            _referenceDatas = referenceDatas;
        }

        [CachingCallHandler(CacheProfile.Short)]
        public virtual IList<ReferenceData> GetBicycleTypes()
        {
            return _referenceDatas.GetBicycleTypesWithHttpMessagesAsync(Jwt.CreateAuthorizationHeader(null)).WaitForServiceCall();
        }

        [CachingCallHandler(CacheProfile.Short)]
        public virtual MinMaxDTO GetSumInuredRange()
        {
            return _referenceDatas.GetSumInuredRangeWithHttpMessagesAsync(Jwt.CreateAuthorizationHeader(null)).WaitForServiceCall();
        }
    }
}
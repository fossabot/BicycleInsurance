using System.Collections.Generic;
using Raci.B2C.Bicycle.ClientApi.Models;

namespace Raci.B2C.Bicycle.Service
{
    public interface IReferenceDataService
    {
        IList<ReferenceData> GetBicycleTypes();
        MinMaxDTO GetSumInuredRange();
    }
}

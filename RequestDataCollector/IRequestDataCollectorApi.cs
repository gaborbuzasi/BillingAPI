using System.Threading.Tasks;

namespace RequestDataCollector.SDK
{
    public interface IRequestDataCollectorApi
    {
        Task<GetRequestDataResponse> GetRequestData(GetRequestDataRequest request);
    }
}

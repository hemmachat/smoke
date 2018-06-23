using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebRanking
{
    public interface IRequest
    {
        Task<string> MakeRequest(string url);
        List<WebItem> FetchWebItems();
    }
}
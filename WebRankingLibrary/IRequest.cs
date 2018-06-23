using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebRankingLibrary
{
    public interface IRequest
    {
        Task<string> MakeRequest(string url);
        List<WebItem> FetchWebItems();
    }
}
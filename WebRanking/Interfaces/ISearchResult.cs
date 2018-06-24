using System.Collections.Generic;
using System.Threading.Tasks;
using WebRanking.Models;

namespace WebRanking.Interfaces
{
    public interface ISearchResult
    {
        Task<List<WebItem>> FetchWebItems(string keyword, string searchUrl);
    }
}
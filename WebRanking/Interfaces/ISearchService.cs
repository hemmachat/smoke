using System.Collections.Generic;
using System.Threading.Tasks;
using WebRanking.Models;

namespace WebRanking.Interfaces
{
    public interface ISearchService
    {
        Task<List<WebItem>> FetchWebItemsAsync(string keyword, string searchUrl);
    }
}
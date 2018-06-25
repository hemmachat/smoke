using System;
using System.Linq;
using Xunit;
using WebRanking;
using System.Threading.Tasks;

namespace WebRankingTest
{
    public class ManualSearchServiceTest
    {
        [Fact]
        public async Task Test_First_RankAsync()
        {
            var searchService = new ManualSearchService();
            var results = await searchService.FetchWebItemsAsync("conveyancing software", "www.smokeball.com.au");

            Assert.Equal(100, results.Count);
            Assert.Equal(true, results[0].ContainUrl);
            Assert.Equal("https://www.smokeball.com.au/", results[0].Link);
            Assert.Equal(1, results[0].Rank);
            Assert.Equal("Smokeball - Smokeball - Cloud <b>Conveyancing Software</b> Program ...", results[0].Title);
            Assert.Equal("Looking for affordable and easy to use cloud <b>conveyancing software</b> program? <br>\nLook no further! Smokeball is exactly that &amp; enables you to become truly&nbsp;...", results[0].Description);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WebRanking;
using WebRanking.ViewModels;
using Moq;
using WebRanking.Interfaces;

namespace WebRankingTest
{
    public class MainWindowViewModelTest
    {
        private readonly Mock<ISearchService> _searchService;
        private readonly MainWindowViewModel _viewModel;

        public MainWindowViewModelTest()
        {
            var factory = new MockRepository(MockBehavior.Loose);
            _searchService = factory.Create<ISearchService>();
            _viewModel = new MainWindowViewModel(_searchService.Object);
        }

        [Fact]
        public void With_Result_Test()
        {
            // setup mock service values for "conveyancing software" and "www.smokeball.com.au"

            // execute the service

            // assert for "1" value
        }

        [Fact]
        public void No_Result_Test()
        {
            // setup mock service values for "conveyancing software" and "any company"

            // execute the service

            // assert for "0" value as there should not be anything

        }
    }
}

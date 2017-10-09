using System;
using System.Collections.Generic;
using System.Linq;
using Darkness.Linq.Extensions;
using Darkness.Linq.Pagination;
using Xunit;

namespace Darkness.Linq.Tests
{
    public class PaginationTest
    {
        
        private static List<TestDto> _tests = new List<TestDto>
        {
            new TestDto
            {
                Id = 1,
                Name = "1"
            },
            new TestDto
            {
                Id = 1,
                Name = "2"
            },
            new TestDto
            {
                Id = 1,
                Name = "3"
            },
        };
        
        [Fact]
        public void TestPaginateWithCustomPaginatedDto()
        {
            var filter = new Paginal(page: 1, take: 2);

            var result = _tests.AsQueryable()
                .PaginateTo(filter, new TestListDto());
            
            Assert.Equal(2, result.Pager.Pages);
            Assert.Equal(3, result.Pager.Total);
        }
        
    }

    public class TestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TestListDto : Paginated<TestDto>
    {
        
    }
}
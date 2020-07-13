using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoardGamesManagerCore.Extensions;
using FluentAssertions;
using Xunit;

namespace BoardGamesManagerCoreTest.Extensions
{
    public class PaginationLinqExtensionsTest
    {
        private static readonly int[] _testCollection = {1, 2, 3, 4, 5, 6, 7};
        private static readonly IQueryable<int> _queryableCollection = _testCollection.AsQueryable();
        private static readonly IEnumerable<int> _enumerableCollection = _testCollection.AsEnumerable();

        [Theory(DisplayName = "IQueryable is correctly paginated")]
        [ClassData(typeof(PaginationTestData))]
        public void IQueryable_is_correctly_paginated(int? pageSize, int? page, int[] expectedPaginatedResult) => TestIQueryable(_queryableCollection, pageSize, page, expectedPaginatedResult);

        [Theory(DisplayName = "IEnumerable is correctly paginated")]
        [ClassData(typeof(PaginationTestData))]
        public void IEnumerable_is_correctly_paginated(int? pageSize, int? page, int[] expectedPaginatedResult) => TestIEnumerable(_enumerableCollection, pageSize, page, expectedPaginatedResult);

        [Theory(DisplayName = "When page and pageSize are null then all elements are returned from IEnumerable")]
        [ClassData(typeof(PaginationNullPageAndPageSizeTestData))]
        public void When_page_and_pageSize_are_null_then_all_elements_are_returned_from_IEnumerable(int? pageSize, int? page, int[] expectedPaginatedResult) => TestIEnumerable(_enumerableCollection, pageSize, page, expectedPaginatedResult);

        [Theory(DisplayName = "When page and pageSize are null then all elements are returned from IQueryable")]
        [ClassData(typeof(PaginationNullPageAndPageSizeTestData))]
        public void When_page_and_pageSize_are_null_then_all_elements_are_returned_from_IQueryable(int? pageSize, int? page, int[] expectedPaginatedResult) => TestIQueryable(_queryableCollection, pageSize, page, expectedPaginatedResult);

        [Theory(DisplayName = "When page is null then first elements of amount equal to pageSize are returned from IEnumerable")]
        [ClassData(typeof(PaginationNullPageTestData))]
        public void When_page_is_null_then_first_elements_of_amount_equal_to_pageSize_are_returned_from_IEnumerable(int? pageSize, int? page, int[] expectedPaginatedResult) => TestIEnumerable(_enumerableCollection, pageSize, page, expectedPaginatedResult);

        [Theory(DisplayName = "When page is null then first elements of amount equal to pageSize are returned from IQueryable")]
        [ClassData(typeof(PaginationNullPageTestData))]
        public void When_page_is_null_then_first_elements_of_amount_equal_to_pageSize_are_returned_from_IQueryable(int? pageSize, int? page, int[] expectedPaginatedResult) => TestIQueryable(_queryableCollection, pageSize, page, expectedPaginatedResult);

        [Theory(DisplayName = "When pageSize is null then all elements regardless of provided page number are returned from IEnumerable")]
        [ClassData(typeof(PaginationNullPageSizeTestData))]
        public void When_pageSize_is_null_then_all_elements_regardless_of_provided_page_number_are_returned_from_IEnumerable(int? pageSize, int? page, int[] expectedPaginatedResult) => TestIEnumerable(_enumerableCollection, pageSize, page, expectedPaginatedResult);

        [Theory(DisplayName = "When pageSize is null then all elements regardless of provided page number are returned from IQueryable")]
        [ClassData(typeof(PaginationNullPageSizeTestData))]
        public void When_pageSize_is_null_then_all_elements_regardless_of_provided_page_number_are_returned_from_IQueryable(int? pageSize, int? page, int[] expectedPaginatedResult) => TestIQueryable(_queryableCollection, pageSize, page, expectedPaginatedResult);

        [Theory(DisplayName = "When page is out of range empty then empty IEnumerable is returned")]
        [ClassData(typeof(PaginationPageOutOfRangeSizeTestData))]
        public void When_page_is_out_of_range_empty_then_empty_IEnumerable_is_returned(int? pageSize, int? page, int[] expectedPaginatedResult) => TestIEnumerable(_enumerableCollection, pageSize, page, expectedPaginatedResult);

        [Theory(DisplayName = "When page is out of range empty then empty IQueryable is returned")]
        [ClassData(typeof(PaginationPageOutOfRangeSizeTestData))]
        public void When_page_is_out_of_range_empty_then_empty_IQueryable_is_returned(int? pageSize, int? page, int[] expectedPaginatedResult) => TestIQueryable(_queryableCollection, pageSize, page, expectedPaginatedResult);

        [Theory(DisplayName = "When IEnumerable is empty then empty IEnumerable is returned after pagination regardless of parameters passed")]
        [ClassData(typeof(PaginationEmptyCollectionTestData))]
        public void When_IEnumerable_is_empty_then_empty_IEnumerable_is_returned_after_pagination_regardless_of_parameters_passed(int? pageSize, int? page) => TestIEnumerable(Enumerable.Empty<int>(), pageSize, page, new int[0]);

        [Theory(DisplayName = "When IQueryable is empty then empty IQueryable is returned after pagination regardless of parameters passed")]
        [ClassData(typeof(PaginationEmptyCollectionTestData))]
        public void When_IQueryable_is_empty_then_empty_IQueryable_is_returned_after_pagination_regardless_of_parameters_passed(int? pageSize, int? page) => TestIQueryable(Enumerable.Empty<int>().AsQueryable(), pageSize, page, new int[0]);

        private void TestIEnumerable<T>(IEnumerable<T> enumerable, int? pageSize, int? page, T[] expectedPaginatedResult)
        {
            var paginatedEnumerable = enumerable.Paginated(pageSize, page).ToArray();

            paginatedEnumerable
                .Should()
                .BeEquivalentTo(expectedPaginatedResult);
        }

        private void TestIQueryable<T>(IQueryable<T> queryable, int? pageSize, int? page, T[] expectedPaginatedResult)
        {
            var paginatedQueryable = queryable.Paginated(pageSize, page).ToArray();

            paginatedQueryable
                .Should()
                .BeEquivalentTo(expectedPaginatedResult);
        }

        private class PaginationTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {2, 1, new[] {1, 2}};
                yield return new object[] {1, 1, new[] {1}};
                yield return new object[] {7, 1, new[] {1, 2, 3, 4, 5, 6, 7}};
                yield return new object[] {2, 3, new[] {5, 6}};
                yield return new object[] {3, 3, new[] {7}};
                yield return new object[] {1, 7, new[] {7}};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class PaginationNullPageAndPageSizeTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {null, null, new[] {1, 2, 3, 4, 5, 6, 7}};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class PaginationNullPageTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {1, null, new[] {1}};
                yield return new object[] {2, null, new[] {1, 2}};
                yield return new object[] {3, null, new[] {1, 2, 3}};
                yield return new object[] {8, null, new[] {1, 2, 3, 4, 5, 6, 7}};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class PaginationNullPageSizeTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {null, 1, new[] {1, 2, 3, 4, 5, 6, 7}};
                yield return new object[] {null, 2, new[] {1, 2, 3, 4, 5, 6, 7}};
                yield return new object[] {null, 3, new[] {1, 2, 3, 4, 5, 6, 7}};
                yield return new object[] {null, 10, new[] {1, 2, 3, 4, 5, 6, 7}};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class PaginationPageOutOfRangeSizeTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {1, 8, new int[0]};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class PaginationEmptyCollectionTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {null, null};
                yield return new object[] {0, null};
                yield return new object[] {1, null};
                yield return new object[] {null, 0};
                yield return new object[] {null, 1};
                yield return new object[] {1, 1};
                yield return new object[] {5, 10};
                yield return new object[] {10, 5};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
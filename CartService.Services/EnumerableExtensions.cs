using System.Collections.Generic;
using System.Linq;

namespace CartService.Services
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchBy)
            => source
                .Select((item, i) => new {item, i})
                .GroupBy(x => x.i / batchBy)
                .Select(g => g.Select(x => x.item));
    }
}
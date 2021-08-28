using System.Collections.Generic;

namespace DevHours.CloudNative.Application.Data.Dtos
{
    public class TableDto<T> where T:class
    {
        public int TotalCount { get; init; }
        public IEnumerable<T> Values { get; init; }
    }
}

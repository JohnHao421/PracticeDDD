using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindSelf.Application.Configuration.Queries.Paging
{
    public class PaggingView<T>
    {
        public IReadOnlyList<T> Datas => _datas;
        private List<T> _datas;
        public int Count => _datas.Count;
        public int Total { get; }
        public int PageSize { get; }
        public int Index { get; }

        public PaggingView(IEnumerable<T> datas, int total, int pageSize, int index)
        {
            _datas = new List<T>();
            _datas.AddRange(datas);

            Total = total;
            PageSize = pageSize;
            Index = index;
        }
    }
}

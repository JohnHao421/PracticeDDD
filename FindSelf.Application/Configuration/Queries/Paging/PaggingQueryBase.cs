using FindSelf.Application.Configuration.Queries;

namespace FindSelf.Application.Configuration.Queries.Paging
{
    public abstract class PaggingQueryBase<T> : IQuery<PaggingView<T>>
    {
        private int pageSize = 20;
        private int index = 1;

        public int PageSize
        {
            get => pageSize;
            set
            {
                if (value > 0) pageSize = value;
            }
        }
        public int Index
        {
            get => index; set
            {
                if (value > 0) index = value;
            }
        }

        public PaggingQueryBase()
        {

        }

        public PaggingQueryBase(int pageSize, int index)
        {
            PageSize = pageSize;
            Index = index;
        }
    }
}
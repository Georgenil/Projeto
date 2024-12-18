using Projeto.Domain.ViewModels;

namespace Projeto.Infra.Utils.ExtensionMethod
{
    public class PagedResponse<TEntity> : Response<TEntity>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        private int _totalPages = 0;
        public int TotalPages
        {
            get
            {
                if (_totalPages == 0 && PageSize > 0)
                    _totalPages = (TotalRecords + PageSize - 1) / PageSize;
                return _totalPages;
            }
            set
            {
                _totalPages = value;
            }
        }
        public int TotalRecords { get; set; }


        public override void Copy<OtherEntity>(Response<OtherEntity> other)
        {
            base.Copy(other);

            if (other is PagedResponse<OtherEntity>)
            {
                var paged = other as PagedResponse<OtherEntity>;

                this.PageNumber = paged.PageNumber;
                this.PageSize = paged.PageSize;
                this.TotalPages = paged.TotalPages;
                this.TotalRecords = paged.TotalRecords;
            }
        }
    }
}

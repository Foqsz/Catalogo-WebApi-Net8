namespace WebApiCatalogo.Catalogo.API.Pagination
{
    public class ProdutosParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = maxPageSize;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize)? maxPageSize : value; //se o valor fornecido for maior que o tamanho da pagina, atribui o valor 50 de max, se for falso, atribui o valor
            }
        }
    }
}

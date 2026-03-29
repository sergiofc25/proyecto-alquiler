using Model;
namespace BlazorAppStayly.HttpRepository;
public class PagingResponse<T> where T : class
{
    public List<T> Cuerpo { get; set; }

    public MetaData MetaData { get; set; }
}


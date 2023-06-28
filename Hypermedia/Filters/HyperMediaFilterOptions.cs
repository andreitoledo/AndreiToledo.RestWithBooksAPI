using AndreiToledo.RestWithBooksAPI.Hypermedia.Abstract;
using System.Collections.Generic;

namespace AndreiToledo.RestWithBooksAPI.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}

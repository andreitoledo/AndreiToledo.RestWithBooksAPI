using System.Collections.Generic;

namespace AndreiToledo.RestWithBooksAPI.Hypermedia.Abstract
{
    public interface ISupportsHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}

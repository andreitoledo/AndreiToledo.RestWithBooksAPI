using System.Collections.Generic;

namespace AndreiToledo.RestWithBooksAPI.Hypermedia.Abstract
{
    public interface ISupportHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}

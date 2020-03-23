using System.IO;

namespace BookClient
{
    public interface IStreamLoader
    {
        Stream GetStreamForFilename(string filename);
    }
}
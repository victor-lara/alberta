using Alberta.Models;

namespace Alberta.Data
{
    public interface IPackageManager
    {
        public double? GetQuote(string source, string target, IEnumerable<Tuple<double?, double?, double?>> dimensions);
    }
}

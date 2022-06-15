using Alberta.Models;

namespace Alberta.Data
{
    public class PackageManager : IPackageManager
    {
        // this is fake data to execute the app, in real life this should be replaced with a datasource
        private readonly List<Package> _context;

        public PackageManager()
        {
            // We populate the fake context, but this should be a simple assignation to the private property
            _context= new List<Package> {
                new Package()
                {
                    Id = Guid.NewGuid(),
                    Height = 1,
                    Width = 2,
                    Length = 3,
                    SourceAddress = "TN",
                    TargetAddress = "TX",
                    Price = 100
                },
                new Package()
                {
                    Id = Guid.NewGuid(),
                    Height = 1,
                    Width = 1,
                    Length = 1,
                    SourceAddress = "TN",
                    TargetAddress = "TX",
                    Price = 50
                },
                new Package()
                {
                    Id = Guid.NewGuid(),
                    Height = 1,
                    Width = 1,
                    Length = 1,
                    SourceAddress = "TN",
                    TargetAddress = "TX",
                    Price = 20
                },
                new Package()
                {
                    Id = Guid.NewGuid(),
                    Height = 1,
                    Width = 1,
                    Length = 1,
                    SourceAddress = "TN",
                    TargetAddress = "TX",
                    Price = 1000
                }};
        }

        //This returns the total of quotes for specific packages.
        //We know the price for a cubic meter, to get the quote, we get the lowest unit price for that shipment, and multiply for the required cubic meters
        public double? GetQuote(string source, string target, IEnumerable<Tuple<double?, double?, double?>> dimensions)
        {
            // find any matching route
            List<Package>? routes = _context.Where(x => x.SourceAddress == source && x.TargetAddress == target).ToList();

            if (routes.Count == 0)
            {
                return null;
            }
            else
            {
                //if there is at least 1 route, we calculate the volume of our packages, and then multiply by the lowest route price, sum the total of packages
                double? total = dimensions.Select(x => (x.Item1 * x.Item2 * x.Item3) * routes.Min(y => y.Price)).Sum();
                return total;
            }
        }
    }
}

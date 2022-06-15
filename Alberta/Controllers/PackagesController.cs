using Alberta.Data;
using Alberta.Models;
using Alberta.Security;
using Microsoft.AspNetCore.Mvc;

namespace Alberta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BasicAuthorization]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageManager _context;

        public PackagesController(IPackageManager context)
        {
            _context = context;
        }

        //todo:
        //refactor the logic on the 3 methods, but this is a quick POC
        //the nature of these 3 methods is unknown, they seems to do the same

        [HttpPost]
        [Route("quote"), FormatFilter]
        public ActionResult<QuoteResponse> Quote([FromBody] QuoteRequest xml)
        {
            //checks if the request is complete and valid
            if (string.IsNullOrWhiteSpace(xml.Source) || string.IsNullOrWhiteSpace(xml.Destination) || xml.Packages == null || !xml.Packages.Any())
            {
                return BadRequest(new QuoteResponse() { Message = "The request is incomplete, please validate" });
            }
            else
            {
                double? quote = _context.GetQuote(xml.Source.ToUpperInvariant(), xml.Destination.ToUpperInvariant(), xml.Packages.Select(x => new Tuple<double?, double?, double?>(x.Height, x.Width, x.Length)));

                if (quote == null)
                {
                    return NotFound(new QuoteResponse() { Message = "There aren't routes matching this request" });
                }
                else
                {
                    return Ok(new QuoteResponse() { Message = "Successful", Total = quote });

                }
            }
        }

        [HttpPost]
        [Route("amount"), FormatFilter]
        public ActionResult<AmountResponse> Amount([FromBody] AmountRequest input)
        {
            //checks if the request is complete and valid
            if (string.IsNullOrWhiteSpace(input.Consignee) || string.IsNullOrWhiteSpace(input.Consignor) || input.Cartons == null || !input.Cartons.Any())
            {
                return BadRequest(new AmountResponse() { Message = "The request is incomplete, please validate" });
            }
            else
            {
                double? quote = _context.GetQuote(input.Consignee.ToUpperInvariant(), input.Consignor.ToUpperInvariant(), input.Cartons.Select(x => new Tuple<double?, double?, double?>(x.Height, x.Width, x.Length)));

                if (quote == null)
                {
                    return NotFound(new AmountResponse() { Message = "There aren't routes matching this request" });
                }
                else
                {
                    return Ok(new AmountResponse() { Message = "Sucessful", Amount = quote });
                }
            }
        }

        [HttpPost]
        [Route("total"), FormatFilter]
        public ActionResult<TotalResponse> Total([FromBody] TotalRequest input)
        {
            //checks if the request is complete and valid
            if (string.IsNullOrWhiteSpace(input.ContactAddress) || string.IsNullOrWhiteSpace(input.WarehouseAddress) || input.Dimensions == null || !input.Dimensions.Any())
            {
                return BadRequest(new TotalResponse() { Message = "The request is incomplete, please validate" });
            }
            else
            {
                double? quote = _context.GetQuote(input.ContactAddress.ToUpperInvariant(), input.WarehouseAddress.ToUpperInvariant(), input.Dimensions.Select(x => new Tuple<double?, double?, double?>(x.Height, x.Width, x.Length)));

                if (quote == null)
                {
                    return NotFound(new TotalResponse() { Message = "There aren't routes matching this request" });
                }
                else
                {
                    return Ok(new TotalResponse() { Message = "Succesful", Total = quote });
                }
            }
        }
    }
}

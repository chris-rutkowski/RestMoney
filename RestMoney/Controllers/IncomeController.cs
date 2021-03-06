using Microsoft.AspNetCore.Mvc;
using RestMoney.Dtos;
using RestMoney.Measurement;

namespace RestMoney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public IncomeController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpGet("uk/{gross}")]
        public ActionResult<IncomeDto> GetUK(decimal gross)
        {
            _ = _measurementService.CountIncomeRequest("UK");

            // TODO verify gross is above 0
            var allowance = 12570.0M;

            if (gross > 100000)
            {
                allowance -= Math.Min(allowance, (gross - 100000) / 2);
            }

            Console.WriteLine("allowance" + allowance);

            var taxable = gross - allowance;
            var taxDue = 0.0M;

            if (taxable > 0)
            {
                var basic = Math.Min(taxable, 37700);
                taxDue += basic * 0.2M;
            }

            if (taxable > 37700)
            {
                var higher = Math.Min(taxable, 150000) - 37700;
                taxDue += higher * 0.4M;
            }

            if (taxable > 150000)
            {
                var additional = taxable - 150000;
                taxDue += additional * 0.45M;
            }


            var niAllowance = 9568.0M;
            var niAble = gross - niAllowance;
            var niDue = 0.0M;

            if (niAble > 0)
            {
                var basic = Math.Min(niAble, 40702);
                niDue += basic * 0.12M;
            }

            if (niAble > 40702)
            {
                var higher = niAble - 40702;
                niDue += higher * 0.02M;
            }

            return new IncomeDto
            {
                TaxDue = taxDue,
                NationalInsurance = niDue,
                Net = gross - taxDue - niDue
            };
        }

        [HttpGet("th/{gross}")]
        public ActionResult<IncomeDto> GetThailand(decimal gross)
        {
            _ = _measurementService.CountIncomeRequest("TH");

            var taxDue = gross * 0.15M;

            return new IncomeDto
            {
                TaxDue = taxDue,
                NationalInsurance = 0,
                Net = gross - taxDue
            };
        }

        [HttpGet("ae/{gross}")]
        public ActionResult<IncomeDto> GetUnitedArabEmirates(decimal gross)
        {
            _ = _measurementService.CountIncomeRequest("AE");

            return new IncomeDto
            {
                TaxDue = 0,
                NationalInsurance = 0,
                Net = gross
            };
        }
    }
}

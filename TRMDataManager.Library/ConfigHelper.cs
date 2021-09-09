using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDataManager.Library
{
    public class ConfigHelper : IConfigHelper
    {
        private readonly IConfiguration _config;

        public ConfigHelper(IConfiguration config)
        {
            _config = config;
        }

        // TODO: Move this from config to the API
        public decimal GetTaxRate()
        {
            //// HACK : 2021. 아래의 구문이 문제가 있어서
            //string rateText = ConfigurationManager.AppSettings["taxRate"];

            //// ConfigHelper 클래스를 제대로 작동하게 변경함.
            /// public static decimal GetTaxRate()=> public decimal GetTaxRate()
            var section = _config.GetSection("AppSettings:taxRate");

            string rateText = section.Value;

            bool IsValidTaxRate = Decimal.TryParse(rateText, out decimal output);

            if (IsValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("The tax rate is not set up properly");
            }

            return output;
        }
    }
}

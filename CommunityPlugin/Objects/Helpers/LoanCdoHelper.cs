using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.BusinessObjects.Loans;
using Newtonsoft.Json;

namespace CommunityPlugin.Objects.Helpers
{
    public static class LoanCdoHelper
    {
        public static T GetLoanCustomDataObjectValue<T>(Loan loan, string cdoName)
        {
            T obj = default(T);
            try
            {
                var cdo = loan.GetCustomDataObject(cdoName);

                if (cdo != null)
                {
                    using (StreamReader sr = new StreamReader(new MemoryStream(cdo.Data)))
                    {
                        obj = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                        return obj;
                    }
                }

                return obj;
            }
            catch (Exception)
            { }

            return obj;
        }

    }
}

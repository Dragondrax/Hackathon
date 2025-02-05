using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalHealth.Fiap.SharedKernel.Utils
{
    public static class Tratamentos
    {
        public static string TratarBinaryDataAzureFunction(BinaryData messageBody)
        {
            string jsonRaw = Encoding.UTF8.GetString(messageBody.ToArray());

            if (jsonRaw.StartsWith("\"") && jsonRaw.EndsWith("\""))
            {
                var innerJson = JsonConvert.DeserializeObject<string>(jsonRaw);
                return innerJson;
            }

            return jsonRaw;
        }
    }
}

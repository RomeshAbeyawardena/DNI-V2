using DNI.Shared.Abstractions;
using DNI.Shared.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Extensions
{
    public static class StringExtensions
    {
        private static void GetEncodingIfNull(ref Encoding encoding)
        {
            encoding = Encoding.UTF8;
        }

        public static string ToBase64String(this string value, Encoding encoding = default)
        {
            GetEncodingIfNull(ref encoding);
            return Convert.ToBase64String(encoding.GetBytes(value));
        }

        public static string FromBase64String(this string value, Encoding encoding = default)
        {
            GetEncodingIfNull(ref encoding);
            return encoding.GetString(Convert.FromBase64String(value));
        }

        public static IPayload ExtractPayload(this string value, IPayloadOptions payloadOptions = default, Encoding encoding = default)
        {
            GetEncodingIfNull(ref encoding);
            
            if(payloadOptions == null)
            {
                payloadOptions = DefaultPayloadOptions.Default;
            }

            var request = value.FromBase64String(encoding);

            var requests = request.Split(payloadOptions.ParameterSeparator, StringSplitOptions.RemoveEmptyEntries);
            DefaultPayload payload = new();

            if (requests.Any())
            {
                var requestHeader = requests[0].Split(payloadOptions.HeaderSeparator);
                if (requestHeader.Any())
                {
                    payload.Name = requestHeader[0];

                    requests[0] = requestHeader[1];
                }

                payload.Parameters = requests.Select(r => r.FromBase64String(encoding));
            }

            return payload;
        }
    }
}

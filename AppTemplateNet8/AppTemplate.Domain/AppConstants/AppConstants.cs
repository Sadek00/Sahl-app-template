using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.AppConstants
{
    public class AppConstants
    {
        /*
         * Base64 Encoding: Requires about 43 characters to represent 32 bytes of binary data.
         * Hexadecimal Encoding: Requires exactly 64 characters to represent 32 bytes of binary data.
         * Ensure the secret key is exactly 32 characters long when using Encoding.ASCII.GetBytes(secretKey).
         */        
        public const string JwtSecretKey = "AL#$Dls3456dFJ3KU87&^kjh712^=SD+";    // ASCII Encoding

        public const string AccessTokenSecret = "SLSECRETKEY2024USBGROUP_SLSECRETKEY2024USBGROUP";
        public const string RefreshTokenSecret = "SLSECRETKEY2024USBGROUP_SLSECRETKEY2024USBGROUP";
        
    }
}

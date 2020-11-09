using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Common.Constants
{
    public class KeyConstants
    {
        /// <summary>
        /// AssemblyServices
        /// </summary>
        public static readonly string AssemblyServices = "ServicesLayer";

        /// <summary>
        /// AssemblyAction
        /// </summary>
        public static readonly string AssemblyAction = "ServicesLayer.Action";

        #region Jwt Claim

        /// <summary>
        /// Init claims JWT Strings
        /// </summary>
        public static class Strings
        {
            public static readonly string IdentityToken = "Token";

            /// <summary>
            /// JwtClaimIdentifiers
            /// </summary>
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id", Admin = "Admin";
            }

            /// <summary>
            /// JwtClaims
            /// </summary>
            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }
        }

        #endregion

        #region [ Keys Constant for Status ]

        /// <summary>
        /// SecretKey
        // todo: get this from somewhere secure
        /// </summary>
        public static readonly string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";

        /// <summary>
        /// ApiUser
        /// </summary>
        public const string PolicyAuthorizeApiUser = "ApiUser";

        /// <summary>
        /// SigningKey
        /// </summary>
        public static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        #endregion
    }
}

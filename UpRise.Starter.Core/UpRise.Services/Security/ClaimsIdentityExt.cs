using System.Security.Claims;

namespace UpRise.Security
{
    public static class ClaimsIdentityExt
    {
        public static string TENANTID = "UpRise.TenantId";

        public static void AddTenantId(this ClaimsIdentity claims, object tenantId)
        {
            claims.AddClaim(new Claim(TENANTID, tenantId?.ToString(), null, "UpRise"));
        }

        public static bool IsTenantIdClaim(this ClaimsIdentity claims, string claimName)
        {
            return claimName == TENANTID;
        }
    }
}
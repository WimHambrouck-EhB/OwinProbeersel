using Microsoft.AspNetCore.Authentication;
using System.Reflection.Emit;
using System.Runtime.Versioning;
using System.Security.Claims;
using System.Security.Principal;

namespace OwinProbeersel.Models
{
    public class WindowsRolesToClaimsTransformer : IClaimsTransformation
    {
        [SupportedOSPlatform("windows")]
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            WindowsIdentity? wi = principal.Identity as WindowsIdentity;
            
            if (wi?.Groups != null)
            {
                foreach (var group in wi.Groups)
                {
                    Claim claim = new(wi.RoleClaimType, group.Value);
                    wi.AddClaim(claim);
                }
            }

            return Task.FromResult(principal);
        }
    }
}
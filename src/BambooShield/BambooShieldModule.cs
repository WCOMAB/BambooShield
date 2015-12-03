using BambooShield.DotBadge;
using Nancy;

namespace BambooShield
{
    // ReSharper disable once UnusedMember.Global
    public class BambooShieldModule : NancyModule
    {
        private static string _indexSvg;
        public BambooShieldModule()
        {
            Get["/"] = _ => Response.AsSvgResponse(
                "BambooShieldModule.svg", 
                _indexSvg ?? (_indexSvg = SvgHelper.GetIndexSvg())
                );

            Get["/planstatus/{style}/{projectKey}-{buildKey}.svg"] = GetPlanStatus;
            Get["/planstatus/{style}/{projectKey}-{buildKey}-{branch}.svg"] = GetPlanStatus;
        }

        private dynamic GetPlanStatus(dynamic _)
        {
            Style style;
            string projectKey;
            string buildKey;
            string branch;
            string filename;
            NancyRequestResponseHelper.ParsePlanStatusParameters(_, out style, out projectKey, out buildKey, out branch, out filename);
            //TODO handle branch
            var statusSvg = SvgHelper.GetPlanStatusSvg(projectKey, buildKey, style);
            return Response.AsSvgResponse(filename, statusSvg);
        }
    }
}
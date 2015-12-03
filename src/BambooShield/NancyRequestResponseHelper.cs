using System;
using BambooShield.DotBadge;
using Nancy;

namespace BambooShield
{
    internal static class NancyRequestResponseHelper
    {
        internal static void ParsePlanStatusParameters(
            dynamic _,
            out Style style,
            out string projectKey,
            out string buildKey,
            out string branch,
            out string filename
            )
        {
            style = Enum.TryParse(_.style, true, out style)
                ? style
                : Style.Flat;

            projectKey = _.projectKey;
            buildKey = _.buildKey;
            branch = _.branch;

            filename = $"{projectKey}-{buildKey}.svg";
        }


        internal static Response AsSvgResponse(this IResponseFormatter response, string filename, string svgContents)
        {
            var svgResponse = response.AsText(svgContents, "image/svg+xml");
            svgResponse.Headers.Add("Content-Disposition", $"inline; filename=\"{filename}\"");
            return svgResponse;
        }

    }
}

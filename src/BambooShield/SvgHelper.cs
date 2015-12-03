using BambooShield.DotBadge;

namespace BambooShield
{
    internal static class SvgHelper
    {
        internal static string GetPlanStatusSvg(string projectKey, string buildKey, Style style)
        {
            string subject;
            string status;
            var buildStatus = BambooApi.GetBuildStatus(
                projectKey,
                buildKey,
                out subject,
                out status
                );

            string statusColor;
            switch (buildStatus)
            {
                case BuildStatus.Unknown:
                    statusColor = ColorScheme.LightGray;
                    break;
                case BuildStatus.Building:
                    statusColor = ColorScheme.Yellow;
                    break;
                case BuildStatus.Passing:
                    statusColor = ColorScheme.BrightGreen;
                    break;
                case BuildStatus.Failing:
                    statusColor = ColorScheme.Red;
                    break;
                case BuildStatus.Error:
                    statusColor = ColorScheme.Orange;
                    break;
                default:
                    statusColor = ColorScheme.Blue;
                    break;
            }


            var statusSvg = new BadgePainter().DrawSVG(subject, status, statusColor, style);
            return statusSvg;
        }

        internal static string GetIndexSvg()
        {
            var assemblyName = typeof(SvgHelper).Assembly.GetName();
            var status = assemblyName.Version.ToString();
            var subject = assemblyName.Name;

            return new BadgePainter().DrawSVG(status, subject, ColorScheme.BrightGreen);
        }
    }
}

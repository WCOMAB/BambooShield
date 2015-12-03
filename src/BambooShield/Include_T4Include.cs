
// ############################################################################
// #                                                                          #
// #        ---==>  T H I S  F I L E  I S   G E N E R A T E D  <==---         #
// #                                                                          #
// # This means that any edits to the .cs file will be lost when its          #
// # regenerated. Changes should instead be applied to the corresponding      #
// # text template file (.tt)                                                 #
// ############################################################################



// ############################################################################
// @@@ INCLUDING: https://raw.github.com/rebornix/DotBadge/master/DotBadge/BadgePainter.cs
// ############################################################################
// Certains directives such as #define and // Resharper comments has to be 
// moved to top in order to work properly    
// ############################################################################
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: https://raw.github.com/rebornix/DotBadge/master/DotBadge/BadgePainter.cs
namespace BambooShield
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    
    namespace DotBadge
    {
        public enum Style
        {
            Flat,
            FlatSquare,
            Plastic
        }
    
        public class ColorScheme
        {
            public static string BrightGreen = "#4c1";
            public static string Green = "#97CA00";
            public static string Yellow = "#dfb317";
            public static string YellowGreen = "#a4a61d";
            public static string Orange = "#fe7d37";
            public static string Red = "#e05d44";
            public static string Blue = "#007ec6";
            public static string Grey = "#555";
            public static string Gray = "#555";
            public static string LightGrey = "#9f9f9f";
            public static string LightGray = "#9f9f9f";
        }
    
        public class BadgePainter
        {
            public string DrawSVG(string subject, string status, string statusColor, Style style = Style.Flat)
            {
                string template;
                switch (style)
                {
                    case Style.Flat:
                        template = Properties.Resources.flat;
                        break;
                    case Style.FlatSquare:
                        template = Properties.Resources.flatSquare;
                        break;
                    case Style.Plastic:
                        template = Properties.Resources.plastic;
                        break;
                    default:
                        template = File.ReadAllText("templates/flat-template.xml");
                        break;
                }
    
                Font font = new Font("DejaVu Sans,Verdana,Geneva,sans-serif", 11, FontStyle.Regular);
                Graphics g = Graphics.FromImage(new Bitmap(1, 1));
                var subjectWidth = g.MeasureString(subject, font).Width;
                var statusWidth = g.MeasureString(status, font).Width;
    
                var result = String.Format(
                    CultureInfo.InvariantCulture, 
                    template,
                    subjectWidth + statusWidth,
                    subjectWidth,
                    statusWidth,
                    subjectWidth / 2 + 1,
                    subjectWidth + statusWidth / 2 - 1,
                    subject,
                    status,
                    statusColor);
                return result;
            }
    
            public string ParseColor(string input)
            {
                var cs = new ColorScheme();
    
                var fieldInfo = cs.GetType().GetField(input);
                if (fieldInfo == null)
                {
                    return String.Empty;
                }
                return (string)fieldInfo.GetValue(cs.GetType());
            }
        }
    }
}
// @@@ END_INCLUDE: https://raw.github.com/rebornix/DotBadge/master/DotBadge/BadgePainter.cs
// ############################################################################
// ############################################################################
// Certains directives such as #define and // Resharper comments has to be 
// moved to bottom in order to work properly    
// ############################################################################
// ############################################################################
namespace BambooShield.Include
{
    static partial class MetaData
    {
        public const string RootPath        = @"https://raw.github.com/";
        public const string IncludeDate     = @"2015-12-03T12:16:17";

        public const string Include_0       = @"https://raw.github.com/rebornix/DotBadge/master/DotBadge/BadgePainter.cs";
    }
}
// ############################################################################




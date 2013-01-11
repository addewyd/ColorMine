﻿using System;
using System.Drawing;

namespace ColorMine.ColorSpaces.Conversions
{
    internal static class XyzConverter
    {
        internal static void ToColorSpace(Color color, IXyz item)
        {
            double r = PivotRgb(color.R/255.0);
            double g = PivotRgb(color.G/255.0);
            double b = PivotRgb(color.B/255.0);

            // Observer. = 2°, Illuminant = D65
            item.X = r*0.4124 + g*0.3576 + b*0.1805;
            item.Y = r*0.2126 + g*0.7152 + b*0.0722;
            item.Z = r*0.0193 + g*0.1192 + b*0.9505;
        }

        internal static Color ToColor(IXyz item)
        {
            // (Observer = 2°, Illuminant = D65)
            double x = item.X/100;
            double y = item.Y/100;
            double z = item.Z/100;

            double r = x*3.2406 + y*-1.5372 + z*-0.4986;
            double g = x*-0.9689 + y*1.8758 + z*0.0415;
            double b = x*0.0557 + y*-0.2040 + z*1.0570;

            r = r > 0.0031308 ? 1.055*Math.Pow(r, 1/2.4) - 0.055 : 12.92*r;
            g = g > 0.0031308 ? 1.055*Math.Pow(g, 1/2.4) - 0.055 : 12.92*g;
            b = b > 0.0031308 ? 1.055*Math.Pow(b, 1/2.4) - 0.055 : 12.92*b;

            return Color.FromArgb(255, (int) (r*255), (int) (g*255), (int) (b*255));
        }

        private static double PivotRgb(double n)
        {
            return (n > 0.04045 ? Math.Pow((n + 0.055)/1.055, 2.4) : n/12.92)*100;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

namespace DrawLegends.Trash
{


    class cCrap
    {

        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }


        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }


        public void aaa()
        {
            //System.Drawing.Color targetcol = System.Drawing.Color.FromArgb(255, 0, 99, 0);
            System.Drawing.Color targetcol = System.Drawing.Color.LightBlue;

            System.Drawing.Color col = System.Drawing.Color.Red;

            double hue;
            double saturation;
            double lightness;


            double targethue;
            double targetsaturation;
            double targetlightness;
            ColorToHSV(targetcol, out targethue, out targetsaturation, out targetlightness);
            ColorToHSV(col, out hue, out saturation, out lightness);

            targetlightness = lightness;
            targetsaturation = saturation;

            col = ColorFromHSV(targethue, targetsaturation, targetlightness);
        }

        public static void MsgBox(object obj)
        {
            if (obj == null)
            {
                System.Windows.Forms.MessageBox.Show("obj is NULL");
            }
            else
                System.Windows.Forms.MessageBox.Show(obj.ToString());
        } // End Sub MsgBox 


        public static Rectangle RectF2Rect(RectangleF rectf)
        {
            return new Rectangle((int)rectf.X, (int)rectf.Y, (int)rectf.Width, (int)rectf.Height);
        } // End Function RectF2Rect


        public static RectangleF Rect2RectF(Rectangle rect)
        {
            return new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
        } // End Function Rect2RectF


        public static System.Drawing.Color[] GetColors()
        {
            System.Drawing.KnownColor[] colors = (System.Drawing.KnownColor[])Enum.GetValues(typeof(System.Drawing.KnownColor));
            System.Collections.Generic.List<System.Drawing.Color> ls = new System.Collections.Generic.List<System.Drawing.Color>();

            for (int i = 27; i < 167; ++i)
            {

                ls.Add(System.Drawing.Color.FromKnownColor(colors[i]));
            } // Next i

            System.Drawing.Color[] cols = ls.ToArray();
            return cols;
        } // End Function GetColors


    } // End Class cCrap


} // End Namespace DrawLegends.Trash

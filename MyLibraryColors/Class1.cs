using System;
using System.Drawing;
using System.Text.RegularExpressions;

Colors colors = new Colors();
colors.ConvertRGBtoHEX(66, 134, 244);

public class Colors
{
    public string ConvertRGBtoHEX(int r, int g, int b)
    {
        if (r < 0 || r > 255)
            throw new ArgumentOutOfRangeException(nameof(r), "Красный компонент должен быть в диапазоне 0-255");
        if (g < 0 || g > 255)
            throw new ArgumentOutOfRangeException(nameof(g), "Зеленый компонент должен быть в диапазоне 0-255");
        if (b < 0 || b > 255)
            throw new ArgumentOutOfRangeException(nameof(b), "Синий компонент должен быть в диапазоне 0-255");

        //Преобразует число в шестнадцатеричную систему счисления
        return $"#{r:X2}{g:X2}{b:X2}";

    }
    public (int r, int g, int b) ConvertHEXtoRGB(string hex)
    {
        if (string.IsNullOrEmpty(hex))
            throw new ArgumentException("HEX строка не может быть null или пустой", nameof(hex));

        // Удаляем символ # 
        hex = hex.TrimStart('#');

        if (hex.Length == 3)
        {
            hex = $"{hex[0]}{hex[0]}{hex[1]}{hex[1]}{hex[2]}{hex[2]}";
        }
        else if (hex.Length != 6)
        {
            throw new ArgumentException("HEX строка должна содержать 3 или 6 символов (без #)", nameof(hex));
        }

        if (!Regex.IsMatch(hex, @"^[0-9A-Fa-f]{6}$"))
            throw new ArgumentException("HEX строка содержит недопустимые символы", nameof(hex));

        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);

        return (r, g, b);
    }
    public (double h, double s, double l) ConvertRGBtoHSL(int r, int g, int b)
    {

        double rd = r / 255.0;
        double gd = g / 255.0;
        double bd = b / 255.0;

        double max = Math.Max(rd, Math.Max(gd, bd));
        double min = Math.Min(rd, Math.Min(gd, bd));
        double delta = max - min;

        double h = 0;
        double s = 0;
        double l = (max + min) / 2;

        if (delta != 0)
        {
            s = delta / (1 - Math.Abs(2 * l - 1));

            if (max == rd)
                h = 60 * ((gd - bd) / delta % 6);
            else if (max == gd)
                h = 60 * ((bd - rd) / delta + 2);
            else
                h = 60 * ((rd - gd) / delta + 4);

            if (h < 0)
                h += 360;
        }

        return (Math.Round(h, 2), Math.Round(s, 4), Math.Round(l, 4));
    }
}







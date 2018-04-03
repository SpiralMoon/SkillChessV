using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Support;

public static class CountryExtension
{
    public static string ToDomain(this Country country)
    {
        var domain = "";

        switch(country)
        {
            case Country.USA:
                domain = "en";
                break;
            case Country.KOREA:
                domain = "kr";
                break;
            case Country.JAPAN:
                domain = "jp";
                break;
        }

        return domain;
    }
}
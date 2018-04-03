using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System;
using System.Text.RegularExpressions;

public static class StringExtension
{
    /// <summary>
    /// 이메일 형식 확인
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsEmail(this string str)
    {
        try
        {
            var email = new MailAddress(str);
            return true;
        }
        catch(Exception e)
        {
            return false;
        }
    }

    /// <summary>
    /// 닉네임 형식 확인
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNickname(this string str)
    {
        if (str.IndexOf(@"\s") != -1)
        {
            return false;
        }

        var r = new Regex(@"[^0-9a-zA-Z가-힣]");

        return !r.IsMatch(str);
    }

    /// <summary>
    /// 방 제목 형식 확인
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsGameTitle(this string str)
    {
        return str.Length <= 25;
    }
}

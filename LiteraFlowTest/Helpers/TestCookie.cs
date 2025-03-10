﻿using LiteraFlow.Web.BL.WebCookie;


namespace LiteraFlowTest.Helpers;

public class TestCookie : IWebCookie
{
    Dictionary<string, string> cookies = new Dictionary<string, string>();

    public void Add(string cookieName, string value, int days = 0)
    {
        if (cookies.ContainsKey(cookieName)) Delete(cookieName);

        cookies.Add(cookieName, value);
    }

    public void AddSecure(string cookieName, string value, int days = 0)
    {
        if (cookies.ContainsKey(cookieName)) Delete(cookieName); 
        cookies.Add(cookieName, value);
    }

    public void Delete(string cookieName)
    {
        cookies.Remove(cookieName);
    }

    public string? Get(string cookieName)
    {
        if (cookies.ContainsKey(cookieName))
            return cookies[cookieName];
        return null;
    }
}

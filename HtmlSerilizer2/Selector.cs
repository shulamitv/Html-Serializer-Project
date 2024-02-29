using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class Selector
{
    public string TagName { get; set; }
    public string Id { get; set; }
    public List<string> Classes { get; set; }
    public Selector Parent { get; set; }
    public Selector Child { get; set; }

    public Selector()
    {
        Classes = new List<string>();
    }

    public static Selector ParseQuery(string queryString)
    {
        List<string> parts = queryString.Split(' ').ToList();
        Selector rootSelector = null;
        Selector currentSelector = null;

        foreach (string part in parts)
        {
            string[] selectorParts = Regex.Split(part, @"(?=[.# ])");
            Console.WriteLine("selectorParts:----");
            foreach (string selectorPart in selectorParts)
            {
                Console.WriteLine(selectorPart);
            }
            Selector newSelector = new Selector();

            foreach (string selectorPart in selectorParts)
            {
                if (!string.IsNullOrWhiteSpace(selectorPart))
                {
                    if (selectorPart.StartsWith("#"))
                    {
                        newSelector.Id = selectorPart.Substring(1);
                    }
                    else if (selectorPart.StartsWith("."))
                    {
                        newSelector.Classes.Add(selectorPart.Substring(1));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(newSelector.TagName) && IsValidTagName(selectorPart))
                        {
                            newSelector.TagName = selectorPart;
                        }
                    }
                }
            }

            if (rootSelector == null)
            {
                rootSelector = newSelector;
                currentSelector = newSelector;
            }
            else if (currentSelector != null)
            {
                currentSelector.Child = newSelector;
                newSelector.Parent = currentSelector;
                currentSelector = newSelector;
            }
        }
        return rootSelector;
    }

    private static bool IsValidTagName(string tagName)
    {
        return !string.IsNullOrWhiteSpace(tagName);
    }
}

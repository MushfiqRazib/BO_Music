using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for DescriptionTextHandler
/// </summary>
public class DescriptionTextHandler
{
    public DescriptionTextHandler()
    {
        //
    }
    
    private static int alloweDescriptionLengthWithoutMoreLink = 220;

    private static bool IsDescriptionLengthExceeds(string fullDescription)
    {
        if (fullDescription.Length > alloweDescriptionLengthWithoutMoreLink)
        {
            return true;
        }
        return false;
    }

    private static string GetShortDescription(string fullDescription)
    {
        return fullDescription.Substring(0, 220);
    }

    public static string GetDescriptionWithMoreIfRequired(string fullDescrip, string moreText)
    {
        StringBuilder descriptionWithMore = new StringBuilder();
        if (fullDescrip.Length > alloweDescriptionLengthWithoutMoreLink)
        {
            descriptionWithMore.AppendFormat("{0}...&nbsp; <a href=\"javascript:void(0)\" class=\"morenewslink pinklink\"> {1}</a>", GetShortDescription(fullDescrip),moreText);
            return descriptionWithMore.ToString();
        }
        return fullDescrip;
    }
}

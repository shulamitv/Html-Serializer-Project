using HtmlSerilizer2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace HtmlSerilizer2
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance => _instance;
        
        public string[] AllHtmlTag { get; set; }
        public string[] WithoutCloseTag { get; set; }
        public HtmlHelper()
        {
            string allContent = File.ReadAllText("allHtml.json");
            AllHtmlTag = JsonSerializer.Deserialize<string[]>(allContent);
            string withoutContent = File.ReadAllText("withoutCloseTag.json");
            WithoutCloseTag = JsonSerializer.Deserialize<string[]>(withoutContent);
        }
        public bool IsSelfClosingTag(string tag)
        {
            return this.WithoutCloseTag.Contains(tag);
        }

        public bool IsHtmlTag(string tag)
        {
            return this.AllHtmlTag.Contains(tag);
        }
    }
}

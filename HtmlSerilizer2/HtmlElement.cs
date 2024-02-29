using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HtmlSerilizer2
{
   public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }
        public HtmlElement()
        {
            Id = "";
            Name = "";
            Attributes = new List<string>();
            Classes = new List<string>();
            InnerHtml = "";
            Parent = null;
            Children = new List<HtmlElement>();
        }
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                HtmlElement currentElement = queue.Dequeue();
                yield return currentElement;

                foreach (var child in currentElement.Children)
                {
                    queue.Enqueue(child);
                }
            }
        } public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement currentAncestor = this.Parent;
            while (currentAncestor != null)
            {
                yield return currentAncestor;
                currentAncestor = currentAncestor.Parent;
            }
        }
        public static List<HtmlElement> FindElementsBySelector(HtmlElement root, Selector selector)
        {
            List<HtmlElement> result = new List<HtmlElement>();
            FindElementsRecursively(root, selector, result);
            return result;
        }
        public static void FindElementsRecursively(HtmlElement currentElement, Selector currentSelector, List<HtmlElement> result)
        {
            foreach (var descendant in currentElement.Descendants())
            {
                if (MatchesSelector(descendant, currentSelector))
                {
                    if (currentSelector.Child == null)
                    {
                        result.Add(descendant);
                    }
                    else
                    {
                        FindElementsRecursively(descendant, currentSelector.Child, result);
                    }
                }
            }
        }
        private static bool MatchesSelector(HtmlElement element, Selector selector)
        {
            return
                (string.IsNullOrEmpty(selector.TagName) || element.Name == selector.TagName) &&
                (string.IsNullOrEmpty(selector.Id) || element.Id == selector.Id) &&
                (element.Classes==null)||
                (selector.Classes.All(c => element.Classes.Contains(c)));
        }

    }
}

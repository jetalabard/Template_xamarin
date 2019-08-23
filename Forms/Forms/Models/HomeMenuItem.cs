using System.Collections.Generic;

namespace Forms.Models
{
    public enum PageType
    {
        About,
        Home,
        Profile,
        Administration
    }

    public class HomeMenuItem
    {
        public PageType Id { get; set; }
        public string Title { get; set; }
        public string PageUri { get; set; }
    }

    public class HomeMenuItemList : List<HomeMenuItem>
    {
        public string Title { get; set; }

        public HomeMenuItemList(string title)
        {
            Title = title;
        }
    }
}

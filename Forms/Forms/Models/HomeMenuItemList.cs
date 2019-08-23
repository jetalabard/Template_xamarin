using System.Collections.Generic;

namespace Forms.Models
{
    public class HomeMenuItemList : List<HomeMenuItem>
    {
        public string Title { get; set; }

        public HomeMenuItemList(string title)
        {
            Title = title;
        }
    }
}

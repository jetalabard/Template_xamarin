using System.Collections.Generic;

namespace Forms.Models
{
    public enum PageType
    {
        /// <summary>
        /// about page.
        /// </summary>
        About,

        /// <summary>
        /// home page.
        /// </summary>
        Home,

        /// <summary>
        /// profile page.
        /// </summary>
        Profile,

        /// <summary>
        /// Administration page.
        /// </summary>
        Administration,
    }

    public class HomeMenuItem
    {
        public PageType Id { get; set; }

        public string Title { get; set; }

        public string PageUri { get; set; }
    }
}

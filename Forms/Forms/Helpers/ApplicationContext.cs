using Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forms.Helpers
{
    public class ApplicationContext
    {
        private static ApplicationContext _instance;

        public static ApplicationContext Instance => _instance ?? (_instance = new ApplicationContext());

        public UserDto CurrentUser { get; set; }
    }
}

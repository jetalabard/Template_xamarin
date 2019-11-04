using System;
using System.ComponentModel;
using Core.Dto;
using Forms.Models;
using Xamarin.Forms;

namespace Forms.Views
{
    /// <summary>
    /// page to create new item.
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description.",
            };

            BindingContext = this;
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
        }
    }
}
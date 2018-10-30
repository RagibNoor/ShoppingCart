using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ShoppingCart.Models.Data;

namespace ShoppingCart.Models.ViewModels
{
    public class Pages
    {
        public Pages()
        {

        }
        public Pages(Pages_tbl row)
        {
            Id = row.Id;
            Title = row.Title;
            Slug = row.Slug;
            Body = row.Body;
            Sorting = row.Sorting;
            HasSlidebar = row.HasSlidebar;
        }
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        

        public string Slug { get; set; }
        [Required]
        public string Body { get; set; }
        public int? Sorting { get; set; }
        public bool HasSlidebar { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Dto.Shop;

namespace Domain.Shop
{
    public class Category
    {
        [JsonIgnore]
        public int id { get; set; }
        [JsonPropertyName("id")]
        public int ApiId { get; set; }
        public string title { get; set; }
        public string? description { get; set; }
        public string? iconImage { get; set; }
        public DateTime modifiedOn { get; set; }
        [JsonPropertyName("items")]
        public virtual ICollection<ApiProductItem> products{ get; set; }
    }
}

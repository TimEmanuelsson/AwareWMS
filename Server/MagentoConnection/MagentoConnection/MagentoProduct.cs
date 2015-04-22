using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Specialized;

namespace MagentoConnection
{
    class MagentoProduct
    {
        public string ProductId { get; set; }
        public string SKU { get; set; }
        public string Set { get; set; }
        public string Type { get; set; }
        public string[] Categories { get; set; }
        public string[] Websites { get; set; }
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        public string Type_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Short_Description { get; set; }
        public string Weight { get; set; }
        public string Status { get; set; }
        public string Url_Key { get; set; }
        public string Url_Path { get; set; }
        public string Visibility { get; set; }
        public string[] Category_Ids { get; set; }
        public string[] Website_Ids { get; set; }
        public string Has_Options { get; set; }
        public string Gift_Message_Available { get; set; }
        public string Price { get; set; }
        public string Special_Price { get; set; }
        public string Special_From_Date { get; set; }
        public string Special_To_Date { get; set; }
        public string Tax_Class_Id { get; set; }
        public string Tier_Price { get; set; }
        public string Meta_Title { get; set; }
        public string Meta_Keyword { get; set; }
        public string Meta_Description { get; set; }
        public string Custom_Design { get; set; }
        public string Custom_Layout_Update { get; set; }
        public string Options_Container { get; set; }
        public OrderedDictionary Additional_Attributes { get; set; }
        public string Enable_Googlecheckout { get; set; }

        public MagentoProduct(
            string productId,
            string sku,
            string set,
            string type,
            string[] categories,
            string[] websites,
            string created_at,
            string updated_at,
            string type_id,
            string name,
            string description,
            string short_description,
            string weight,
            string status,
            string url_key,
            string url_path,
            string visibility,
            string[] category_ids,
            string[] website_ids,
            string has_options,
            string gift_message_available,
            string price,
            string special_price,
            string special_from_date,
            string special_to_date,
            string tax_class_id,
            string tier_price,
            string meta_title,
            string meta_keyword,
            string meta_description,
            string custom_design,
            string custom_layout_update,
            string options_container,
            OrderedDictionary additional_attributes,
            string enable_googlecheckout
            )
        {
            ProductId = productId;
            SKU = sku;
            Set = set;
            Type = type;
            Categories = categories;
            Websites = websites;
            Created_At = created_at;
            Updated_At = updated_at;
            Type_Id = type_id;
            Name = name;
            Description = description;
            Short_Description = short_description;
            Weight = weight;
            Status = status;
            Url_Key = url_key;
            Url_Path = url_path;
            Visibility = visibility;
            Category_Ids = category_ids;
            Website_Ids = website_ids;
            Has_Options = has_options;
            Gift_Message_Available = gift_message_available;
            Price = price;
            Special_Price = special_price;
            Special_From_Date = special_from_date;
            Special_To_Date = special_to_date;
            Tax_Class_Id = tax_class_id;
            Tier_Price = tier_price;
            Meta_Title = meta_title;
            Meta_Keyword = meta_keyword;
            Meta_Description = meta_description;
            Custom_Design = custom_design;
            Custom_Layout_Update = custom_layout_update;
            Options_Container = options_container;
            Additional_Attributes = additional_attributes;
            Enable_Googlecheckout = enable_googlecheckout;
        }
    }
}

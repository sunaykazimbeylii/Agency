﻿using Agency.Models.Base;

namespace Agency.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
         public  List<Project>? Projects{ get; set;}
    }
}

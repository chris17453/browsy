using System;
namespace browsy.model
{
    public class file_system_item{
        public string type {get; set;}
        public string name {get; set;}
        public string path {get; set;}
        public string size {get; set;}
        public string extension { get; set; }
        public string modified {get; set;}
        public bool   hidden {get; set; }
    }
}

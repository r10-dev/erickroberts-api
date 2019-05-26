using System;

namespace api.models {
    public class ContentTags : BaseEntity
    {
        public int contenttagid { get; set; }
        public string url { get; set; }
        public int contentid { get; set; }
        public string description { get; set; }
        public string tags { get; set; }
    }
}
using System;

namespace api.models
{
    public class Comment : BaseEntity
    {
        public int commentid { get; set; }
        public string shorttext { get; set; }
        public string body { get; set; }
        public int userid { get; set; }
        public int contentid { get; set; }
    }
}
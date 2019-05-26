namespace api.models {
    using System;
    public class Content : BaseEntity
    {
        public int contentid { get; set; }
        public string slug { get; set; }
        public int authorid { get; set; }
        public string title { get; set; }
        public string headerimage { get; set; }
        public string tabimage { get; set; }
        public int views { get; set; }
        public float stars { get; set; }
        public bool published { get; set; }
        public bool staged { get; set; }
        public bool  draft { get; set; }
        public DateTime created_on { get; set; }
        public DateTime published_on { get; set; }
    }


}
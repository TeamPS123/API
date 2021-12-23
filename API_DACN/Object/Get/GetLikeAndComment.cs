using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetLikeAndComment
    {
        private int status;
        private string notification;
        private IEnumerable<GetLike> getlike;
        private IEnumerable<GetComment> comments;

        public GetLikeAndComment(int status, string notification, IEnumerable<GetLike> getlike, IEnumerable<GetComment> comments)
        {
            this.status = status;
            this.notification = notification;
            this.getlike = getlike;
            this.comments = comments;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<GetLike> Getlike { get => getlike; set => getlike = value; }
        public IEnumerable<GetComment> Comments { get => comments; set => comments = value; }
    }

    public class GetComment
    {
        public string name { get; set; }
        public string imgUser { get; set; }
        public string content { get; set; }
        public string date { get; set; }    
    }

    public class GetLike
    {
        public string userId { get; set; }
        public string name { get; set; }
    }
}

using Core.Interfaces;

namespace OnlineLearningAppApi.Core.Entities
{
    public class Token<TUser> where TUser : IUser
    {
        public Guid Id { get; set; }
        public string ClientId { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public string? UserId { get; set; }
        public TUser? User { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}

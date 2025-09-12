
using Domain.Abstractions;
using Domain.ValueObjects;
using System.IO;

namespace Domain.Models
{
    public class Topic : Entity<TopicId>
    {
        public string Title { get; set; } = default!;
        public DateTime? EventStart { get; set; } = default!;
        public string Summary {  get; set; } = default!;    
        public string TopicType {  get; set; } = default!;
        public Location Location { get; set; } = default!;

        public static Topic Create (TopicId id, string title, DateTime eventStart, string summary, string topicType, Location location)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title);
            ArgumentException.ThrowIfNullOrWhiteSpace(summary);
            ArgumentException.ThrowIfNullOrWhiteSpace(topicType);
            return new Topic 
            { 
                Id = id, 
                Title = title, 
                EventStart = eventStart, 
                Summary = summary, 
                TopicType = topicType, 
                Location = location 
            };
        }

        public void Update(string title, string summary, string topicType, DateTime eventStart, Location location)
        {
            this.Title = title;
            this.Summary = summary;
            this.TopicType = topicType;
            this.EventStart = eventStart;
            this.Location = Location.Of(
                location.City,
                location.Street);
        }
    }
}

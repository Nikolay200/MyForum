
namespace Application.Exceptions
{
    public class TopicNotFoundException : NotFoundException
    {
        public TopicNotFoundException(string message) : base(message) 
        {
        
        }

        public TopicNotFoundException(Guid topicId) : base($"Топик с id {topicId} не найден!")
        {

        }
    }
}

using MoviePortal.ApplicationCore.Model;

namespace MoviePortal.ApplicationCore.Interfaces.Service
{
	public interface IMessagePublisher
	{
		Task Publish();
	}
}

namespace MoviePortal.ApplicationCore.Interfaces.Model
{
	public interface IChangeTracker
	{
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        int CreatedById { get; set; }
        int UpdatedById { get; set; }
    }
}

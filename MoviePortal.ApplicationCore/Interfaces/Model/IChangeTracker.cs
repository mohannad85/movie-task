namespace MoviePortal.ApplicationCore.Interfaces.Model
{
	public interface IChangeTracker
	{
        DateTime? CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}

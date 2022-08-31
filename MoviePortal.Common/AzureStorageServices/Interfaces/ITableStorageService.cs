using MoviePortal.Common.Movie.Model;

namespace MoviePortal.Common.AzureStorageServices.Interfaces
{
	public interface ITableStorageService
	{
		Task<MovieEntity> GetEntityAsync();
		Task UpsertEntityAsync(MovieEntity entity);
	}
}

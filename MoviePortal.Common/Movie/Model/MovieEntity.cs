using Azure;
using Azure.Data.Tables;

namespace MoviePortal.Common.Movie.Model
{
	public class MovieEntity : ITableEntity
	{
		public string PartitionKey { get; set; } = "movie";
		public string RowKey { get; set; } = string.Format("{0:D19}", DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks);
		public DateTimeOffset? Timestamp { get; set; }
		public ETag ETag { get; set; }
		public string Content { get; set; }
	}
}

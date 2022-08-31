using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using MoviePortal.Common.AzureStorageServices.Interfaces;
using MoviePortal.Common.Movie.Model;
using MoviePortal.Common.Responses;

namespace MoviePortal.Common.AzureStorageServices.Services
{
	public class TableStorageService : ITableStorageService
    {
        private readonly IConfiguration _configuration;

        public TableStorageService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

		public async Task<MovieEntity> GetEntityAsync()
		{
            var tableClient = await GetTableClient();
            var queryResultsFilter = tableClient.Query<MovieEntity>(filter: $"PartitionKey eq 'movie'");
            return queryResultsFilter.LastOrDefault();
		}

		public async Task UpsertEntityAsync(MovieEntity entity)
		{
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(entity);
        }

        private async Task<TableClient> GetTableClient()
        {
            var serviceClient = new TableServiceClient(_configuration["StorageAccountOptions:ConnectionString"]);
            var tableClient = serviceClient.GetTableClient(_configuration["StorageAccountOptions:TableName"]);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }
	}
}

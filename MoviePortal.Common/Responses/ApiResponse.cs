using System.Text.Json.Serialization;

namespace MoviePortal.Common.Responses
{
	public class ApiResponse
	{
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("messageStatus"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string MessageStatus { get; set; }
        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }
        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
    }

    public class Country
    {
        [JsonPropertyName("name"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Name { get; set; }
        [JsonPropertyName("uuid"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string UUID { get; set; }
    }

    public class EmbedUrl
    {
        [JsonPropertyName("server"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Server { get; set; }
        [JsonPropertyName("url"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Url { get; set; }
        [JsonPropertyName("priority")]
        public int Priority { get; set; }
    }

    public class Genre
    {
        [JsonPropertyName("name"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Name { get; set; }
        [JsonPropertyName("uuid"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string UUID { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("actors")]
        public List<object> Actors { get; set; }
        [JsonPropertyName("directors")]
        public List<object> Directors { get; set; }
        [JsonPropertyName("escritors")]
        public List<object> Escritors { get; set; }
        [JsonPropertyName("otherTitles")]
        public List<object> OtherTitles { get; set; }
        [JsonPropertyName("_id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Id { get; set; }
        [JsonPropertyName("image"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Image { get; set; }
        [JsonPropertyName("title"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Title { get; set; }
        [JsonPropertyName("rating"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Rating { get; set; }
        [JsonPropertyName("year"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Year { get; set; }
        [JsonPropertyName("titleOriginal"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string TitleOriginal { get; set; }
        [JsonPropertyName("uuid"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string UUID { get; set; }
        [JsonPropertyName("description"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Description { get; set; }
        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }
        [JsonPropertyName("countries")]
        public List<Country> Countries { get; set; }
        [JsonPropertyName("release"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Release { get; set; }
        [JsonPropertyName("EmbedUrls")]
        public List<EmbedUrl> embedUrls { get; set; }
        [JsonPropertyName("index")]
        public int Index { get; set; }
        [JsonPropertyName("episodes")]
        public List<object> Episodes { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}

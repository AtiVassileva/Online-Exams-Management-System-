using Newtonsoft.Json;
using OnlineExamSystem.Data.Models;

namespace OnlineExamSystem.API.Models
{
    public class QuestionsByExamResponse
    {
        [JsonProperty("$id")]
        public string Id { get; set; }

        [JsonProperty("$values")]
        public List<Question> Values { get; set; }
    }
}
using System.Runtime.Serialization;

namespace BuyBackAPI.Models
{
    public class Request
    {
        public string Id { get; set; }
    }

    public class SettingModel
    {
        public string connectionString { get; set; }
    }

    [DataContract]
    public class Response
    {
        [DataMember(Name = "Status")]
        public int Status { get; set; }

        [DataMember(Name = "Message")]
        public string Message { get; set; }

        [DataMember(Name = "Count")]
        public int Count { get; set; }

        [DataMember(Name = "Data")]
        public object Data { get; set; }
    }
}

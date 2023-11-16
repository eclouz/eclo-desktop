using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ViewModels.Users;

public class RegisterResponseViewModel
{
    [JsonProperty("StatuCode")]
    public int StatusCode {  get; set; }

    [JsonProperty("ErrorMessage")]
    public string ErrorMessage { get; set; }=String.Empty;
}

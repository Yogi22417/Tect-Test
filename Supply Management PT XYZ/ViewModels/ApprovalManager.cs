using System.Text.Json.Serialization;

namespace Supply_Management_PT_XYZ.ViewModels
{
    public class ApprovalManager
    {
        public string UsernameVendor { get; set; } = string.Empty;

        public PersetujuanManager Persetujuan { get; set; }
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PersetujuanManager
{
    setuju = 3,

    tolak = 10
}
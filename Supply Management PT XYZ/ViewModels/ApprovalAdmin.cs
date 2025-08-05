using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Supply_Management_PT_XYZ.ViewModels
{
    public class ApprovalAdmin
    {
        public string UsernameVendor { get; set; } = string.Empty;

        public PersetujuanStatus Persetujuan { get; set; }
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PersetujuanStatus
{
    setuju = 1,

    tolak = 10
}

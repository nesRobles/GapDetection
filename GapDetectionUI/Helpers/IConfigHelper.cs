using Microsoft.Extensions.Configuration;

namespace GapDetectionUI.Helpers
{
    public interface IConfigHelper
    {
        IConfiguration BuildConfiguration();
    }
}
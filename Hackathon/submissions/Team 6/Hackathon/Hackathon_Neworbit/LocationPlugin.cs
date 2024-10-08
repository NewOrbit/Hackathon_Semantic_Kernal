using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace Hackathon_Neworbit
{
    public class LocationPlugin
    {
        [KernelFunction("GetLocations")]
        [Description("List of favourite locations")]
        [return: Description("Locations data")]
        public List<string> GetLocations()
        {
            return new List<string> { "Chalgrove", "Marlow", "Slough" };
        }

    }
}

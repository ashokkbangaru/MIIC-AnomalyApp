using PluginContracts;
using System.Windows.Forms;

namespace SamplePlugin
{
    public class SamplePlugin : IPlugin
    {
        public string Name => "Sample Plugin";
        public string Description => "This is a sample plugin.";

        public void Execute()
        {
            MessageBox.Show("Sample Plugin executed!", "Plugin");
        }
    }
}

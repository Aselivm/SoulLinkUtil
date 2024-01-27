using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;

namespace SoulLinkUtil
{
    public class SoulLinkUtilSettings : ISettings
    {
        public ToggleNode Enable { get; set; } = new ToggleNode(false);
        public RangeNode<int> TimeBetweenCasts { get; set; } = new RangeNode<int>(1, 1, 60);
        public HotkeyNode CastKey { get; set; } = new HotkeyNode(System.Windows.Forms.Keys.F1);
    }
}

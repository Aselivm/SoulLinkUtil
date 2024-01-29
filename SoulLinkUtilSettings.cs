using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using ExileCore.Shared.Attributes;
using System.Windows.Forms;

namespace SoulLinkUtil
{
    public class SoulLinkUtilSettings : ISettings
    {
        public ToggleNode Enable { get; set; } = new ToggleNode(false);
        [Menu("Time between casts")] public RangeNode<int> TimeBetweenCasts { get; set; } = new RangeNode<int>(1, 1, 60);
        [Menu("Distance to leader")] public RangeNode<int> ClearPathDistance { get; set; } = new RangeNode<int>(900, 500, 1500);
        [Menu("Leader Name")] public TextNode LeaderName { get; set; } = new TextNode("");
        public HotkeyNode CastKey { get; set; } = new HotkeyNode(System.Windows.Forms.Keys.F1);
    }
}

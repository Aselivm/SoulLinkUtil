using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using ExileCore.Shared.Attributes;
using System.Windows.Forms;

namespace SoulLinkUtil
{
    public class SoulLinkUtilSettings : ISettings
    {
        public ToggleNode Enable { get; set; } = new ToggleNode(false);
        [Menu("Soul Link Timer Threshold")] public RangeNode<float> SoulLinkTimerThreshold { get; set; } = new RangeNode<float>(4, 1, 10);
        [Menu("Time between casts")] public RangeNode<float> TimeBetweenCasts { get; set; } = new RangeNode<float>(1, 0, 40);
        [Menu("Distance to leader")] public RangeNode<int> ClearPathDistance { get; set; } = new RangeNode<int>(900, 500, 1500);
        [Menu("Leader Name")] public TextNode LeaderName { get; set; } = new TextNode("");
        public HotkeyNode CastKey { get; set; } = new HotkeyNode(System.Windows.Forms.Keys.F1);
    }
}

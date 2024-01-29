using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared.Nodes;
using System.Linq;
using SharpDX;
using System;
using System.Threading.Tasks;

namespace SoulLinkUtil
{
    public class SoulLinkUtil : BaseSettingsPlugin<SoulLinkUtilSettings>
    {
        private static DateTime lastSoulLinkCastTime = DateTime.MinValue;

        public override bool Initialise()=> true;

        public override Job Tick()
        {
            var _followTarget = GetFollowingTarget();

            if (_followTarget == null) return null;

            var distanceFromFollower = Vector3.Distance(GameController.Player.Pos, _followTarget.Pos);

            if (distanceFromFollower >= Settings.ClearPathDistance.Value) return null;

            var soulLinkBuff = GameController.Player.Buffs.FirstOrDefault(b => b.Name == "soul_link_source");

            if (soulLinkBuff == null && IsCooldownOver(lastSoulLinkCastTime))
            {
                ApplySoulLink();
                return null;
            }

            if (IsSoulLinkReady(soulLinkBuff) && IsCooldownOver(lastSoulLinkCastTime))
            {
                ApplySoulLink();
            }

            return null;
        }

        private void ApplySoulLink()
        {
            Input.KeyDown(Settings.CastKey.Value);
            lastSoulLinkCastTime = DateTime.Now;
            Task.Delay(50).Wait(); // Подождем немного
            Input.KeyUp(Settings.CastKey.Value);
        }

        private Entity GetFollowingTarget()
        {
            var leaderName = Settings.LeaderName.Value.ToLower();
            try
            {
                return GameController.Entities
                    .Where(x => x.Type == ExileCore.Shared.Enums.EntityType.Player)
                    .FirstOrDefault(x => x.GetComponent<Player>().PlayerName.ToLower() == leaderName);
            }
            // Sometimes we can get "Collection was modified; enumeration operation may not execute" exception
            catch
            {
                return null;
            }
        }

        private bool IsSoulLinkReady(Buff buff) => buff.Timer < Settings.SoulLinkTimerThreshold.Value;

        private bool IsCooldownOver(DateTime lastCastTime) =>
            (DateTime.Now - lastCastTime).TotalSeconds > Settings.TimeBetweenCasts;

        public override void Render()
        {
            Graphics.DrawText($"Plugin {GetType().Name} is working.", new Vector2(100, 100), Color.Red);
        }
    }
}

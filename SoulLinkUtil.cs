using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using SharpDX;
using System;
using System.Threading.Tasks;

namespace SoulLinkUtil
{
    public class SoulLinkUtil : BaseSettingsPlugin<SoulLinkUtilSettings>
    {
        private static DateTime lastSoulLinkCastTime = DateTime.MinValue;

        public override bool Initialise() => true;

        public override Job Tick()
        {
            foreach (var entity in GameController.Entities)
            {
                if (IsEntityValid(entity))
                {
                    foreach (var buff in entity.Buffs)
                    {
                        if (IsSoulLinkReady(buff) && IsCooldownOver(lastSoulLinkCastTime))
                        {
                            Input.KeyDown(Settings.CastKey.Value);
                            lastSoulLinkCastTime = DateTime.Now;
                            Task.Delay(50).Wait(); // Подождем немного
                            Input.KeyUp(Settings.CastKey.Value);
                        }

                    }
                }
            }

            return null;
        }

        private bool IsEntityValid(Entity entity) =>
            entity != null && entity.Buffs != null;

        private bool IsSoulLinkReady(Buff buff) =>
            buff.Name == "soul_link_source" && buff.Timer < 4;

        private bool IsCooldownOver(DateTime lastCastTime) =>
            (DateTime.Now - lastCastTime).TotalSeconds > Settings.TimeBetweenCasts;

        public override void Render()
        {
            Graphics.DrawText($"Plugin {GetType().Name} is working.", new Vector2(100, 100), Color.Red);
        }
    }
}

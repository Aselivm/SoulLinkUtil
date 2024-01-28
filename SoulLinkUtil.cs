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
        private static bool isSoulLinkOnCooldown = false;

        private static readonly object cooldownLock = new object();

        public override bool Initialise() => true;

        public override Job Tick()
        {
            foreach (var entity in GameController.Entities)
            {
                if (IsEntityValid(entity))
                {
                    foreach (var buff in entity.Buffs)
                    {

                        if (IsSoulLinkReady(buff))
                        {
                            CastSoulLink();
                        }
                    }
                }
            }

            return null;
        }

        private bool IsEntityValid(Entity entity) =>
            entity != null && entity.Buffs != null;

        private bool IsSoulLinkReady(Buff buff) =>
            buff.Name == "soul_link_source" && buff.Timer < 4 && !isSoulLinkOnCooldown;

        private void CastSoulLink()
        {
            Input.KeyDown(Settings.CastKey.Value);
            SetCooldownTimer();
        }

        private void SetCooldownTimer()
        {
            lock (cooldownLock)
            {
                isSoulLinkOnCooldown = true;
            }

            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(2));

                lock (cooldownLock)
                {
                    isSoulLinkOnCooldown = false;
                }
            });
        }


        public override void Render()
        {
            Graphics.DrawText($"Plugin {GetType().Name} is working.", new Vector2(100, 100), Color.Red);
        }
    }
}

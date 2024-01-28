﻿using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using SharpDX;
using System;
using System.Security.Cryptography.Pkcs;

namespace SoulLinkUtil
{
    public class SoulLinkUtil : BaseSettingsPlugin<SoulLinkUtilSettings>
    {
        private DateTime lastSoulLinkCastTime = DateTime.MinValue;

        public override bool Initialise()
        {
            return true;
        }

        public override Job Tick()
        {
            foreach (var entity in GameController.Entities)
            {
                if (entity != null && entity.Buffs != null)
                {
                    var buffs = entity.Buffs;

                    foreach (var buff in buffs)
                    {
                        //Graphics.DrawText(buff.Name, new Vector2(500, 140));
                        if (buff.Name == "soul_link_source" && buff.Timer < 4 && (DateTime.Now - lastSoulLinkCastTime).TotalSeconds > Settings.TimeBetweenCasts)
                        {
                            CastSoulLink();
                            lastSoulLinkCastTime = DateTime.Now;
                        }
                    }
                }
            }

            return null;
        }

        private void CastSoulLink()
        {
            Input.KeyDown(Settings.CastKey.Value);
        }

        public override void Render()
        {
            Graphics.DrawText($"Plugin {GetType().Name} is working.", new Vector2(100, 100), Color.Red);
        }
    }
}

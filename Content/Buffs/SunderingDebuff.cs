using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ExtraPets2.Content.Projectiles;

namespace ExtraPets2.Content.Buffs {
    public class SunderingDebuff : ModBuff {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Buffs/SunderingDebuff";

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Sundering");
            Description.SetDefault("You shouldn't have done that.");

            Main.buffNoTimeDisplay[Type] = true;
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.immune = false;
            player.immuneTime = 0;
            player.lifeRegen = 0;
            player.lifeRegenTime = 0;
            player.moveSpeed = 0.1f;
            player.noFallDmg = false;
            player.wingTime = 0;
            player.wingTimeMax = 0;
            player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByCustomReason(player.name + " no longer exists."), player.statLifeMax2/100, 0);
        }

        public override void Update(NPC npc, ref int buffIndex) {
            npc.lifeRegen -= npc.lifeMax*2;
        }
    }
}
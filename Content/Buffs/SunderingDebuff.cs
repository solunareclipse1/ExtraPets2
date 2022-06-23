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
            Description.SetDefault("Back to dust...");

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
            //if (player.statLife > 1) {
                player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByCustomReason(player.name + " became divided amongst themself."), player.statLifeMax2/160, 0);
            //}
        }

        public override void Update(NPC npc, ref int buffIndex) {
            npc.defense = 0;
            npc.lifeRegen -= npc.lifeMax*10;
            npc.HitEffect(0, 20);
        }
    }
}
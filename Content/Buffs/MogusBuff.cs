using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;

using ExtraPets2.Content.Projectiles;


namespace ExtraPets2.Content.Buffs {
    public class MogusBuff : ModBuff {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Buffs/MogusBuff";

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Trustworthy");
            Description.SetDefault("A mogus has deemed you trustworthy, and is now following you.");

            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.buffTime[buffIndex] = 18000;
            
            if (player.name.Contains("sus") || player.name == "Imposter") {
                player.AddBuff(ModContent.BuffType<Suspicious>(), 505513);
            }

            int projType = ModContent.ProjectileType<MogusProjectile>();

            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0) {
				var entitySource = player.GetSource_Buff(buffIndex);
				
				Projectile.NewProjectile(entitySource, player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
			}
        }
    }
}
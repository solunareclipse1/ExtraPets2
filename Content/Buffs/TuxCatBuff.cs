using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;

using ExtraPets2.Content.Projectiles;


namespace ExtraPets2.Content.Buffs {
    public class TuxCatBuff : ModBuff {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Buffs/TuxCatBuff";

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Tuxedo Cat");
            Description.SetDefault("Note: Resembelance to any snack food product is entirely coincidental\nFeline is not designed for consumption");

            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.buffTime[buffIndex] = 18000;

            int projType = ModContent.ProjectileType<TuxCatProjectile>();

            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0) {
				var entitySource = player.GetSource_Buff(buffIndex);
				
				Projectile.NewProjectile(entitySource, player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
			}
        }
    }
}
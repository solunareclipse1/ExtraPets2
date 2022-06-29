using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;

using ExtraPets2.Content.Projectiles;


namespace ExtraPets2.Content.Buffs {
    public class ShiningStarBuff : ModBuff {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Buffs/ShiningStarBuff";

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Shining Star");
            Description.SetDefault("Horrible pixel art lights your way");

            Main.buffNoTimeDisplay[Type] = true;
            Main.lightPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.buffTime[buffIndex] = 18000;

            int projType = ModContent.ProjectileType<ShiningStarProjectile>();

            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0) {
				var entitySource = player.GetSource_Buff(buffIndex);
				
				Projectile.NewProjectile(entitySource, player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
			}
        }
    }
}
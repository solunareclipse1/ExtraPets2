using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ExtraPets2.Content.Projectiles;

namespace ExtraPets2.Content.Buffs {
    public class QuantumBallBuff : ModBuff {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/QuantumBall";

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Quantum Ball");
            Description.SetDefault("Horrible pixel art lights your way");

            Main.buffNoTimeDisplay[Type] = true;
            Main.lightPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.buffTime[buffIndex] = 18000;
            
            //switch (player.name) {
            //    case "The Overseer":
            //        player.immune = true;
            //        player.immuneAlpha = 0;
            //        player.immuneTime = 1;
            //        break;
            //    case "QuartzShard":
            //        player.immune = false;
            //        player.immuneTime = 0;
            //        break;
            //    default:
            //        break;
            //}

            int projType = ModContent.ProjectileType<QuantumBallProjectile>();

            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0) {
				var entitySource = player.GetSource_Buff(buffIndex);
				
				Projectile.NewProjectile(entitySource, player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
			}
        }
    }
}
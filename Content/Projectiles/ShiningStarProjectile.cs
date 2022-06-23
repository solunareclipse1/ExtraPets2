using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ExtraPets2.Content.Buffs;

namespace ExtraPets2.Content.Projectiles {
	public class ShiningStarProjectile : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/ShiningStarPet";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Shining Star");

			Main.projFrames[Projectile.type] = 10;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Wisp);
			AIType = ProjectileID.Wisp;
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner];

			return true;
		}

		public override void AI() {
			Player player = Main.player[Projectile.owner];
			
			if (!player.dead && player.HasBuff(ModContent.BuffType<ShiningStarBuff>())) {
				Projectile.timeLeft = 2;
			}
		}
	}
}

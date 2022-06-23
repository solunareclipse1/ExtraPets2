using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ExtraPets2.Content.Buffs;

namespace ExtraPets2.Content.Projectiles {
	public class TuxCatProjectile : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/TuxCat";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Tuxedo Cat");

			Main.projFrames[Projectile.type] = 11;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.BlackCat);
			AIType = ProjectileID.BlackCat;
			Projectile.height = 24;
			DrawOriginOffsetY = 2;
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner];

			return true;
		}

		public override void AI() {
			Player player = Main.player[Projectile.owner];
			
			if (!player.dead && player.HasBuff(ModContent.BuffType<TuxCatBuff>())) {
				Projectile.timeLeft = 2;
			}
		}
	}
}

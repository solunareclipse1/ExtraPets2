using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ExtraPets2.Content.Buffs;

namespace ExtraPets2.Content.Projectiles {
	public class QuantumBallProjectile : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/QuantumBall";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Quantum Ball");

			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.EyeOfCthulhuPet);
			Projectile.scale = 1.3f;

			AIType = ProjectileID.EyeOfCthulhuPet;
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner];

			return true;
		}

		public override void AI() {
			Player player = Main.player[Projectile.owner];

			// Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
			if (!player.dead && player.HasBuff(ModContent.BuffType<QuantumBallBuff>())) {
				Projectile.timeLeft = 2;
			}
		}
	}
}

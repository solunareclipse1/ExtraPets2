using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ExtraPets2.Content.Buffs;

namespace ExtraPets2.Content.Projectiles {
	public class MogusProjectile : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/Mogus";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Incredulous Astronaut");

			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Bunny);
			AIType = ProjectileID.Bunny;
			Projectile.scale = 2;
			DrawOriginOffsetX = 1;
			DrawOriginOffsetY = 11;
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner];

			return true;
		}

		public override void AI() {
			Player player = Main.player[Projectile.owner];
			
			if (!player.dead && player.HasBuff(ModContent.BuffType<MogusBuff>())) {
				Projectile.timeLeft = 2;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(ModContent.BuffType<Suspicious>(), 101);
		}

		public override void OnHitPlayer (Player target, int damage, bool crit) {
			target.AddBuff(ModContent.BuffType<Suspicious>(), 101);
		}

		public override void OnHitPvp (Player target, int damage, bool crit) {
			target.AddBuff(ModContent.BuffType<Suspicious>(), 101);
		}
	}
}

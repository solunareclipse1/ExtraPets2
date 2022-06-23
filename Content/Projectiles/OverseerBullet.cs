using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ExtraPets2.Content.Buffs;

namespace ExtraPets2.Content.Projectiles {
	public class OverseerBullet : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/OverseerBullet";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Lightspeed Matter");

			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.BulletHighVelocity);
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = 16;

			AIType = ProjectileID.BulletHighVelocity;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(ModContent.BuffType<SunderingDebuff>(), 1616);
		}

		public override void OnHitPlayer (Player target, int damage, bool crit) {
			target.AddBuff(ModContent.BuffType<SunderingDebuff>(), 1616);
		}

		public override void OnHitPvp (Player target, int damage, bool crit) {
			target.AddBuff(ModContent.BuffType<SunderingDebuff>(), 1616);
		}
	}
}

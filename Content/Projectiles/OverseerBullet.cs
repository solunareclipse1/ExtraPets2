using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ExtraPets2.Content.Buffs;

namespace ExtraPets2.Content.Projectiles {
	public class OverseerBullet : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/OverseerBullet";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Lightspeed Matter");

			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.ShadowBeamFriendly);
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = 1616;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 1;

			AIType = ProjectileID.Bullet;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(ModContent.BuffType<SunderingDebuff>(), 960);
		}

		public override void ModifyHitPvp(Player target, ref int damage, ref bool crit) {
			target.AddBuff(ModContent.BuffType<SunderingDebuff>(), 960);
		}
	}
}

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraPets2.Content.Projectiles.SnasBoss {
	public class SnasBone : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/SnasBone";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("snas bnoe");

			Main.projFrames[Projectile.type] = 1;
		}
		
		public override void SetDefaults() {
			Projectile.penetrate = -1;
			Projectile.width = 24;
			Projectile.height = 24;
			Projectile.knockBack = 0;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.aiStyle = -1;
		}

		public override void AI() {
			Projectile.rotation += Projectile.velocity.X * 0.1f;
		}

		public override bool OnTileCollide(Vector2 lastVelocity) {
			Projectile.ai[0]++;
			if (Projectile.velocity.X != lastVelocity.X) {
				Projectile.velocity.X = lastVelocity.X * -1f;
			}
			if (Projectile.velocity.Y != lastVelocity.Y) {
				Projectile.velocity.Y = lastVelocity.Y * -1f;
			}
			return Projectile.ai[0] >= 3;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit) {
			target.immune = false;
			target.immuneTime = 0;
			if (target.HasBuff(BuffID.Venom)) {
				int idx = target.FindBuffIndex(BuffID.Venom);
				target.buffTime[idx] += 5;
			} else {
				target.AddBuff(BuffID.Venom, 60);
			}
		}
	}
}

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;

namespace ExtraPets2.Content.Projectiles {
	public class SkullTombstone : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/SkullTombstone";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("tbmotsneo");

			Main.projFrames[Projectile.type] = 1;
		}
		
		public override void SetDefaults() {
			Projectile.penetrate = -1;
			Projectile.width = 24;
			Projectile.height = 24;
			Projectile.knockBack = 100;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.aiStyle = -1;
		}

		public override void AI() {
			if (Projectile.velocity.Y == 0f) {
				Projectile.velocity.X *= 0.98f;
			}
			Projectile.rotation += Projectile.velocity.X * 0.1f;
			Projectile.velocity.Y += 0.2f;
		}

		public override bool OnTileCollide(Vector2 lastVelocity) {
			Projectile.ai[0]++;
			if (Projectile.velocity.X != lastVelocity.X) {
				Projectile.velocity.X = lastVelocity.X * -0.75f;
			}
			if (Projectile.velocity.Y != lastVelocity.Y && (double)lastVelocity.Y > 1.5) {
				Projectile.velocity.Y = lastVelocity.Y * -0.7f;
			}
			return Projectile.ai[0] >= 10;
		}
	}
}

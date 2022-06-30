using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace ExtraPets2.Content.Projectiles {
	public class MagnumOpusArrow : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/MagnumOpusArrow";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Philosophical Arrow");

			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Flamelash);
			Projectile.width = 38;
			Projectile.height = 13;
			Projectile.penetrate = 1;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

		public override void AI() {
			float lerpValue = Utils.GetLerpValue(0f, 10f, Projectile.localAI[0], clamped: true);
			Color newColor = Color.Lerp(Color.Transparent, Color.Crimson, lerpValue);
			if (Main.rand.Next(6) == 0) {
				Dust dust2 = Dust.NewDustDirect(Projectile.Center, 0, 0, 6, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, newColor, 3.5f);
				dust2.noGravity = true;
				dust2.velocity *= 1.4f;
				dust2.velocity += Main.rand.NextVector2Circular(1f, 1f);
				dust2.velocity += Projectile.velocity * 0.15f;
			}
			if (Main.rand.Next(12) == 0) {
				Dust dust3 = Dust.NewDustDirect(Projectile.Center, 0, 0, 6, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, newColor, 1.5f);
				dust3.velocity += Main.rand.NextVector2Circular(1f, 1f);
				dust3.velocity += Projectile.velocity * 0.15f;
			}
			if (Projectile.velocity.Length() > 0.1f && Vector2.Dot(Projectile.oldVelocity.SafeNormalize(Vector2.Zero), Projectile.velocity.SafeNormalize(Vector2.Zero)) < 0.2f) {
				int num11 = Main.rand.Next(2, 5 + (int)(lerpValue * 4f));
				for (int j = 0; j < num11; j++)
				{
					Dust dust4 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, newColor, 1.5f);
					dust4.velocity *= 0.3f;
					dust4.position = Projectile.Center;
					dust4.noGravity = true;
					dust4.velocity += Main.rand.NextVector2Circular(0.5f, 0.5f);
					dust4.fadeIn = 2.2f;
					dust4.position += (dust4.position - Projectile.Center) * lerpValue * 10f;
				}
			}
		}
		// This doesnt seem to work, and idk how to make it work.
		//
		//public override bool PreDraw(ref Color lightColor) {
		//	default(FlameLashDrawer).Draw(Projectile);
		//	return true;
		//}
	}
}

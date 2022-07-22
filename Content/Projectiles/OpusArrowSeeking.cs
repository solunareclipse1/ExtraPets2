using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ExtraPets2.Content.Projectiles {
	public class OpusArrowSeeking : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/MagnumOpusArrow";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Philosophical Arrow");

			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Flamelash);
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.timeLeft = 1000;
			Projectile.penetrate = 1;
			Projectile.ignoreWater = true;
			Projectile.light = 0f;

			AIType = ProjectileID.Flamelash;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			if (Projectile.ai[0] == -1f) {
				Projectile.ai[1] = -1f;
				Projectile.netUpdate = true;
			}
		}

		public override void AI() {
			Player player = Main.player[Projectile.owner];
			if (Projectile.timeLeft <= 60 && player.channel) {
				SoundEngine.PlaySound(EPSoundStyles.MagnumOpusExpire, Projectile.position);
				Projectile.timeLeft = 1;
			}
		}

		public override bool OnTileCollide(Vector2 lastVelocity) {
			bool killProj = Projectile.owner == Main.myPlayer;
			bool currentlyControlled = Projectile.ai[0] >= 0f;
			killProj = killProj && !currentlyControlled;
			if (currentlyControlled) {
				if (Projectile.velocity.X != lastVelocity.X) {
					Projectile.velocity.X *= 0.1f;
				}
				
				if (Projectile.velocity.Y != lastVelocity.Y) {
					Projectile.velocity.Y *= 0.1f;
				}
			}
			
			return killProj;
		}
		
		// TODO: pain 
		//public override bool PreDraw(ref Color lightColor) {
		//	default(FlameLashDrawer).Draw(Projectile);
		//	return true;
		//}
	}
}

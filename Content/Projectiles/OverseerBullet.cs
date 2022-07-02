using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Graphics.Light;

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
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.extraUpdates = 100;
			Projectile.timeLeft = 300;
			Projectile.alpha = 254;

			AIType = 0;
		}
		
		public override bool PreAI() {
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 11f) {
				if (Projectile.timeLeft == 294) {
					//mini shockwave from sky fracture
					Projectile.alpha = 255;
					for (int i = 0; (float)i < 16; i++) {
						Vector2 border = Vector2.UnitX * 0;
						border += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2 / 16)) * new Vector2(1, 4);
						border = border.RotatedBy(Projectile.velocity.ToRotation());
						int shockwaveID = Dust.NewDust(Projectile.Center, 0, 0, 180);
						Dust shockwave = Main.dust[shockwaveID];
						shockwave.scale = 1.5f;
						shockwave.noGravity = true;
						shockwave.position = Projectile.Center + border;
						shockwave.velocity = Projectile.velocity * 0 + border.SafeNormalize(Vector2.UnitY) * 1;
					}
				}
				for (int i = 0; i < 4; i++) {
					Vector2 projPos = Projectile.position;
					projPos -= Projectile.velocity * ((float)i * 0.25f);
					int plasmaID = Dust.NewDust(projPos, 1, 1, 173);
					Dust plasma = Main.dust[plasmaID];
					plasma.position = projPos;
					plasma.position.X += Projectile.width / 2;
					plasma.position.Y += Projectile.height / 2;
					plasma.scale = (float)Main.rand.Next(70, 110) * 0.013f;
					plasma.velocity *= 0.2f;
				}
			}
			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(ModContent.BuffType<SunderingDebuff>(), 160);
		}
		
		public override void OnHitPvp(Player target, int damage, bool crit) {
			target.AddBuff(ModContent.BuffType<SunderingDebuff>(), 160, false);
		}
	}
}

using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraPets2.Content.Projectiles {
	public class OpusArrowSwarming : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/MagnumOpusArrow2";
		private int[] debuffList = {
			20,24,31,39,44,69,70,103,119,120,137,153,203,204,320,323,324
		};

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Philosophical Arrow");

			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.FairyQueenMagicItemShot);
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.penetrate = 1;
			Projectile.aiStyle = -1;
			Projectile.tileCollide = false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(debuffList[Main.rand.Next(0,debuffList.Length)], 270);
			int newTarget = FindTargetIgnoreBlocks();
			if (newTarget != -1) {
				Projectile.ai[0] = newTarget;
				Projectile.netUpdate = true;
			}
		}

		public override void AI() {
			FireParticles();
			bool slowdown = false;
			bool canChase = false;
			float slowUntil = 180f;
			float chaseUntil = 20f;
			float slowFactor = 0.97f;
			float lerp1 = 0.075f;
			float lerp2 = 0.125f;
			float chaseVel = 30f;

			if (Projectile.timeLeft == 238) {
				int projAlpha = Projectile.alpha;
				Projectile.alpha = 0;
				Color dustColor = new Color(181, 47, 109);
				Projectile.alpha = projAlpha;
				for (int i = 0; i < 3; i++) {
					Dust dust = Dust.NewDustPerfect(Projectile.Center, 267, Main.rand.NextVector2CircularEdge(3f, 3f) * (Main.rand.NextFloat() * 0.5f + 0.5f), 0, dustColor);
					dust.scale *= 1.2f;
					dust.noGravity = true;
				}
			}

			if ((float)Projectile.timeLeft > slowUntil) {
				slowdown = true;
			} else if ((float)Projectile.timeLeft > chaseUntil) {
				canChase = true;
			}

			if (slowdown) {
				Projectile.velocity *= slowFactor;
			}
			if (Projectile.friendly) {
				int currentTarget = (int)Projectile.ai[0];
				if (Main.npc.IndexInRange(currentTarget) && !Main.npc[currentTarget].CanBeChasedBy(this)) {
					currentTarget = -1;
					Projectile.ai[0] = -1f;
					Projectile.netUpdate = true;
				}
				
				if (currentTarget == -1) {
					int target = FindTargetIgnoreBlocks();
					if (target != -1) {
						currentTarget = target;
						Projectile.ai[0] = target;
						Projectile.netUpdate = true;
					}
				}
			}

			if (canChase) {
				int target = (int)Projectile.ai[0];
				Vector2 newVel = Projectile.velocity;
				if (Main.npc.IndexInRange(target)) {
					NPC nPC = Main.npc[target];
					newVel = Projectile.DirectionTo(nPC.Center) * chaseVel;
				}
				else {
					Projectile.timeLeft -= 2;
				}
				float amount = MathHelper.Lerp(lerp1, lerp2, Utils.GetLerpValue(slowUntil, 30f, Projectile.timeLeft, clamped: true));
				Projectile.velocity = Vector2.SmoothStep(Projectile.velocity, newVel, amount);
				Projectile.velocity *= MathHelper.Lerp(0.85f, 1f, Utils.GetLerpValue(0f, 90f, Projectile.timeLeft, clamped: true));
			}
			Projectile.Opacity = Utils.GetLerpValue(240f, 220f, Projectile.timeLeft, clamped: true);
			Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}

		public int FindTargetIgnoreBlocks(float maxRange = 800f) {
			float maxDist = maxRange;
			int foundTarget = -1;
			for (int i = 0; i < 200; i++) {
				NPC nPC = Main.npc[i];
				bool npcIsValid = nPC.CanBeChasedBy();
				if (Projectile.localNPCImmunity[i] != 0) {
					npcIsValid = false;
				}

				if (npcIsValid) {
					float dist = Projectile.Distance(Main.npc[i].Center);
					if (dist < maxDist)
					{
						maxDist = dist;
						foundTarget = i;
					}
				}
			}
			return foundTarget;
		}

		public void FireParticles() {
			bool flag3 = Projectile.velocity.Length() > 0.1f && Vector2.Dot(Projectile.oldVelocity.SafeNormalize(Vector2.Zero), Projectile.velocity.SafeNormalize(Vector2.Zero)) < 0.2f;
			float lerpValue = Utils.GetLerpValue(0f, 10f, Projectile.localAI[0], clamped: true);
			Color newColor = Color.Lerp(Color.Transparent, Color.Crimson, lerpValue);
			if (Main.rand.Next(6) == 0)
			{
				Dust dust2 = Dust.NewDustDirect(Projectile.Center, 0, 0, 6, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, newColor, 3.5f);
				dust2.noGravity = true;
				dust2.velocity *= 1.4f;
				dust2.velocity += Main.rand.NextVector2Circular(1f, 1f);
				dust2.velocity += Projectile.velocity * 0.15f;
			}
			if (Main.rand.Next(12) == 0)
			{
				Dust dust3 = Dust.NewDustDirect(Projectile.Center, 0, 0, 6, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, newColor, 1.5f);
				dust3.velocity += Main.rand.NextVector2Circular(1f, 1f);
				dust3.velocity += Projectile.velocity * 0.15f;
			}
			if (flag3)
			{
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
	}
}

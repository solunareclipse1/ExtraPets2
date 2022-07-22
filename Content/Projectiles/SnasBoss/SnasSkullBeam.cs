using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using ExtraPets2.Content.NPCs.SnasBoss;

namespace ExtraPets2.Content.Projectiles.SnasBoss {
	public class SnasSkullBeam : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/SnasSkullBeam";
		private const float MOVE_DISTANCE = 60f;

		public float Distance {
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("gastr balsteer bref");

			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.PhantasmalDeathray);
			Projectile.timeLeft = 30;
			Projectile.aiStyle = -1;
			Projectile.width = 10;
			Projectile.height = 10;
		}

		// TODO: laser drawing, dunno how?
		//public override bool PreDraw(ref Color lightColor) {
		//		if (Projectile.velocity == Vector2.Zero)
		//		{
		//			return true;
		//		}
		//		Texture2D value45 = ModContent.Request<Texture2D>(ExtraPets2.AssetPath + "Textures/Projectiles/SnasSkullBeam");
		//		Texture2D value46 = TextureAssets.Extra[21].Value;
		//		Texture2D value47 = TextureAssets.Extra[22].Value;
		//		float num115 = Projectile.localAI[1];
		//		Color color68 = new Color(255, 255, 255, 0) * 0.9f;
		//		Main.EntitySpriteDraw(value45, Projectile.Center - Main.screenPosition, null, color68, Projectile.rotation, value45.Size() / 2f, Projectile.scale, SpriteEffects.None, 0);
		//		num115 -= (float)(value45.Height / 2 + value47.Height) * Projectile.scale;
		//		Vector2 center2 = Projectile.Center;
		//		center2 += Projectile.velocity * Projectile.scale * value45.Height / 2f;
		//		if (num115 > 0f)
		//		{
		//			float num116 = 0f;
		//			Rectangle value48 = new Rectangle(0, 16 * (Projectile.timeLeft / 3 % 5), value46.Width, 16);
		//			while (num116 + 1f < num115)
		//			{
		//				if (num115 - num116 < (float)value48.Height)
		//				{
		//					value48.Height = (int)(num115 - num116);
		//				}
		//				Main.EntitySpriteDraw(value46, center2 - Main.screenPosition, value48, color68, Projectile.rotation, new Vector2(value48.Width / 2, 0f), Projectile.scale, SpriteEffects.None, 0);
		//				num116 += (float)value48.Height * Projectile.scale;
		//				center2 += Projectile.velocity * value48.Height * Projectile.scale;
		//				value48.Y += 16;
		//				if (value48.Y + value48.Height > value46.Height)
		//				{
		//					value48.Y = 0;
		//				}
		//			}
		//		}
		//		Main.EntitySpriteDraw(value47, center2 - Main.screenPosition, null, color68, Projectile.rotation, value47.Frame().Top(), Projectile.scale, SpriteEffects.None, 0);
		//		return true;
		//}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
			float collisionPoint11 = 0f;
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * Projectile.localAI[1], 36f * Projectile.scale, ref collisionPoint11))
			{
				return true;
			}
			return false;
		}
		
		public override void AI() {
			Vector2? vector63 = null;
			if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
			{
				Projectile.velocity = -Vector2.UnitY;
			}
			if (true && Main.npc[(int)Projectile.ai[1]].active && Main.npc[(int)Projectile.ai[1]].type == ModContent.NPCType<SnasSkull>())
			{
				if (Main.npc[(int)Projectile.ai[1]].ai[0] == -2f)
				{
					Projectile.Kill();
					return;
				}
				Vector2 elipseSizes = new Vector2(27f, 59f) * Main.npc[(int)Projectile.ai[1]].localAI[1];
				Vector2 vector64 = Utils.Vector2FromElipse(Main.npc[(int)Projectile.ai[1]].localAI[0].ToRotationVector2(), elipseSizes);
				Projectile.position = Main.npc[(int)Projectile.ai[1]].Center + vector64 - new Vector2(Projectile.width, Projectile.height) / 2f;
			}
			else if (true && Main.npc[(int)Projectile.ai[1]].active && Main.npc[(int)Projectile.ai[1]].type == 400)
			{
				Vector2 elipseSizes = new Vector2(30f, 30f) * Main.npc[(int)Projectile.ai[1]].localAI[1];
				Vector2 vector65 = Utils.Vector2FromElipse(Main.npc[(int)Projectile.ai[1]].localAI[0].ToRotationVector2(), elipseSizes);
				Projectile.position = Main.npc[(int)Projectile.ai[1]].Center + vector65 - new Vector2(Projectile.width, Projectile.height) / 2f;
			}
			else
			{
				if (Projectile.type != 632 || !Main.projectile[(int)Projectile.ai[1]].active || Main.projectile[(int)Projectile.ai[1]].type != 633)
				{
					Projectile.Kill();
					return;
				}
				float num699 = (float)(int)Projectile.ai[0] - 2.5f;
				Vector2 vector68 = Vector2.Normalize(Main.projectile[(int)Projectile.ai[1]].velocity);
				Projectile projectile = Main.projectile[(int)Projectile.ai[1]];
				float num700 = num699 * ((float)Math.PI / 6f);
				float num701 = 20f;
				Vector2 zero = Vector2.Zero;
				float num702 = 1f;
				float num703 = 15f;
				float num704 = -2f;
				if (Projectile.ai[0] < 180f)
				{
					num702 = 1f - Projectile.ai[0] / 180f;
					num703 = 20f - Projectile.ai[0] / 180f * 14f;
					if (Projectile.ai[0] < 120f)
					{
						num701 = 20f - 4f * (Projectile.ai[0] / 120f);
						Projectile.Opacity = Projectile.ai[0] / 120f * 0.4f;
					}
					else
					{
						num701 = 16f - 10f * ((Projectile.ai[0] - 120f) / 60f);
						Projectile.Opacity = 0.4f + (Projectile.ai[0] - 120f) / 60f * 0.6f;
					}
					num704 = -22f + Projectile.ai[0] / 180f * 20f;
				}
				else
				{
					num702 = 0f;
					num701 = 1.75f;
					num703 = 6f;
					Projectile.Opacity = 1f;
					num704 = -2f;
				}
				float num705 = (Projectile.ai[0] + num699 * num701) / (num701 * 6f) * ((float)Math.PI * 2f);
				num700 = Vector2.UnitY.RotatedBy(num705).Y * ((float)Math.PI / 6f) * num702;
				zero = (Vector2.UnitY.RotatedBy(num705) * new Vector2(4f, num703)).RotatedBy(Projectile.velocity.ToRotation());
				Projectile.position = Projectile.Center + vector68 * 16f - Projectile.Size / 2f + new Vector2(0f, 0f - Main.projectile[(int)Projectile.ai[1]].gfxOffY);
				Projectile.position += Projectile.velocity.ToRotation().ToRotationVector2() * num704;
				Projectile.position += zero;
				Projectile.velocity = Vector2.Normalize(Projectile.velocity).RotatedBy(num700);
				Projectile.scale = 1.4f * (1f - num702);
				Projectile.damage = Projectile.damage;
				if (Projectile.ai[0] >= 180f)
				{
					Projectile.damage *= 3;
					vector63 = Projectile.Center;
				}
				if (!Collision.CanHitLine(Main.player[Projectile.owner].Center, 0, 0, Projectile.Center, 0, 0))
				{
					vector63 = Main.player[Projectile.owner].Center;
				}
				Projectile.friendly = Projectile.ai[0] > 30f;
			}
			if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
			{
				Projectile.velocity = -Vector2.UnitY;
			}
			//if (Projectile.localAI[0] == 0f)
			//{
			//	SoundEngine.PlaySound(29, (int)Projectile.position.X, (int)Projectile.position.Y, 104);
			//}
			float num706 = 1f;
			if (Main.npc[(int)Projectile.ai[1]].type == 400)
			{
				num706 = 0.4f;
			}
			Projectile.localAI[0]++;
			if (Projectile.localAI[0] >= 180f)
			{
				Projectile.Kill();
				return;
			}
			Projectile.scale = (float)Math.Sin(Projectile.localAI[0] * (float)Math.PI / 180f) * 10f * num706;
			if (Projectile.scale > num706)
			{
				Projectile.scale = num706;
			}
			float num709 = Projectile.velocity.ToRotation();
			num709 += Projectile.ai[0];
			Projectile.rotation = num709 - (float)Math.PI / 2f;
			Projectile.velocity = num709.ToRotationVector2();
			float num710 = 0f;
			float num711 = 0f;
			Vector2 samplingPoint = Projectile.Center;
			if (vector63.HasValue)
			{
				samplingPoint = vector63.Value;
			}
			num710 = 3f;
			num711 = Projectile.width;
			float[] array5 = new float[(int)num710];
			Collision.LaserScan(samplingPoint, Projectile.velocity, num711 * Projectile.scale, 2400f, array5);
			float num712 = 0f;
			for (int num713 = 0; num713 < array5.Length; num713++)
			{
				num712 += array5[num713];
			}
			num712 /= num710;
			float amount = 0.5f;
			NPC nPC7 = Main.npc[(int)Projectile.ai[1]];
			if (nPC7.type == ModContent.NPCType<SnasSkull>())
			{
				Player player9 = Main.player[nPC7.target];
				if (!Collision.CanHitLine(nPC7.position, nPC7.width, nPC7.height, player9.position, player9.width, player9.height))
				{
					num712 = Math.Min(2400f, Vector2.Distance(nPC7.Center, player9.Center) + 150f);
					amount = 0.75f;
				}
			}
			Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], num712, amount);
			Vector2 vector69 = Projectile.Center + Projectile.velocity * (Projectile.localAI[1] - 14f);
			for (int num714 = 0; num714 < 2; num714++)
			{
				float num715 = Projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? (-1f) : 1f) * ((float)Math.PI / 2f);
				float num716 = (float)Main.rand.NextDouble() * 2f + 2f;
				Vector2 vector70 = new Vector2((float)Math.Cos(num715) * num716, (float)Math.Sin(num715) * num716);
				int num717 = Dust.NewDust(vector69, 0, 0, 229, vector70.X, vector70.Y);
				Main.dust[num717].noGravity = true;
				Main.dust[num717].scale = 1.7f;
			}
			if (Main.rand.Next(5) == 0)
			{
				Vector2 vector71 = Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
				int num718 = Dust.NewDust(vector69 + vector71 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
				Dust dust119 = Main.dust[num718];
				Dust dust3 = dust119;
				dust3.velocity *= 0.5f;
				Main.dust[num718].velocity.Y = 0f - Math.Abs(Main.dust[num718].velocity.Y);
			}
			DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * Projectile.localAI[1], (float)Projectile.width * Projectile.scale, DelegateMethods.CastLight);
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

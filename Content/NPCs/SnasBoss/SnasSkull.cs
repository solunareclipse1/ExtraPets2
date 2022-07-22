using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using ExtraPets2.Content.Projectiles.SnasBoss;

namespace ExtraPets2.Content.NPCs.SnasBoss {
	public class SnasSkull : ModNPC {

        public override string Texture => ExtraPets2.AssetPath + "Textures/NPCs/SnasSkull";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("gater blsatter");
			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.GiantCursedSkull];
			
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0) {
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);

			NPCID.Sets.ProjectileNPC[Type] = true;
			NPCID.Sets.DontDoHardmodeScaling[Type] = true;
		}

		public override void SetDefaults() {
			NPC.CloneDefaults(NPCID.GiantCursedSkull);
			NPC.dontTakeDamage = true;
			NPC.alpha = 0;
			NPC.damage = 0;
			NPC.defense = 2000000000;
			NPC.lifeMax = 2000000000;
			NPC.value = 0;

			AnimationType = NPCID.GiantCursedSkull;
		}

		public override void AI() {
			if (NPC.ai[3] == 0 && NPC.localAI[0] == 1) {
				NPC.localAI[0] += 1;
			}
			if (NPC.alpha >= 255) {
				NPC.life = 0;
			}
			if (NPC.localAI[0] == 2) {
				NPC.alpha += 15;
			}
			float num146 = 1f;
			float num147 = 0.011f;
			NPC.TargetClosest();
			Vector2 projPos = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float projSpeedX = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - projPos.X;
			float projSpeedY = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - projPos.Y;
			float num150 = (float)Math.Sqrt(projSpeedX * projSpeedX + projSpeedY * projSpeedY);
			float num151 = num150;
			NPC.ai[1] += 1f;
			if (NPC.ai[1] > 600f)
			{
				num147 *= 8f;
				num146 = 4f;
				if (NPC.ai[1] > 650f)
				{
					NPC.ai[1] = 0f;
				}
			}
			else if (num151 < 250f)
			{
				NPC.ai[0] += 0.9f;
				if (NPC.ai[0] > 0f)
				{
					NPC.velocity.Y += 0.019f;
				}
				else
				{
					NPC.velocity.Y -= 0.019f;
				}
				if (NPC.ai[0] < -100f || NPC.ai[0] > 100f)
				{
					NPC.velocity.X += 0.019f;
				}
				else
				{
					NPC.velocity.X -= 0.019f;
				}
				if (NPC.ai[0] > 200f)
				{
					NPC.ai[0] = -200f;
				}
			}
			if (num151 > 350f)
			{
				num146 = 5f;
				num147 = 0.3f;
			}
			else if (num151 > 300f)
			{
				num146 = 3f;
				num147 = 0.2f;
			}
			else if (num151 > 250f)
			{
				num146 = 1.5f;
				num147 = 0.1f;
			}
			num150 = num146 / num150;
			projSpeedX *= num150;
			projSpeedY *= num150;
			if (Main.player[NPC.target].dead)
			{
				projSpeedX = (float)NPC.direction * num146 / 2f;
				projSpeedY = (0f - num146) / 2f;
			}
			if (NPC.velocity.X < projSpeedX)
			{
				NPC.velocity.X += num147;
			}
			else if (NPC.velocity.X > projSpeedX)
			{
				NPC.velocity.X -= num147;
			}
			if (NPC.velocity.Y < projSpeedY)
			{
				NPC.velocity.Y += num147;
			}
			else if (NPC.velocity.Y > projSpeedY)
			{
				NPC.velocity.Y -= num147;
			}
			if (projSpeedX > 0f)
			{
				NPC.spriteDirection = -1;
				NPC.rotation = (float)Math.Atan2(projSpeedY, projSpeedX);
			}
			if (projSpeedX < 0f)
			{
				NPC.spriteDirection = 1;
				NPC.rotation = (float)Math.Atan2(projSpeedY, projSpeedX) + 3.14f;
			}
			if (NPC.justHit)
			{
				NPC.ai[2] = 0f;
				NPC.ai[3] = 0f;
			}
			projPos = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			projSpeedX = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - projPos.X;
			projSpeedY = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - projPos.Y;
			num150 = (float)Math.Sqrt(projSpeedX * projSpeedX + projSpeedY * projSpeedY);
			if (num150 <= 500f)
			{
				NPC.ai[2] += 1f;
				if (NPC.ai[3] == 0f)
				{
					if (NPC.ai[2] > 120f)
					{
						NPC.ai[2] = 0f;
						NPC.ai[3] = 1f;
						NPC.netUpdate = true;
					}
					return;
				}
				if (NPC.ai[2] > 40f)
				{
					NPC.ai[3] = 0f;
				}
				if (Main.netMode != 1 && NPC.ai[2] == 20f)
				{
					projSpeedX *= 1;
					projSpeedY *= 1;
					int proj = Projectile.NewProjectile(NPC.GetSource_FromAI(), projPos.X, projPos.Y, projSpeedX, projSpeedY, ModContent.ProjectileType<SnasSkullBeam>(), 1, 0f, Main.myPlayer, 0, NPC.whoAmI);
					NPC.localAI[0] += 1;
				}
			}
			else
			{
				NPC.ai[2] = 0f;
				NPC.ai[3] = 0f;
			}
			return;
		}
	}
}

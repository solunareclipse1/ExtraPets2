using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;

namespace ExtraPets2.Content.NPCs {
	public class SnasUdertal : ModNPC {

        public override string Texture => ExtraPets2.AssetPath + "Textures/NPCs/SnasUdertal";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("snas udertal");
			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.GreekSkeleton];

			//NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0) {
			//	Velocity = 1f
			//};
			//NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

		public override void SetDefaults() {
			NPC.CloneDefaults(NPCID.GreekSkeleton);
			NPC.aiStyle = -1;

			AnimationType = NPCID.GreekSkeleton;
		}

		public override void HitEffect(int hitDirection, double damage) {
			NPC.velocity *= 0f;
			NPC.ai[3] = 0f;
			NPC.position += NPC.netOffset;
			SoundEngine.PlaySound(EPSoundStyles.Sus4, NPC.position);
			Vector2 vector19 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float num55 = NPC.oldPos[2].X + (float)NPC.width * 0.5f - vector19.X;
			float num56 = NPC.oldPos[2].Y + (float)NPC.height * 0.5f - vector19.Y;
			float num57 = (float)Math.Sqrt(num55 * num55 + num56 * num56);
			num57 = 2f / num57;
			num55 *= num57;
			num56 *= num57;
			for (int num58 = 0; num58 < 20; num58++)
			{
				int num59 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 71, num55, num56, 200, default(Color), 2f);
				Main.dust[num59].noGravity = true;
				Main.dust[num59].velocity.X *= 2f;
			}
			for (int num60 = 0; num60 < 20; num60++)
			{
				int num61 = Dust.NewDust(NPC.oldPos[2], NPC.width, NPC.height, 71, 0f - num55, 0f - num56, 200, default(Color), 2f);
				Main.dust[num61].noGravity = true;
				Main.dust[num61].velocity.X *= 2f;
			}
			NPC.position -= NPC.netOffset;
		}

		public override bool PreAI() {
			if (Main.rand.Next(0, 1000) >= 950) {
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<SnasSkull>());
			} else if (Main.rand.Next(0, 10000) == 0) {
				for (int i = 0; i < 10; i++) {
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<SnasSkull>());
				}
			}
			if (Main.rand.Next(0,10) == 0) {
				SoundEngine.PlaySound(EPSoundStyles.SnasSpeak, NPC.position);
			}
			if (NPC.ai[0] == 69) {
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<SnasUdertal>());
			}
			return true;
		}

		public override void AI() {
			if (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height == NPC.position.Y + (float)NPC.height) {
				NPC.directionY = -1;
			}
			bool flag = false;
			bool flag5 = false;
			bool flag6 = false;
			if (NPC.velocity.X == 0f) {
				flag6 = true;
			}
			if (NPC.justHit) {
				flag6 = false;
			}
			int num54 = 60;
			// tele code
			//if (NPC.type == 120)
			//{
			//	num54 = 180;
			//	if (NPC.ai[3] == -120f)
			//	{
			//		NPC.velocity *= 0f;
			//		NPC.ai[3] = 0f;
			//		NPC.position += NPC.netOffset;
			//		//SoundEngine.PlaySound(in SoundID.Item8, NPC.position);
			//		Vector2 vector19 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			//		float num55 = NPC.oldPos[2].X + (float)NPC.width * 0.5f - vector19.X;
			//		float num56 = NPC.oldPos[2].Y + (float)NPC.height * 0.5f - vector19.Y;
			//		float num57 = (float)Math.Sqrt(num55 * num55 + num56 * num56);
			//		num57 = 2f / num57;
			//		num55 *= num57;
			//		num56 *= num57;
			//		for (int num58 = 0; num58 < 20; num58++)
			//		{
			//			int num59 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 71, num55, num56, 200, default(Color), 2f);
			//			Main.dust[num59].noGravity = true;
			//			Main.dust[num59].velocity.X *= 2f;
			//		}
			//		for (int num60 = 0; num60 < 20; num60++)
			//		{
			//			int num61 = Dust.NewDust(NPC.oldPos[2], NPC.width, NPC.height, 71, 0f - num55, 0f - num56, 200, default(Color), 2f);
			//			Main.dust[num61].noGravity = true;
			//			Main.dust[num61].velocity.X *= 2f;
			//		}
			//		NPC.position -= NPC.netOffset;
			//	}
			//}
			bool flag7 = false;
			bool flag8 = true;
			bool flag9 = false;
			int num62 = NPC.type;
			bool flag10 = true;
			if (!flag9 && flag10)
			{
				if (NPC.velocity.Y == 0f && ((NPC.velocity.X > 0f && NPC.direction < 0) || (NPC.velocity.X < 0f && NPC.direction > 0)))
				{
					flag7 = true;
				}
				if (NPC.position.X == NPC.oldPosition.X || NPC.ai[3] >= (float)num54 || flag7)
				{
					NPC.ai[3] += 1f;
				}
				else if ((double)Math.Abs(NPC.velocity.X) > 0.9 && NPC.ai[3] > 0f)
				{
					NPC.ai[3] -= 1f;
				}
				if (NPC.ai[3] > (float)(num54 * 10))
				{
					NPC.ai[3] = 0f;
				}
				if (NPC.justHit)
				{
					NPC.ai[3] = 0f;
				}
				if (NPC.ai[3] == (float)num54)
				{
					NPC.netUpdate = true;
				}
				if (Main.player[NPC.target].Hitbox.Intersects(NPC.Hitbox))
				{
					NPC.ai[3] = 0f;
				}
			}
			if (NPC.ai[3] < (float)num54 && NPC.DespawnEncouragement_AIStyle3_Fighters_NotDiscouraged(NPC.type, NPC.position, NPC))
			{
				if ((NPC.type == 3 || NPC.type == 591 || NPC.type == 590 || NPC.type == 331 || NPC.type == 332 || NPC.type == 21 || (NPC.type >= 449 && NPC.type <= 452) || NPC.type == 31 || NPC.type == 294 || NPC.type == 295 || NPC.type == 296 || NPC.type == 77 || NPC.type == 110 || NPC.type == 132 || NPC.type == 167 || NPC.type == 161 || NPC.type == 162 || NPC.type == 186 || NPC.type == 187 || NPC.type == 188 || NPC.type == 189 || NPC.type == 197 || NPC.type == 200 || NPC.type == 201 || NPC.type == 202 || NPC.type == 203 || NPC.type == 223 || NPC.type == 291 || NPC.type == 292 || NPC.type == 293 || NPC.type == 320 || NPC.type == 321 || NPC.type == 319 || NPC.type == 481 || NPC.type == 632 || NPC.type == 635) && Main.rand.Next(1000) == 0)
				{
					//SoundEngine.PlaySound(14, (int)NPC.position.X, (int)NPC.position.Y);
				}
				if ((NPC.type == 489 || NPC.type == 586) && Main.rand.Next(800) == 0)
				{
					//SoundEngine.PlaySound(14, (int)NPC.position.X, (int)NPC.position.Y, NPC.type);
				}
				if ((NPC.type == 78 || NPC.type == 79 || NPC.type == 80 || NPC.type == 630) && Main.rand.Next(500) == 0)
				{
					//SoundEngine.PlaySound(26, (int)NPC.position.X, (int)NPC.position.Y);
				}
				if (NPC.type == 159 && Main.rand.Next(500) == 0)
				{
					//SoundEngine.PlaySound(29, (int)NPC.position.X, (int)NPC.position.Y, 7);
				}
				if (NPC.type == 162 && Main.rand.Next(500) == 0)
				{
					//SoundEngine.PlaySound(29, (int)NPC.position.X, (int)NPC.position.Y, 6);
				}
				if (NPC.type == 181 && Main.rand.Next(500) == 0)
				{
					//SoundEngine.PlaySound(29, (int)NPC.position.X, (int)NPC.position.Y, 8);
				}
				if (NPC.type >= 269 && NPC.type <= 280 && Main.rand.Next(1000) == 0)
				{
					//SoundEngine.PlaySound(14, (int)NPC.position.X, (int)NPC.position.Y);
				}
				NPC.TargetClosest();
				if (NPC.directionY > 0 && Main.player[NPC.target].Center.Y <= NPC.Bottom.Y)
				{
					NPC.directionY = -1;
				}
			}
			else if (!(NPC.ai[2] > 0f) || !NPC.DespawnEncouragement_AIStyle3_Fighters_CanBeBusyWithAction(NPC.type))
			{
				if (Main.dayTime && (double)(NPC.position.Y / 16f) < Main.worldSurface && NPC.type != 624 && NPC.type != 631)
				{
					NPC.ai[0] = 69;
				}
				if (NPC.velocity.X == 0f)
				{
					if (NPC.velocity.Y == 0f)
					{
						NPC.ai[0] += 1f;
						if (NPC.ai[0] >= 2f)
						{
							NPC.direction *= -1;
							NPC.spriteDirection = NPC.direction;
							NPC.ai[0] = 0f;
						}
					}
				}
				else
				{
					NPC.ai[0] = 0f;
				}
				if (NPC.direction == 0)
				{
					NPC.direction = 1;
				}
			}
			//if (NPC.type == 287) {
				float num83 = 8f;
				float num84 = 0.2f;
				if (NPC.velocity.X < 0f - num83 || NPC.velocity.X > num83)
				{
					if (NPC.velocity.Y == 0f)
					{
						NPC.velocity *= 0.7f;
					}
				}
				else if (NPC.velocity.X < num83 && NPC.direction == 1)
				{
					NPC.velocity.X += num84;
					if (NPC.velocity.X > num83)
					{
						NPC.velocity.X = num83;
					}
				}
				else if (NPC.velocity.X > 0f - num83 && NPC.direction == -1)
				{
					NPC.velocity.X -= num84;
					if (NPC.velocity.X < 0f - num83)
					{
						NPC.velocity.X = 0f - num83;
					}
				}
			//}
			if (Main.netMode != 1)
			{
				if (Main.expertMode && NPC.target >= 0 && (NPC.type == 163 || NPC.type == 238 || NPC.type == 236 || NPC.type == 237) && Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1))
				{
					NPC.localAI[0] += 1f;
					if (NPC.justHit)
					{
						NPC.localAI[0] -= Main.rand.Next(20, 60);
						if (NPC.localAI[0] < 0f)
						{
							NPC.localAI[0] = 0f;
						}
					}
					if (NPC.localAI[0] > (float)Main.rand.Next(180, 900))
					{
						NPC.localAI[0] = 0f;
						Vector2 vector31 = Main.player[NPC.target].Center - NPC.Center;
						vector31.Normalize();
						vector31 *= 8f;
						int attackDamage_ForProjectiles2 = NPC.GetAttackDamage_ForProjectiles(18f, 18f);
						Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, vector31.X, vector31.Y, 472, attackDamage_ForProjectiles2, 0f, Main.myPlayer);
					}
				}
				if (NPC.velocity.Y == 0f)
				{
					int num116 = -1;
					switch (NPC.type)
					{
					case 164:
						num116 = 165;
						break;
					case 236:
						num116 = 237;
						break;
					case 163:
						num116 = 238;
						break;
					case 239:
						num116 = 240;
						break;
					case 530:
						num116 = 531;
						break;
					}
					if (num116 != -1 && NPC.NPCCanStickToWalls())
					{
						NPC.Transform(num116);
					}
				}
			}
			if (NPC.velocity.Y == 0f || flag)
			{
				int y2 = (int)(NPC.position.Y + (float)NPC.height + 7f) / 16;
				int y3 = (int)(NPC.position.Y - 9f) / 16;
				int num166 = (int)NPC.position.X / 16;
				int num167 = (int)(NPC.position.X + (float)NPC.width) / 16;
				int num168 = (int)(NPC.position.X + 8f) / 16;
				int num169 = (int)(NPC.position.X + (float)NPC.width - 8f) / 16;
				bool flag20 = false;
				for (int num170 = num168; num170 <= num169; num170++)
				{
					if (num170 >= num166 && num170 <= num167 && Main.tile[num170, y2] == null)
					{
						flag20 = true;
						continue;
					}
					if (Main.tile[num170, y3] != null && Main.tile[num170, y3].HasUnactuatedTile && Main.tileSolid[Main.tile[num170, y3].TileType])
					{
						flag5 = false;
						break;
					}
					if (!flag20 && num170 >= num166 && num170 <= num167 && Main.tile[num170, y2].HasUnactuatedTile && Main.tileSolid[Main.tile[num170, y2].TileType])
					{
						flag5 = true;
					}
				}
				if (!flag5 && NPC.velocity.Y < 0f)
				{
					NPC.velocity.Y = 0f;
				}
				if (flag20)
				{
					return;
				}
			}
			if (NPC.velocity.Y >= 0f && (NPC.type != 580 || NPC.directionY != 1))
			{
				int num171 = 0;
				if (NPC.velocity.X < 0f)
				{
					num171 = -1;
				}
				if (NPC.velocity.X > 0f)
				{
					num171 = 1;
				}
				Vector2 vector37 = NPC.position;
				vector37.X += NPC.velocity.X;
				int num172 = (int)((vector37.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * num171)) / 16f);
				int num173 = (int)((vector37.Y + (float)NPC.height - 1f) / 16f);
				if (WorldGen.InWorld(num172, num173, 4))
				{
					if (Main.tile[num172, num173] == null)
					{
						//Main.tile[num172, num173] = default(Tile);
					}
					if (Main.tile[num172, num173 - 1] == null)
					{
						//Main.tile[num172, num173 - 1] = default(Tile);
					}
					if (Main.tile[num172, num173 - 2] == null)
					{
						//Main.tile[num172, num173 - 2] = default(Tile);
					}
					if (Main.tile[num172, num173 - 3] == null)
					{
						//Main.tile[num172, num173 - 3] = default(Tile);
					}
					if (Main.tile[num172, num173 + 1] == null)
					{
						//Main.tile[num172, num173 + 1] = default(Tile);
					}
					if (Main.tile[num172 - num171, num173 - 3] == null)
					{
						//Main.tile[num172 - num171, num173 - 3] = default(Tile);
					}
					if ((float)(num172 * 16) < vector37.X + (float)NPC.width && (float)(num172 * 16 + 16) > vector37.X && ((Main.tile[num172, num173].HasUnactuatedTile && !Main.tile[num172, num173].TopSlope && !Main.tile[num172, num173 - 1].TopSlope && Main.tileSolid[Main.tile[num172, num173].TileType] && !Main.tileSolidTop[Main.tile[num172, num173].TileType]) || (Main.tile[num172, num173 - 1].IsHalfBlock && Main.tile[num172, num173 - 1].HasUnactuatedTile)) && (!Main.tile[num172, num173 - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 1].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 1].TileType] || (Main.tile[num172, num173 - 1].IsHalfBlock && (!Main.tile[num172, num173 - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 4].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 4].TileType]))) && (!Main.tile[num172, num173 - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 2].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 2].TileType]) && (!Main.tile[num172, num173 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 3].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 3].TileType]) && (!Main.tile[num172 - num171, num173 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172 - num171, num173 - 3].TileType]))
					{
						float num174 = num173 * 16;
						if (Main.tile[num172, num173].IsHalfBlock)
						{
							num174 += 8f;
						}
						if (Main.tile[num172, num173 - 1].IsHalfBlock)
						{
							num174 -= 8f;
						}
						if (num174 < vector37.Y + (float)NPC.height)
						{
							float num175 = vector37.Y + (float)NPC.height - num174;
							float num176 = 16.1f;
							if (NPC.type == 163 || NPC.type == 164 || NPC.type == 236 || NPC.type == 239 || NPC.type == 530)
							{
								num176 += 8f;
							}
							if (num175 <= num176)
							{
								NPC.gfxOffY += NPC.position.Y + (float)NPC.height - num174;
								NPC.position.Y = num174 - (float)NPC.height;
								if (num175 < 9f)
								{
									NPC.stepSpeed = 1f;
								}
								else
								{
									NPC.stepSpeed = 2f;
								}
							}
						}
					}
				}
			}
			if (flag5)
			{
				int num177 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)(15 * NPC.direction)) / 16f);
				int num178 = (int)((NPC.position.Y + (float)NPC.height - 15f) / 16f);
				if (NPC.type == 109 || NPC.type == 163 || NPC.type == 164 || NPC.type == 199 || NPC.type == 236 || NPC.type == 239 || NPC.type == 257 || NPC.type == 258 || NPC.type == 290 || NPC.type == 391 || NPC.type == 425 || NPC.type == 427 || NPC.type == 426 || NPC.type == 580 || NPC.type == 508 || NPC.type == 415 || NPC.type == 530 || NPC.type == 532 || NPC.type == 582)
				{
					num177 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 16) * NPC.direction)) / 16f);
				}
				if (Main.tile[num177, num178] == null)
				{
					//Main.tile[num177, num178] = default(Tile);
				}
				if (Main.tile[num177, num178 - 1] == null)
				{
					//Main.tile[num177, num178 - 1] = default(Tile);
				}
				if (Main.tile[num177, num178 - 2] == null)
				{
					//Main.tile[num177, num178 - 2] = default(Tile);
				}
				if (Main.tile[num177, num178 - 3] == null)
				{
					//Main.tile[num177, num178 - 3] = default(Tile);
				}
				if (Main.tile[num177, num178 + 1] == null)
				{
					//Main.tile[num177, num178 + 1] = default(Tile);
				}
				if (Main.tile[num177 + NPC.direction, num178 - 1] == null)
				{
					//Main.tile[num177 + NPC.direction, num178 - 1] = default(Tile);
				}
				if (Main.tile[num177 + NPC.direction, num178 + 1] == null)
				{
					//Main.tile[num177 + NPC.direction, num178 + 1] = default(Tile);
				}
				if (Main.tile[num177 - NPC.direction, num178 + 1] == null)
				{
					//Main.tile[num177 - NPC.direction, num178 + 1] = default(Tile);
				}
				//Main.tile[num177, num178 + 1].IsHalfBlock;
				if (Main.tile[num177, num178 - 1].HasUnactuatedTile && (TileLoader.IsClosedDoor(Main.tile[num177, num178 - 1]) || Main.tile[num177, num178 - 1].TileType == 388) && flag8)
				{
					NPC.ai[2] += 1f;
					NPC.ai[3] = 0f;
					if (NPC.ai[2] >= 60f)
					{
						bool flag21 = NPC.type == 3 || NPC.type == 430 || NPC.type == 590 || NPC.type == 331 || NPC.type == 332 || NPC.type == 132 || NPC.type == 161 || NPC.type == 186 || NPC.type == 187 || NPC.type == 188 || NPC.type == 189 || NPC.type == 200 || NPC.type == 223 || NPC.type == 320 || NPC.type == 321 || NPC.type == 319 || NPC.type == 21 || NPC.type == 324 || NPC.type == 323 || NPC.type == 322 || NPC.type == 44 || NPC.type == 196 || NPC.type == 167 || NPC.type == 77 || NPC.type == 197 || NPC.type == 202 || NPC.type == 203 || NPC.type == 449 || NPC.type == 450 || NPC.type == 451 || NPC.type == 452 || NPC.type == 481 || NPC.type == 201 || NPC.type == 635;
						bool flag22 = Main.player[NPC.target].ZoneGraveyard && Main.rand.Next(60) == 0;
						if ((!Main.bloodMoon || Main.getGoodWorld) && !flag22 && flag21)
						{
							NPC.ai[1] = 0f;
						}
						NPC.velocity.X = 0.5f * (float)(-NPC.direction);
						int num179 = 5;
						if (Main.tile[num177, num178 - 1].TileType == 388)
						{
							num179 = 2;
						}
						NPC.ai[1] += num179;
						if (NPC.type == 27)
						{
							NPC.ai[1] += 1f;
						}
						NPC.ai[2] = 0f;
						bool flag23 = false;
						if (NPC.ai[1] >= 10f)
						{
							flag23 = true;
							NPC.ai[1] = 10f;
						}
						WorldGen.KillTile(num177, num178 - 1, fail: true);
						if ((Main.netMode != 1 || !flag23) && flag23 && Main.netMode != 1)
						{
							if (NPC.type == 26)
							{
								WorldGen.KillTile(num177, num178 - 1);
								if (Main.netMode == 2)
								{
									NetMessage.SendData(17, -1, -1, null, 0, num177, num178 - 1);
								}
							}
							else
							{
								if (TileLoader.OpenDoorID(Main.tile[num177, num178 - 1]) >= 0)
								{
									bool flag24 = WorldGen.OpenDoor(num177, num178 - 1, NPC.direction);
									if (!flag24)
									{
										NPC.ai[3] = num54;
										NPC.netUpdate = true;
									}
									if (Main.netMode == 2 && flag24)
									{
										NetMessage.SendData(19, -1, -1, null, 0, num177, num178 - 1, NPC.direction);
									}
								}
								if (Main.tile[num177, num178 - 1].TileType == 388)
								{
									bool flag25 = WorldGen.ShiftTallGate(num177, num178 - 1, closing: false);
									if (!flag25)
									{
										NPC.ai[3] = num54;
										NPC.netUpdate = true;
									}
									if (Main.netMode == 2 && flag25)
									{
										NetMessage.SendData(19, -1, -1, null, 4, num177, num178 - 1);
									}
								}
							}
						}
					}
				}
				else
				{
					int num180 = NPC.spriteDirection;
					if ((NPC.velocity.X < 0f && num180 == -1) || (NPC.velocity.X > 0f && num180 == 1))
					{
						if (NPC.height >= 32 && Main.tile[num177, num178 - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[num177, num178 - 2].TileType])
						{
							if (Main.tile[num177, num178 - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[num177, num178 - 3].TileType])
							{
								NPC.velocity.Y = -8f;
								NPC.netUpdate = true;
							}
							else
							{
								NPC.velocity.Y = -7f;
								NPC.netUpdate = true;
							}
						}
						else if (Main.tile[num177, num178 - 1].HasUnactuatedTile && Main.tileSolid[Main.tile[num177, num178 - 1].TileType])
						{
							NPC.velocity.Y = -6f;
							NPC.netUpdate = true;
						}
						else if (NPC.position.Y + (float)NPC.height - (float)(num178 * 16) > 20f && Main.tile[num177, num178].HasUnactuatedTile && !Main.tile[num177, num178].TopSlope && Main.tileSolid[Main.tile[num177, num178].TileType])
						{
							NPC.velocity.Y = -5f;
							NPC.netUpdate = true;
						}
						else if (NPC.directionY < 0 && NPC.type != 67 && (!Main.tile[num177, num178 + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num177, num178 + 1].TileType]) && (!Main.tile[num177 + NPC.direction, num178 + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num177 + NPC.direction, num178 + 1].TileType]))
						{
							NPC.velocity.Y = -8f;
							NPC.velocity.X *= 1.5f;
							NPC.netUpdate = true;
						}
						else if (flag8)
						{
							NPC.ai[1] = 0f;
							NPC.ai[2] = 0f;
						}
						if (NPC.velocity.Y == 0f && flag6 && NPC.ai[3] == 1f)
						{
							NPC.velocity.Y = -5f;
						}
						if (NPC.velocity.Y == 0f && (Main.expertMode || NPC.type == 586) && Main.player[NPC.target].Bottom.Y < NPC.Top.Y && Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < (float)(Main.player[NPC.target].width * 3) && Collision.CanHit(NPC, Main.player[NPC.target]))
						{
							if (NPC.velocity.Y == 0f)
							{
								int num183 = 6;
								if (Main.player[NPC.target].Bottom.Y > NPC.Top.Y - (float)(num183 * 16))
								{
									NPC.velocity.Y = -7.9f;
								}
								else
								{
									int x2 = (int)(NPC.Center.X / 16f);
									int num184 = (int)(NPC.Bottom.Y / 16f) - 1;
									for (int num185 = num184; num185 > num184 - num183; num185--)
									{
										if (Main.tile[x2, num185].HasUnactuatedTile && TileID.Sets.Platforms[Main.tile[x2, num185].TileType])
										{
											NPC.velocity.Y = -7.9f;
											break;
										}
									}
								}
							}
						}
					}
					if ((NPC.type == 287 || true) && NPC.velocity.Y == 0f && Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))) < 150f && Math.Abs(NPC.position.Y + (float)(NPC.height / 2) - (Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2))) < 50f && ((NPC.direction > 0 && NPC.velocity.X >= 1f) || (NPC.direction < 0 && NPC.velocity.X <= -1f)))
					{
						NPC.velocity.X = 12 * NPC.direction;
						NPC.velocity.Y = -4f;
						NPC.netUpdate = true;
					}
					if ((NPC.type == 287 || true) && NPC.velocity.Y < 0f)
					{
						NPC.velocity.X *= 2f;
						NPC.velocity.Y *= 1.8f;
					}
				}
			}
			else if (flag8)
			{
				NPC.ai[1] = 0f;
				NPC.ai[2] = 0f;
			}
			if (Main.netMode == 1 || NPC.type != 120 || !(NPC.ai[3] >= (float)num54))
			{
				return;
			}
			// chaos elemental only
			//int num188 = (int)Main.player[NPC.target].position.X / 16;
			//int num189 = (int)Main.player[NPC.target].position.Y / 16;
			//int num190 = (int)NPC.position.X / 16;
			//int num191 = (int)NPC.position.Y / 16;
			//int num192 = 20;
			//int num193 = 0;
			//bool flag26 = false;
			//if (Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) + Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 2000f)
			//{
			//	num193 = 100;
			//	flag26 = true;
			//}
			//while (!flag26 && num193 < 100)
			//{
			//	num193++;
			//	int num194 = Main.rand.Next(num188 - num192, num188 + num192);
			//	for (int num195 = Main.rand.Next(num189 - num192, num189 + num192); num195 < num189 + num192; num195++)
			//	{
			//		if ((num195 < num189 - 4 || num195 > num189 + 4 || num194 < num188 - 4 || num194 > num188 + 4) && (num195 < num191 - 1 || num195 > num191 + 1 || num194 < num190 - 1 || num194 > num190 + 1) && Main.tile[num194, num195].HasUnactuatedTile)
			//		{
			//			bool flag27 = true;
			//			if (NPC.type == 32 && Main.tile[num194, num195 - 1].WallType == 0)
			//			{
			//				flag27 = false;
			//			}
			//			else if (Main.tile[num194, num195 - 1].LiquidType == 1)
			//			{
			//				flag27 = false;
			//			}
			//			if (flag27 && Main.tileSolid[Main.tile[num194, num195].TileType] && !Collision.SolidTiles(num194 - 1, num194 + 1, num195 - 4, num195 - 1))
			//			{
			//				NPC.position.X = num194 * 16 - NPC.width / 2;
			//				NPC.position.Y = num195 * 16 - NPC.height;
			//				NPC.netUpdate = true;
			//				NPC.ai[3] = -120f;
			//			}
			//		}
			//	}
			//}
		}
	}
}

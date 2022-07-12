using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

using ExtraPets2.Content.Projectiles;
using ExtraPets2.Content.Items.SnasBoss;

namespace ExtraPets2.Content.NPCs {
	public class SnasUdertal : ModNPC {

        public override string Texture => ExtraPets2.AssetPath + "Textures/NPCs/SnasUdertal";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("snas udertal");
			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BoneThrowingSkeleton];

			//NPCID.Sets.MPAllowedEnemies[Type] = true;

			NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData {
				ImmuneToAllBuffsThatAreNotWhips = true,
				ImmuneToWhips = true
			};

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0) {
				Velocity = 1f
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
			NPCID.Sets.BossBestiaryPriority.Add(Type);
		}

		public override void SetDefaults() {
			NPC.CloneDefaults(NPCID.BoneThrowingSkeleton);
			NPC.aiStyle = -1;
			NPC.lifeMax = 9999;
			NPC.defense = 0;
			NPC.damage = 1;
			NPC.knockBackResist = 0;
			NPC.boss = true;

			AnimationType = NPCID.BoneThrowingSkeleton;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
				new FlavorTextBestiaryInfoElement("A comedic skeleton from another world, corrupted into a being of chaos. They roam the Dungeon with an insatiable thirst for battle.")
			});
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot) {
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<SnasBag>()));

			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SnasTrophy>(), 10));

			npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<SnasRelic>()));

			npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<SnasBag>(), 4)); // second bag as placeholder, need pet

			LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
			
			notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SnasMask>(), 7));

			npcLoot.Add(notExpertRule);
		}

		public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit) {
			if (!DodgeLogic()) { // Dodgelogic returns false when we are dodging
				damage = 0;
				knockback = 0;
				crit = false;
			} else {
				damage = 999999999;
			}
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) {
			if (!DodgeLogic()) {
				damage = 0;
				knockback = 0;
				crit = false;
			} else {
				damage = 999999999;
			}
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

		public override void AI() {

			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active) {
				NPC.TargetClosest();
			}

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
			bool flag7 = false;
			bool flag9 = false;
			bool flag10 = true;
			bool enraged = false;
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
				NPC.TargetClosest();
				if (NPC.directionY > 0 && Main.player[NPC.target].Center.Y <= NPC.Bottom.Y)
				{
					NPC.directionY = -1;
				}
			}
			else if (!NPC.DespawnEncouragement_AIStyle3_Fighters_CanBeBusyWithAction(NPC.type))
			{
				if (Main.dayTime && (double)(NPC.position.Y / 16f) < Main.worldSurface)
				{
					enraged = true;
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
				if ((NPC.type == 287 || true) && NPC.velocity.Y == 0f && Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))) < 150f && Math.Abs(NPC.position.Y + (float)(NPC.height / 2) - (Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2))) < 50f && ((NPC.direction > 0 && NPC.velocity.X >= 1f) || (NPC.direction < 0 && NPC.velocity.X <= -1f))) {
					NPC.velocity.X = 12 * NPC.direction;
					NPC.velocity.Y = -4f;
					NPC.netUpdate = true;
				}
				if ((NPC.type == 287 || true) && NPC.velocity.Y < 0f) {
					NPC.velocity.X *= 2f;
					NPC.velocity.Y *= 1.8f;
				}
			}
			int dodgeChance = 1000;
			if (enraged) {
				dodgeChance = 999999999;
			}
			if (Main.netMode != 1 && Main.rand.Next(0,dodgeChance) == 0) {
				NPC.ai[2] = 1; // dodge fails if ai[2] = 1
				NPC.netUpdate = true;
			} else if (Main.netMode != 1 && NPC.ai[2] != 0) {
				NPC.ai[2] = 0;
				NPC.netUpdate = true;
			}

			// enraged attacks
			if (enraged && Main.rand.Next(0, 100) == 0 && Main.netMode != 1) {
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<SnasUdertal>());
			}
			if (enraged && Main.rand.Next(0, 250) == 0 && Main.netMode != 1) {
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DungeonGuardian);
			}
			if (enraged && Main.rand.Next(0, 500) == 0 && Main.netMode != 1) {
				for (int i = 0; i < 25; i++) {
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.oldPos[2].X, (int)NPC.oldPos[2].Y, ModContent.NPCType<SnasSkull>());
				}
			}
			if (enraged && Main.netMode != 1) {
				NPC.TargetClosest();
				if (!Dodge()) {
					int offset = 200;
					int teleX = Main.rand.Next((int)Main.player[NPC.target].position.X - offset, (int)Main.player[NPC.target].position.X + offset);
					int teleY = Main.rand.Next((int)Main.player[NPC.target].position.Y - offset, (int)Main.player[NPC.target].position.Y + offset);
					NPC.position = new Vector2(teleX, teleY);
					NPC.netUpdate = true;
				}
			}
			if (enraged && Main.rand.Next(0, 1000) == 0 && Main.netMode != 1) {
				Projectile.NewProjectile(NPC.GetSource_FromAI(), Main.player[NPC.target].position, Vector2.Zero, ProjectileID.HallowBossSplitShotCore, 420, 10);
			}


			// normal attacks
			if (!enraged && Main.rand.Next(0, 250) == 0 && Main.netMode != 1) {
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<SnasSkull>());
			}
			if (!enraged && Main.rand.Next(0, 1000) == 0 && Main.netMode != 1) {
				for (int i = 0; i < 10; i++) {
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<SnasSkull>());
				}
			}
			if (!enraged && Main.rand.Next(0, 500) == 0 && Main.netMode != 1) {
				Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, ModContent.ProjectileType<SnasTombstone>(), 1, 10);
			}
			if (!enraged && Main.rand.Next(0, 2500) == 0 && Main.netMode != 1) {
				for (int i = 0; i < 100; i++) {
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position, new Vector2(Main.rand.NextFloat(-100,100),Main.rand.NextFloat(-100,100)), ModContent.ProjectileType<SnasTombstone>(), 1, 10);
				}
			}

			// eueueueueueueueueueuueueu
			if (Main.rand.Next(0,5) == 0) {
				SoundEngine.PlaySound(EPSoundStyles.SnasSpeak with {MaxInstances = 10}, NPC.position);
			}

			// despawn on players dead
			if (Main.player[NPC.target].dead) {
				NPC.noTileCollide = true;
				NPC.EncourageDespawn(10);
			}
		}

		public bool DodgeLogic() {
			bool dodgeResult = DoesDodgeFail();
			if (dodgeResult) {
				if (NPC.ai[1] == 1) {
					NPC.velocity *= 0f;
					NPC.ai[3] = 0f;
					NPC.position += NPC.netOffset;
					SoundEngine.PlaySound(EPSoundStyles.Sus4, NPC.oldPos[2]);
					Vector2 vector19 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num55 = NPC.oldPos[2].X + (float)NPC.width * 0.5f - vector19.X;
					float num56 = NPC.oldPos[2].Y + (float)NPC.height * 0.5f - vector19.Y;
					float num57 = (float)Math.Sqrt(num55 * num55 + num56 * num56);
					num57 = 2f / num57;
					num55 *= num57;
					num56 *= num57;
					for (int num58 = 0; num58 < 20; num58++) {
						int num59 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 71, num55, num56, 200, default(Color), 2f);
						Main.dust[num59].noGravity = true;
						Main.dust[num59].velocity.X *= 2f;
					}
					for (int num60 = 0; num60 < 20; num60++) {
						int num61 = Dust.NewDust(NPC.oldPos[2], NPC.width, NPC.height, 71, 0f - num55, 0f - num56, 200, default(Color), 2f);
						Main.dust[num61].noGravity = true;
						Main.dust[num61].velocity.X *= 2f;
					}
					NPC.position -= NPC.netOffset;
				}
			}
			return dodgeResult;
		}

		public bool DoesDodgeFail() {
			if (Main.netMode != 1) {
				Dodge();
			}
			if (NPC.ai[2] != 0) {
				return true;
			}
			return false;
		}

		public bool Dodge() {
			if (!Main.player.IndexInRange(NPC.target)) {
				return false;
			}
			int targetX = (int)Main.player[NPC.target].position.X / 16;
			int targetY = (int)Main.player[NPC.target].position.Y / 16;
			int myX = (int)NPC.position.X / 16;
			int myY = (int)NPC.position.Y / 16;
			int maxOffset = 20;
			if (Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) + Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 2000f) {
				return false; // dont teleport if target is too far away?
			}
			int teleAttempts = 0;
			while (teleAttempts < 100) { // attempt teleport, 100 tries
				teleAttempts++;
				int teleSpotX = Main.rand.Next(targetX - maxOffset, targetX + maxOffset);
				for (int teleSpotY = Main.rand.Next(targetY - maxOffset, targetY + maxOffset); teleSpotY < targetY + maxOffset; teleSpotY++) {
					if ((teleSpotY < targetY - 4 || teleSpotY > targetY + 4 || teleSpotX < targetX - 4 || teleSpotX > targetX + 4) && (teleSpotY < myY - 1 || teleSpotY > myY + 1 || teleSpotX < myX - 1 || teleSpotX > myX + 1) && Main.tile[teleSpotX, teleSpotY].HasUnactuatedTile) {
						bool proceedWithTele = true;
						if (Main.tile[teleSpotX, teleSpotY - 1].LiquidType == 1) {
							proceedWithTele = false; // if the chosen position is lava, dont teleport there
						}
						if (proceedWithTele && Main.tileSolid[Main.tile[teleSpotX, teleSpotY].TileType] && !Collision.SolidTiles(teleSpotX - 1, teleSpotX + 1, teleSpotY - 4, teleSpotY - 1)) {
							NPC.position.X = teleSpotX * 16 - NPC.width / 2;
							NPC.position.Y = teleSpotY * 16 - NPC.height;
							NPC.netUpdate = true;
							return true;
						}
					}
				}
			}
			return false;
		}
	}
}

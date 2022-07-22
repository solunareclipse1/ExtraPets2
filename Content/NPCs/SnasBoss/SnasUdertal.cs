using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

using ExtraPets2.Content.Misc;
using ExtraPets2.Content.Buffs;
using ExtraPets2.Content.Utilities;
using ExtraPets2.Content.Items.Weapons;
using ExtraPets2.Content.Items.SnasBoss;
using ExtraPets2.Content.Projectiles.SnasBoss;
using ExtraPets2.Content.Items.Equippable.Armor;

namespace ExtraPets2.Content.NPCs.SnasBoss {
	[AutoloadBossHead]
	public class SnasUdertal : ModNPC {
        public override string Texture => ExtraPets2.AssetPath + "Textures/NPCs/SnasUdertal";

		// helpful stuff
		public int[] skeletonList = {
			-53,-52,-51,-50,-49,-48,-47,-46,-15,-14,-13,21,31,32,39,44,45,77,110,167,172,197,201,202,203,269,270,271,272,273,274,275,276,277,278,279,280,281,282,283,284,285,286,287,291,292,293,294,295,296,449,450,451,452,453,481,635
		};

		public bool IsEnraged() {
			return (double)(NPC.position.Y / 16f) < Main.worldSurface || !Main.player[NPC.target].ZoneDungeon;
		}

		public Vector2[] ShootAtPlayerCalc() {
			Player target = GetTargetedPlayer();
			Vector2 spawnPos = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float projSpeedX = target.position.X + target.width * 0.5f - spawnPos.X + (float)Main.rand.Next(-10, 11);
			float projSpeedY = target.position.Y + target.height * 0.5f - spawnPos.Y + (float)Main.rand.Next(-10, 11);
			float projSpeedVect = (float)Math.Sqrt(projSpeedX * projSpeedX + projSpeedY * projSpeedY);
			projSpeedVect = 10f / projSpeedVect;
			projSpeedX *= projSpeedVect;
			projSpeedY *= projSpeedVect;
			Vector2	spawnVel = new Vector2(projSpeedX, projSpeedY);
			Vector2[] result = {spawnPos, spawnVel};
			return result;
		}

		public Player GetTargetedPlayer() {
			if (!Main.player.IndexInRange(NPC.target)) {
				return null;
			} else {
				return Main.player[NPC.target];
			}
		}

		public bool ValidTargetExists() { // i feel like this may have issues with multiplayer?
			if (!GetTargetedPlayer().active || GetTargetedPlayer().dead || GetTargetedPlayer().ghost) {
				return false;
			}
			return true;
		}

		public bool CanDodge {
			get => NPC.ai[2] == 0;
			set => NPC.ai[2] = value ? 0 : 1;
		}

		public static int DifficultyLevel() {
			if (Main.getGoodWorld) {
				return 2;
			} else if (Main.expertMode) {
				return 1;
			} else {
				return 0;
			}
		}

		// defining properties / defaults
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("snas udertal");
			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BoneThrowingSkeleton];

			NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData {
				ImmuneToAllBuffsThatAreNotWhips = true,
				ImmuneToWhips = true
			};
			NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0) {
				Velocity = 1f
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
			NPCID.Sets.BossBestiaryPriority.Add(Type);
		}

		public override void SetDefaults() {
			NPC.CloneDefaults(NPCID.BoneThrowingSkeleton);
			NPC.aiStyle = -1;
			NPC.lifeMax = 1;
			NPC.defense = 999;
			NPC.damage = 1;
			NPC.knockBackResist = 0;
			NPC.boss = true;
			NPC.npcSlots = 10f;
			NPC.value = Item.buyPrice(9,9,9,9);
			NPC.BossBar = ModContent.GetInstance<SnasBossBar>();
			NPC.lavaImmune = true;

			AnimationType = NPCID.BoneThrowingSkeleton;

			if (!Main.dedServ) {
				Music = MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/SnasMusic");
			}
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
				new FlavorTextBestiaryInfoElement("A comedic skeleton from another world, corrupted into a being of pure chaos. They roam the Dungeon with an insatiable thirst for battle.")
			});
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot) {
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<SnasBag>()));

			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SnasTrophy>(), 10));

			npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<SnasRelic>()));

			npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<SnasBag>(), 4)); // second bag as placeholder, need pet

			LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
			
			notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SnasMask>(), 7));
			notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SnasJacket>(), 7));
			notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SnasSlipper>(), 7));
			notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GraveDanger>(), 666));

			npcLoot.Add(notExpertRule);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.damage = 1;
            NPC.lifeMax = 1;
        }

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) {
			return false;
		}

		// behavior
		public override void AI() {
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active) {
				NPC.TargetClosest();
			}

			if (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height == NPC.position.Y + (float)NPC.height) {
				NPC.directionY = -1;
			}

			bool shouldOpenDoor = false;
			bool flag6 = false;
			if (NPC.velocity.X == 0f) {
				flag6 = true;
			}

			if (NPC.justHit) {
				flag6 = false;
			}

			bool flag7 = false;
			if (NPC.velocity.Y == 0f && ((NPC.velocity.X > 0f && NPC.direction < 0) || (NPC.velocity.X < 0f && NPC.direction > 0))) {
				flag7 = true;
			}

			if (NPC.position.X == NPC.oldPosition.X || NPC.ai[3] >= 60f || flag7) {
				NPC.ai[3] += 1f;
			} else if ((double)Math.Abs(NPC.velocity.X) > 0.9 && NPC.ai[3] > 0f) {
				NPC.ai[3] -= 1f;
			}

			if (NPC.ai[3] > (float)(60 * 10)) {
				NPC.ai[3] = 0f;
			}

			if (NPC.justHit) {
				NPC.ai[3] = 0f;
			}

			if (NPC.ai[3] == (float)60) {
				NPC.netUpdate = true;
			}

			if (Main.player[NPC.target].Hitbox.Intersects(NPC.Hitbox)) {
				NPC.ai[3] = 0f;
			}

			if (NPC.ai[3] < 60f && NPC.DespawnEncouragement_AIStyle3_Fighters_NotDiscouraged(NPC.type, NPC.position, NPC)) {
				NPC.TargetClosest();
				if (NPC.directionY > 0 && Main.player[NPC.target].Center.Y <= NPC.Bottom.Y) {
					NPC.directionY = -1;
				}
			} else if (!NPC.DespawnEncouragement_AIStyle3_Fighters_CanBeBusyWithAction(NPC.type)) {
				if (NPC.velocity.X == 0f) {
					if (NPC.velocity.Y == 0f) {
						NPC.ai[0] += 1f;
						if (NPC.ai[0] >= 2f)
						{
							NPC.direction *= -1;
							NPC.spriteDirection = NPC.direction;
							NPC.ai[0] = 0f;
						}
					}
				} else {
					NPC.ai[0] = 0f;
				}
				if (NPC.direction == 0) {
					NPC.direction = 1;
				}
			}

			if (NPC.velocity.X < 0f - 8f || NPC.velocity.X > 8f) {
				if (NPC.velocity.Y == 0f) {
					NPC.velocity *= 0.7f;
				}
			} else if (NPC.velocity.X < 8f && NPC.direction == 1) {
				NPC.velocity.X += 0.2f;
				if (NPC.velocity.X > 8f) {
					NPC.velocity.X = 8f;
				}
			} else if (NPC.velocity.X > 0f - 8f && NPC.direction == -1) {
				NPC.velocity.X -= 0.2f;
				if (NPC.velocity.X < 0f - 8f) {
					NPC.velocity.X = 0f - 8f;
				}
			}

			if (NPC.velocity.Y == 0f) {
				int y2 = (int)(NPC.position.Y + (float)NPC.height + 7f) / 16;
				int y3 = (int)(NPC.position.Y - 9f) / 16;
				int npcTilePosX = (int)NPC.position.X / 16;
				int num167 = (int)(NPC.position.X + (float)NPC.width) / 16;
				int num168 = (int)(NPC.position.X + 8f) / 16;
				int num169 = (int)(NPC.position.X + (float)NPC.width - 8f) / 16;
				bool flag20 = false;
				for (int num170 = num168; num170 <= num169; num170++) {
					if (num170 >= npcTilePosX && num170 <= num167 && Main.tile[num170, y2] == null) {
						flag20 = true;
						continue;
					}

					if (Main.tile[num170, y3] != null && Main.tile[num170, y3].HasUnactuatedTile && Main.tileSolid[Main.tile[num170, y3].TileType]) {
						shouldOpenDoor = false;
						break;
					}

					if (!flag20 && num170 >= npcTilePosX && num170 <= num167 && Main.tile[num170, y2].HasUnactuatedTile && Main.tileSolid[Main.tile[num170, y2].TileType]) {
						shouldOpenDoor = true;
					}
				}

				if (!shouldOpenDoor && NPC.velocity.Y < 0f) {
					NPC.velocity.Y = 0f;
				}

				if (flag20) {
					return;
				}
			}
			
			if (NPC.velocity.Y >= 0f) {
				int movingDir = 0;
				if (NPC.velocity.X < 0f) {
					movingDir = -1;
				}

				if (NPC.velocity.X > 0f) {
					movingDir = 1;
				}

				Vector2 posPlusXVel = NPC.position;
				posPlusXVel.X += NPC.velocity.X;
				int num172 = (int)((posPlusXVel.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * movingDir)) / 16f);
				int num173 = (int)((posPlusXVel.Y + (float)NPC.height - 1f) / 16f);
				if (WorldGen.InWorld(num172, num173, 4)) {
					if ((float)(num172 * 16) < posPlusXVel.X + (float)NPC.width && (float)(num172 * 16 + 16) > posPlusXVel.X && ((Main.tile[num172, num173].HasUnactuatedTile && !Main.tile[num172, num173].TopSlope && !Main.tile[num172, num173 - 1].TopSlope && Main.tileSolid[Main.tile[num172, num173].TileType] && !Main.tileSolidTop[Main.tile[num172, num173].TileType]) || (Main.tile[num172, num173 - 1].IsHalfBlock && Main.tile[num172, num173 - 1].HasUnactuatedTile)) && (!Main.tile[num172, num173 - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 1].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 1].TileType] || (Main.tile[num172, num173 - 1].IsHalfBlock && (!Main.tile[num172, num173 - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 4].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 4].TileType]))) && (!Main.tile[num172, num173 - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 2].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 2].TileType]) && (!Main.tile[num172, num173 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 3].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 3].TileType]) && (!Main.tile[num172 - movingDir, num173 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172 - movingDir, num173 - 3].TileType])) {
						float num174 = num173 * 16;
						if (Main.tile[num172, num173].IsHalfBlock) {
							num174 += 8f;
						}

						if (Main.tile[num172, num173 - 1].IsHalfBlock) {
							num174 -= 8f;
						}

						if (num174 < posPlusXVel.Y + (float)NPC.height)
						{
							float num175 = posPlusXVel.Y + (float)NPC.height - num174;
							float num176 = 16.1f;
							if (num175 <= num176) {
								NPC.gfxOffY += NPC.position.Y + (float)NPC.height - num174;
								NPC.position.Y = num174 - (float)NPC.height;
								if (num175 < 9f) {
									NPC.stepSpeed = 1f;
								} else {
									NPC.stepSpeed = 2f;
								}
							}
						}
					}
				}
			}

			if (shouldOpenDoor) {
				int num177 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)(15 * NPC.direction)) / 16f);
				int num178 = (int)((NPC.position.Y + (float)NPC.height - 15f) / 16f);
				int spriteDir = NPC.spriteDirection;
				if ((NPC.velocity.X < 0f && spriteDir == -1) || (NPC.velocity.X > 0f && spriteDir == 1)) {
					if (NPC.height >= 32 && Main.tile[num177, num178 - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[num177, num178 - 2].TileType]) {
						if (Main.tile[num177, num178 - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[num177, num178 - 3].TileType]) {
							NPC.velocity.Y = -8f;
							NPC.netUpdate = true;
						} else {
							NPC.velocity.Y = -7f;
							NPC.netUpdate = true;
						}
					} else if (Main.tile[num177, num178 - 1].HasUnactuatedTile && Main.tileSolid[Main.tile[num177, num178 - 1].TileType]) {
						NPC.velocity.Y = -6f;
						NPC.netUpdate = true;
					} else if (NPC.position.Y + (float)NPC.height - (float)(num178 * 16) > 20f && Main.tile[num177, num178].HasUnactuatedTile && !Main.tile[num177, num178].TopSlope && Main.tileSolid[Main.tile[num177, num178].TileType]) {
						NPC.velocity.Y = -5f;
						NPC.netUpdate = true;
					} else if (NPC.directionY < 0 && (!Main.tile[num177, num178 + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num177, num178 + 1].TileType]) && (!Main.tile[num177 + NPC.direction, num178 + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num177 + NPC.direction, num178 + 1].TileType])) {
						NPC.velocity.Y = -8f;
						NPC.velocity.X *= 1.5f;
						NPC.netUpdate = true;
					}

					if (NPC.velocity.Y == 0f && flag6 && NPC.ai[3] == 1f) {
						NPC.velocity.Y = -5f;
					}

					if (NPC.velocity.Y == 0f && Main.expertMode && Main.player[NPC.target].Bottom.Y < NPC.Top.Y && Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < (float)(Main.player[NPC.target].width * 3) && Collision.CanHit(NPC, Main.player[NPC.target])) {
						if (NPC.velocity.Y == 0f) {
							if (Main.player[NPC.target].Bottom.Y > NPC.Top.Y - (float)(6 * 16)) {
								NPC.velocity.Y = -7.9f;
							} else {
								int x2 = (int)(NPC.Center.X / 16f);
								int num184 = (int)(NPC.Bottom.Y / 16f) - 1;
								for (int num185 = num184; num185 > num184 - 6; num185--) {
									if (Main.tile[x2, num185].HasUnactuatedTile && TileID.Sets.Platforms[Main.tile[x2, num185].TileType]) {
										NPC.velocity.Y = -7.9f;
										break;
									}
								}
							}
						}
					}
				}

				if (NPC.velocity.Y == 0f && Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))) < 150f && Math.Abs(NPC.position.Y + (float)(NPC.height / 2) - (Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2))) < 50f && ((NPC.direction > 0 && NPC.velocity.X >= 1f) || (NPC.direction < 0 && NPC.velocity.X <= -1f))) {
					NPC.velocity.X = 12 * NPC.direction;
					NPC.velocity.Y = -4f;
					NPC.netUpdate = true;
				}

				if (NPC.velocity.Y < 0f) {
					NPC.velocity.X *= 2f;
					NPC.velocity.Y *= 1.8f;
				}
			}

			int dodgeChance = NPC.defense * (DifficultyLevel() + 1);
			if (IsEnraged()) {
				dodgeChance = 999999999;
			}
			if (Main.netMode != 1 && Main.rand.NextBool(dodgeChance)) {
				CanDodge = false;
				NPC.netUpdate = true;
			} else if (Main.netMode != 1 && !CanDodge) {
				CanDodge = true;
				NPC.netUpdate = true;
			}

			bool targetGone = Main.player[NPC.target].dead || !Main.player[NPC.target].active || Main.player[NPC.target].statLife <= 0;

			// try attacking
			if (IsEnraged()) {
				EnragedAttacks();
			}
			if (DifficultyLevel() > 1) {
				WorthyAttacks();
			}
			if (DifficultyLevel() > 0) {
				ExpertAttacks();
			}
			NormalAttacks();

			// eueueueueueueueueueuueueu
			if (Main.rand.NextBool(5)) {
				SoundEngine.PlaySound(EPSoundStyles.SnasSpeak with {MaxInstances = 10}, NPC.position);
			} else if (Main.rand.NextBool(10000)) {
				SoundEngine.PlaySound(EPSoundStyles.SnasSpeakRare with {Volume = 6}, NPC.position);
			}

			// despawn on players dead
			if (!ValidTargetExists()) {
				NPC.noTileCollide = true;
				NPC.EncourageDespawn(10);
			}
		}

		//// dodge logic
		// custom hit detection // TODO: fix with zenith
		public override bool? CanBeHitByItem(Player player, Item item) {
			if (HitboxHelperItem.meleeHitboxes[player.whoAmI].HasValue) {
				Rectangle hitbox = HitboxHelperItem.meleeHitboxes[player.whoAmI].Value;
				if (hitbox.Intersects(NPC.getRect())) {
					if (Dodge() > 0) {
						DodgeParticles();
						return false;
					}
				}
			}
			if (CanDodge) { // failsafe for things that do hit detection in a non standard way
				return false; // stuff like zenith, last prism, charged blaster cannon laser
			} else return null; // 
		}

		public override bool? CanBeHitByProjectile(Projectile projectile) {
			if (projectile.getRect().Intersects(NPC.getRect())) {
				if (Dodge() > 0) {
					DodgeParticles();
					return false;
				}
			}
			if (CanDodge) {
				return false;
			} else return null;
		}

		// old
		//public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit) {
		//	if (Dodge() > 0) {
		//		damage = 0;
		//		knockback = 0;
		//		crit = false;
		//	} else {
		//		damage = 9999999;
		//	}
		//}

		//public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref float knockback, ref bool crit, ref int hitDir) {
		//	if (Dodge() > 0) {
		//		damage = 0;
		//		knockback = 0;
		//		crit = false;
		//	} else {
		//		damage = 9999999;
		//	}
		//}

		// particles
		public void DodgeParticles() {
			NPC.velocity *= 0f;
			NPC.ai[3] = 0f;
			NPC.position += NPC.netOffset;
			SoundEngine.PlaySound(EPSoundStyles.Sus4, NPC.position);
			Vector2 npcCenter = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float dustSpeedX = NPC.oldPos[2].X + (float)NPC.width * 0.5f - npcCenter.X;
			float dustSpeedY = NPC.oldPos[2].Y + (float)NPC.height * 0.5f - npcCenter.Y;
			float dustSpeedVector = (float)Math.Sqrt(dustSpeedX * dustSpeedX + dustSpeedY * dustSpeedY);
			dustSpeedVector = 2f / dustSpeedVector;
			dustSpeedX *= dustSpeedVector;
			dustSpeedY *= dustSpeedVector;
			for (int num58 = 0; num58 < 20; num58++) {
				int teleDustTo = Dust.NewDust(NPC.position, NPC.width, NPC.height, 71, dustSpeedX, dustSpeedY, 200, default(Color), 2f);
				Main.dust[teleDustTo].noGravity = true;
				Main.dust[teleDustTo].velocity.X *= 2f;
			}

			for (int num60 = 0; num60 < 20; num60++) {
				int teleDustFrom = Dust.NewDust(NPC.oldPos[2], NPC.width, NPC.height, 71, 0f - dustSpeedX, 0f - dustSpeedY, 200, default(Color), 2f);
				Main.dust[teleDustFrom].noGravity = true;
				Main.dust[teleDustFrom].velocity.X *= 2f;
			}
			NPC.position -= NPC.netOffset;
		}

		// actual dodge code
		public int Dodge() { // 0: failed, 1: chaos elemental dodge, 2: teleport to player dodge
			int dodgeState = 1;
			if (Main.netMode != 1 && CanDodge) {
				if (!DodgeTele()) {
					if (DifficultyLevel() > 0) {
						NPC.ai[3] = 0f;
						int offset = 200;
						if (DifficultyLevel() > 1) {
							offset = 100;
						}
						int teleX = Main.rand.Next((int)Main.player[NPC.target].position.X - offset, (int)Main.player[NPC.target].position.X + offset);
						int teleY = Main.rand.Next((int)Main.player[NPC.target].position.Y - offset, (int)Main.player[NPC.target].position.Y + offset);
						NPC.position = new Vector2(teleX, teleY);
						NPC.netUpdate = true;
						dodgeState = 2;
					} else {
						dodgeState = 1;
					}
				} else {
					dodgeState = 1;
				}
			}
			if (!CanDodge) {
				dodgeState = 0;
			}
			return dodgeState;
		}

		public bool DodgeTele() {
			if (!Main.player.IndexInRange(NPC.target)) {
				return false;
			}
			int targetX = (int)Main.player[NPC.target].position.X / 16;
			int targetY = (int)Main.player[NPC.target].position.Y / 16;
			int myX = (int)NPC.position.X / 16;
			int myY = (int)NPC.position.Y / 16;
			int maxOffset = 20;
			if (Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) + Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 2000f) {
				return false;
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

		//// attacks
		public void NormalAttacks() {
			if (GetTargetedPlayer() == null || Main.netMode == 1) {
				return;
			}
			if (Main.rand.NextBool(250)) {
				int spawnPosX = Main.rand.Next((int)GetTargetedPlayer().position.X - 200, (int)GetTargetedPlayer().position.X + 200);
				int spawnPosY = Main.rand.Next((int)GetTargetedPlayer().position.Y - 200, (int)GetTargetedPlayer().position.Y + 200);
				NPC.NewNPC(NPC.GetSource_FromAI(), spawnPosX, spawnPosY, ModContent.NPCType<SnasSkull>());
				//Main.NewText("Skull");
			}
			if (Main.rand.NextBool(500)) {
				Vector2[] result = ShootAtPlayerCalc();
				Projectile.NewProjectile(NPC.GetSource_FromAI(), result[0], result[1], ModContent.ProjectileType<SnasBone>(), 1, 0);
				//Main.NewText("Bone");
			}
			if (Main.rand.NextBool(500)) {
				Vector2[] result = ShootAtPlayerCalc();
				Projectile.NewProjectile(NPC.GetSource_FromAI(), result[0], result[1], ModContent.ProjectileType<SnasTombstone>(), 1, 0, ai1: NPC.target);
				//Main.NewText("One Grave");
			}
			if (Main.rand.NextBool(250)) {
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, skeletonList[Main.rand.Next(0,skeletonList.Length-1)]);
				//Main.NewText("Single Skeleton");
			}
		}

		public void ExpertAttacks() {
			if (GetTargetedPlayer() == null || Main.netMode == 1) {
				return;
			}
			if (Main.rand.NextBool(600)) {
				Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position, Vector2.Zero, ModContent.ProjectileType<SnasOrb>(), 1, 0, ai1: NPC.target);
				if (!DodgeTele()) {
					int offset = 200;
					int teleX = Main.rand.Next((int)Main.player[NPC.target].position.X - offset, (int)Main.player[NPC.target].position.X + offset);
					int teleY = Main.rand.Next((int)Main.player[NPC.target].position.Y - offset, (int)Main.player[NPC.target].position.Y + offset);
					NPC.position = new Vector2(teleX, teleY);
					NPC.netUpdate = true;
				}
				//Main.NewText("Teleport");
			}
			if (Main.rand.NextBool(550)) {
				int amount = Main.rand.Next(1,15);
				for (int i = 0; i < amount; i++) {
					Vector2 randVect = new Vector2(Main.rand.NextFloat(-20,20),Main.rand.NextFloat(-20,20));
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position, randVect, ModContent.ProjectileType<SnasTombstone>(), 1, 0, ai1: NPC.target);
				}
				//Main.NewText("Many Graves");
			}
			if (Main.rand.NextBool(800)) {
				int amount = Main.rand.Next(1,30);
				for (int i = 0; i < amount; i++) {
					Vector2 randVect = new Vector2(Main.rand.NextFloat(-20,20),Main.rand.NextFloat(-20,20));
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position, randVect, ModContent.ProjectileType<SnasBone>(), 1, 0);
				}
				//Main.NewText("Many Bones");
			}
			if (Main.rand.NextBool(1000)) {
				int amount = Main.rand.Next(1,10);
				for (int i = 0; i < amount; i++) {
					int spawnPosX = Main.rand.Next((int)GetTargetedPlayer().position.X - 200, (int)GetTargetedPlayer().position.X + 200);
					int spawnPosY = Main.rand.Next((int)GetTargetedPlayer().position.Y - 200, (int)GetTargetedPlayer().position.Y + 200);
					NPC.NewNPC(NPC.GetSource_FromAI(), spawnPosX, spawnPosY, ModContent.NPCType<SnasSkull>());
				}
				//Main.NewText("Many skulls");
			}
			if (Main.rand.NextBool(1000)) {
				int amount = Main.rand.Next(1,6);
				for (int i = 0; i < amount; i++) {
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, skeletonList[Main.rand.Next(0,skeletonList.Length-1)]);
				}
				//Main.NewText("Many Skeletons");
			}
		}

		public void WorthyAttacks() {
			if (GetTargetedPlayer() == null || Main.netMode == 1) {
				return;
			}
			// PLACEHOLDER! TODO: weak clone
			if (Main.rand.NextBool(650)) {
				GetTargetedPlayer().AddBuff(ModContent.BuffType<SnasControl>(), 600);
				//Main.NewText("Gravity");
			}
		}

		public void EnragedAttacks() {
			NormalAttacks();
			ExpertAttacks();
			WorthyAttacks();
			if (Main.rand.NextBool(250) && Main.netMode != 1) {
				//NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<SnasUdertal>());
				//Main.NewText("SUPER Clone");
			}
			if (Main.rand.NextBool(250) && Main.netMode != 1) {
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DungeonGuardian);
				//Main.NewText("DungeonGuard");
			}
			if (Main.rand.NextBool(250) && Main.netMode != 1) {
				int spawnPosX = (int) GetTargetedPlayer().position.X + Main.rand.Next(-200,200);
				int spawnPosY = (int) GetTargetedPlayer().position.Y + Main.rand.Next(-200,200);
				for (int i = 0; i < 25; i++) {
					NPC.NewNPC(NPC.GetSource_FromAI(), spawnPosX, spawnPosY, ModContent.NPCType<SnasSkull>());
				}
				//Main.NewText("SkullSpam");
			}
			if (Main.netMode != 1 && ValidTargetExists()) {
				if (!DodgeTele()) {
					int offset = 200;
					int teleX = Main.rand.Next((int)Main.player[NPC.target].position.X - offset, (int)Main.player[NPC.target].position.X + offset);
					int teleY = Main.rand.Next((int)Main.player[NPC.target].position.Y - offset, (int)Main.player[NPC.target].position.Y + offset);
					NPC.position = new Vector2(teleX, teleY);
					NPC.netUpdate = true;
				}
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit) {
			target.immune = false;
			target.immuneTime = 0;
			if (target.HasBuff(BuffID.Venom)) {
				int idx = target.FindBuffIndex(BuffID.Venom);
				target.buffTime[idx] += 20;
			} else {
				target.AddBuff(BuffID.Venom, 60);
			}
		}
	}
}

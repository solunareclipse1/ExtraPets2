using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.DataStructures;

using ExtraPets2.Content.NPCs.SnasBoss;

namespace ExtraPets2.Content.Projectiles.SnasBoss {
	public class SnasTombstone : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/SkullTombstone";

		private int[] gravesList = {
			43,201,202,203,204,205,527,528,529,530,531
		}; // list of gravestone projectile ids

		private int[] skeletonList = {
			-53,-52,-51,-50,-49,-48,-47,-46,-15,-14,-13,21,31,32,39,44,45,77,110,167,172,197,201,202,203,269,270,271,272,273,274,275,276,277,278,279,280,281,282,283,284,285,286,287,291,292,293,294,295,296,449,450,451,452,453,481,635
		}; // big list of skeletons, on 1 line for file readability
		   // could probably do these better to save on filesize/not having 1 really long line, but eh

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("sens gvraeenstoe");

			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.penetrate = -1;
			Projectile.width = 24;
			Projectile.height = 24;
			Projectile.knockBack = 100;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.aiStyle = -1;
		}

		public override void Kill(int timeLeft) {
			if (Main.netMode == 1 || (!Main.getGoodWorld && Main.player[(int)Projectile.ai[1]].ZoneDungeon)) {
				return;
			}
			int projPosX = (int)Projectile.position.X;
			int projPosY = (int)Projectile.position.Y;

			int spawnProjId = -69;
			int spawnNpcId = -69;
			int spawnTimes = 1;
			int spawnDamage = 1;
			float spawnKnockback = 0f;
			int spawnX = projPosX;
			int spawnY = projPosY;
			Vector2 spawnVel = Vector2.Zero;
			IEntitySource spawnSource = Projectile.GetSource_FromThis();
			
			bool randVel = false;
			int randAmt = 0;
			bool fuzzPos = false;
			int fuzzAmount = 0;
			bool passTarget = false;

			switch (Main.rand.Next(0,10)) {
				case 0: {
					spawnNpcId = ModContent.NPCType<SnasSkull>();
					spawnTimes = Main.rand.Next(2,5);
					fuzzPos = true;
					fuzzAmount = 100;
					break;
				}
				case 1: {
					spawnProjId = ModContent.ProjectileType<SnasHead>();
					spawnTimes = Main.rand.Next(3,6);
					randVel = true;
					randAmt = 1;
					break;
				}
				case 2: {
					spawnProjId = ModContent.ProjectileType<SnasBone>();
					spawnTimes = Main.rand.Next(10,25);
					randVel = true;
					randAmt = 20;
					break;
				}
				case 3: {
					spawnProjId = ModContent.ProjectileType<SnasOrb>();
					passTarget = true;
					break;
				}
				case 4: {
					spawnProjId = ProjectileID.Tombstone;
					spawnTimes = Main.rand.Next(1,25);
					spawnDamage = 0;
					randVel = true;
					randAmt = 20;
					break;
				}
				case 5: {
					spawnProjId = Type;
					spawnTimes = Main.rand.Next(1,3);
					randVel = true;
					randAmt = 20;
					break;
				}
				case 6: {
					spawnNpcId = NPCID.Skeleton;
					spawnTimes = Main.rand.Next(1,10);
					fuzzPos = true;
					fuzzAmount = 10;
					break;
				}
				case 7: {
					SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
					return;
				}
				case 8: {
					SoundEngine.PlaySound(SoundID.Roar, Projectile.position);
					return;
				}
				case 9: {
					SoundEngine.PlaySound(EPSoundStyles.SnasSpeak, Projectile.position);
					return;
				}
				case 10: {
					if (Main.rand.NextBool(1000)) {
						spawnNpcId = NPCID.DungeonGuardian;
						break;
					} else {
						return;
					}
				}
			}


			bool randomizeSkeleton = false;
			bool randomizeGrave = false;
			if (spawnNpcId == NPCID.Skeleton) {
				randomizeSkeleton = true;
			}
			if (spawnProjId == ProjectileID.Tombstone) {
				randomizeGrave = true;
			}
			for (int i = 0; i < spawnTimes; i++) {
				if (randomizeSkeleton) {
					spawnNpcId = skeletonList[Main.rand.Next(0, skeletonList.Length)];
				}
				if (randomizeGrave) {
					spawnProjId = gravesList[Main.rand.Next(0, gravesList.Length)];
				}
				if (randVel) {
					spawnVel = new Vector2(Main.rand.NextFloat(-randAmt,randAmt),Main.rand.NextFloat(-randAmt,randAmt));
				}
				if (fuzzPos) {
					spawnX = Main.rand.Next(spawnX - fuzzAmount, spawnX + fuzzAmount);
					spawnY = Main.rand.Next(spawnY - fuzzAmount, spawnY + fuzzAmount);
				}
				int projID;
				int npcID;
				if (spawnProjId != -69 && !passTarget) {
					projID = Projectile.NewProjectile(spawnSource, new Vector2(spawnX, spawnY), spawnVel, spawnProjId, spawnDamage, spawnKnockback);
				} else if (spawnProjId != -69) {
					projID = Projectile.NewProjectile(spawnSource, new Vector2(spawnX, spawnY), spawnVel, spawnProjId, spawnDamage, spawnKnockback, ai1: Projectile.ai[1]);
				}
				if (spawnNpcId != -69) {
					npcID = NPC.NewNPC(spawnSource, spawnX, spawnY, spawnNpcId);
					if (Main.npc[npcID].type == NPCID.DungeonGuardian) Main.npc[npcID].life = 100;
				}
			}
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

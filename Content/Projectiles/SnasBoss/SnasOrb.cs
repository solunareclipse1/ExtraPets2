using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraPets2.Content.Projectiles.SnasBoss {
	public class SnasOrb : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/SnasOrb";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("senis aey");

			Main.projFrames[Projectile.type] = 4;
		}
		
		public override void SetDefaults() {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
		}

        public override void AI() {
            if (Projectile.alpha > 0 && Projectile.ai[0] <= 200) {
                Projectile.alpha -= 10;
            } else if (Projectile.alpha < 0) {
                Projectile.alpha = 0;
            }

            if (++Projectile.frameCounter >= 2) {
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= Main.projFrames[Projectile.type])
					Projectile.frame = 0;
			}

            if (!Main.player.IndexInRange((int)Projectile.ai[1])) {
                Projectile.Kill();
            }
            if (Projectile.ai[0] <= 200) {
                Projectile.ai[0]++;

                if (Projectile.ai[0] >= 100 && (Projectile.ai[0] == 120 || Projectile.ai[0] == 125 || Projectile.ai[0] == 130 || Projectile.ai[0] == 150 || Projectile.ai[0] == 155 || Projectile.ai[0] == 160 || Projectile.ai[0] == 180 || Projectile.ai[0] == 185 || Projectile.ai[0] == 190)) {
                    Player target = Main.player[(int)Projectile.ai[1]];
					Vector2 myCenter = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
					float projSpeedX = target.position.X + target.width * 0.5f - myCenter.X + (float)Main.rand.Next(-10, 11);
					float projSpeedY = target.position.Y + target.height * 0.5f - myCenter.Y + (float)Main.rand.Next(-10, 11);
					float num113 = (float)Math.Sqrt(projSpeedX * projSpeedX + projSpeedY * projSpeedY);
					num113 = 10f / num113;
					projSpeedX *= num113;
					projSpeedY *= num113;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), myCenter.X, myCenter.Y, projSpeedX, projSpeedY, ModContent.ProjectileType<SnasBone>(), 1, 0f);
                }
            } else {
                Projectile.alpha += 10;
                if (Projectile.alpha >= 255) {
                    for (int i = 0; i < 10 && (Main.getGoodWorld || !Main.player[(int)Projectile.ai[1]].ZoneDungeon); i++) {
                        Vector2 randVect = new Vector2(Main.rand.NextFloat(-25,25),Main.rand.NextFloat(-25,25));
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, randVect, ModContent.ProjectileType<SnasBone>(), 1, 0f);
                    }
                    Projectile.Kill();
                }
            }
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

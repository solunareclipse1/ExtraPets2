using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ExtraPets2.Content.Buffs;

namespace ExtraPets2.Content.Projectiles {
	public class MogusProjectile : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/Mogus";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Incredulous Astronaut");

			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Bunny);
			AIType = ProjectileID.Bunny;
			Projectile.scale = 2;
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.minion = true;
			Projectile.minionSlots = 0;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
			DrawOriginOffsetX = 1;
			DrawOriginOffsetY = 10;
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner];

			return true;
		}

		public override void AI() {
			Player player = Main.player[Projectile.owner];
			
			if (!player.dead && player.HasBuff(ModContent.BuffType<MogusBuff>())) {
				Projectile.timeLeft = 2;
			}

			Projectile.friendly = true;
			Projectile.damage = 1;
		}

		public override bool MinionContactDamage() {
			return true;
		}

		public override void OnHitNPC (NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(ModContent.BuffType<Suspicious>(), Main.rand.Next(1,360));
		}

		public override void OnHitPlayer (Player target, int damage, bool crit) {
			target.AddBuff(ModContent.BuffType<Suspicious>(), Main.rand.Next(1,360));
		}

		public override void OnHitPvp (Player target, int damage, bool crit) {
			target.AddBuff(ModContent.BuffType<Suspicious>(), Main.rand.Next(1,360));
		}
	}
}

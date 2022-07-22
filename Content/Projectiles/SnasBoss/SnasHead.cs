using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraPets2.Content.Projectiles.SnasBoss {
	public class SnasHead : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/SnasHead";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("senis slkul");

			Main.projFrames[Projectile.type] = 1;
		}
		
		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Skull);
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
			Projectile.damage = 1;
			Projectile.knockBack = 0;

			AIType = ProjectileID.Skull;
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

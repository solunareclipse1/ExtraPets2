using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraPets2.Content.Projectiles {
	public class RockProjectile : ModProjectile {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/Rock";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("a rock");

			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
			Projectile.DamageType = DamageClass.Generic;
			Projectile.penetrate = -1;
			Projectile.width = 240;
			Projectile.height = 240;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 1;

			AIType = ProjectileID.BoulderStaffOfEarth;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			CombatText.NewText(target.getRect(), Colors.CoinSilver, "rock", true);
		}

		public override void OnHitPlayer (Player target, int damage, bool crit) {
			target.AddBuff(BuffID.Stoned, 600);
			target.AddBuff(BuffID.Ironskin, 600);
			target.AddBuff(BuffID.ObsidianSkin, 600);
			target.AddBuff(BuffID.Endurance, 600);
			CombatText.NewText(target.getRect(), Colors.CoinSilver, "rock", true);
		}

		public override void OnHitPvp (Player target, int damage, bool crit) {
			target.AddBuff(BuffID.Stoned, 600);
			target.AddBuff(BuffID.Ironskin, 600);
			target.AddBuff(BuffID.ObsidianSkin, 600);
			target.AddBuff(BuffID.Endurance, 600);
			CombatText.NewText(target.getRect(), Colors.CoinSilver, "rock", true);
		}
	}
}

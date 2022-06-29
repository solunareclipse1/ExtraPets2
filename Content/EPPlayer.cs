using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

using ExtraPets2.Content.Buffs;
using ExtraPets2.Content.Projectiles;

namespace ExtraPets2.Content {
    public class EPPlayer : ModPlayer {
        public bool sunderingDebuff = false;

        string[] sunderDeathReasons = {
            " is divided amongst themself.",
            " was reduced to atoms.",
            " turned to dust.",
            " does not vibe with this universe.",
            " had their indivisible particles smashed.",
            " was decimated.",
            " is gone.",
            " doesn't feel so good.",
            " fell apart."
        };

        public override void ResetEffects() {
            sunderingDebuff = false;
        }

        public override void UpdateBadLifeRegen() {
            if (sunderingDebuff) {
                if (Player.lifeRegen > 0) {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= (int) ((Player.statLifeMax2*2)/1.6);
                Player.immune = false;
                Player.immuneTime = 0;
                Player.moveSpeed = 0.1f;
                Player.noFallDmg = false;
                Player.wingTime = 0;
                Player.wingTimeMax = 0;
                Dust dust = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.FoodPiece, default, default, default, Colors.RarityNormal, 0.75f);
                dust.shader = GameShaders.Armor.GetSecondaryShader(97, Main.LocalPlayer);
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
            if (damage == 10 && hitDirection == 0 && damageSource.SourceOtherIndex == 8 && sunderingDebuff) {
                damageSource = PlayerDeathReason.ByCustomReason(Player.name + sunderDeathReasons[Main.rand.Next(0, 8)]);
                playSound = false;
            }
            //Main.NewText($"damage {damage}");
            //Main.NewText($"hitDirection {hitDirection}");
            //Main.NewText($"pvp {pvp}");
            //Main.NewText($"playSound {playSound}");
            //Main.NewText($"genGore {genGore}");
            //Main.NewText($"damageSource {damageSource.SourceOtherIndex}");
            return true;
        }

        //hacky fix for overseer bullet not applying debuff on pvp hits
        //public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit) {
        //    if (proj.type == ModContent.ProjectileType<OverseerBullet>()) {
        //        Player.AddBuff(ModContent.BuffType<SunderingDebuff>(), 960);
        //    }
        //}
    }
}
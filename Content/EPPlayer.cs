using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

using ExtraPets2.Content.Buffs;
using ExtraPets2.Content.Items.Weapons;


namespace ExtraPets2.Content {
    public class EPPlayer : ModPlayer {
        public int sunderingDebuff;
        public bool equippedOpus = false;
        public int philoTextCooldown = 0;

        string[] philoTexts = {
            "what is it you think you are doing..?",
            "you do realize that will not work, correct..?",
            "cease that at once, it is detrimental to both of us...",
            "did i not already inform you that you cannot use 2 stones at once..?",
            "have you lost your senses..? that would be most unfortunate...",
            "exactly what did you mean to accomplish with that..?",
            "that was not a good idea, you should refrain from doing that again..."
        };

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

        string[] EMCDeathReasons = {
            " became EMC.",
            " performed human transmutation."
        };

        public override void ResetEffects() {
            equippedOpus = false;
        }

        public override void PostUpdate() {
            if (philoTextCooldown > 0) {
                philoTextCooldown--;
            }
        }

        public override void UpdateBadLifeRegen() {
            if (sunderingDebuff > 0) {
                if (Player.FindBuffIndex(ModContent.BuffType<SunderingDebuff>()) < 0) {
                    sunderingDebuff = 0;
                    return;
                }
                if (Player.lifeRegen > 0) {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= (int) ((Player.statLifeMax2 * (sunderingDebuff * 2)) / 16);
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
            if (hitDirection == 0 && damageSource.SourceOtherIndex == 8 && (Player.FindBuffIndex(ModContent.BuffType<SunderingDebuff>()) < 0 || sunderingDebuff > 0)) {
                damageSource = PlayerDeathReason.ByCustomReason(Player.name + sunderDeathReasons[Main.rand.Next(0, 8)]);
                playSound = false;
            }
            return true;
        }

        public override bool CanUseItem(Item item) {
            if (item.type == ModContent.ItemType<MagnumOpus>() && equippedOpus) {
                Player.Hurt(PlayerDeathReason.ByCustomReason(Player.name + sunderDeathReasons[Main.rand.Next(0, 1)]), Player.statLifeMax2 - Player.statManaMax2, 0);
                if (philoTextCooldown == 0) {
                    philoTextCooldown = 120;
                    CombatText.NewText(Player.getRect(), new Color(181, 47, 109), philoTexts[Main.rand.Next(0,6)], true);
                }
                return false;
            }
            return true;
        }
    }
}
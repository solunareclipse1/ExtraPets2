using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;


namespace ExtraPets2.Content.Buffs {
    public class SnasControl : ModBuff {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Buffs/SnasControl";

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("ctnrodled");
            Description.SetDefault("gvraity mksae noe snese");

			Main.debuff[Type] = true;
        }

        // Player
        public override bool ReApply(Player player, int time, int buffIndex) {
            EPPlayer mplr = player.GetModPlayer<EPPlayer>();
            if (mplr.forcedGravDir == 0) {
                mplr.forcedGravDir = -1;
            } else {
                mplr.forcedGravDir *= -1;
            }
            player.buffTime[buffIndex] = time;
            return true;
        }

        public override void Update(Player player, ref int buffIndex) {
            EPPlayer mplr = player.GetModPlayer<EPPlayer>();
            mplr.controlled = true;
            player.gravControl2 = true;
            if (mplr.forcedGravDir == 0) {
                mplr.forcedGravDir = -1;
            }
        }
    }
}
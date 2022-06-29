using Terraria;
using Terraria.ModLoader;


namespace ExtraPets2.Content.Buffs {
    public class SunderingDebuff : ModBuff {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Buffs/SunderingDebuff";

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Sundering");
            Description.SetDefault("Back to dust...");

			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<EPPlayer>().sunderingDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex) {
            npc.GetGlobalNPC<EPNPC>().sunderingDebuff = true;
        }
    }
}
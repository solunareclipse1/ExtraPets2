using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;


namespace ExtraPets2.Content.Buffs {
    public class SunderingDebuff : ModBuff {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Buffs/SunderingDebuff";

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Sundering");
            Description.SetDefault("Back to dust...");

			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
            BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
        }

        // NPC
        public override bool ReApply(NPC npc, int time, int buffIndex) {
            EPNPC gnpc = npc.GetGlobalNPC<EPNPC>();
            if (gnpc.sunderingDebuff < 8) {
                gnpc.sunderingDebuff++;
                npc.netUpdate = true;
            }
            npc.buffTime[buffIndex] = time;
            return true;
        }

        public override void Update(NPC npc, ref int buffIndex) {
            EPNPC gnpc = npc.GetGlobalNPC<EPNPC>();
            if (gnpc.sunderingDebuff <= 0) {
                gnpc.sunderingDebuff = 1;
                npc.netUpdate = true;
            }
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.FoodPiece, default, default, default, Colors.RarityNormal, 0.25f * (gnpc.sunderingDebuff / 2));
            dust.shader = GameShaders.Armor.GetSecondaryShader(97, Main.LocalPlayer);
        }

        // Player
        public override bool ReApply(Player player, int time, int buffIndex) {
            EPPlayer mplr = player.GetModPlayer<EPPlayer>();
            if (mplr.sunderingDebuff < 16) {
                mplr.sunderingDebuff++;
            }
            player.buffTime[buffIndex] = time;
            return true;
        }

        public override void Update(Player player, ref int buffIndex) {
            EPPlayer mplr = player.GetModPlayer<EPPlayer>();
            if (mplr.sunderingDebuff <= 0) {
                mplr.sunderingDebuff = 1;
            }
            Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.FoodPiece, default, default, default, Colors.RarityNormal, (mplr.sunderingDebuff / 8));
            dust.shader = GameShaders.Armor.GetSecondaryShader(97, Main.LocalPlayer);
        }
    }
}
using Terraria;
using Terraria.ModLoader;


using ExtraPets2.Content.Buffs;

namespace ExtraPets2.Content {
    public class EPNPC : GlobalNPC {
        public int sunderingDebuff;

        public override bool InstancePerEntity {
            get {
                return true;
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage) {
            if (sunderingDebuff > 0) {
                if (npc.FindBuffIndex(ModContent.BuffType<SunderingDebuff>()) < 0) {
                    sunderingDebuff = 0;
                    return;
                }
                if (npc.lifeRegen > 0) {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= (int) ((npc.lifeMax * (sunderingDebuff * 2)) / 16);
                damage = (int) ((npc.lifeMax * (sunderingDebuff * 2)) / 480);
            }
        }
    }
}
using Terraria;
using Terraria.ModLoader;

using ExtraPets2.Content.Buffs;
using ExtraPets2.Content.NPCs.SnasBoss;

namespace ExtraPets2.Content {
    public class EPNPC : GlobalNPC {
        public int sunderingDebuff;

        public override bool InstancePerEntity {
            get {
                return true;
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage) {
            if (npc.type == ModContent.NPCType<SnasUdertal>()) {
                if (!Main.player[npc.target].ZoneDungeon) {
                    npc.lifeRegen = 9999 * 2;
                    damage = 100;
                } else if (Main.getGoodWorld) {
                    npc.lifeRegen = 1;
                    damage = 1;
                }
            } else if (sunderingDebuff > 0) {
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
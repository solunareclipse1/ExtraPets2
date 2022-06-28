using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace ExtraPets2.Content {
    public class EPNPC : GlobalNPC {
        public bool sunderingDebuff = false;

        public override bool InstancePerEntity {
            get {
                return true;
            }
        }

        public override void ResetEffects(NPC npc) {
            sunderingDebuff = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage) {
            if (sunderingDebuff) {
                if (npc.lifeRegen > 0) {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= (int) ((npc.lifeMax*2)/1.6);
                damage = (int) (npc.lifeMax/50);
                Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.FoodPiece, default, default, default, Colors.RarityNormal, 0.75f);
                dust.shader = GameShaders.Armor.GetSecondaryShader(97, Main.LocalPlayer);
            }
        }
    }
}
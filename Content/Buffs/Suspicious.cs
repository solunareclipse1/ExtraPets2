using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ExtraPets2.Content.Buffs {
    public class Suspicious : ModBuff {

        SoundStyle[] soundList = new SoundStyle[] {
            EP2SoundStyles.EP2Sus0,
            EP2SoundStyles.EP2Sus1,
            EP2SoundStyles.EP2Sus2,
            EP2SoundStyles.EP2Sus3,
            EP2SoundStyles.EP2Sus4,
            EP2SoundStyles.EP2Sus5,
            EP2SoundStyles.EP2Sus6,
            EP2SoundStyles.EP2Sus7,
            EP2SoundStyles.EP2Sus8,
            EP2SoundStyles.EP2Sus9
        };

        public override string Texture => ExtraPets2.AssetPath + "Textures/Buffs/Suspicious";

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Suspicious");
            Description.SetDefault("You feel rather untrustworthy");

			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            SoundStyle chosenSound = Main.rand.Next(soundList);
            if (Main.rand.Next(0, 9001) == 1337) {
                SoundEngine.PlaySound(chosenSound);
            }
        }

        public override void Update(NPC npc, ref int buffIndex) {
            SoundStyle chosenSound = Main.rand.Next(soundList);
            if (Main.rand.Next(0, 9001) == 1337) {
                SoundEngine.PlaySound(chosenSound);
            }
        }
    }
}
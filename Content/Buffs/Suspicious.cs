using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;


namespace ExtraPets2.Content.Buffs {
    public class Suspicious : ModBuff {

        SoundStyle[] soundList = new SoundStyle[] {
            EPSoundStyles.Sus0,
            EPSoundStyles.Sus1,
            EPSoundStyles.Sus2,
            EPSoundStyles.Sus3,
            EPSoundStyles.Sus4,
            EPSoundStyles.Sus5,
            EPSoundStyles.Sus6,
            EPSoundStyles.Sus7,
            EPSoundStyles.Sus8,
            EPSoundStyles.Sus9
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
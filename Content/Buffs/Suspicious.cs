using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;


namespace ExtraPets2.Content.Buffs {
    public class Suspicious : ModBuff {

        SoundStyle[] soundList = new SoundStyle[] {
            EPSoundStyles.EP2Sus0,
            EPSoundStyles.EP2Sus1,
            EPSoundStyles.EP2Sus2,
            EPSoundStyles.EP2Sus3,
            EPSoundStyles.EP2Sus4,
            EPSoundStyles.EP2Sus5,
            EPSoundStyles.EP2Sus6,
            EPSoundStyles.EP2Sus7,
            EPSoundStyles.EP2Sus8,
            EPSoundStyles.EP2Sus9
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
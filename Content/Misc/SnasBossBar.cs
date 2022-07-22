using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;

using ExtraPets2.Content.NPCs.SnasBoss;

namespace ExtraPets2.Content.Misc {
	public class SnasBossBar : ModBossBar {
		private int bossHeadIndex = -1;

		public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame) {
			if (bossHeadIndex != -1) {
				return TextureAssets.NpcHeadBoss[bossHeadIndex];
			}
			return null;
		}

		public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float lifePercent, ref float shieldPercent) {

			NPC npc = Main.npc[info.npcIndexToAimAt];
			if (!npc.active)
				return false;

			bossHeadIndex = npc.GetBossHeadTextureIndex();

			if (npc.ModNPC is SnasUdertal) {
			    lifePercent = 1f;
				shieldPercent = Utils.Clamp((float)npc.life / npc.lifeMax, 0f, 1f);
			}

			return true;
		}
	}
}

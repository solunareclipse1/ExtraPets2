using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;

namespace ExtraPets2.Content.Utilities {
    public class HitboxHelperItem : GlobalItem {
        public static Rectangle?[] meleeHitboxes = new Rectangle?[256];

		public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox) {
			meleeHitboxes[player.whoAmI] = hitbox;
		}
    }
}
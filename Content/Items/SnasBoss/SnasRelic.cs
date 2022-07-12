using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

using ExtraPets2.Content.Tiles.SnasBoss;

namespace ExtraPets2.Content.Items.SnasBoss {
	public class SnasRelic : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/SnasBoss/SnasRelic";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("snsa unrdtalle rilec");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(ModContent.TileType<SnasRelicTile>(), 0);

			Item.width = 30;
			Item.height = 40;
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Master;
			Item.master = true;
			Item.value = Item.buyPrice(0, 5);
		}
	}
}
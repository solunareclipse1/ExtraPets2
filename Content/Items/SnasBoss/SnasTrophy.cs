using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

using ExtraPets2.Content.Tiles.SnasBoss;

namespace ExtraPets2.Content.Items.SnasBoss {
	public class SnasTrophy : ModItem	{

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/SnasBoss/SnasTrophy";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("sens udnertlae torhpey");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(ModContent.TileType<SnasTrophyTile>());

			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(0, 1);
		}
	}
}
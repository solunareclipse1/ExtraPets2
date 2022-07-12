using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace ExtraPets2.Content.Items.SnasBoss {
	[AutoloadEquip(EquipType.Head)]
	public class SnasMask : ModItem
	{
        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/SnasBoss/SnasMask";
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("sanes dunlterale msak");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 22;
			Item.height = 28;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 75);
			Item.vanity = true;
			Item.maxStack = 1;
		}
	}
}
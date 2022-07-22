using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace ExtraPets2.Content.Items.Equippable.Armor {
	[AutoloadEquip(EquipType.Legs)]
	public class SnasSlipper : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/SnasSlipper";

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("snns sliper");
			Tooltip.SetDefault("encrises mvmint spaed");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.None;
			Item.width = 16;
			Item.height = 16;
			Item.rare = ItemRarityID.Purple;
			Item.value = Item.sellPrice(10, 0, 0);
		}

		public override void UpdateEquip(Player player) {
			player.moveSpeed += 0.1f;
		}
	}
}

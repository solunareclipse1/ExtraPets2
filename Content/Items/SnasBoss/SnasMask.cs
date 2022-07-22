using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

using ExtraPets2.Content.Items.Equippable.Armor;

namespace ExtraPets2.Content.Items.SnasBoss {
	[AutoloadEquip(EquipType.Head)]
	public class SnasMask : ModItem
	{
        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/SnasBoss/SnasMask";
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("sanes dunlterale msak");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ModContent.ItemType<SnasJacket>() && legs.type == ModContent.ItemType<SnasSlipper>();
		}

		public override void UpdateArmorSet(Player player) {
			player.setBonus = "grtealy ncrezse actak spid nd defnes\n" +
							"rmvoes yur  ifrmeas and mkaes you sfufr";
			player.GetAttackSpeed(DamageClass.Generic) += 10f;
			player.statDefense += 200;
			player.immune = false;
			player.immuneTime = 0;
		}

		public override void SetDefaults() {
			Item.width = 22;
			Item.height = 28;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 75);
			Item.maxStack = 1;
		}
	}
}
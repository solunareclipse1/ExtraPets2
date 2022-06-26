using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using ExtraPets2.Content.Buffs;
using ExtraPets2.Content.Projectiles;

namespace ExtraPets2.Content.Items.Pets {
	public class Rock : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/Rock";

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("rock");
			Tooltip.SetDefault("rock strong. rock sturdy. rock powerful.");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.Shuriken);
			Item.noUseGraphic = true;
			Item.damage = 1000;
			Item.DamageType = DamageClass.Generic;
			Item.knockBack = 25f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ModContent.ProjectileType<RockProjectile>();
			Item.width = 25;
			Item.height = 25;
			Item.scale = 0.1f;
			Item.UseSound = SoundID.Tink;
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.rare = ItemRarityID.Gray;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 0, 0, 1);
			Item.autoReuse = true;
			Item.shootSpeed = 20f;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.Boulder, 999)
				.AddIngredient(ItemID.Granite, 999)
				.AddIngredient(ItemID.Marble, 999)
				.AddIngredient(ItemID.RockGolemHead, 1)
				.AddTile(TileID.BoulderStatue)
				.Register();
		}
	}
}

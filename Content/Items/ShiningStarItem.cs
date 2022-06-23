using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using ExtraPets2.Content.Buffs;
using ExtraPets2.Content.Projectiles;

namespace ExtraPets2.Content.Items {
	public class ShiningStarItem : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/ShiningStarItem";

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Shining Star");
			Tooltip.SetDefault("Summons horrible pixel art that follows you around");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.shoot = ModContent.ProjectileType<ShiningStarProjectile>();
			Item.width = 30;
			Item.height = 30;
			Item.UseSound = SoundID.MaxMana;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.Yellow;
			Item.noMelee = true;
			Item.value = Item.sellPrice(2, 4, 8);
			Item.buffType = ModContent.BuffType<ShiningStarBuff>();
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame) {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
				player.AddBuff(Item.buffType, 3600);
			}
		}
		
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.SuspiciousLookingTentacle, 1)
				.AddIngredient(ItemID.Cog, 30)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}

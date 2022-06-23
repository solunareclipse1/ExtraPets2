using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using ExtraPets2.Content.Buffs;
using ExtraPets2.Content.Projectiles;

namespace ExtraPets2.Content.Items.Pets {
	public class SharpLookingCat : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/SharpLookingCat";

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Sharp Looking Cat");
			Tooltip.SetDefault("Summons a shoddily retextured cat to keep you company\nNote: Feline is not meant for consumption.");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ModContent.ProjectileType<TuxCatProjectile>();
			Item.width = 30;
			Item.height = 30;
			Item.UseSound = SoundID.AbigailCry;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.Yellow;
			Item.noMelee = true;
			Item.value = Item.sellPrice(4, 8, 16);
			Item.buffType = ModContent.BuffType<TuxCatBuff>();
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame) {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
				player.AddBuff(Item.buffType, 3600);
			}
		}
		
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.FlinxFur, 10)
				.AddIngredient(ItemID.BlackDye, 6)
				.AddTile(TileID.LivingLoom)
				.Register();
		}
	}
}

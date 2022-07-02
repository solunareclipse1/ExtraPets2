using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using ExtraPets2.Content.Buffs;
using ExtraPets2.Content.Projectiles;

namespace ExtraPets2.Content.Items.Pets {
	public class MogusItem : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/Mogus";

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Incredulous Astronaut");
			Tooltip.SetDefault("Summons a small spaceman of dubious origin\nNOTE: Possible cognitohazard");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ModContent.ProjectileType<MogusProjectile>();
			Item.width = 16;
			Item.height = 16;
			Item.UseSound = EPSoundStyles.Mogus;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.Red;
			Item.noMelee = true;
			Item.value = Item.sellPrice(5, 0, 5);
			Item.buffType = ModContent.BuffType<MogusBuff>();
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame) {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
				player.AddBuff(Item.buffType, 3600);
			}
		}
		
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.SuspiciousLookingEye, 1)
				.AddIngredient(ItemID.SuspiciousLookingTentacle, 1)
				.AddIngredient(ItemID.FishBowl, 1)
				.AddIngredient(ItemID.MartianConduitPlating, 20)
				.AddTile(TileID.SteampunkBoiler)
				.Register();
		}
	}
}

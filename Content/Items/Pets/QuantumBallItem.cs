using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using ExtraPets2.Content.Buffs;
using ExtraPets2.Content.Projectiles;

namespace ExtraPets2.Content.Items.Pets {
	public class QuantumBallItem : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/QuantumBall";

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Quantum Ball");
			Tooltip.SetDefault("Summons ugly tech to float in front of you");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ModContent.ProjectileType<QuantumBallProjectile>();
			Item.width = 30;
			Item.height = 30;
			Item.UseSound = SoundID.Research;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.Yellow;
			Item.noMelee = true;
			Item.value = Item.sellPrice(1, 2, 4);
			Item.buffType = ModContent.BuffType<QuantumBallBuff>();
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
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}

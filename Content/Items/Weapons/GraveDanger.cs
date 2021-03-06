using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

using ExtraPets2.Content.Projectiles;

namespace ExtraPets2.Content.Items.Weapons {
	public class GraveDanger : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/SkullTombstone";

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Grave Danger");
			Tooltip.SetDefault("...you are in.");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.TacticalShotgun);
			Item.noUseGraphic = true;
			Item.damage = 666;
			Item.DamageType = DamageClass.Throwing;
			Item.knockBack = 6f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ModContent.ProjectileType<SkullTombstone>();
			Item.width = 30;
			Item.height = 30;
			Item.scale = 1.0f;
			Item.UseSound = SoundID.Dig;
			Item.useAnimation = 60;
			Item.useTime = 60;
			Item.rare = ItemRarityID.Master;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 6, 6, 6);
			Item.autoReuse = true;
			Item.shootSpeed = 50f;
			Item.useAmmo = 0;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			for (int i = 0; i < 10; i++) {
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));
				newVelocity *= 1f - Main.rand.NextFloat(0.3f);
				Projectile.NewProjectileDirect(source, position, newVelocity, ModContent.ProjectileType<SkullTombstone>(), damage, knockback, player.whoAmI);
			}
			return false;
		}
	}
}

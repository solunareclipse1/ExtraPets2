using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using ExtraPets2.Content.Buffs;
using ExtraPets2.Content.Projectiles;

namespace ExtraPets2.Content.Items.Pets {
	public class OverseerRifle : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/OverseerRifle";

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Overseer's Railgun");
			Tooltip.SetDefault("Use of this weapon is biometrically restricted\nUnauthorized contact with weapon is forbidden");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 16;
			Item.DamageType = DamageClass.Ranged;
			Item.knockBack = 16f;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.width = 44;
			Item.height = 14;
			Item.scale = 2.0f;
			Item.UseSound = SoundID.Item96;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.Master;
			Item.noMelee = true;
			Item.value = Item.sellPrice(999, 99, 99, 99);
			Item.useAmmo = AmmoID.Bullet;
			Item.autoReuse = false;
			Item.shootSpeed = 20f;
		}

		public override void UpdateInventory(Player player) {
			if (player.name != "The Overseer") {
				player.AddBuff(ModContent.BuffType<SunderingDebuff>(), 16161616);
			}
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			if (type == ProjectileID.Bullet) {
				type = ModContent.ProjectileType<OverseerBullet>();
			}
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-3, 3);
		}
	}
}
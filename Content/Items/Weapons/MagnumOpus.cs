using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

using ExtraPets2.Content.Buffs;
using ExtraPets2.Content.Projectiles;


namespace ExtraPets2.Content.Items.Pets {
	public class MagnumOpus : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/MagnumOpus";

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Magnum Opus");
			Tooltip.SetDefault("\n[c/B52F6D:i must say, this world is rather intriguing...]\n[c/B52F6D:i shall lend you my assistance, so that i may observe more of it...]");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.MagicMissile);
			Item.damage = 200;
			Item.DamageType = DamageClass.Magic;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;
			Item.mana = 14;
			Item.width = 30;
			Item.height = 30;
			Item.UseSound = EPSoundStyles.EPMagnumOpus with {
				PitchRange = (-0.375000020955f, -0.16666669978f),
				MaxInstances = 30
			};
			Item.useAnimation = 3;
			Item.useTime = 3;
			Item.rare = ItemRarityID.Master;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.value = Item.sellPrice(999, 99, 99, 99);
			Item.shootSpeed = 6f;
			Item.shoot = ModContent.ProjectileType<MagnumOpusArrow>();
		}
	}
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

using ExtraPets2.Content.Projectiles;


namespace ExtraPets2.Content.Items.Weapons {
	public class MagnumOpus : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/MagnumOpus";

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Magnum Opus");
			Tooltip.SetDefault("Can be used as a weapon or an accessory\n"
							+ "As a weapon:\nRapidly summons controllable arrows that inflict a variety of debuffs\n"
							+ "As an accessory:\nGrants immunity to mana sickness\nImproves magic weapon proficiency by 10%, and maximum mana by 20\n"
							+ "Cannot use as both at the same time\n"
							+ "[c/B52F6D:i must say, this world is rather intriguing...]\n[c/B52F6D:i shall lend you my assistance, so that i may observe more of it...]");
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
			Item.UseSound = EPSoundStyles.MagnumOpus with {
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

			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetDamage(DamageClass.Magic) += 0.1f;
			player.GetCritChance(DamageClass.Magic) += 10f;
			player.GetAttackSpeed(DamageClass.Magic) += 0.1f;
			player.GetArmorPenetration(DamageClass.Magic) += 10f;
			player.GetKnockback(DamageClass.Magic) += 0.1f;
			player.manaCost -= 0.1f;
			player.statManaMax2 += 20;
			player.buffImmune[BuffID.ManaSickness] = true;
			player.GetModPlayer<EPPlayer>().equippedOpus = true;
		}
	}
}

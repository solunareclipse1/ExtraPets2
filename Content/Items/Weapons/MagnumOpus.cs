using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

using ExtraPets2.Content.Projectiles;

namespace ExtraPets2.Content.Items.Weapons {
	public class MagnumOpus : ModItem {
        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/MagnumOpus";

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Magnum Opus");
			Tooltip.SetDefault("Can be used as a weapon or an accessory\n"
							+ "As a weapon:\nLeft click to rapidly summon controllable arrows\nRight click to fire debuffing arrows in all directions\n"
							+ "-\n"
							+ "As an accessory:\nIncreases maximum mana by 100\nGreatly improves vitality and magic weapon proficiency\nGrants immunity to Mana Sickness\n"
							+ "-\n"
							+ "Cannot use as both at the same time\n"
							+ "[c/B52F6D:i must say, this world is rather intriguing...]\n[c/B52F6D:i shall lend you my assistance, so that i may observe more of it...]");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		int cooldown;
		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.MagicMissile);
			Item.damage = 333;
			Item.DamageType = DamageClass.Magic;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;
			Item.mana = 7;
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
			Item.value = Item.sellPrice(0, 20, 0, 0);
			Item.shootSpeed = 6f;
			Item.shoot = ModContent.ProjectileType<OpusArrowSeeking>();
			cooldown = 90;

			Item.accessory = true;
		}

		public override bool AltFunctionUse(Player player) {
			return cooldown <= 0;
		}

		public override bool CanUseItem(Player player) {
			return cooldown <= 0;
		}

		public override bool Shoot(Player plr, EntitySource_ItemUse_WithAmmo src, Vector2 pos, Vector2 vel, int type, int dmg, float kb) {
			if (plr.altFunctionUse == 2 && cooldown <= 0) {
				for (int i = 0; i < 10; i++) {
					if (!plr.CheckMana(14, true)) break;
					Vector2 randVel = new Vector2(Main.rand.NextFloat(-5,5),Main.rand.NextFloat(-5,5));
					Projectile.NewProjectile(src, plr.Center, randVel, ModContent.ProjectileType<OpusArrowSwarming>(), dmg, kb, plr.whoAmI, -1f);
					SoundEngine.PlaySound(EPSoundStyles.MagnumOpus with {PitchRange = (-0.375000020955f, -0.16666669978f),MaxInstances = 30}, plr.Center);
				}
				SoundEngine.PlaySound(EPSoundStyles.MagnumOpusAlt);
				cooldown = 90;
				return false;
			} else return true;
		}

		public override void UpdateInventory(Player plr) {
			if (cooldown > -1) cooldown--;
			if (cooldown == 0) SoundEngine.PlaySound(EPSoundStyles.MagnumOpusReady, plr.Center);
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.lifeRegen += 10;
			player.pStone = true;
			player.GetDamage(DamageClass.Magic) += 0.25f;
			player.GetCritChance(DamageClass.Magic) += 25f;
			player.GetAttackSpeed(DamageClass.Magic) += 0.25f;
			player.GetArmorPenetration(DamageClass.Magic) += 25f;
			player.GetKnockback(DamageClass.Magic) += 0.25f;
			player.manaCost -= 0.25f;
			player.statManaMax2 += 100;
			player.manaRegen += 25;
			player.buffImmune[BuffID.ManaSickness] = true;
			player.GetModPlayer<EPPlayer>().equippedOpus = true;
		}
	}
}

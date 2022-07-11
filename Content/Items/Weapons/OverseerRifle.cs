using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

using ExtraPets2.Content.Utilities;
using ExtraPets2.Content.DrawLayers;
using ExtraPets2.Content.Projectiles;


namespace ExtraPets2.Content.Items.Weapons {
	public class OverseerRifle : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/OverseerRifle";
		public static Asset<Texture2D> glowmask;

		public override void Unload() {
			glowmask = null;
		}

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Elite");
			Tooltip.SetDefault("Accelerates bullets to near lightspeed\n"
							+ "These will inflict a stackable debuff\n"
							+ "Debuff scales damage based on maximum health");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			if (!Main.dedServ) {
				glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");

				HeldItemLayer.RegisterData(Item.type, new DrawLayerData()
				{
					Texture = glowmask,
					Color = (PlayerDrawSet drawInfo) => new Color(255, 255, 255, 50) * 0.75f
				});
			}
		}

		public override void SetDefaults() {
			Item.damage = 1000;
			Item.DamageType = DamageClass.Ranged;
			Item.knockBack = 16f;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.width = 34;
			Item.height = 14;
			Item.scale = 2.0f;
			Item.UseSound = SoundID.Item96;
			Item.useAnimation = 55;
			Item.useTime = 55;
			Item.rare = ItemRarityID.Master;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 20, 0, 0);
			Item.useAmmo = AmmoID.Bullet;
			Item.autoReuse = true;
			Item.shootSpeed = 6f;
		}

		// Old funny code
		//public override void UpdateInventory(Player player) {
		//	if (player.name == "test man") {
		//		player.AddBuff(ModContent.BuffType<SunderingDebuff>(), 160);
		//	}
		//}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			type = ModContent.ProjectileType<OverseerBullet>();
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-3, 3);
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) {
			Item.DroppedGlowmask(spriteBatch, glowmask.Value, new Color(255,255,255,50)*0.75f, rotation, scale);
		}
	}
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Creative;

namespace ExtraPets2.Content.Items.Equippable.Accessories {
	public class HyperMarrow : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Projectiles/SnasOrb";

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("golwnig iye");
			Tooltip.SetDefault("mkaes yuo moev vry fast\n" +
                            "'I am now Sans Undertale, go into my eye'");

            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(2, 4));
			ItemID.Sets.AnimatesAsSoul[Type] = true;
            
            ItemID.Sets.ItemIconPulse[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.None;
			Item.width = 16;
			Item.height = 16;
			Item.rare = ItemRarityID.Expert;
            Item.expert = true;
			Item.value = Item.sellPrice(20, 0, 0);
            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.moveSpeed = 2;
            player.maxRunSpeed = 10;
            player.runSlowdown = 1;
            player.accRunSpeed = 10;
            player.runAcceleration *= 5;
            player.ignoreWater = true;
            player.autoJump = true;
            player.frogLegJumpBoost = true;
            if (player.direction == -1 && !hideVisual) {
                player.yoraiz0rEye = 2;
                player.cYorai = GameShaders.Armor.GetShaderIdFromItemId(ItemID.IntenseBlueFlameDye);
            }
		}
	}
}

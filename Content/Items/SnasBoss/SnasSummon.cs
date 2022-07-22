using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

using ExtraPets2.Content.NPCs.SnasBoss;

namespace ExtraPets2.Content.Items.SnasBoss {
	public class SnasSummon : ModItem {

        public override string Texture => ExtraPets2.AssetPath + "Textures/Items/TrueQuantumBall";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("temp summon item");
			Tooltip.SetDefault("placeholder, summons snas");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
			ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;

			NPCID.Sets.MPAllowedEnemies[ModContent.NPCType<SnasUdertal>()] = true;
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			Item.value = 100;
			Item.rare = ItemRarityID.Blue;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = false;
		}

		public override bool CanUseItem(Player player) {
			return !NPC.AnyNPCs(ModContent.NPCType<SnasUdertal>()) && player.ZoneDungeon;
		}

		public override bool? UseItem(Player player) {
			if (player.whoAmI == Main.myPlayer) {
				SoundEngine.PlaySound(SoundID.Roar, player.position);

				int type = ModContent.NPCType<SnasUdertal>();

				if (Main.netMode != NetmodeID.MultiplayerClient) {
					NPC.SpawnOnPlayer(player.whoAmI, type);
				}
				else {
					NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type);
				}
			}

			return true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.Bone)
				.AddTile(TileID.DemonAltar)
				.Register();
		}
	}
}
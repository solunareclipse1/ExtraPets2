using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;
using Terraria.DataStructures;

using ExtraPets2.Content.Items.SnasBoss;

namespace ExtraPets2.Content.Tiles.SnasBoss {
	public class SnasTrophyTile : ModTile	{

        public override string Texture => ExtraPets2.AssetPath + "Textures/Tiles/SnasBoss/SnasTrophy";

		public override void SetStaticDefaults() {
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.FramesOnKillWall[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(120, 85, 60), Language.GetText("MapObject.Trophy"));
			DustType = 7;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) {
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<SnasTrophy>());
		}
	}
}
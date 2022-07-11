using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria.DataStructures;

/// PLEASE READ THE note.txt FILE! ///

namespace ExtraPets2.Content.DrawLayers {
	public class DrawLayerData {
		public static Color DefaultColor(PlayerDrawSet drawInfo) => new Color(255, 255, 255, 0) * 0.8f;

		public Asset<Texture2D> Texture { get; init; }

		public Func<PlayerDrawSet, Color> Color { get; init; } = DefaultColor;
	}
}

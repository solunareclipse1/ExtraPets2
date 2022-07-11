using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;


namespace ExtraPets2.Content.Utilities {
    internal static class ItemHelper {
        public static void DroppedGlowmask(this Item item, SpriteBatch spriteBatch, Texture2D glowTexture, Color color, float rotation, float scale) {
			spriteBatch.Draw(
				glowTexture,
				new Vector2 (
					item.position.X - Main.screenPosition.X + item.width * 0.5f,
					item.position.Y - Main.screenPosition.Y + item.height - glowTexture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, glowTexture.Width, glowTexture.Height),
				Color.White,
				rotation,
				glowTexture.Size() * 0.5f,
				scale, 
				SpriteEffects.None, 
				0f
			);
        }
    }
}
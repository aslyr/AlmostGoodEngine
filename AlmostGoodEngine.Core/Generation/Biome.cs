﻿using AlmostGoodEngine.Core.Tiling;
using AlmostGoodEngine.Core.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Generation
{
	public class Biome(string name, Vector2 temperature, Vector2 humidity, Vector2 continentalness, Vector2 erosion, Vector2 weirdness)
	{
		public string Name { get; set; } = name;

		public Vector2 Temperature { get; set; } = temperature;

		public Vector2 Humidity { get; set; } = humidity;

		public Vector2 Continentalness { get; set; } = continentalness;

		public Vector2 Erosion { get; set; } = erosion;

		public Vector2 Weirdness { get; set; } = weirdness;

		public List<TileHeight> Tiles { get; set; } = [];

		public bool TilesetVariation { get; set; } = false;
		public string TilesetVariationName { get; set; } = "";

		public void RegisterTile(Tile tile, float min = 0.0f, float max = 1.0f)
		{
			if (Exists(tile))
			{
				return;
			}

			Tiles.Add(new()
			{
				Min = min,
				Max = max,
				Tile = tile,
			});
		}

		/// <summary>
		/// Get the right tile depending on the height (-1, 1)
		/// </summary>
		/// <param name="height"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public Tile GetTile(float height, float min = -1f, float max = 1f)
		{
			float h = BetterMath.To1(min, max, height);
			foreach (var tileHeight in Tiles)
			{
				if (h >= tileHeight.Min && h <= tileHeight.Max)
				{
					return tileHeight.Tile;
				}
			}

			//if (Tiles.Count > 0)
			//{
			//	return Tiles[0].Tile;
			//}

			return Tile.Null;
		}

		private bool Exists(Tile tile)
		{
			foreach (var tileHeight in Tiles)
			{
				if (tileHeight.Tile.X == tile.X &&
					tileHeight.Tile.Y == tile.Y)
				{
					return true;
				}
			}

			return false;
		}
	}
}

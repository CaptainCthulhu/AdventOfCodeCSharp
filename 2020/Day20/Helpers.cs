using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using System.Reflection.Emit;
using System.Data;

namespace Day20
{
    class Helper
    {
        static public string GetRightEdge(char[,] Tile)
        {
            var xMax = Tile.GetLength(0) - 1;
            var result = "";
            for (int y = 0; y < Tile.GetLength(1); y++)
            {
                result += Tile[xMax, y];
            }

            return result;
        }

        static public string GetLeftEdge(char[,] Tile)
        {
            var result = "";
            for (int y = 0; y < Tile.GetLength(1); y++)
            {
                result += Tile[0, y];
            }

            return result;
        }

        static public string GetTopEdge(char[,] Tile)
        {
			var result = "";
            for (int x = 0; x < Tile.GetLength(0); x++)
            {
                result += Tile[x, 0];
            }

            return result;
        }
		static public string GetBottomEdge(char[,] Tile)
        {
			var yMax = Tile.GetLength(1) - 1;
            var result = "";
            for (int x = 0; x < Tile.GetLength(0); x++)
            {
                result += Tile[x, yMax];
            }

            return result;
        }

        public static char[,] FlipVertical(char[,] tile)
		{
			int xLength = tile.GetLength(1);			
			char[,] returnTile = new char[xLength, xLength];

			for (int y = 0; y < xLength; ++y)
			{
				for (int x = 0; x < xLength; ++x)
				{
					returnTile[x, y] = tile[xLength - x - 1, y];
				}
			}

			return returnTile;
		}

		public static char[,] FlipHorizontal(char[,] tile)
		{
			int yLength = tile.GetLength(0);
			
			char[,] returnTile = new char[yLength, yLength];

			for (int y = 0; y < yLength; ++y)
			{
				for (int x = 0; x < yLength; ++x)
				{
					returnTile[x, y] = tile[x, yLength - y - 1];
				}
			}

			return returnTile;
        }
        
        public static char[,] RotateTile(char[,] tile)
		{
			int n = tile.GetLength(0);			
			char[,] returnTile = new char[n, n];

			for (int y = 0; y < n; ++y)
			{
				for (int x = 0; x < n; ++x)
				{
					returnTile[x, y] = tile[y, n - x - 1];
				}
			}
			return returnTile;
        }

        public static bool ContainsNumber(int[,] tilesArray, int number)
        {
            foreach (var i in tilesArray)
            {
                if (i == number)
                    return true;
            }
            return false;
        }

		public static Tuple<int, int> CoordinatesOf(int[,] matrix, int value)
		{
			int xMax = matrix.GetLength(0); // width
			int yMax = matrix.GetLength(1); // height

			for (int y = 0; y < yMax; ++y)
				{
				for (int x = 0; x < xMax; ++x)
				{				
					if (matrix[x, y].Equals(value))
						return Tuple.Create(x, y);
				}
			}
			return Tuple.Create(-1, -1);
		}
    }
}
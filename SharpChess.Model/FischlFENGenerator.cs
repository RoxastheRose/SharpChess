using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpChess.Model
{
    class FischlFENGenerator
    {
        public static string Generate()
        {
            string result = "";
            string[] rows = new string[8];
            // rqkbbrnn / pppppppp / 8 / 8 / 8 / 8 / PPPPPPPP / RQKBBRNN
            rows[1] = "pppppppp";
            rows[2] = "8";
            rows[3] = "8";
            rows[4] = "8";
            rows[5] = "8";
            rows[6] = "PPPPPPPP";

            char[] blackRow = new char[8];

            Random rng = new Random();

            // Place Rooks, then King between them
            int leftRookPlacement = rng.Next(0, 6);
            int rightRookPlacement = rng.Next(leftRookPlacement + 2, 8);
            int kingPlacement = rng.Next(leftRookPlacement + 1, rightRookPlacement);

            // Create an array of ints to denote which spaces are still open
            int[] openSpaces = new int[5];
            int emptySpaces = 0;
            for (int i = 0; i < 8 && emptySpaces < 5; i++)
            {
                if (i != leftRookPlacement && i != rightRookPlacement && i != kingPlacement)
                {
                    openSpaces[emptySpaces++] = i;
                }
            }

            // Place Bishops
            int rngInt = 0;
            int firstBishopPlacement = openSpaces[rngInt];
            bool even = firstBishopPlacement % 2 == 0;

            List<int> viableSpaces = new List<int>();
            if (even)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (i % 2 != 0)
                    {
                        viableSpaces.Add(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (i % 2 == 0)
                    {
                        viableSpaces.Add(i);
                    }
                }
            }

            viableSpaces.Remove(kingPlacement);
            viableSpaces.Remove(leftRookPlacement);
            viableSpaces.Remove(rightRookPlacement);

            rngInt = rng.Next(0, viableSpaces.Count);
            int secondBishopPlacement = viableSpaces[rngInt];

            viableSpaces = new List<int>();
            foreach (int space in openSpaces)
            {
                if (space != firstBishopPlacement && space != secondBishopPlacement)
                {
                    viableSpaces.Add(space);
                }
            }

            rngInt = rng.Next(0, viableSpaces.Count);
            int queenPlacement = viableSpaces[rngInt];
            viableSpaces.RemoveAt(rngInt);

            rngInt = rng.Next(0, viableSpaces.Count);
            int firstKnightPlacement = viableSpaces[rngInt];
            viableSpaces.RemoveAt(rngInt);

            int secondKnightPlacement = viableSpaces[0];

            {
                blackRow[kingPlacement] = 'k';
                blackRow[queenPlacement] = 'q';
                blackRow[firstBishopPlacement] = 'b';
                blackRow[secondBishopPlacement] = 'b';
                blackRow[firstKnightPlacement] = 'n';
                blackRow[secondKnightPlacement] = 'n';
                blackRow[leftRookPlacement] = 'r';
                blackRow[rightRookPlacement] = 'r';
            }

            char[] whiteRow = new char[8];
            for (int i = 0; i < 8; i++)
            {
                whiteRow[i] = (char)(blackRow[i] - 32);
            }

            string blackRowString = "";
            for (int i = 0; i < blackRow.Length; i++)
            {
                blackRowString += blackRow[i];
            }

            string whiteRowString = "";
            for (int i = 0; i < whiteRow.Length; i++)
            {
                whiteRowString += whiteRow[i];
            }

            rows[0] = blackRowString;
            rows[7] = whiteRowString;

            for (int i = 0; i < 8; i++)
            {
                if (i < 7)
                {
                    result += rows[i] + "/";
                }
                else
                {
                    result += rows[i];
                }
            }

            return result;
        }
    }
}

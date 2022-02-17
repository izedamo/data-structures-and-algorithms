using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Leetcode
{
    public class TicTacToe
    {
        private readonly int MarksRequiredToWin;

        //P1 move records.
        private readonly int[] p1RowConnects;
        private readonly int[] p1ColConnects;
        private readonly int[] p1DiagConnects;

        //P2 move records.
        private readonly int[] p2RowConnects;
        private readonly int[] p2ColConnects;
        private readonly int[] p2DiagConnects;

        public TicTacToe(int n)
        {
            MarksRequiredToWin = n;

            p1RowConnects = new int[n];
            p1ColConnects = new int[n];
            p1DiagConnects = new int[2]; //Diagonals are always 2!

            p2RowConnects = new int[n];
            p2ColConnects = new int[n];
            p2DiagConnects = new int[2];
        }

        //Player can be either 1 or 2.
        public int Move(int row, int col, int player)
        {
            //pointers to current player's records.
            int[] pRowConnects;
            int[] pColConnects;
            int[] pDiagConnects;

            if (player == 1)
            {
                pRowConnects = p1RowConnects;
                pColConnects = p1ColConnects;
                pDiagConnects = p1DiagConnects;
            }
            else
            {
                pRowConnects = p2RowConnects;
                pColConnects = p2ColConnects;
                pDiagConnects = p2DiagConnects;
            }

            //if mark placed in 1st diagonal.
            if (row == col)
            {
                // storing 1st diag total marks in diag array 0th elem.
                pDiagConnects[0]++;

                if (pDiagConnects[0] == MarksRequiredToWin)
                    return player;
            }

            //2nd diagonal. sum of indices of any cell of 2nd is equal to size of board - 1.
            if(row + col == MarksRequiredToWin - 1)
            {
                pDiagConnects[1]++;

                if (pDiagConnects[1] == MarksRequiredToWin)
                    return player;
            }

            pRowConnects[row]++;
            if (pRowConnects[row] == MarksRequiredToWin)
                return player;

            pColConnects[col]++;
            if (pColConnects[col] == MarksRequiredToWin)
                return player;

            //no player has won yet.
            return 0;
        }
    }
}

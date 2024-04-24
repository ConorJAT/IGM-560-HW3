using System;
using System.Collections.Generic;
using Record;
using RPS;
using UnityEditor.Timeline.Actions;

namespace NGram
{
    public class NGramPredictor
    {
        public Dictionary<string, KeyDataRecord> Data { get; private set; }
        public int nValue { get; private set; }

        public NGramPredictor(int nVal = 3)
        {
            Data = new Dictionary<string, KeyDataRecord>();
            nValue = nVal;
        }

        public void RegisterSequence(List<RPSMove> sequences)
        {
            List<RPSMove> key = sequences.GetRange(0, nValue);
            string keyString = ListToString(key);
			RPSMove value = sequences[nValue];

			KeyDataRecord keyData;

            if (!Data.ContainsKey(keyString))
            {
                keyData = Data[keyString] = new KeyDataRecord();
                keyData.Counts[value] = 0;
            }
            else if (!Data[keyString].Counts.ContainsKey(value))
            {
                keyData = Data[keyString];
                keyData.Counts[value] = 0;
            }
            else
            {
                keyData = Data[keyString];
            }

            keyData.Counts[value]++;
            keyData.Total++;
        }

        public RPSMove GetBestMove(List<RPSMove> sequences)
        {
            List<RPSMove> key = sequences.GetRange(0, nValue);
			string keyString = ListToString(key);
			KeyDataRecord keyData = Data[keyString];

            int highestValue = 0;
            RPSMove bestMove = RPSMove.Scissors;

            Dictionary<RPSMove, int>.KeyCollection actions = keyData.Counts.Keys;

            foreach (RPSMove action in actions)
            {
                if (keyData.Counts[action] > highestValue)
                {
                    highestValue = keyData.Counts[action];
                    bestMove = action;
                }
            }

            return bestMove;
        }

        private string ListToString(List<RPSMove> moves) 
        {
            string result = "";
            
            foreach (RPSMove move in moves)
            {
                if (move == RPSMove.Rock) result += "r";
                else if (move == RPSMove.Paper) result += "p";
                else result += "s";
            }

            return result;
        }
    }
}

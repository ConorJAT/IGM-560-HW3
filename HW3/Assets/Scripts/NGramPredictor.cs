using System;
using System.Collections.Generic;
using Record;
using RPS;
using UnityEditor.Timeline.Actions;

namespace NGram
{
    public class NGramPredictor
    {
        public Dictionary<List<RPSMove>, KeyDataRecord> Data { get; private set; }
        public int nValue { get; private set; }

        public NGramPredictor(int nVal = 3)
        {
            Data = new Dictionary<List<RPSMove>, KeyDataRecord>();
            nValue = nVal;
        }

        public void RegisterSequence(List<RPSMove> sequences)
        {
            List<RPSMove> key = sequences.GetRange(0, nValue);
            RPSMove value = sequences[nValue - 1];

            KeyDataRecord keyData;

            if (!Data.ContainsKey(key))
            {
                keyData = Data[key] = new KeyDataRecord();
                keyData.Counts[value] = 0;
            } 
            else
            {
                keyData = Data[key];
            }

            keyData.Counts[value]++;
            keyData.Total++;
        }

        public RPSMove GetBestMove(List<RPSMove> sequences)
        {
            List<RPSMove> key = sequences.GetRange(0, nValue);
            KeyDataRecord keyData = Data[key];

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
    }
}

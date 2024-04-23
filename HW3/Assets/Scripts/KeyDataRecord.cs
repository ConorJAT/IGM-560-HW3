using System;
using System.Collections.Generic;
using RPS;

namespace Record
{
    public class KeyDataRecord
    {
        public Dictionary<RPSMove, int> Counts { get; set; }
        public int Total {  get; set; }

        public KeyDataRecord() {
            Total = 0;
            Counts = new Dictionary<RPSMove, int>();
        }
    }
}

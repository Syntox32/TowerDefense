using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGamma
{
    public enum WaveState
    {
        OnGoing,   // the wave is in progress
        LaunchPad, // waiting for new wave
        Depot,     // between waves
        None
    }

    public class Wave
    {
        public int WaveNumber { get; set; }
        public int EnemyCount { get; set; }

        public Wave(int waveNumber, int enemyCount)
        {
            WaveNumber = waveNumber;
            EnemyCount = enemyCount;
        }
    }
}

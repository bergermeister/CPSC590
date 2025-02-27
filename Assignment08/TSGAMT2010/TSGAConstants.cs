﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSGAMT2010
{
    class TSGAConstants
    {
        public static int NumCities = 10;  
        public static float MutationRate = 0.05f;  // 5%
        public static float CrossoverRate = 0.50f; // 50%
        public static int PopSize = 100;
        public static int NumIterations = 50000;
        
        public static int NumMembersToExchange = 3; // Number of opulation memebers to exchange in 
                                                    // multi threaded version
        public static int ExchangeAfterIterations = 500;
    }
}

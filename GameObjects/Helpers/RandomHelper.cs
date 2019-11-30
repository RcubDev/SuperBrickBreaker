using System;
namespace Helpers {
    
    public static class RandomHelper {
        private static Random rand = new Random(); 
        public static int GetRandomNumber(int start, int end){
            return rand.Next(start, end);
        }
    }
}
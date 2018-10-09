using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    public class Grid
    {
        T[,] InitializeArray<T>(int length) where T : new()
        {
            T[,] array = new T[length,length];

            for (int j = 0; j < length; j++)
            {    
                for (int i = 0; i < length; i++)
                {
                    array[i,j] = new T();
                }
            }

            return array;
        }

       

        public Grid()
        {
            InitializeArray<object>(4);
            List<List<object>> list = new List<List<object>>();
        }
    }
}

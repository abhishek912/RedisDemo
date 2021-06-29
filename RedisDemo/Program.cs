using System;

namespace RedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine("Saving random data in cache!!!");
            p.SaveBigData();

            Console.WriteLine("Reading data from cache!!!");
            p.ReadData();


            Console.ReadLine();

            //Console.WriteLine("Hello World!");
        }

        public void SaveBigData()
        {
            var deviceCount = 100;
            var rnd = new Random();
            var cache = RedisConnectionHelper.Connection.GetDatabase();

            for(int i = 0; i<deviceCount; i++)
            {
                var value = rnd.Next(0, 10000);
                cache.StringSet($"Device_Status:{i}", value);
            }
        }

        public void ReadData()
        {
            var cache = RedisConnectionHelper.Connection.GetDatabase();
            var decicesCount = 100;
            for(int i = 0; i<decicesCount; i++)
            {
                var value = cache.StringGet($"Device_Status:{i}");
                Console.WriteLine($"Value={value}");
            }

            Console.WriteLine("Deleting keys!!!");

            for (int i = 0; i < decicesCount; i++)
            {
                cache.KeyDelete($"Device_Status:{i}");
            }
        }
    }
}

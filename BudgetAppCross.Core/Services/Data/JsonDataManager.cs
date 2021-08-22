//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace BudgetAppCross.Core.Services
//{
//    public class JsonDataManager// : IDataManager
//    {
//        #region Fields
//        private string libFolder = FileSystem.AppDataDirectory;
//        #endregion

//        #region Properties
//        #endregion

//        #region Singleton
//        private static JsonDataManager instance;
//        public static JsonDataManager Instance
//        {
//            get { return instance ?? (instance = new JsonDataManager()); }
//        }

//        private JsonDataManager()
//        {

//        }
//        #endregion 

//        #region Methods

//        #endregion




//        //public void LoadData()
//        //{
//        //    var path = $"{libFolder}/test";
//        //    string text = File.ReadAllText(path);
//        //    Console.WriteLine(text);
//        //    //using(StreamReader file = File.OpenText(path))
//        //    //{
//        //    //    string text = File.ReadAllText(path);
//        //    //}
//        //}

//        //public void SaveData()
//        //{
//        //    var movie = new Movie
//        //    {
//        //        Name = "Bad Boys",
//        //        Year = 1995
//        //    };
//        //    var path = $"{libFolder}/test";
//        //    using(StreamWriter file = File.CreateText(path))
//        //    {
//        //        JsonSerializer serializer = new JsonSerializer();
//        //        serializer.Serialize(file, movie);

//        //        serializer.Serialize(file, )
//        //    }
//        //}
//    }

//    public class Movie
//    {
//        public string Name { get; set; }
//        public int Year { get; set; }
//    }
//}

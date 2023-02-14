// namespace Final_project;
//
// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Runtime.Serialization.Formatters.Binary;
// using System.Text;
// using System.Threading.Tasks;
//
// {
//     class FileManger
//     {
//         public static List<Friend> Load(string fileName)
//         {
//             List<Friend> list = new List<Friend>();
//             // Check if we had previously Save information of our friends
//             // previously
//             if (File.Exists(fileName))
//             {
//
//                 try
//                 {
//                     // Create a FileStream will gain read access to the
//                     // data file.
//                     FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
//
//                     BinaryFormatter formatter = new BinaryFormatter();
//                     list = (List<Friend>)   formatter.Deserialize(stream);
//                     
//                 }
//                 catch (Exception ex)
//                 {
//                     Console.WriteLine(ex.Message);
//                 }
//
//             }
//             return list;
//         }
//         public static void Save (string fileName, List<Friend> list)
//         {
//             // Gain code access to the file that we are going
//             // to write to
//             try
//             {
//                 // Create a FileStream that will write data to file.
//                 BinaryFormatter formatter = new BinaryFormatter();
//                 FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
//                 formatter.Serialize(stream, list);
//                 stream.Flush();
//                 stream.Close();
//
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine(ex.Message);
//             }
//         }
//     }
// }
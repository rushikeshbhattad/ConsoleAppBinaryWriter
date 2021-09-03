using System;
using System.IO;

namespace BinaryWriterDemo
{
    class BinaryTesting
    {
        FileStream fs;
        string studentName;
        int[] studentMarks = new int[3];
        int avgMarks;
        public static int numOfStudents = 0;
        public BinaryTesting(string studentName, int[] studentMarks, int avgMarks)
        {
            this.studentName = studentName;
            this.studentMarks = studentMarks;
            this.avgMarks = avgMarks;
        }
        public void WriteData()
        {
            fs = new FileStream(@"/Users/swapnilnawale/Desktop/stud.dat", FileMode.Open, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(studentName);
            for (int i = 0; i < 3; i++)
            {
                bw.Write(studentMarks[i]);
            }
            bw.Write(avgMarks);
            bw.Flush();
            bw.Close();
            fs.Close();
            numOfStudents++;
        }
        public void ReadData()
        {
            fs = new FileStream(@"/Users/swapnilnawale/Desktop/stud.dat", FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            for (int j = 0; j < numOfStudents; j++)
            {
                studentName = br.ReadString();
                if (studentName == null)
                    break;
                Console.Write(studentName + "\t");
                for (int i = 0; i < 3; i++)
                {
                    studentMarks[i] = br.ReadInt32();
                    Console.Write(studentMarks[i] + "\t");
                }
                avgMarks = br.ReadInt32();
                Console.WriteLine(avgMarks);
            }
            br.Close();
            fs.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string studentName;
            int[] studentMarks = new int[3];
            int i = 0, totMarks = 0, avgMarks;
            string resp;
            BinaryTesting bt;
            do
            {
                i++;
                Console.Write("Enter Student {0} Name: ", i);
                studentName = Console.ReadLine();

                for (int j = 0; j < 3; j++)
                {
                    Console.Write("Enter Student {0} Subject {1} Marks: ", i, (j + 1));
                    studentMarks[j] = Convert.ToInt32(Console.ReadLine());
                    totMarks += studentMarks[j];
                }
                avgMarks = totMarks / 3;
                bt = new BinaryTesting(studentName, studentMarks, avgMarks);
                bt.WriteData();
                Console.WriteLine("Press Y to continue");
                resp = Console.ReadLine();
            } while (resp == "Y" || resp == "y");
            bt.ReadData();
        }
    }
}

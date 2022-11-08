using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DatabaseSwipeRecyclerView.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Environment = System.Environment;

namespace DatabaseSwipeRecyclerView
{
    public class StudentDatabase
    {
        public static string DBname = "SQLite.db3";
        public static string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DBname);

        SQLiteConnection sqliteconnection;

        public StudentDatabase()
        {
            try
            {

                Console.WriteLine(DatabasePath);
                sqliteconnection = new SQLiteConnection(DatabasePath);
                Console.WriteLine("Database Created Sucessfully");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Exception" + ex);

            }

        }
        public void CreateStudent()
        {
            try
            {

                var create = sqliteconnection.CreateTable<StudentDetails>();
                Console.WriteLine("Table Created Sucessfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Table creation Exception" + ex);

            }
        }

        public bool InstertStudent(StudentDetails students)
        {
            var result = sqliteconnection.Insert(students);
            if (result == -1)
            {

                return false;
            }
            else
            {
                Console.WriteLine("Inserted data Sucessfully");
                return true;

            }

        }

       public bool UpdateStudents(StudentDetails students)
       {
            var result = sqliteconnection.Update(students);
            if(result == -1)
            {
                return false;
            }
            else
            {
                Console.WriteLine("Data Updated Successfully");
                return true;
            }
        }

        public bool DeleteStudent(StudentDetails students)
        {
            var result = sqliteconnection.Delete(students);
            if (result == -1)
            {

                return false;
            }
            else
            {
                Console.WriteLine("Data deleted Sucessfully");
                return true;

            }

        }

        public List<StudentDetails> GetStudents()
        {
            var sdata = sqliteconnection.Table<StudentDetails>().ToList();
            return sdata;


        }

        public StudentDetails GetStudentsByRollNo(int studRollno)
        {

            var userdata = sqliteconnection.Table<StudentDetails>().Where(u => u.RollNumber == studRollno).FirstOrDefault();
            return userdata;


        }
    }
}
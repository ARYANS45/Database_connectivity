using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Data;

namespace DBconnectivity
{
    internal class Program
    {
        static string lstrcon = "server = localhost\\SQLEXPRESS; Database = student_db;Trusted_Connection=True;Trustservercertificate=True;";
        static void Main(string[] args)
        {
            // string lstrconshopify = "server = .\\SQLEXPRESS; Database =Shopify;Trusted_Connection=True;Trustservercertificate=True;";
            // string lstrQuery2 = "select top 10 customerID,ContactName,Address from Customers";

            Console.WriteLine("Choose options to perform operation :");
            Console.WriteLine("1.TO Insert Record.");
            Console.WriteLine("2.TO Update Record.");
            Console.WriteLine("3.TO Run TXT File.");
            Console.WriteLine("4.TO Delete Records.");
            Console.WriteLine("5.TO View Records.");
            Console.WriteLine("6.TO Exit.");


            while (true)
            {
                Console.WriteLine("Enter your choice to perform operation :");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        InsertStudentRecord();
                        break;
                    case 2:
                        UpdateStudentRecoed();
                        break;
                    case 3:
                        runTXTfile();
                        break;
                    case 4:
                        DeleteStudentRecord();
                        break;
                    case 5:
                        viewStudentRecord();
                        break;
                    case 6:
                        Console.WriteLine("Existing the program..");
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
        private static void runTXTfile()
        {
            string lstrQuery = File.ReadAllText("Queries\\AllStudent.txt");
            DBaccessor lobjstudentDB = new DBaccessor(lstrcon);
            DataTable lobjDT = lobjstudentDB.GetDataTableByQuery(lstrQuery);
            lobjstudentDB.PrintDataTable(lobjDT);
        }
        private static void InsertStudentRecord()
        {
            student_fields lobj = new student_fields();

            Console.WriteLine("Enter First Name of student to Insert Record :");
            lobj.lstrFN = Console.ReadLine();
            
            Console.WriteLine("Enter Last Name of student to Insert Record :");
            lobj.lstrLN = Console.ReadLine();

            Console.WriteLine("Enter Mobile Number of student to Insert Record :");
            lobj.lstrFN = Console.ReadLine();
           
            Console.WriteLine("Enter Email of student to Insert Record  :");
            lobj.lstrEmail = Console.ReadLine();

            string insertstudent = $"Insert into Student_details (First_Name,Last_Name,Mobile,Email) Values('{lobj.lstrFN}','{lobj.lstrLN}','{lobj.lstrmobile}','{lobj.lstrEmail}')";
           
            DBaccessor lobjStudentDB = new DBaccessor(lstrcon);
            int lintResult = lobjStudentDB.RunDMLQuery(insertstudent);

            if (lintResult == 1)
            {
                Console.WriteLine("Record inserted Successfully ");
            }
            else
            {
                Console.WriteLine("no record inserted");
            }
        }
        private static void UpdateStudentRecoed()
        {
            student_fields lobj = new student_fields();

            Console.WriteLine("Enter SID of student to update :");
            lobj.lintID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter First Name of student to update :");
            lobj.lstrFN= Console.ReadLine();
            Console.WriteLine("Enter Last Name of student to update :");
            lobj.lstrLN = Console.ReadLine();
            Console.WriteLine("Enter Mobile Number of student to update :");
            lobj.lstrmobile = Console.ReadLine();
            Console.WriteLine("Enter Email of student  to update:");
            lobj.lstrEmail = Console.ReadLine();

            string lstrUpdateStudent = $"update student_details set First_Name = '{lobj.lstrFN}',Last_Name='{lobj.lstrLN}',Mobile ='{lobj.lstrmobile}',Email = '{lobj.lstrEmail }' where SID = {lobj.lintID}";
            DBaccessor lobjStudentDB = new DBaccessor(lstrcon);
            int lintResult = lobjStudentDB.RunDMLQuery(lstrUpdateStudent);

            if (lintResult == 1)
            {
                Console.WriteLine("Record Updated Successfully ");
            }
            else
            {
                Console.WriteLine("No record Updated");
            }
        }
        private static void DeleteStudentRecord()
        {
            Console.WriteLine("Enter SIDs to Delete Record :");
            string lintID = Console.ReadLine();
            string lstrDeleteStudent = $"delete from student_details where SID in ({lintID})";
            
            DBaccessor lobjStudentDB = new DBaccessor(lstrcon);
            int lintResult = lobjStudentDB.RunDMLQuery(lstrDeleteStudent);

            if (lintResult > 0)
            {
                Console.WriteLine("Record Deleted Successfully ");
            }
            else
            {
                Console.WriteLine("No record Deleted");
            }
        }
        private static void viewStudentRecord()
        {
            string lstrViewStudent = "Select * from student_details ";
            DBaccessor lobjstudentDB = new DBaccessor(lstrcon);
            DataTable lintResult = lobjstudentDB.GetDataTableByQuery(lstrViewStudent);

            lobjstudentDB.PrintDataTable(lintResult);
        }
    }
}
        
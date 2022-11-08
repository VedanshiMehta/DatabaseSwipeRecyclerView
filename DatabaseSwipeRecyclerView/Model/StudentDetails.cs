using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseSwipeRecyclerView.Model
{
    [Table("Students")]
    public class StudentDetails
    {
        

        [PrimaryKey, Column("RollNumber"), AutoIncrement]
        public int RollNumber { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("EmergencyContact")]
        public string EmergencyContact { get; set; }

        [Column("DateOfBirth")]
        public string DateOfBirth { get; set; } 

    }
}
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using DatabaseSwipeRecyclerView.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseSwipeRecyclerView
{
    public class StudentAdapter : RecyclerView.Adapter
    {
        private List<StudentDetails> student;
        private StudentDatabase _studentDatabase = new StudentDatabase();
        public event EventHandler<StudentAdapterEventArgs> itemClick;

        public StudentAdapter(List<StudentDetails> student)
        {
            this.student = student;
        }

        public override int ItemCount => student == null ? 0 : student.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
           StudentViewHolder studentViewHolder = holder as StudentViewHolder;
            studentViewHolder.BindData(student[position]);
        }

        
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
          View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.studentdatarowitem,parent, false);
            return new StudentViewHolder(view, onClick);
        }
        private void onClick(StudentAdapterEventArgs args) => itemClick?.Invoke(this, args);
        public StudentDetails GetItem(int postion)
        {
            return student[postion];
        }
        public void RemoveItem(int position)
        {

            var studentDetail = _studentDatabase.GetStudentsByRollNo(student[position].RollNumber);
            _studentDatabase.DeleteStudent(studentDetail);
            student.RemoveAt(position);
            NotifyItemChanged(position);
        }
       
    }
    public class StudentViewHolder : RecyclerView.ViewHolder
    {
        public TextView _textViewStudentName;
        public TextView _textViewEmergencyContact;
        public TextView _textViewDateOfBirth;
        public StudentViewHolder(View itemView, Action<StudentAdapterEventArgs> clickListener) : base(itemView)
        {
            _textViewStudentName = itemView.FindViewById<TextView>(Resource.Id.textViewStudentName);
            _textViewDateOfBirth = itemView.FindViewById<TextView>(Resource.Id.textViewDateOfBirth);
            _textViewEmergencyContact = itemView.FindViewById<TextView>(Resource.Id.textViewEmergencyContact);
            itemView.Click += (s, e) => clickListener(new StudentAdapterEventArgs { View = itemView, Position = AdapterPosition });
        }

        internal void BindData(StudentDetails studentDetails)
        {
            _textViewStudentName.Text = studentDetails.Name;
            _textViewEmergencyContact.Text = studentDetails.EmergencyContact;
            _textViewDateOfBirth.Text = studentDetails.DateOfBirth; 
        }
    }
    public class StudentAdapterEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}
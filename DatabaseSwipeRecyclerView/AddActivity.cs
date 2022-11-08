using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using DatabaseSwipeRecyclerView.Model;
using Google.Android.Material.Button;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace DatabaseSwipeRecyclerView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class AddActivity: AppCompatActivity 
    {
        private Toolbar _toolbar;
        private EditText _studentName;
        private EditText _emergencyContactNumber;
        private EditText _dateOfBirth;
        private MaterialButton _materialButtonAdd;
        private StudentDatabase _studentDatabase;
        private StudentDetails _studentDetails;
        private int postions;
        private bool _isUpdate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.addlayout);
            UIReferences();
            UIClickEvents();
            SetSupportActionBar(_toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            _studentDatabase = new StudentDatabase();
             _studentDetails = new StudentDetails();
            _isUpdate = Intent.GetBooleanExtra("Update", false);
            if (_isUpdate)
            {
                GetUpdate();
            }
            


        }

        private void GetUpdate()
        {
          
            
                SupportActionBar.Title = "Update Student Details";
                _materialButtonAdd.Text = "Update";
                var studentDetails = _studentDatabase.GetStudents();
                var student = studentDetails[postions];
                 _studentDetails.RollNumber = student.RollNumber;
                _studentName.Text = student.Name;
                _emergencyContactNumber.Text = student.EmergencyContact;
                _dateOfBirth.Text = student.DateOfBirth;

            
        }

        private void UIClickEvents()
        {
            _materialButtonAdd.Click += _materialButtonAdd_Click;
            _toolbar.NavigationClick += _toolbar_NavigationClick;
        }

        private void _toolbar_NavigationClick(object sender, Toolbar.NavigationClickEventArgs e)
        {
            Finish();
        }
        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return base.OnSupportNavigateUp();

        }

        private void _materialButtonAdd_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(_studentName.Text)
                && !string.IsNullOrEmpty(_emergencyContactNumber.Text) && !string.IsNullOrEmpty(_dateOfBirth.Text))
            {
                if (!_isUpdate)

                {
                    _studentDetails.Name = _studentName.Text;
                    _studentDetails.EmergencyContact = _emergencyContactNumber.Text;
                    _studentDetails.DateOfBirth = _dateOfBirth.Text;
                    var insterted = _studentDatabase.InstertStudent(_studentDetails);
                    if (insterted == true)
                    {
                        Toast.MakeText(this, "Data is Inserted Successfully", ToastLength.Short).Show();
                        Finish();
                    }
                    else
                    {
                        Toast.MakeText(this, "Failure Occured ", ToastLength.Short).Show();
                    }
                }
                else if (_isUpdate)
                {
                    _studentDetails.Name = _studentName.Text;
                    _studentDetails.EmergencyContact = _emergencyContactNumber.Text;
                    _studentDetails.DateOfBirth = _dateOfBirth.Text;
                    
                    var update = _studentDatabase.UpdateStudents(_studentDetails);
                    if (update == true)
                    {
                        Toast.MakeText(this, "Data is Updated Successfully", ToastLength.Short).Show();
                        Finish();
                    }
                    else
                    {
                        Toast.MakeText(this, "Failure Occured ", ToastLength.Short).Show();
                    }


                }

            }
            else
            {
                Toast.MakeText(this,"Fill Details",ToastLength.Short).Show();
            }
        }

        private void UIReferences()
        {
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbarInsert);
            _studentName = FindViewById<EditText>(Resource.Id.editTextStudentName);
            _emergencyContactNumber = FindViewById<EditText>(Resource.Id.editTextEmergencyContact);
            _dateOfBirth = FindViewById<EditText>(Resource.Id.editTextDateOfBirth);
            _materialButtonAdd = FindViewById<MaterialButton>(Resource.Id.materialButtonAdd);
        }
    }
}
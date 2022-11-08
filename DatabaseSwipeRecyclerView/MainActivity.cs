using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.RecyclerView.Widget;
using DatabaseSwipeRecyclerView.Model;
using System;
using System.Collections.Generic;

namespace DatabaseSwipeRecyclerView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
       
        private RecyclerView _recyclerView;
        private Toolbar _toolbar;
        private RecyclerView.LayoutManager _layoutManager;
        private StudentAdapter _adapter;
        private List<StudentDetails> _student;
        private StudentDatabase _studentDatabase;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            UIRefernces();
            _toolbar.InflateMenu(Resource.Menu.menu1);
            _toolbar.MenuItemClick += _toolbar_MenuItemClick;
            _studentDatabase = new StudentDatabase();
            _studentDatabase.CreateStudent();
            GetStudentData();
            
        }
        protected override void OnResume()
        {
            base.OnResume();
            GetStudentData();
        }
        private void GetStudentData()
        {
            _student = new List<StudentDetails>();
            _layoutManager = new LinearLayoutManager(this);
            _recyclerView.SetLayoutManager(_layoutManager);
            _student = _studentDatabase.GetStudents();
            _adapter = new StudentAdapter(_student);
            _recyclerView.SetAdapter(_adapter);

            var swipe = new SwipeHelperClass(0, ItemTouchHelper.Left | ItemTouchHelper.Right, this, _adapter);
            ItemTouchHelper itemTouchHelper = new ItemTouchHelper(swipe);
            itemTouchHelper.AttachToRecyclerView(_recyclerView);

        }

        private void _toolbar_MenuItemClick(object sender, Toolbar.MenuItemClickEventArgs e)
        {
            if (e.Item.ItemId == Resource.Id.action_add)
            {
                StartActivity(new Intent(this, typeof(AddActivity)));
            }
        }

        private void UIRefernces()
        {
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewViewData);
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
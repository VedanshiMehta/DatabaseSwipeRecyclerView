using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.Core.Content;
using Android.Provider;

namespace DatabaseSwipeRecyclerView
{
    public class SwipeHelperClass : ItemTouchHelper.SimpleCallback
    {
        private Context mContext;
        private StudentAdapter mStudentAdapter;
        private Drawable _deleteicon;
        private Drawable _updateicon;
        private int _iconIntrinsicWidth;
        private int _iconIntrinsicHeight;
        private int _updateIntrinsicHeight;
        private int _updateIntrinsicWidth;
        private ColorDrawable _background;
        private Paint _deletepaint;
        public SwipeHelperClass(int dragDirs, int swipeDirs,Context mContext) : base(dragDirs, swipeDirs)
        {
            this.mContext = mContext;
            _deleteicon = ContextCompat.GetDrawable(mContext, Resource.Drawable.ic_delete);
            _updateicon = ContextCompat.GetDrawable(mContext, Resource.Drawable.ic_update);
            _updateIntrinsicHeight = _updateicon.IntrinsicHeight;
            _updateIntrinsicWidth = _updateicon.IntrinsicWidth;
            _iconIntrinsicWidth = _deleteicon.IntrinsicWidth;
            _iconIntrinsicHeight = _deleteicon.IntrinsicHeight;
            _background = new ColorDrawable();
            _background.Color = Color.PaleVioletRed;
            _deletepaint = new Paint();
            _deletepaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.Clear));
        }
        public SwipeHelperClass(int dragDirs, int swipeDirs, Context mContext, StudentAdapter mStudentAdapter) : this(dragDirs, swipeDirs, mContext)
        {
            this.mContext = mContext;
            this.mStudentAdapter = mStudentAdapter;
            _deleteicon = ContextCompat.GetDrawable(mContext, Resource.Drawable.ic_delete);
            _updateicon = ContextCompat.GetDrawable(mContext, Resource.Drawable.ic_update);
            _updateIntrinsicHeight = _updateicon.IntrinsicHeight;
            _updateIntrinsicWidth = _updateicon.IntrinsicWidth;
            _iconIntrinsicWidth = _deleteicon.IntrinsicWidth;
            _iconIntrinsicHeight = _deleteicon.IntrinsicHeight;
            _background = new ColorDrawable();
            _background.Color = Color.PaleVioletRed;
            _deletepaint = new Paint();
            _deletepaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.Clear));
        }

        public override bool OnMove(RecyclerView p0, RecyclerView.ViewHolder p1, RecyclerView.ViewHolder p2)
        {
            return false;
        }
        public override void OnChildDraw(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive)
        {
            base.OnChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
        }
        public override void OnChildDrawOver(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive)
        {
            var itemView = viewHolder.ItemView;
            var itemHeight = itemView.Bottom - itemView.Top;
            var isCancelled = dX == 0f && !isCurrentlyActive;

            if (isCancelled)
            {
                clearCanvas(c, itemView.Right + dX, (float)itemView.Top, (float)itemView.Right, (float)itemView.Bottom);
                base.OnChildDrawOver(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
                return;
            }
            else
            {
                if (dX < 0)
                {
                    _background.Color = Color.PaleVioletRed;
                    _background.SetBounds(itemView.Right + (int)dX, itemView.Top, itemView.Right, itemView.Bottom);
                    _background.Draw(c);

                    var deleteIconTop = itemView.Top + (itemHeight - _iconIntrinsicHeight) / 2;
                    var deleteIconMargin = (itemHeight - _iconIntrinsicHeight) / 2;
                    var deleteIconLeft = itemView.Right - deleteIconMargin - _iconIntrinsicWidth;
                    var deleteIconRight = itemView.Right - deleteIconMargin;
                    var deleteIconBottom = deleteIconTop + _iconIntrinsicHeight;

                    _deleteicon.SetBounds(deleteIconLeft, deleteIconTop, deleteIconRight, deleteIconBottom);
                    _deleteicon.Draw(c);
                }
                else if(dX > 0)
                {

                    _background.Color = Color.LightGoldenrodYellow;
                    _background.SetBounds(itemView.Left, itemView.Top, itemView.Right + (int)dX, itemView.Bottom);
                    _background.Draw(c);


                    
                    var updateIconTop = itemView.Top + (itemHeight - _updateIntrinsicHeight) / 2;
                    var updateIconMargin = (itemHeight - _updateIntrinsicHeight) / 2;
                    var updateIconLeft = itemView.Left + updateIconMargin;
                    var updateIconRight = itemView.Left + updateIconMargin + _updateIntrinsicWidth;
                    var updateIconBottom = updateIconTop + _updateIntrinsicHeight;

                    _updateicon.SetBounds(updateIconLeft, updateIconTop, updateIconRight, updateIconBottom);
                    _updateicon.Draw(c);
                }

            }
            base.OnChildDrawOver(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
        }
        private void clearCanvas(Canvas c, float v, float top, float right, float bottom)
        {
            c.DrawRect(v, top, right, bottom, _deletepaint);
        }

        public override void OnSwiped(RecyclerView.ViewHolder p0, int p1)
        {
          
            if (p1 == ItemTouchHelper.Left)
            {
                mStudentAdapter.RemoveItem(p0.AdapterPosition);
            }
            else if (p1 == ItemTouchHelper.Right)
            {
                bool isUpdate = true;
            
                Intent intent = new Intent(mContext,typeof(AddActivity));
                intent.PutExtra("postion", p0.AdapterPosition);
                intent.PutExtra("Update",isUpdate);
                mContext.StartActivity(intent);
                mStudentAdapter.NotifyDataSetChanged();

            }

        }
        public override void ClearView(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
        {
            base.ClearView(recyclerView, viewHolder);
        }
    }
}